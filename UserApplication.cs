
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using FF14Chat.Controls;
using FF14Chat.Models;
using FF14Chat.Common;
using FF14Chat.Actions;
using System.Collections.Generic;
using System;

namespace FF14Chat {
	public class FF14Chat_Main : IActPluginV1 {

		private System.Windows.Forms.Label _lblStatus;

		private Inject inject;
		private FF14ChatUi PluginUI;
		private Service service;

		private Player player;
		private string playerName="";
		public static LoginUserResult loginUserResult;

		private BackgroundWorker _injectProcessSwitcher;
		private BackgroundWorker _ServiceProcessSwitcher;

		public static bool isReady = false;
		public static bool isLogin = false;
		public static HashSet<String> allowWord;
		public static HashSet<String> notAllowWord;
		public static List<String> userBlanklist; //存userID
		public static List<UserItem> userList;
		public static string[] bindBucket;
		// 0 : "0010"
		// 1 : "0015"
		// 2 : "0017"


		#region init/de
		public void InitPlugin(TabPage pluginScreenSpace, System.Windows.Forms.Label pluginStatusText) {
			pluginScreenSpace.Text = "世界频道";
			_lblStatus = pluginStatusText;

			init(pluginScreenSpace);
		}

		public void DeInitPlugin() {
			isReady = false;
			detch();
			_lblStatus.Text = "世界频道停止 ";
		}

		public void init(TabPage pluginScreenSpace) {
			allowWord = new HashSet<String>();
			notAllowWord = new HashSet<String>();
			userBlanklist = new List<String>();
			userList = new List<UserItem>();
			bindBucket = new string[] { "0000", "0000", "0000" };

			Log.init();
			inject = new Inject(this);
			PluginUI = new FF14ChatUi(this);
			service = new Service(this);
			pluginScreenSpace.Controls.Add(PluginUI);

			_lblStatus.Text = "世界频道启动 :D";

			startInjectProcess();
		}

		public void detch() {
			//ActGlobals.oFormActMain.OnLogLineRead -= PluginUI.oFormActMain_OnLogLineRead;
			_injectProcessSwitcher?.CancelAsync();
			_ServiceProcessSwitcher?.CancelAsync();
			inject?.gameDetach();
			Log.Shutdown();
		}
		#endregion

		private void startInjectProcess() {
			_injectProcessSwitcher = new BackgroundWorker { WorkerSupportsCancellation = true };
			_injectProcessSwitcher.DoWork += inject.ProcessSwitcher;
			_injectProcessSwitcher.RunWorkerAsync();
		}

		public void startServiceProcess() {
			_ServiceProcessSwitcher = new BackgroundWorker { WorkerSupportsCancellation = true };
			_ServiceProcessSwitcher.DoWork += service.ProcessSwitcherGetMsg;
			_ServiceProcessSwitcher.DoWork += service.ProcessSwitcherHeartbeat;
			_ServiceProcessSwitcher.DoWork += service.ProcessSwitcherGetUserList;
			_ServiceProcessSwitcher.RunWorkerAsync();

			// 订阅 ChatLogReceived 事件
			ActGlobals.oFormActMain.OnLogLineRead += service.oFormActMain_OnLogLineRead;
		}


		#region getter/setter
		public BackgroundWorker getInjectProcessSwitcher() {
			return _injectProcessSwitcher;
		}

		public BackgroundWorker getServiceProcessSwitcher() {
			return _ServiceProcessSwitcher;
		}


		public void setLabelFFXIVPluginStatusRun() {
			PluginUI.setLabelFFXIVPluginStatusRun();
		}

		public void setLabelGameProcessStatus(int processNum) {
			PluginUI.setLabelGameProcessStatus(processNum);
		}

		public void setPlayer(Player player) {
			this.player = player;
		}

		public void setPlayerName(string playerName) {
			if(!"".Equals(this.playerName) && !this.playerName.Equals(playerName)) {
				Log.info("当前由" + this.playerName + "切换用户为" + playerName);
				//TODO: 切换用户 
			}
			this.playerName = playerName;

			PluginUI.setLabelLoginStatus(playerName);

			if(PluginUI.registerForm != null) {
				PluginUI.registerForm.setPlayer(playerName, player);
			}
		}

		public void setCommand(Command command) {
			PluginUI.SetCommand(command);
		}

		public void addMessage(string message) {
			PluginUI.addMessage(message);
		}

		public void dataGridMessage1Add(string time, string message) {
			PluginUI.dataGridMessage1Add(time, message);
		}
		public void dataGridMessage1Add(MsgItem msgItem) {
			PluginUI.dataGridMessage1Add(msgItem);
		}

		public void dataGridMessage2Add(string time, string message) {
			PluginUI.dataGridMessage2Add(time, message);
		}
		public void dataGridMessage2Add(MsgItem msgItem) {
			PluginUI.dataGridMessage2Add(msgItem);
		}

		public void dataGridMessage3Add(string time, string message) {
			PluginUI.dataGridMessage3Add(time, message);
		}
		public void dataGridMessage3Add(MsgItem msgItem) {
			PluginUI.dataGridMessage3Add(msgItem);
		}

		public void dataGridUserAdd(DataGridViewRow row) {
			PluginUI.dataGridUserAdd(row);
		}
		public void dataGridUserRemove(int index) {
			PluginUI.dataGridUserRemove(index);
		}

		public void reportMessage(long msgID) { 
			service.reportMessage(msgID);
		}

		public void autoLogin() {
			PluginUI.autoLoginAsync();
		}



		#endregion
	}
}
//TODO
//修改为无注入
//自动登录完善