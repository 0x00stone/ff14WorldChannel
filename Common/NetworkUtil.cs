
using Advanced_Combat_Tracker;
using FF14Chat.Common;
using FF14Chat.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF14Chat.Controls {
	public class NetworkUtil {
		private static HttpClient httpClient;

		private static string getServerAddress = "/ff14chat/server/v1/serverList";
		private static string userRegisterAddress = "/ff14chat/user/v1/register";
		private static string userLoginAddress = "/ff14chat/user/v1/login";
		private static string getUsersAddress = "/ff14chat/user/v1/getLoginUser/";
		private static string msgAddress = "/ff14chat/msg/v1/";
		private static string postheartbeatAddress = "/ff14chat/user/v1/heartbeat";
		private static string reportMessageAddress = "/ff14chat/msg/v1/reportMsg";
		private static string getVersionAddress = "/ff14chat/system/v1/getVersion";
		private static string getDllAddress = "/FF14Chat-c.dll";

		public static void setURL(string url) {
			getServerAddress = url + getServerAddress;
			userRegisterAddress = url + userRegisterAddress;
			userLoginAddress = url + userLoginAddress;
			getUsersAddress = url + getUsersAddress;
			msgAddress = url + msgAddress;
			postheartbeatAddress = url + postheartbeatAddress;
			reportMessageAddress = url + reportMessageAddress;
			getVersionAddress = url + getVersionAddress;
			getDllAddress = url + getDllAddress;
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

			if(200 == ((int)responseJson["code"])) {
				MessageBox.Show("登录成功", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result.setToken((string)responseJson["data"]);
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
				result.setToken((string)responseJson["data"]);
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
			timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
			string userResponse = await NetworkUtil.SendGetRequest(getUsersAddress + result.getServerId());
			return userResponse;
		}

		public static async Task<string> GetUsersByIncrement(LoginUserResult result) {
			string userResponse = await NetworkUtil.SendGetRequest(getUsersAddress + result.getServerId() + "/" + timestamp);
			timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
			Log.info("GetUsersByIncrement ,time" + timestamp);
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

		public static async Task<string> SendGetRequest(string url) {
			if(httpClient == null)
				httpClient = new HttpClient();
			try {
				HttpResponseMessage response = await httpClient.GetAsync(url);
				response.EnsureSuccessStatusCode();

				string responseData = await response.Content.ReadAsStringAsync();

				return responseData;
			} catch(Exception ex) {
				return null;
			}
		}
		public static async Task<string> SendPostRequest(string url, string requestBody) {
			if(httpClient == null)
				httpClient = new HttpClient();
			try {
				StringContent content = new StringContent(requestBody, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				string responseData = await response.Content.ReadAsStringAsync();

				return responseData;
			} catch(Exception ex) {
				return null;
			}
		}

		public static async Task<string> SendHeartbeat(string url,string token) {
			if(httpClient == null)
				httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Add("token", token);
			string responseData = null;

			try {
				StringContent content = new StringContent("", Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, content);
				response.EnsureSuccessStatusCode();

				responseData = await response.Content.ReadAsStringAsync();

				
			} catch(Exception ex) {
				
			}
			httpClient.DefaultRequestHeaders.Remove("token");
			return responseData;
		}

		public static async Task<string> SendMessageRequest(string url,string token, string requestBody) {
			if(httpClient == null)
				httpClient = new HttpClient();
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
