
using FF14Chat.Common;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF14Chat.Network {
	public class Update {

		public async void getNewVersionAsync() {
			Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
			Version latestVersion = await GetLatestVersionAsync();

			Log.info($"currentVersion: {currentVersion}");
			Log.info($"latestVersion: {latestVersion}");

			if(currentVersion < latestVersion) {
				bool isDownload = false;
				while(!isDownload) {
					isDownload = await NetworkUtil.downloadDll();
				}

				MessageBox.Show("ff14chat 版本已更新，请重新启动ACT", "更新提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private async Task<Version> GetLatestVersionAsync() {
			string version = await NetworkUtil.getVersion();
			return new Version(version);
		}

	}
}
