using Advanced_Combat_Tracker;
using FF14Chat;
using FF14Chat.Common;
using FF14Chat.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF14Chat_c.Actions {
	public class Update {

		public async void getNewVersionAsync(FF14Chat_Main main) {
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
