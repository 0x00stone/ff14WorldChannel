
using FF14Chat.Common;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF14Chat.Network {
	public class Update {

		private static string getDllAddress = "http://101.43.27.147/FF14Chat-c.dll";
		private static string getVersionAddress = "http://101.43.27.147/ff14chat/system/v1/getVersion";



		public async void getNewVersionAsync() {
			Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
			Version latestVersion = await GetLatestVersionAsync();

			Log.info($"currentVersion: {currentVersion}");
			Log.info($"latestVersion: {latestVersion}");

			if(currentVersion < latestVersion) {
				bool isDownload = false;
				while(!isDownload) {
					isDownload = await downloadDll();
				}

				MessageBox.Show("ff14chat 版本已更新，请重新启动ACT", "更新提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async Task<Version> GetLatestVersionAsync() {
			string version = await getVersion();
			return new Version(version);
		}
		public static async Task<bool> downloadDll() {

			HttpResponseMessage response = await NetworkUtil.httpClient.GetAsync(getDllAddress);

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

	}
}
