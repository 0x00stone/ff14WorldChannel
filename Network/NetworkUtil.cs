
using FF14Chat.Common;
using FF14Chat.Controls;
using FF14Chat.Models;
using FF14Chat_c.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF14Chat.Network {
	public class NetworkUtil {
		private static HttpClient httpClient;

		private static string getServerAddress = "http://101.43.27.147/ff14chat/server/v1/serverList";
		private static string userRegisterAddress = "http://101.43.27.147/ff14chat/user/v1/register";
		private static string userLoginAddress = "http://101.43.27.147/ff14chat/user/v1/login";
		private static string getUsersAddress = "http://101.43.27.147/ff14chat/user/v1/getLoginUser/";
		private static string msgAddress = "http://101.43.27.147/ff14chat/msg/v1/";
		private static string postheartbeatAddress = "http://101.43.27.147/ff14chat/user/v1/heartbeat";
		private static string reportMessageAddress = "http://101.43.27.147/ff14chat/msg/v1/reportMsg";
		private static string getVersionAddress = "http://101.43.27.147/ff14chat/system/v1/getVersion";
		private static string getDllAddress = "http://101.43.27.147/FF14Chat-c.dll";
		private static string postNamazuAddress = "http://127.0.0.1:1002/command";


		public static async Task<LoginUserResult> loginUser(LoginUser loginUser) {
			if(loginUser.Name.Length >= 10) {
				MessageBox.Show("用户名称大于10个字符", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			if(System.Text.RegularExpressions.Regex.IsMatch(loginUser.Name, @"[^a-zA-Z0-9_\-\u4e00-\u9fa5]")) {
				MessageBox.Show("用户名只可以使用字母，数字，中文和_-", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			if(loginUser.Name == null || "".Equals(loginUser.Name) || loginUser.Password == null || "".Equals(loginUser.Password)) {
				MessageBox.Show("用户名或密码为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			if("".Equals(loginUser.ServerId) || loginUser.ServerId == null ) {
				MessageBox.Show("未选择服务器", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}

			var jsonObject = new {
				aliasName = loginUser.Name.Trim(),
				password = loginUser.Password.Trim().GetHashCode(),
				serverId = loginUser.ServerId.Trim()
			};
			string json = SerializeUtil.ToJson(jsonObject);

			return await NetworkUtil.Login(json);
		}

		public static async Task<bool> downloadDll() {

			HttpResponseMessage response = await httpClient.GetAsync(getDllAddress);

			if(response.IsSuccessStatusCode) {
				byte[] fileContents = await response.Content.ReadAsByteArrayAsync();
				string path = Path.Combine("H:\\ff14\\ACT\\AppData\\Advanced Combat Tracker", "../../Plugins/ff14Chat/FF14Chat-c.dll");

				File.WriteAllBytes(path, fileContents);
				Log.info("dll文件下载成功！");
				return true;
			} else {
				Log.info("dll文件下载失败！");
				return false;
			}
		}
	
		public static async Task<string> getVersion() {
			string responseData = await NetworkUtil.SendGetRequest(getVersionAddress);
			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取服务器版本数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "1.0.0.0";
			}

			dynamic json = JObject.Parse(responseData);
			return json["data"].ToString();
		}

		public static async Task getServer(Dictionary<string, Dictionary<string, string>> resultMap) {
			string responseData = await NetworkUtil.SendGetRequest(getServerAddress);
			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取服务器数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dynamic json = JObject.Parse(responseData);

			JToken dataToken = json["data"];
			foreach(JProperty property in dataToken) {

				Dictionary<string, string> partitionDictionary = new Dictionary<string, string>();

				foreach(JProperty server in property.Value)
					partitionDictionary.Add(server.Name, server.Value.Value<string>());

				resultMap.Add(property.Name, partitionDictionary);
			}
		}

		public static async void register(string json) {
			string responseData = await NetworkUtil.SendPostRequest(userRegisterAddress, json);
			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取服务器数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dynamic responseJson = JObject.Parse(responseData);

			if(200 == (int)responseJson["code"]) {
				MessageBox.Show("注册成功", "注册提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				string message = null;
				if(responseJson != null) {
					if(responseJson["errorMessage"] != null) {
						message = (string)responseJson["errorMessage"];
					} else if(responseJson["error"] != null) {
						message = (string)responseJson["error"];
					} else {
						message = "发生错误";
					}
				} else {
					message = "发生错误";
				}
				MessageBox.Show(message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		public static async Task<LoginUserResult> Login(string json) {
			string responseData = await NetworkUtil.SendPostRequest(userLoginAddress, json);

			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取登录数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}

			dynamic responseJson = JObject.Parse(responseData);
			LoginUserResult result = new LoginUserResult("", "", "", "");

			if(200 == ((int)responseJson["code"]) && responseJson["data"]!=null) {
				dynamic payload = JObject.Parse((string)responseJson["data"]);
				Log.info("LoginResponse: " + (string)responseJson["data"]);

				ulong contentId = ulong.Parse((string)payload["contentId"]);
				Log.info(contentId+"");
				if(FF14Chat_Main.playerContent!=0 && contentId != FF14Chat_Main.playerContent) { 
					//TODO:
					//1. 尝试切换用户
				}

				result.setToken((string)payload["token"]);
				result.setContent((string)payload["contentId"]);

				MessageBox.Show("登录成功", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				string message = null;
				if(responseJson != null) {
					if(responseJson["errorMessage"] != null) {
						message = (string)responseJson["errorMessage"];
					} else if(responseJson["error"] != null) {
						message = (string)responseJson["error"];
					} else {
						message = "发生错误";
					}
				} else {
					message = "发生错误";
				}
				MessageBox.Show(message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return result;
		}


		public static async Task<LoginUserResult> Heartbeat(LoginUserResult result) {
			string responseData = await NetworkUtil.SendHeartbeat(postheartbeatAddress, result.getToken());

			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取心跳服务器数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result.setToken("");
				return result;
			}

			dynamic responseJson = JObject.Parse(responseData);

			if(200 == ((int)responseJson["code"])) {
				dynamic payload = JObject.Parse((string)responseJson["data"]);
				Log.debug("LoginResponse: " + (string)responseJson["data"]);

				ulong contentId = ulong.Parse((string)payload["contentId"]);
				Log.debug(contentId + "");
				if(FF14Chat_Main.playerContent != 0 && contentId != FF14Chat_Main.playerContent) {
					//TODO:
					//1. 尝试切换用户
				}

				result.setToken((string)payload["token"]);
				result.setContent((string)payload["contentId"]);
			} else if(401 == ((int)responseJson["code"])) {

				var jsonObject = new {
					aliasName = result.getAliasname().Trim(),
					password = result.getPassword().Trim().GetHashCode(),
					serverId = result.getServerId()
				};

				string json = SerializeUtil.ToJson(jsonObject);
				result.setToken(((LoginUserResult)await NetworkUtil.Login(json)).getToken());

			} else {
				string message = null;
				if(responseJson != null) {
					if(responseJson["errorMessage"] != null) {
						message = (string)responseJson["errorMessage"];
					} else if(responseJson["error"] != null) {
						message = (string)responseJson["error"];
					} else {
						message = "发生错误";
					}
				} else {
					message = "发生错误";
				}
				MessageBox.Show(message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result.setToken("");
			}
			return result;
		}

		private static string timestamp = "";

		public static async Task<string> GetUsersByStock(LoginUserResult result) {
			string userResponse = await NetworkUtil.SendGetRequest(getUsersAddress + result.getServerId()+"/0");
			timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
			return userResponse;
		}

		public static async Task<string> GetUsersByIncrement(LoginUserResult result) {
			string userResponse = await NetworkUtil.SendGetRequest(getUsersAddress + result.getServerId() + "/" + timestamp);
			timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
			Log.info("GetUsersByIncrement ,time：" + timestamp);
			return userResponse;
		}

		public static async Task<string> GetMsgsByStock(LoginUserResult result) {
			return await NetworkUtil.SendGetRequest(msgAddress + result.getServerId());
		}

		public static async Task<string> GetMsgsByIncrement(LoginUserResult result,long maxID) {
			return await NetworkUtil.SendGetRequest(msgAddress + result.getServerId() + "/" + maxID);
		}

		public static async Task<string> reportMessage(LoginUserResult result, string json) { 
			Log.info("report msg : "+json);
			string responseData = await NetworkUtil.SendMessageRequest(reportMessageAddress, result.getToken(), json);
			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取msg服务器数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "发生错误";
			}
			dynamic responseJson = JObject.Parse(responseData);
			string text = "";

			if(200 == ((int)responseJson["code"])) {
				text = responseJson["data"];
			} else if(401 == ((int)responseJson["code"])) {
				text = "请登录聊天账户";
			} else {
				if(responseJson != null) {
					if(responseJson["errorMessage"] != null) {
						text = (string)responseJson["errorMessage"];
					} else if(responseJson["error"] != null) {
						text = (string)responseJson["error"];
					} else {
						text = "发生错误";
					}
				} else {
					text = "发生错误";
				}
			}
			return text;
		}

		public static async Task<string> sendMessage(LoginUserResult result,string json) {
			string responseData = await NetworkUtil.SendMessageRequest(msgAddress, result.getToken(), json);
			if(responseData == null || "".Equals(responseData)) {
				MessageBox.Show("获取msg服务器数据失败", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "发生错误";
			}
			dynamic responseJson = JObject.Parse(responseData);
			string text = "";

			if(200 == ((int)responseJson["code"])) {
				text = "发送成功";
			} else if(401 == ((int)responseJson["code"])) {
				text = "请登录聊天账户";
			} else {
				if(responseJson != null) {
					if(responseJson["errorMessage"] != null) {
						text = (string)responseJson["errorMessage"];
					} else if(responseJson["error"] != null) {
						text = (string)responseJson["error"];
					} else {
						text = "发生错误";
					}
				} else {
					text = "发生错误";
				}
			}
			return text;
		}

		public static async void sendToPostNamazu(string s) {
			NetworkUtil.SendPostRequest(postNamazuAddress, s);
		}

		public static async Task<string> SendGetRequest(string url) {
			if(httpClient == null)
				httpClient = new HttpClient();
			Log.debug($"SendGetRequest - url:{url}");
			try {
				HttpResponseMessage response = await httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();

				string responseData = await response.Content.ReadAsStringAsync();

				return responseData;
			} catch(Exception ex) {
				Log.error($"SendGetRequest: {ex.Message}");
				return null;
			}
		}



		public static async Task<string> SendPostRequest(string url, string requestBody) {
			if(httpClient == null)
				httpClient = new HttpClient();
			Log.debug($"SendPostRequest - url:{url} - requestBody{requestBody}");
			try {
				StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				string responseData = await response.Content.ReadAsStringAsync();

				return responseData;
			} catch(Exception ex) {
				Log.error($"SendPostRequest: { ex.Message}");
				return null;
			}
		}

		public static async Task<string> SendHeartbeat(string url,string token) {
			if(httpClient == null)
				httpClient = new HttpClient();
			Log.debug($"SendHeartbeat - url:{url} - token:{token}");
			httpClient.DefaultRequestHeaders.Add("token", token);
			string responseData = null;

			try {
				StringContent content = new StringContent("", Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				responseData = await response.Content.ReadAsStringAsync();

				
			} catch(Exception ex) {
				Log.error($"SendHeartbeat: {ex.ToString()}");
			}
			httpClient.DefaultRequestHeaders.Remove("token");
			return responseData;
		}

		public static async Task<string> SendMessageRequest(string url,string token, string requestBody) {
			if(httpClient == null)
				httpClient = new HttpClient();
			Log.debug($"SendMessageRequest - url:{url} - token:{token} - requestBody{requestBody}");
			string responseData="";
			try {
				httpClient.DefaultRequestHeaders.Add("token", token);
				StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				responseData = await response.Content.ReadAsStringAsync();

			} catch(Exception ex) {
				Log.error(ex.Message);
			} 
			httpClient.DefaultRequestHeaders.Remove("token");
			return responseData;
		}
	}
}
