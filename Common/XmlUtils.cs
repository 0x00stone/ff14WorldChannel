using Advanced_Combat_Tracker;
using FF14Chat.Common;
using FF14Chat_c.Models;
using System;
using System.IO;
using System.Xml;

namespace FF14Chat.Controls {
	public class XmlUtils {
		//用户xml加载用户信息
		//系统xml加载 通讯贝，聊天过滤器，黑名单

		public static readonly string UserSettingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "ff14Chat\\ff14WroldChannel.userconfig.xml");
		public static readonly string SysSettingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "ff14Chat\\ff14WroldChannel.sysconfig.xml");

		private static string lowerBound = "0010";
		private static string upperBound = "0017";

		public static void LoadSysSettings(FF14ChatUi ff14ChatUi) {
			if(File.Exists(SysSettingsFile)) {
				XmlDocument doc = new XmlDocument();
				try {
					doc.Load(SysSettingsFile);

					XmlNode configNode = doc.SelectSingleNode("/root/bind");
					if(configNode == null)
						throw new InvalidOperationException("Config node not found in the XML document.");

					string bindBucket0 = configNode.SelectSingleNode("bindBucket0")?.InnerText;
					string bindBucket1 = configNode.SelectSingleNode("bindBucket1")?.InnerText;
					string bindBucket2 = configNode.SelectSingleNode("bindBucket2")?.InnerText;

					if(bindBucket0 != null) 
						if("0000".Equals(bindBucket0)) {
							FF14Chat_Main.bindBucket[0] = "0000";
							ff14ChatUi.comboBox1.SelectedIndex = 0;
							Log.info($"get bind : bindBucket0 - 0");
						} else if(bindBucket0.CompareTo(lowerBound) >= 0 && bindBucket0.CompareTo(upperBound) <= 0) {
							FF14Chat_Main.bindBucket[0] = bindBucket0;
							ff14ChatUi.comboBox1.SelectedIndex = int.Parse(bindBucket0) - 9;
							Log.info($"get bind : bindBucket0 - {int.Parse(bindBucket0) - 9}");
						}
					

					if(bindBucket1 != null) 
						if("0000".Equals(bindBucket1)) {
							FF14Chat_Main.bindBucket[1] = "0000";
							ff14ChatUi.comboBox2.SelectedIndex = 0;
							Log.info($"get bind : bindBucket1 - 0");
						} else if(bindBucket1.CompareTo(lowerBound) >= 0 && bindBucket1.CompareTo(upperBound) <= 0) {
							FF14Chat_Main.bindBucket[1] = bindBucket0;
							ff14ChatUi.comboBox2.SelectedIndex = int.Parse(bindBucket1) - 9;
							Log.info($"get bind : bindBucket1 - {int.Parse(bindBucket1) - 9}");
						}
					

					if(bindBucket2 != null) 
						if("0000".Equals(bindBucket2)) {
							FF14Chat_Main.bindBucket[2] = "0000";
							ff14ChatUi.comboBox3.SelectedIndex = 0;
							Log.info($"get bind : bindBucket2 - 0");
						} else if(bindBucket2.CompareTo(lowerBound) >= 0 && bindBucket2.CompareTo(upperBound) <= 0) {
							FF14Chat_Main.bindBucket[2] = bindBucket2;
							ff14ChatUi.comboBox3.SelectedIndex = int.Parse(bindBucket2) - 9;
							Log.info($"get bind : bindBucket2 - {int.Parse(bindBucket2) - 9}");
						}
					

					XmlNode ruleNode = doc.SelectSingleNode("/root/rule");
					XmlNodeList allowList = ruleNode?.SelectNodes("allow");
					if(allowList != null) {
						foreach(XmlNode allow in allowList) {
							ff14ChatUi.addAllowWord(allow.InnerText);
							Log.info($"get allow word : {allow.Name} - {allow.InnerText}");
						}
					}
					XmlNodeList notallowList = ruleNode?.SelectNodes("notAllow");
					if(notallowList != null) {
						foreach(XmlNode notallow in notallowList) {
							ff14ChatUi.addNotAllowWord(notallow.InnerText);
							Log.info($"get not allow word : {notallow.Name} - {notallow.InnerText}");
						}
					}

					XmlNode blacklistNode = doc.SelectSingleNode("/root/blackList");
					if(blacklistNode != null) {
						foreach(XmlNode node in blacklistNode.ChildNodes) {
							string id = node.Name.Substring(2, node.Name.Length-2);
							ff14ChatUi.blacklistAddUser( node.InnerText,id );
							Log.info($"get blackList user : {id} - {node.InnerText}");
						}
					}


				} catch(Exception e) {
					Log.error("配置sys文件载入异常");
					Log.error("已清除错误的配置文件");
					Log.error("设置已被重置");
					Log.error(e.Message);
				}
			}
		}


		public static void SaveSysSettingsBind(string bind,int bucket) {
			XmlDocument doc = new XmlDocument();

			if(!File.Exists(SysSettingsFile)) {
				string directoryPath = Path.GetDirectoryName(SysSettingsFile);
				Directory.CreateDirectory(directoryPath);

				XmlElement rootElement = doc.CreateElement("root");
				doc.AppendChild(rootElement);
			} else {
				doc.Load(SysSettingsFile);
			}


			XmlNodeList configElements = doc.GetElementsByTagName("bind");
			if(configElements.Count == 0) {
				XmlElement configElement = doc.CreateElement("bind");
				doc.DocumentElement?.AppendChild(configElement);

				XmlNode BindNode = doc.CreateElement("bindBucket"+bucket);
				configElement?.AppendChild(BindNode);
				BindNode.InnerText = bind;
			} else {
				bool isExist = false;
				foreach(XmlElement temp in configElements) {
					foreach(XmlElement a in temp) {
						if(("bindBucket" + bucket).Equals(a.Name)) {
							a.InnerText = bind;
							isExist = true;
							break;
						}
					}
				}
				if(!isExist) {
					XmlNode configElement = doc.SelectSingleNode("/root/bind");
					XmlNode BindNode = doc.CreateElement("bindBucket" + bucket);
					configElement.AppendChild(BindNode);
					BindNode.InnerText = bind;
				}
			}
			doc.Save(SysSettingsFile);
		}

		public static void SaveSysSettingsWord(bool isBanned,string word) {
			if(word == null || "".Equals(word))
				return;

			XmlDocument doc = new XmlDocument();

			if(!File.Exists(SysSettingsFile)) {
				string directoryPath = Path.GetDirectoryName(SysSettingsFile);
				Directory.CreateDirectory(directoryPath);

				XmlElement rootElement = doc.CreateElement("root");
				doc.AppendChild(rootElement);
			} else {
				doc.Load(SysSettingsFile);
			}

			XmlNodeList configElements = doc.GetElementsByTagName("rule");
			if(configElements.Count == 0) {
				XmlElement configElement = doc.CreateElement("rule");
				doc.DocumentElement?.AppendChild(configElement);
			}

			bool isExist = false;

			XmlNodeList allowElements = doc.GetElementsByTagName("allow");
			if(allowElements.Count != 0) {
				foreach(XmlNode allowNode in allowElements) {
					if(word.Equals(allowNode.InnerText)) {
						isExist = true;
					}
				}
			}

			XmlNodeList notAllowElements = doc.GetElementsByTagName("notAllow");
			if(notAllowElements.Count != 0) {
				foreach(XmlNode notAllowNode in notAllowElements) {
					if(word.Equals(notAllowNode.InnerText)) {
						isExist = true;
					}
				}
			}

			if(!isExist) {
				XmlElement newElement;
				if(isBanned) {
					newElement = doc.CreateElement("notAllow");
					
				} else {
					newElement = doc.CreateElement("allow");
				}
				newElement.InnerText = word;
				doc.SelectSingleNode("/root/rule").AppendChild(newElement);

				doc.Save(SysSettingsFile);
			}
		}

		public static void SaveSysSettingsRemoveWord(string word) {
			if(word == null || "".Equals(word))
				return;

			XmlDocument doc = new XmlDocument();

			if(!File.Exists(SysSettingsFile)) {
				string directoryPath = Path.GetDirectoryName(SysSettingsFile);
				Directory.CreateDirectory(directoryPath);

				XmlElement rootElement = doc.CreateElement("root");
				doc.AppendChild(rootElement);
			} else {
				doc.Load(SysSettingsFile);
			}

			XmlNodeList configElements = doc.GetElementsByTagName("rule");
			if(configElements.Count == 0) {
				XmlElement configElement = doc.CreateElement("rule");
				doc.DocumentElement?.AppendChild(configElement);
			}

			XmlNode removeNode = null;
			XmlNodeList allowElements = doc.GetElementsByTagName("allow");
			if(allowElements.Count != 0) {
				foreach(XmlNode allowNode in allowElements) {
					if(word.Equals(allowNode.InnerText)) {
						removeNode = allowNode; break;
					}
				}
			}

			XmlNodeList notAllowElements = doc.GetElementsByTagName("notAllow");
			if(notAllowElements.Count != 0) {
				foreach(XmlNode notAllowNode in notAllowElements) {
					if(word.Equals(notAllowNode.InnerText)) {
						removeNode = notAllowNode; break;
					}
				}
			}

			if(removeNode != null) {
				doc.SelectSingleNode("/root/rule").RemoveChild(removeNode);

				doc.Save(SysSettingsFile);
			}
		}

		public static void SaveSysSettingsBlack(string id,string name) {
			if(name == null || "".Equals(name) || id==null || "0".Equals(id))
				return;
			id = "id" + id;

			XmlDocument doc = new XmlDocument();

			if(!File.Exists(SysSettingsFile)) {
				string directoryPath = Path.GetDirectoryName(SysSettingsFile);
				Directory.CreateDirectory(directoryPath);

				XmlElement rootElement = doc.CreateElement("root");
				doc.AppendChild(rootElement);
			} else {
				doc.Load(SysSettingsFile);
			}

			XmlNodeList configElements = doc.GetElementsByTagName("rule");
			if(configElements.Count == 0) {
				XmlElement configElement = doc.CreateElement("rule");
				doc.DocumentElement?.AppendChild(configElement);
			}
			XmlNode blackListNode = doc.SelectSingleNode("/root/blackList");

			bool isExist = false;

			if(blackListNode != null) {
				foreach(XmlNode node in blackListNode.ChildNodes) {
					if(name.Equals(node.InnerText) && id.Equals(node.Name)) {
						isExist = true;
					}
				}
			} else {
				XmlElement newElement = doc.CreateElement("blackList");
				doc.SelectSingleNode("/root").AppendChild(newElement);
			}

			if(!isExist) {
				XmlElement newElement = doc.CreateElement(id);
				newElement.InnerText = name;

				doc.SelectSingleNode("/root/blackList").AppendChild(newElement);
				doc.Save(SysSettingsFile);
			}
		}

		public static void SaveSysSettingsRemoveBlackList(string id) {
			if( id == null || "".Equals(id))
				return;
			id = "id" + id;

			XmlDocument doc = new XmlDocument();

			if(!File.Exists(SysSettingsFile)) {
				string directoryPath = Path.GetDirectoryName(SysSettingsFile);
				Directory.CreateDirectory(directoryPath);

				XmlElement rootElement = doc.CreateElement("root");
				doc.AppendChild(rootElement);
			} else {
				doc.Load(SysSettingsFile);
			}

			XmlNodeList configElements = doc.GetElementsByTagName("rule");
			if(configElements.Count == 0) {
				XmlElement configElement = doc.CreateElement("rule");
				doc.DocumentElement?.AppendChild(configElement);
			}

			XmlNode removeNode = null;
			XmlNode blackListNode = doc.SelectSingleNode("/root/blackList");

			if(blackListNode != null) {
				foreach(XmlNode node in blackListNode.ChildNodes) {
					if( id.Equals(node.Name)) {
						removeNode = node;
					}
				}
			}

			if(removeNode != null) {
				doc.SelectSingleNode("/root/blackList").RemoveChild(removeNode);
				doc.Save(SysSettingsFile);
			}
		}

		public static void LoadUserSettings(LoginForm loginForm) {
			if(File.Exists(UserSettingsFile)) {
				XmlDocument doc = new XmlDocument();
				try {
					doc.Load(UserSettingsFile);

					XmlNode configNode = doc.SelectSingleNode("/root/config");
					if(configNode == null)
						throw new InvalidOperationException("Config node not found in the XML document.");
					string rememberPassword = configNode.SelectSingleNode("rememberPassword")?.InnerText;
					string lastLoginUser = configNode.SelectSingleNode("lastLoginuser")?.InnerText;
					string autoLogin = configNode.SelectSingleNode("isautoLogin")?.InnerText;

					loginForm.setRememberaliasname(bool.Parse(rememberPassword));
					loginForm.setAutoLogin(bool.Parse(autoLogin));


					if(bool.Parse(rememberPassword)) {
						XmlNode userNode = doc.SelectSingleNode($"/root/id{lastLoginUser}");
						if(userNode != null) {
							loginForm.setServerId(userNode.SelectSingleNode("serverName")?.InnerText);
							loginForm.setPassword(userNode.SelectSingleNode("password")?.InnerText);
							loginForm.setAliasName(userNode.SelectSingleNode("name")?.InnerText);
						}
					}

				} catch(Exception) {
					Log.error("配置user文件载入异常");
					Log.error("已清除错误的配置文件");
					Log.error("设置已被重置");
					File.Delete(UserSettingsFile);
				}
			}
		}

		public static LoginUser LoadUserSettingsByAutoLogin(ulong content) {
			if(File.Exists(UserSettingsFile)) {
				LoginUser loginUser = new LoginUser();
				XmlDocument doc = new XmlDocument();
				try {
					doc.Load(UserSettingsFile);

					XmlNode configNode = doc.SelectSingleNode("/root/config");
					if(configNode == null)
						throw new InvalidOperationException("Config node not found in the XML document.");
					string autoLogin = configNode.SelectSingleNode("isautoLogin")?.InnerText;

					XmlNode userNode = doc.SelectSingleNode($"/root/id{content}");
					if(userNode != null && bool.Parse(autoLogin)) {
						loginUser.ServerId = userNode.SelectSingleNode("serverID")?.InnerText;
						loginUser.Password = userNode.SelectSingleNode("password")?.InnerText;
						loginUser.Name = userNode.SelectSingleNode("name")?.InnerText;
					}
					return loginUser;
				} catch(Exception) {
					Log.error("配置user文件载入异常");
					Log.error("已清除错误的配置文件");
					Log.error("设置已被重置");
					File.Delete(UserSettingsFile);
				}
			}
			return null;
		}

		public static void SaveUserSettings(bool isRememberPassword, bool isAutologin, string aliasname, string serverName, string password,string contentID,string serverId) {
			XmlDocument doc = new XmlDocument();

			if(!File.Exists(UserSettingsFile)) {
				string directoryPath = Path.GetDirectoryName(UserSettingsFile);
				Directory.CreateDirectory(directoryPath);

				XmlElement rootElement = doc.CreateElement("root");
				doc.AppendChild(rootElement);
			} else {
				doc.Load(UserSettingsFile);
			}


			XmlNodeList configElements = doc.GetElementsByTagName("config");
			if(configElements.Count == 0) {
				XmlElement configElement = doc.CreateElement("config");
				doc.DocumentElement?.AppendChild(configElement);

				XmlElement rememberPasswordNode = doc.CreateElement("rememberPassword");
				configElement?.AppendChild(rememberPasswordNode);

				XmlElement lastLoginuserNode = doc.CreateElement("lastLoginuser");
				configElement?.AppendChild(lastLoginuserNode);

				XmlElement autoLoginNode = doc.CreateElement("isautoLogin");
				configElement?.AppendChild(autoLoginNode);

				rememberPasswordNode.InnerText = isRememberPassword.ToString();
				autoLoginNode.InnerText = isAutologin.ToString();
				lastLoginuserNode.InnerText = contentID;
			} else {
				foreach(XmlElement temp in configElements) {
					foreach(XmlElement a in temp) {
						if("rememberPassword".Equals(a.Name)) {
							a.InnerText = isRememberPassword.ToString();
						} else if("lastLoginuser".Equals(a.Name)) {
							a.InnerText = contentID;
						} else if("isautoLogin".Equals(a.Name)) {
							a.InnerText = isAutologin.ToString();
						}
					}
				}
			}

			XmlNodeList nodeList = doc.GetElementsByTagName("id"+contentID);
			if(nodeList.Count == 0) {
				XmlElement usernameElement = doc.CreateElement("id" + contentID);
				doc.DocumentElement?.AppendChild(usernameElement);

				XmlElement serverNameElement = doc.CreateElement("serverName");
				serverNameElement.InnerText = serverName;
				usernameElement?.AppendChild(serverNameElement);

				XmlElement passwordElement = doc.CreateElement("password");
				passwordElement.InnerText = password;
				usernameElement?.AppendChild(passwordElement);

				XmlElement nameElement = doc.CreateElement("name");
				nameElement.InnerText = aliasname;
				usernameElement?.AppendChild(nameElement);

				XmlElement serverIdElement = doc.CreateElement("serverID");
				serverIdElement.InnerText = serverId.ToString();
				usernameElement?.AppendChild(serverIdElement);
			} else {
				foreach(XmlElement temp in nodeList) {
					foreach(XmlElement a in temp) {
						if("serverName".Equals(a.Name)) {
							a.InnerText = serverName;
						} else if("password".Equals(a.Name)) {
							a.InnerText = password;
						} else if("name".Equals(a.Name)) {
							a.InnerText = aliasname;
						} else if("serverID".Equals(a.Name)) {
							a.InnerText = serverId.ToString();
						}
					}
				}
			}
			doc.Save(UserSettingsFile);
		}
	}
}
