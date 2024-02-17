using Advanced_Combat_Tracker;
using FF14Chat.Common;
using FF14Chat.Models;
using FFXIV_ACT_Plugin.Common;
using GreyMagic;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FF14Chat.Actions {
	public class Inject {
		private FFXIV_ACT_Plugin.FFXIV_ACT_Plugin FFXIV_ACT_Plugin;

		internal Process FFXIV;
		IntPtr instanceAddress;
		internal ExternalProcessMemory Memory;
		internal SigScanner SigScanner;
		private IntPtr _entrancePtr;
		private FF14Chat_Main main;

		public Inject(FF14Chat_Main main) {
			this.main = main;
		}

		public void ProcessSwitcher(object sender, DoWorkEventArgs e) {

			//1. 获取ACT FFXIV解析插件
			FFXIV_ACT_Plugin = GetFFXIVPlugin();
			if(FFXIV_ACT_Plugin == null) {
				Log.error("FFXIV_ACT_Plugin NULL");
			}

			while(true) {
				if(main.getInjectProcessSwitcher().CancellationPending) {
					Log.info("processSwitcher cancel");
					e.Cancel = true;
					break;
				}

				try {
					Log.info("1. try to get ffxivProcess");
					if(FFXIV == GetFFXIVProcess()) {
						getPlayer();
						continue;
					}

					Log.info("2. try to game detch");
					gameDetach();

					Log.info("3. try to get ffxivProcess again");
					FFXIV = GetFFXIVProcess();

					Log.info("4. try to game detch");
					if(FFXIV == null) {
						Log.error("ProcessSwitcher FFXIV NULL");
						continue;
					}

					Log.info("5. try to get ffxiv process name");
					if(FFXIV.ProcessName == "ffxiv") {
						Log.error("错误：游戏运行于DX9模式下");
					} else if(GetOffsets()) {
						Log.info("6. try to attach game");
						Attach();
					}

				} catch(Exception ex) {
					Log.error(ex.Message);
				} finally {
					Thread.Sleep(3000);
				}
			}
		}


		private FFXIV_ACT_Plugin.FFXIV_ACT_Plugin GetFFXIVPlugin() {
			var plugin = ActGlobals.oFormActMain.ActPlugins.FirstOrDefault(x => x.pluginObj?.GetType().ToString() == "FFXIV_ACT_Plugin.FFXIV_ACT_Plugin")?.pluginObj;

			if(plugin != null) {
				Log.info("FFXIVPlugin Running...");
				main.setLabelFFXIVPluginStatusRun();
				return (FFXIV_ACT_Plugin.FFXIV_ACT_Plugin)plugin ?? throw new Exception("找不到FFXIV解析插件，请确保其加载顺序位于鲶鱼精邮差之前。");
			} else {
				Log.error("FFXIVPlugin null");
				throw new Exception("找不到FFXIV解析插件，请确保其加载顺序位于鲶鱼精邮差之前。");
			}
		}


		private Process GetFFXIVProcess() {
			return FFXIV_ACT_Plugin.DataRepository.GetCurrentFFXIVProcess();
		}

		private bool GetOffsets() {
			SigScanner = new SigScanner(FFXIV);
			try {
				_entrancePtr = SigScanner.ScanText("4C 8B DC 53 56 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 83 B9");
				Log.info("Getting Offsets _entrancePtr success");
				return true;
			} catch(ArgumentOutOfRangeException) {
				Log.error("无法对当前进程注入\n可能是已经被其他进程注入了");
			}

			try {
				_entrancePtr = SigScanner.ScanText("E9 ?? ?? ?? ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 84 24 ?? ?? ?? ?? 48 83 B9");
				Log.info("Getting Offsets _entrancePtr success");
				return true;
			} catch(ArgumentOutOfRangeException) {
				Log.error("无法对当前进程注入\n可能是已经被其他进程注入了？");
			}
			Log.info("Getting Offsets _entrancePtr fail");
			return false;
		}

		private void Attach() {
			Log.info("Getting Attcahing......");
			try {
				Memory = new ExternalProcessMemory(FFXIV, true, false, _entrancePtr, false, 5, true);
				main.setLabelGameProcessStatus(FFXIV.Id);
				Log.info("已找到FFXIV进程 "+FFXIV.Id);
			} catch(Exception ex) {
				Log.error("注入进程时发生错误！\n"+ex);
				MessageBox.Show("注入进程时发生错误！\n"+ex, "世界频道", MessageBoxButtons.OK, MessageBoxIcon.Error);
				gameDetach();
			}
			Command command = new Command();
			command.Setup(SigScanner, Memory);

			IntPtr staticAddress = SigScanner.ScanText("48 8D 0D ?? ?? ?? ?? 4D 8B F9") + 3;
			instanceAddress = new IntPtr(staticAddress.ToInt64() + this.Memory.Read<uint>(staticAddress) + 4);

			main.setCommand(command);
		}



		private unsafe void getPlayer() {
			if(instanceAddress == null || Memory == null) {
				Log.error("Process getPlayer()  instanceAddress == null || Memory == null");
				return;
			}
			Player player = this.Memory.Read<Player>(instanceAddress);
			main.setPlayer(player);
			string playerName = Encoding.UTF8.GetString(player.CharacterName, 64);
			main.setPlayerName(playerName);

			/*Log.info($"playerName: {playerName}");
			Log.info($"PSNOnlineID: {Encoding.UTF8.GetString(player.PSNOnlineID, 17)}");
			Log.info($"ObjectId: {player.ObjectId}");
			Log.info($"ContentId: {player.ContentId}");
			Log.info($"Sex: {player.Sex}");
			Log.info($"Tribe: {player.Tribe}");
			Log.info($"GuardianDeity: {player.GuardianDeity}");
			Log.info($"BirthMonth: {player.BirthMonth}");
			Log.info($"BirthDay: {player.BirthDay}");
			Log.info($"StartTown: {player.StartTown}");*/

			FFXIV_ACT_Plugin.Common.Models.Player playeract =  FFXIV_ACT_Plugin.DataRepository.GetPlayer();
			Log.info($"ContentId: {player.ContentId}");
			Log.info("playerId:" +playeract.LocalContentId);
			Log.info("playerId2:" + FFXIV_ACT_Plugin.DataRepository.GetCurrentPlayerID());
			if((!FF14Chat_Main.isLogin)  && player.ContentId != 0) {
				main.autoLogin();
				Thread.Sleep(5000);
			}
		
		}

		public void gameDetach() {
			Log.info("Detaching......");
			try {
				if(Memory != null && !Memory.Process.HasExited)
					Memory.Dispose();
			} catch(Exception) {
				// ignored
			}
		}
	}
}
