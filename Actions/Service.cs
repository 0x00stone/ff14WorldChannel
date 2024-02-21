using Advanced_Combat_Tracker;
using FF14Chat.Common;
using FF14Chat.Controls;
using FF14Chat.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Transactions;
using System.Diagnostics;
using System.Linq;
using FF14Chat.Network;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace FF14Chat.Actions {
	public class Service {
		private FFXIV_ACT_Plugin.FFXIV_ACT_Plugin FFXIV_ACT_Plugin;
		private FF14Chat_Main main;
		public Service(FF14Chat_Main main) {
			this.main = main;
		}

		#region ProcessSwitcherHeartbeat
		public async void ProcessSwitcherHeartbeat(object sender, DoWorkEventArgs e) {
			while(FF14Chat_Main.isRunning) {

				//登录（心跳）
				LoginUserResult localresult = await NetworkUtil.Heartbeat(FF14Chat_Main.loginUserResult);
				if(localresult.getErrorMessage() != null) {
					MessageBox.Show(localresult.getErrorMessage(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				} else {
					string token = localresult.getToken();
					if("".Equals(token)) {
						Log.info("心跳获取token为空");
					} else {
						FF14Chat_Main.loginUserResult = localresult;
					}

					if(FF14Chat_Main.playerContent != 0 && ulong.Parse(localresult.getcontent()) != FF14Chat_Main.playerContent) {
						//1. 尝试切换用户
						Log.error("游戏帐户与聊天账户非绑定，请重新登录");
						MessageBox.Show("游戏帐户与聊天账户非绑定，请重新登录", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
						main.restart();
					}
				}
				Thread.Sleep(5000);
			}
		}
		#endregion

		#region ProcessSwitcherGetUserList
		public async void ProcessSwitcherGetUserList(object sender, DoWorkEventArgs e) {
			while(FF14Chat_Main.isRunning) {

				//读用户(3s一次)
				string textCommand = "";

				if(FF14Chat_Main.userList.Count == 0) {
					string userResponse = await NetworkUtil.GetUsersByStock(FF14Chat_Main.loginUserResult);
					if(userResponse == null || "".Equals(userResponse)) {
						textCommand = "获取用户服务器存量数据失败";
					} else {
						textCommand = parseUsersJSON(userResponse);
					}
				} else {
					string userResponse = await NetworkUtil.GetUsersByStock(FF14Chat_Main.loginUserResult);
					if(userResponse == null || "".Equals(userResponse)) {
						textCommand = "获取用户服务器增量数据失败";
					} else {
						textCommand = parseUsersJSON(userResponse);
					}
				}

				if(!"".Equals(textCommand)) {
					main.addMessage(textCommand);
				}

				Thread.Sleep(3000);
			}
		}

		private string parseUsersJSON(string json) {
			dynamic userJSON = JObject.Parse(json);
			string textCommand = "";

			if(200 == ((int)userJSON["code"])) {
				Log.track($"code:{userJSON["code"]} , data: {userJSON["data"]}");
				JToken dataToken = userJSON["data"];
				Dictionary<string, string> statusTable = new Dictionary<string, string>();

				foreach(JValue jValue in dataToken) {
					string id = jValue.ToString().Split(':')[0];
					string name = jValue.ToString().Split(':')[1];
					if(statusTable.ContainsKey(id)) {
						statusTable[id] = name;
					} else {
						statusTable.Add(id, name);
					}
				}

				foreach(string key in statusTable.Keys) {
					Log.track($"key: {key} , value: {statusTable[key]}");
					if("@".Equals(statusTable[key])) {
						removeUser(key);
					} else {
						addUser(key, statusTable[key]);
					}
				}
				int i = 0;
				foreach(UserItem u in FF14Chat_Main.userList) {
					Log.track($"{i} : id={u.Id},name={u.Name}");
				}

			} else if(401 == ((int)userJSON["code"])) {
				textCommand = "获取当前用户发生错误: " + "请先登录聊天账号";
			} else {
				if(userJSON != null) {
					if(userJSON["errorMessage"] != null) {
						textCommand = (string)userJSON["errorMessage"];
					} else if(userJSON["error"] != null) {
						textCommand = (string)userJSON["error"];
					} else {
						textCommand = "获取当前用户发生错误";
					}
				} else {
					textCommand = "获取当前用户发生错误";
				}
			}
			Log.track("");
			Log.track("");
			Log.track("");
			Log.track(textCommand);
			return textCommand;
		}
		private void addUser(string id, string name) {
			TransactionOptions options = new TransactionOptions();
			options.Timeout = TimeSpan.FromSeconds(3);

			using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options)) {
				if(FF14Chat_Main.userList.FindIndex(item => item.Id.Equals(id)) < 0) {
					UserItem item = new UserItem(id, name);
					DataGridViewRow row = new DataGridViewRow();
					DataGridViewCell cell = new DataGridViewTextBoxCell();
					cell.Value = item;
					row.Cells.Add(cell);
					FF14Chat_Main.userList.Add(item);
					main.dataGridUserAdd(row);
				}
				Log.track($"addUser : id={id}, name={name}");
				scope.Complete();
			}
			Log.info($"addUser: id={id} , name={name}");
		}
		private void removeUser(string id) {
			TransactionOptions options = new TransactionOptions();
			options.Timeout = TimeSpan.FromSeconds(3);

			using(TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options)) {
				int index = FF14Chat_Main.userList.FindIndex(item => item.Id.Equals(id));
				if(index >= 0) {
					FF14Chat_Main.userList.RemoveAt(index);
					main.dataGridUserRemove(index);
				}
				Log.track($"removeUser : id={id}");
				scope.Complete();
			}
			Log.info($"removeUser: id={id} ");
		}
		#endregion

		#region ProcessSwitcherGetMsg

		private long maxID = -1;
		public async void ProcessSwitcherGetMsg(object sender, DoWorkEventArgs e) {

			while(FF14Chat_Main.isRunning) {

				//读msg
				string msgData;
				if(maxID == -1) {
					msgData = await NetworkUtil.GetMsgsByStock(FF14Chat_Main.loginUserResult);
				} else {
					msgData = await NetworkUtil.GetMsgsByIncrement(FF14Chat_Main.loginUserResult, maxID);
				}

				Log.debug($"get ProcessSwitcherGetMsg {msgData}");
				if(msgData == null || "".Equals(msgData)) {
					main.addMessage("获取服务器数据失败");
				} else {
					JObject msgJSON = JObject.Parse(msgData);

					if(200 == ((int)msgJSON["code"])) {
						if(msgJSON["data"] != null && (string)msgJSON["data"]!="[]") {
							JArray dataArray = JArray.Parse(msgJSON["data"].ToString());
							foreach(JToken dataToken in dataArray) {
								Log.info(dataToken.ToString());
								JObject dataObject = JObject.Parse(dataToken.ToString());
								MsgItem msg = JsonConvert.DeserializeObject<MsgItem>(dataObject.ToString());

								if(FF14Chat_Main.allowWord.Count > 0) {
									bool allow = false;
									foreach(string word in FF14Chat_Main.allowWord) {
										if(msg.message.Contains(word)) {
											allow = true;
											break;
										}
									}
									if(!allow) {
										maxID = Math.Max(maxID, msg.Id);
										continue;
									}
								}
								if(FF14Chat_Main.notAllowWord.Count > 0) {
									bool notallow = false;
									foreach(string word in FF14Chat_Main.notAllowWord) {
										if(msg.message.Contains(word)) {
											notallow = true;
											break;
										}
									}
									if(notallow) {
										maxID = Math.Max(maxID, msg.Id);
										continue;
									}
								}
								if(FF14Chat_Main.userBlanklist.Count > 0) {
									bool notallow = false;
									foreach(string id in FF14Chat_Main.userBlanklist) {
										if(msg.UserId == long.Parse(id)) {
											notallow = true;
											break;
										}
									}
									if(notallow) {
										maxID = Math.Max(maxID, msg.Id);
										continue;
									}
								}

								if(1 == msg.Type) {
									if(ContainsAllowWord(msg.message) && !ContainsNotAllowWord(msg.message)) {
										msg.Time = "[" + DateTimeOffset.FromUnixTimeSeconds(msg.Timestamp / 1000).LocalDateTime.ToString("HH:mm:ss") + "]";
										main.dataGridMessage1Add(msg);
									}
								} else if(2 == msg.Type) {
									if(ContainsAllowWord(msg.message) && !ContainsNotAllowWord(msg.message)) {
										msg.Time = "[" + DateTimeOffset.FromUnixTimeSeconds(msg.Timestamp / 1000).LocalDateTime.ToString("HH:mm:ss") + "]";
										main.dataGridMessage2Add(msg);
									}
								} else if(3 == msg.Type) {
									if(ContainsAllowWord(msg.message) && !ContainsNotAllowWord(msg.message)) {
										msg.Time = "[" + DateTimeOffset.FromUnixTimeSeconds(msg.Timestamp / 1000).LocalDateTime.ToString("HH:mm:ss") + "]";
										main.dataGridMessage3Add(msg);
									}
								}
								maxID = Math.Max(maxID, msg.Id);
							}
						}
					} else if(401 == ((int)msgJSON["code"])) {
						main.addMessage("获取当前用户发生错误: " + "请先登录聊天账号");
					} else {
						string msgErrorMessage = null;
						if(msgJSON != null) {
							if(msgJSON["errorMessage"] != null) {
								msgErrorMessage = (string)msgJSON["errorMessage"];
							} else if(msgJSON["error"] != null) {
								msgErrorMessage = (string)msgJSON["error"];
							} else {
								msgErrorMessage = "发生错误";
							}
						} else {
							msgErrorMessage = "发生错误";
						}
						main.addMessage("获取当前用户发生错误: " + msgErrorMessage);
					}
				}
				Thread.Sleep(1000);
			}
		}

		private bool ContainsAllowWord(string message) {
			if(FF14Chat_Main.allowWord.Count == 0)
				return true;

			foreach(var word in FF14Chat_Main.allowWord) {
				if(message.Contains(word))
					return true;
			}
			return false;
		}

		private bool ContainsNotAllowWord(string message) {
			if(FF14Chat_Main.notAllowWord.Count == 0)
				return false;

			foreach(var word in FF14Chat_Main.notAllowWord) {
				if(message.Contains(word))
					return true;
			}
			return false;
		}
		#endregion

		#region oFormActMain_OnLogLineRead

		//[22:08:27.000] ChatLog 00:0011
		private Regex regexLogline = new Regex(@"\[\d{2}:\d{2}:\d{2}\.\d{3}\] ChatLog 00:001\d");

		//TODO: 会读取发送到游戏的消息
		public async void oFormActMain_OnLogLineRead(bool isImport, LogLineEventArgs logInfo) {
			if(FF14Chat_Main.loginUserResult == null)
				return;
			if(logInfo.logLine.Length < 30)
				return;

			string substring = logInfo.logLine.Substring(0, 30);

			if(regexLogline.Match(substring).Success) {
				Log.info($"get logline : {logInfo.logLine}");
				string channal = substring.Substring(26, 4);
				string substring2 = logInfo.logLine.Substring(31);
				string message = substring2.Substring(substring2.IndexOf(':') + 1);
				
				Log.info($"get message : {message}");

				for(int i = 0; i < 3; i++) {
					if(FF14Chat_Main.bindBucket[i].Equals(channal)) {
						if("/start".Equals(message)) {
							FF14Chat_Main.bindIsOpen[i] = true;
						} else if("/stop".Equals(message)) {
							FF14Chat_Main.bindIsOpen[i] = false;
						} else {

							if(!"0000".Equals(FF14Chat_Main.bindBucket[i]) && FF14Chat_Main.bindIsOpen[i]) {

								var jsonObject = new {
									message = message.Trim(),
									type = i+1,
								};
								string json = SerializeUtil.ToJson(jsonObject);
								string text = await NetworkUtil.sendMessage(FF14Chat_Main.loginUserResult, json);


								main.addMessage( text);
							}
						}
						break;
					}
				}
			}
		}

		public async void reportMessage(long msgID) {
			if(msgID == 0) {
				main.addMessage("系统消息无法举报");
				return;
			}

			var jsonObject = new {
				msgID = msgID
			};
			string response = await NetworkUtil.reportMessage(FF14Chat_Main.loginUserResult, SerializeUtil.ToJson(jsonObject)); //TODO
			main.addMessage(response);
		}
		#endregion

		

		#region ProcessSwitcher
		private bool hasAutoLogin = false;

		public void ProcessSwitcher(object sender, DoWorkEventArgs e) {

			//1. 获取ACT FFXIV解析插件
			FFXIV_ACT_Plugin = GetFFXIVPlugin();
			if(FFXIV_ACT_Plugin == null) {
				Log.error("FFXIV_ACT_Plugin null");
			}

			while(FF14Chat_Main.isRunning) {

				Process process = FFXIV_ACT_Plugin.DataRepository.GetCurrentFFXIVProcess();

				if(process != null) {
					int processID = process.Id;
					Log.info("processID:" + processID);


					if(processID != 0) {
						main.setLabelGameProcessStatus(processID);

						//getPlayerID
						FFXIV_ACT_Plugin.Common.Models.Player playeract = FFXIV_ACT_Plugin.DataRepository.GetPlayer();
						ulong playerContent = playeract.LocalContentId;
						Log.info("playerId: " + playeract.LocalContentId);
						main.setPlayerContent(playerContent);

						//autoLogin
						if(playerContent != 0 && (!FF14Chat_Main.isLogin) && !hasAutoLogin) {
							main.autoLogin(playerContent);
							hasAutoLogin = true;
						}
					}
				}

				Thread.Sleep(1000);
			}
		}


		private FFXIV_ACT_Plugin.FFXIV_ACT_Plugin GetFFXIVPlugin() {
			var plugin = ActGlobals.oFormActMain.ActPlugins.FirstOrDefault(x => x.pluginObj?.GetType().ToString() == "FFXIV_ACT_Plugin.FFXIV_ACT_Plugin")?.pluginObj;

			if(plugin != null) {
				Log.info("FFXIVPlugin Running...");
				main.setLabelFFXIVPluginStatusRun();
				return (FFXIV_ACT_Plugin.FFXIV_ACT_Plugin)plugin ?? throw new Exception("找不到FFXIV解析插件，请确保其加载顺序位于世界频道之前。");
			} else {
				Log.error("FFXIVPlugin null");
				throw new Exception("找不到FFXIV解析插件，请确保其加载顺序位于世界频道之前。");
			}
		}

		#endregion
	}
}
