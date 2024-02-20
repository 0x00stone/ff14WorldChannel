
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
using FF14Chat.Network;
using System.Text;

namespace FF14Chat {
	public class FF14Chat_Main : IActPluginV1 {

		private System.Windows.Forms.Label _lblStatus;

		private FF14ChatUi PluginUI;
		private Service service;

		public static LoginUserResult loginUserResult;

		private BackgroundWorker _ServiceProcessSwitcher;
		private BackgroundWorker _clientProcessSwitcher;

		//public static bool isGameOn = false; //游戏是否打开
		public static bool isLogin = false; //聊天账户是否登录
		public static bool isRunning = true; //插件是否运行

		public static HashSet<String> allowWord;
		public static HashSet<String> notAllowWord;
		public static List<String> userBlanklist; //存userID
		public static List<UserItem> userList;
		public static string[] bindBucket;
		public static bool[] bindIsOpen = { true, true, true };
		// 0 : "0010"
		// 1 : "0015"
		// 2 : "0017"
		TabPage tabPage;

		public static ulong playerContent = 0;

		public void restart() {
			DeInitPlugin();
			init(tabPage);
		}


		#region init/de
		public void InitPlugin(TabPage pluginScreenSpace, System.Windows.Forms.Label pluginStatusText) {
			this.tabPage = pluginScreenSpace;
			pluginScreenSpace.Text = "世界频道";
			_lblStatus = pluginStatusText;
			init(pluginScreenSpace);
			

			_lblStatus.Text = "世界频道启动 :D";
		}

		public void DeInitPlugin() {
			isRunning = false;
			//ActGlobals.oFormActMain.OnLogLineRead -= PluginUI.oFormActMain_OnLogLineRead;
			Log.Shutdown();
			_lblStatus.Text = "世界频道停止 :(";
		}

		private void init(TabPage pluginScreenSpace) {
			allowWord = new HashSet<String>();
			notAllowWord = new HashSet<String>();
			userBlanklist = new List<String>();
			userList = new List<UserItem>();
			bindBucket = new string[] { "0000", "0000", "0000" };

			Log.init();
			PluginUI = new FF14ChatUi(this);
			service = new Service(this);
			pluginScreenSpace.Controls.Add(PluginUI);


			_clientProcessSwitcher = new BackgroundWorker { WorkerSupportsCancellation = true };
			_clientProcessSwitcher.DoWork += service.ProcessSwitcher;
			_clientProcessSwitcher.RunWorkerAsync();
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
		#endregion


		#region getter/setter


		public void setLabelFFXIVPluginStatusRun() {
			PluginUI.setLabelFFXIVPluginStatusRun();
		}

		public void setLabelGameProcessStatus(int processNum) {
			PluginUI.setLabelGameProcessStatus(processNum);
		}



		public void setPlayerContent(ulong playerContent) {
			if((FF14Chat_Main.playerContent != 0) && (FF14Chat_Main.playerContent != playerContent)) {
				//change player
				Log.info("当前由 " + FF14Chat_Main.playerContent + "切换用户为" + playerContent);
				//TODO: 切换用户 
			}

			FF14Chat_Main.playerContent = playerContent;

			PluginUI.setLabelLoginStatus(playerContent +"");
		}

		public void addMessage(string message) {
			PluginUI.addMessage(message);
		}

		public void dataGridMessage1Add(MsgItem msgItem) {
			PluginUI.dataGridMessage1Add(msgItem);
		}


		public void dataGridMessage2Add(MsgItem msgItem) {
			PluginUI.dataGridMessage2Add(msgItem);
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

		public void autoLogin(ulong contentID) {
			PluginUI.autoLoginAsync(contentID);
		}

		#endregion

		
	}
}
//TODO
//login时间特别长
//注册数据库用户content为0
//鲶鱼精发送消息