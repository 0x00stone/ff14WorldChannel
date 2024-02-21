

using FF14Chat.Actions;
using FF14Chat.Common;
using FF14Chat.Models;
using FF14Chat.Network;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FF14Chat.Controls {
	public partial class FF14ChatUi : UserControl {

		private Command command;
		private FF14Chat_Main main;

		public FF14ChatUi(FF14Chat_Main main) {
			this.main = main;
			this.command = new Command();
			InitializeComponent();
			this.dataGridViewTextBoxColumn21.Width = 75;
			this.dataGridViewTextBoxColumn31.Width = 75;

			allGroupsUnVisiable();
			setComboboxValue();
			XmlUtils.LoadSysSettings(this);
		

			addMessage($"当前插件版本为:{Assembly.GetExecutingAssembly().GetName().Version}");
			new Update().getNewVersionAsync();
		}

		#region Menu
		private MsgItem selectDataGrid1Message;
		private MsgItem selectDataGrid2Message;
		private MsgItem selectDataGrid3Message;
		void dataGrid1Message_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
			if(e.Button == MouseButtons.Right) {
				int rowIndex = e.RowIndex;
				if(rowIndex >= 0) {
					MsgItem asMsg = dataGridMessage1.Rows[rowIndex].Cells[1].Value as MsgItem;
					if(asMsg != null) {
						selectDataGrid1Message = asMsg;
					}
					contextMenuMsg1.Show(MousePosition.X, MousePosition.Y);
				}
			}
		}

		void dataGrid2Message_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
			if(e.Button == MouseButtons.Right) {
				int rowIndex = e.RowIndex;
				if(rowIndex >= 0) {
					MsgItem asMsg = dataGridMessage2.Rows[rowIndex].Cells[1].Value as MsgItem;
					if(asMsg != null) {
						selectDataGrid2Message = asMsg;
					}
					contextMenuMsg2.Show(MousePosition.X, MousePosition.Y);
				}
			}
		}

		void dataGrid3Message_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
			if(e.Button == MouseButtons.Right) {
				int rowIndex = e.RowIndex;
				if(rowIndex >= 0) {
		
					MsgItem asMsg = dataGridMessage3.Rows[rowIndex].Cells[1].Value as MsgItem;
					if(asMsg != null) {
						selectDataGrid3Message = asMsg;
					} 

					contextMenuMsg3.Show(MousePosition.X, MousePosition.Y);
					
				}
			}
		}

		//msg 屏蔽发送者
		private void ToolStripMenu_black1_Click(object sender, EventArgs e) {
			if(selectDataGrid1Message != null) {
				blacklistAddUser(selectDataGrid1Message.Username, selectDataGrid1Message.UserId + "");
				XmlUtils.SaveSysSettingsBlack(selectDataGrid1Message.UserId + "", selectDataGrid1Message.Username);
			}
		}
		private void ToolStripMenu_black2_Click(object sender, EventArgs e) {
			if(selectDataGrid2Message != null) {
				blacklistAddUser(selectDataGrid2Message.Username, selectDataGrid2Message.UserId + "");
				XmlUtils.SaveSysSettingsBlack(selectDataGrid2Message.UserId + "", selectDataGrid2Message.Username);
			}
		}
		private void ToolStripMenu_black3_Click(object sender, EventArgs e) {
			if(selectDataGrid3Message != null) {
				blacklistAddUser(selectDataGrid3Message.Username, selectDataGrid3Message.UserId + "");
				XmlUtils.SaveSysSettingsBlack(selectDataGrid3Message.UserId + "", selectDataGrid3Message.Username);
			}
		}

		//msg 举报
		private void ToolStripMenu_Report1_Click(object sender, EventArgs e) {
			if(selectDataGrid1Message != null) {
				if(selectDataGrid1Message.Id != 0)
					main.reportMessage(selectDataGrid1Message.Id);
			}
		}
		private void ToolStripMenu_Report2_Click(object sender, EventArgs e) {
			if(selectDataGrid2Message != null) {
				if(selectDataGrid2Message.Id != 0)
					main.reportMessage(selectDataGrid2Message.Id);
			}
		}
		private void ToolStripMenu_Report3_Click(object sender, EventArgs e) {
			if(selectDataGrid3Message != null) {
				if(selectDataGrid3Message.Id != 0)
					main.reportMessage(selectDataGrid3Message.Id);
			}
		}

		#endregion

		#region UserListDataGrid
		private UserItem selectdataGridUser;
		void dataGridUser_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
			if(e.Button == MouseButtons.Right) {
				int rowIndex = e.RowIndex;
				if(rowIndex >= 0) {

					UserItem asUserItem = dataGridUser.Rows[rowIndex].Cells[0].Value as UserItem;
					if(asUserItem != null) {
						selectdataGridUser = asUserItem;
					}

					contextMenuUserlist.Show(MousePosition.X, MousePosition.Y);
				}
			}
		}

		//userlist 屏蔽该用户
		private void ToolStripMenuItemBlank_Click(object sender, EventArgs e) {
			if(selectdataGridUser != null) {
				blacklistAddUser(selectdataGridUser.Name,selectdataGridUser.Id);
				XmlUtils.SaveSysSettingsBlack(selectdataGridUser.Id , selectdataGridUser.Name);
			}
		}
		#endregion

		#region BlackListDataGrid
		private UserItem selectDataGridBlacklist;
		void dataGridBlacklist_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
			if(e.Button == MouseButtons.Right) {
				int rowIndex = e.RowIndex;
				if(rowIndex >= 0) {

					UserItem asUserItem = dataGridBlacklist.Rows[rowIndex].Cells[0].Value as UserItem;
					if(asUserItem != null) {
						selectDataGridBlacklist = asUserItem;
					}

					contextMenuBlanklist.Show(MousePosition.X, MousePosition.Y);
				}
			}
		}

		//blacklist 恢复该用户
		private void ToolStripMenuItemResume_Click(object sender, EventArgs e) {
			if(selectDataGridBlacklist != null) {
				blacklistRemoveUser(selectDataGridBlacklist.Id);
			}
		}

		public void blacklistAddUser(string name, string id) {
			if(FF14Chat_Main.userBlanklist.FindIndex(item => item.Equals(id)) < 0) {
				DataGridViewRow row = new DataGridViewRow();
				DataGridViewCell cell = new DataGridViewTextBoxCell();
				cell.Value = new UserItem(id, name);
				row.Cells.Add(cell);
				FF14Chat_Main.userBlanklist.Add(id);
				dataGridBlacklist.Rows.Add(row);
			}
		}

		private void blacklistRemoveUser(string id) {
			int index = FF14Chat_Main.userBlanklist.FindIndex(item => item.Equals(id));
			
			if(index >= 0) {
				dataGridBlacklist.Rows.RemoveAt(index);
				FF14Chat_Main.userBlanklist.RemoveAt(index);
				selectDataGridBlacklist = null;
				XmlUtils.SaveSysSettingsRemoveBlackList(id);
			}
		}
		#endregion


		#region Communication Bay
		//通讯贝绑定下拉框初始化
		private void setComboboxValue() {
			ChannelItem comboBoxItem0 = new ChannelItem("无绑定", "0000");
			ChannelItem comboBoxItem1 = new ChannelItem("本地通讯贝1", "0010");
			ChannelItem comboBoxItem2 = new ChannelItem("本地通讯贝2", "0011");
			ChannelItem comboBoxItem3 = new ChannelItem("本地通讯贝3", "0012");
			ChannelItem comboBoxItem4 = new ChannelItem("本地通讯贝4", "0013");
			ChannelItem comboBoxItem5 = new ChannelItem("本地通讯贝5", "0014");
			ChannelItem comboBoxItem6 = new ChannelItem("本地通讯贝6", "0015");
			ChannelItem comboBoxItem7 = new ChannelItem("本地通讯贝7", "0016");
			ChannelItem comboBoxItem8 = new ChannelItem("本地通讯贝8", "0017");

			this.comboBox1.Items.AddRange(new ChannelItem[] {
				comboBoxItem0,
				comboBoxItem1,
				comboBoxItem2,
				comboBoxItem3,
				comboBoxItem4,
				comboBoxItem5,
				comboBoxItem6,
				comboBoxItem7,
				comboBoxItem8});
			this.comboBox2.Items.AddRange(new ChannelItem[] {
				comboBoxItem0,
				comboBoxItem1,
				comboBoxItem2,
				comboBoxItem3,
				comboBoxItem4,
				comboBoxItem5,
				comboBoxItem6,
				comboBoxItem7,
				comboBoxItem8});
			this.comboBox3.Items.AddRange(new ChannelItem[] {
				comboBoxItem0,
				comboBoxItem1,
				comboBoxItem2,
				comboBoxItem3,
				comboBoxItem4,
				comboBoxItem5,
				comboBoxItem6,
				comboBoxItem7,
				comboBoxItem8});
		}

		//通讯贝绑定下拉框绑定内容不重复
		private void combobox1SelectedIndexChanged(object sender, EventArgs e) {
			string value = ((ChannelItem)comboBox1.SelectedItem).channelString;
			if(FF14Chat_Main.bindBucket[0].Equals(value))
				return;
			if("0000".Equals(value)) {
				FF14Chat_Main.bindBucket[0] = "0000";
				XmlUtils.SaveSysSettingsBind("0000",0);
				return;
			}
			if(FF14Chat_Main.bindBucket[1].Equals(value) || FF14Chat_Main.bindBucket[2].Equals(value)) {
				comboBox1.SelectedIndex = 0;
				XmlUtils.SaveSysSettingsBind("0000", 0);
				return;
			}

			XmlUtils.SaveSysSettingsBind(value, 0);
			FF14Chat_Main.bindBucket[0] = value;
			Log.info("combobox1SelectedIndexChanged" + FF14Chat_Main.bindBucket[0] + "||1:" + FF14Chat_Main.bindBucket[1] + "||2:" + FF14Chat_Main.bindBucket[2]);
		}
		private void combobox2SelectedIndexChanged(object sender, EventArgs e) {
			string value = ((ChannelItem)comboBox2.SelectedItem).channelString;
			if(FF14Chat_Main.bindBucket[1].Equals(value))
				return;
			if("0000".Equals(value)) {
				FF14Chat_Main.bindBucket[1] = "0000";
				XmlUtils.SaveSysSettingsBind("0000", 1);
				return;
			}
			if(FF14Chat_Main.bindBucket[0].Equals(value) || FF14Chat_Main.bindBucket[2].Equals(value)) {
				comboBox2.SelectedIndex = 0;
				XmlUtils.SaveSysSettingsBind("0000", 1);
				return;
			}

			XmlUtils.SaveSysSettingsBind(value, 1);
			FF14Chat_Main.bindBucket[1] = value;
			Log.info("combobox2SelectedIndexChanged" + FF14Chat_Main.bindBucket[1] + "||0:" + FF14Chat_Main.bindBucket[0] + "||2:" + FF14Chat_Main.bindBucket[2]);
		}
		private void combobox3SelectedIndexChanged(object sender, EventArgs e) {
			string value = ((ChannelItem)comboBox3.SelectedItem).channelString;
			if(FF14Chat_Main.bindBucket[2].Equals(value))
				return;
			if("0000".Equals(value)) {
				FF14Chat_Main.bindBucket[2] = "0000";
				XmlUtils.SaveSysSettingsBind("0000", 2);
				return;
			}
			if(FF14Chat_Main.bindBucket[1].Equals(value) || FF14Chat_Main.bindBucket[0].Equals(value)) {
				comboBox3.SelectedIndex = 0;
				XmlUtils.SaveSysSettingsBind("0000", 2);
				return;
			}

			XmlUtils.SaveSysSettingsBind(value, 2);
			FF14Chat_Main.bindBucket[2] = value;
			Log.info("combobox3SelectedIndexChanged" + FF14Chat_Main.bindBucket[2] + "||0:" + FF14Chat_Main.bindBucket[0] + "||1:" + FF14Chat_Main.bindBucket[1]);
		}
		#endregion

		#region tabControl
		// tabControl 切换页面
		private void TabControl_SelectedIndexChanged(object sender, EventArgs e) {
			if(tabControl.SelectedTab == tabPageRegion) {
				tabPageRegion.Controls.Add(dataGridMessage1);
				tabPageTeamUp.Controls.Remove(dataGridMessage2);
				tabPageTrade.Controls.Remove(dataGridMessage3);
			} else if(tabControl.SelectedTab == tabPageTeamUp) {
				tabPageRegion.Controls.Remove(dataGridMessage1);
				tabPageTeamUp.Controls.Add(dataGridMessage2);
				tabPageTrade.Controls.Remove(dataGridMessage3);
			} else if(tabControl.SelectedTab == tabPageTrade) {
				tabPageRegion.Controls.Remove(dataGridMessage1);
				tabPageTeamUp.Controls.Remove(dataGridMessage2);
				tabPageTrade.Controls.Add(dataGridMessage3);
			}
		}
		#endregion

		#region component function

	
		//添加过滤规则
		private void buttonRuleAdd_Click(object sender, EventArgs e) {
			string labelName = this.textBoxRule.Text;

			if(checkBoxRuleSelect.Checked) {
				addNotAllowWord(labelName);
				XmlUtils.SaveSysSettingsWord(true, labelName);
			} else {
				addAllowWord(labelName);
				XmlUtils.SaveSysSettingsWord(false, labelName);
			}
			this.textBoxRule.Clear();
		}

		public void addAllowWord(string allowWord) {
			if(!wordIsContain(allowWord)) {
				Label label = newLabel(allowWord);
				label.Text = allowWord;
				FF14Chat_Main.allowWord.Add(allowWord);
				label.BackColor = System.Drawing.ColorTranslator.FromHtml("#43BF73");

				this.flowLayoutRule.Controls.Add(label);
			}
		}
		public void addNotAllowWord(string notAllowWord) {
			if(!wordIsContain(notAllowWord)) {
				Label label = newLabel(notAllowWord);
				label.Text = "不包括 " + notAllowWord;
				FF14Chat_Main.notAllowWord.Add(notAllowWord);
				label.BackColor = System.Drawing.ColorTranslator.FromHtml("#D9363A");

				this.flowLayoutRule.Controls.Add(label);
			}
		}

		private bool wordIsContain(string word) {
			if(FF14Chat_Main.notAllowWord.Contains(word) || FF14Chat_Main.allowWord.Contains(word)) {
				MessageBox.Show("过滤器已包括当前添加内容", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return true;
			}
			return false;
		}

		//添加新标签
		private Label newLabel(String labelName) {
			// 创建标签并添加到模块容器中
			Label label = new Label();
			label.AutoSize = true;
			label.Click += (s, eventArgs) => {
				this.flowLayoutRule.Controls.Remove(label);
				if(label.BackColor == System.Drawing.ColorTranslator.FromHtml("#43BF73")) {
					FF14Chat_Main.allowWord.Remove(labelName);
				} else {
					FF14Chat_Main.notAllowWord.Remove(labelName);
				}
				XmlUtils.SaveSysSettingsRemoveWord(labelName);
			};
			return label;
		}

		// 检查文本是否为默认文本，如果是则清空文本
		private void textBoxRule_GotFocus(object sender, EventArgs e) {
			if(this.textBoxRule.Text == "过滤器文字") {
				this.textBoxRule.Text = string.Empty;
				this.textBoxRule.ForeColor = SystemColors.WindowText;
			}
		}

		// 检查文本是否为空，如果是则重新设置默认文本
		private void textBoxRule_LostFocus(object sender, EventArgs e) {
			if(string.IsNullOrEmpty(this.textBoxRule.Text)) {
				this.textBoxRule.Text = "过滤器文字";
				this.textBoxRule.ForeColor = SystemColors.GrayText;
			}
		}

		//是否为包括项
		private void checkBoxRuleSelect_Click(object sender, EventArgs e) {
			if(this.checkBoxRuleSelect.Text == "不包括项") {
				this.checkBoxRuleSelect.Text = "包括项";
			} else {
				this.checkBoxRuleSelect.Text = "不包括项";
			}
		}
		#endregion

		#region message
		public void addMessage(string message) {
			dataGridMessage1Add($"[{DateTime.Now:HH:mm:ss}]", message);
			dataGridMessage2Add($"[{DateTime.Now:HH:mm:ss}]", message);
			dataGridMessage3Add($"[{DateTime.Now:HH:mm:ss}]", message);
			command.DoTextCommand($"/e [系统消息] : {message}");
		}

		//系统消息
		public void dataGridMessage1Add(string time, string message) {
			MsgItem msgItem = new MsgItem();
			msgItem.Time = time;
			msgItem.UserId = 0;
			msgItem.message = message;
			msgItem.Username = "ff14chat";
			dataGridMessage1Add(msgItem);
		}
		public void dataGridMessage2Add(string time, string message) {
			MsgItem msgItem = new MsgItem();
			msgItem.Time = time;
			msgItem.UserId = 0;
			msgItem.message = message;
			msgItem.Username = "ff14chat";
			dataGridMessage2Add(msgItem);
		}
		public void dataGridMessage3Add(string time, string message) {
			MsgItem msgItem = new MsgItem();
			msgItem.Time = time;
			msgItem.UserId = 0;
			msgItem.message = message;
			msgItem.Username = "ff14chat";
			dataGridMessage3Add(msgItem);
		}


		//用户消息
		public void dataGridMessage1Add(MsgItem msgItem) {
			if(dataGridMessage1.Rows.Count > 500)
				dataGridMessage1.Rows.RemoveAt(0);
			bool scroll1 = (dataGridMessage1.FirstDisplayedScrollingRowIndex == dataGridMessage1.Rows.Count - 1);

			DataGridViewRow row = new DataGridViewRow();
			DataGridViewCell cell1 = new DataGridViewTextBoxCell();
			cell1.Value = msgItem.Time;
			row.Cells.Add(cell1);
			DataGridViewCell cell2 = new DataGridViewTextBoxCell();
			cell2.Value = msgItem;
			row.Cells.Add(cell2);
			if(msgItem.UserId == 0) {
				row.DefaultCellStyle.BackColor = Color.Gray;
			}
			dataGridMessage1.Rows.Add(row);

			if(scroll1)
				dataGridMessage1.FirstDisplayedScrollingRowIndex = dataGridMessage1.Rows.Count - 1;

			if(!"0000".Equals(FF14Chat_Main.bindBucket[0]) && msgItem.UserId != 0) {
				command.DoTextCommand($"/e [区域频道] {msgItem.Username} : {msgItem.message}");
			}
		}
		public void dataGridMessage2Add(MsgItem msgItem) {
			if(dataGridMessage2.Rows.Count > 500)
				dataGridMessage2.Rows.RemoveAt(0);
			bool scroll2 = (dataGridMessage2.FirstDisplayedScrollingRowIndex == dataGridMessage2.Rows.Count - 1);

			DataGridViewRow row = new DataGridViewRow();
			DataGridViewCell cell1 = new DataGridViewTextBoxCell();
			cell1.Value = msgItem.Time;
			row.Cells.Add(cell1);
			DataGridViewCell cell2 = new DataGridViewTextBoxCell();
			cell2.Value = msgItem;
			row.Cells.Add(cell2);
			if(msgItem.UserId == 0) {
				row.DefaultCellStyle.BackColor = Color.Gray;
			}
			dataGridMessage2.Rows.Add(row);

			if(scroll2)
				dataGridMessage2.FirstDisplayedScrollingRowIndex = dataGridMessage2.Rows.Count - 1;
			if(!"0000".Equals(FF14Chat_Main.bindBucket[1]) && msgItem.UserId != 0) {
				command.DoTextCommand($"/e [组队频道] {msgItem.Username} : {msgItem.message}");
			}
		}
		public void dataGridMessage3Add(MsgItem msgItem) {
			if(dataGridMessage3.Rows.Count > 500)
				dataGridMessage3.Rows.RemoveAt(0);
			bool scroll3 = (dataGridMessage3.FirstDisplayedScrollingRowIndex == dataGridMessage3.Rows.Count - 1);

			DataGridViewRow row = new DataGridViewRow();
			DataGridViewCell cell1 = new DataGridViewTextBoxCell();
			cell1.Value = msgItem.Time;
			row.Cells.Add(cell1);
			DataGridViewCell cell2 = new DataGridViewTextBoxCell();
			cell2.Value = msgItem;
			row.Cells.Add(cell2);
			if(msgItem.UserId == 0) {
				row.DefaultCellStyle.BackColor = Color.Gray;
			}
			dataGridMessage3.Rows.Add(row);

			if(scroll3)
				dataGridMessage3.FirstDisplayedScrollingRowIndex = dataGridMessage3.Rows.Count - 1;
			if(!"0000".Equals(FF14Chat_Main.bindBucket[2]) && msgItem.UserId!=0) {
				command.DoTextCommand($"/e [交易频道] {msgItem.Username} : {msgItem.message}");
			}
		}
		#endregion

		#region log and register
		//登录界面
		public async void buttonLogin_Click(object sender, EventArgs e) {
			this.loginForm = await LoginForm.CreateLoginForm();
			loginForm.ContentClosed += Form_ContentClosed;
			loginForm.Show();
		}

		public async Task autoLoginAsync(ulong contentID) {

			LoginUser loginUser = XmlUtils.LoadUserSettingsByAutoLogin(contentID);
			Log.info($"loginUser: {loginUser.ServerId},{loginUser.Name},{loginUser.Password}");

			if(loginUser.ServerId == "" || loginUser.Name == "" || loginUser.Password == "") {
				Log.error($"xml存储{contentID} 的ServerId：{loginUser.ServerId}，Name：{loginUser.Name}，Password：{loginUser.Password}存在空值");
				return;
			}

			LoginUserResult result = await NetworkUtil.loginUser(loginUser);

			string token = result.getToken();
			if("".Equals(token)) {
				Log.info("心跳获取token为空");
				return;
			} else {
				result.setServerId(loginUser.ServerId);
				result.setAliasName(loginUser.Name.Trim());
				result.setPassword(loginUser.Password.Trim());
				FF14Chat_Main.isLogin = true;
			}

			if(FF14Chat_Main.playerContent != 0 && ulong.Parse(result.getcontent()) != FF14Chat_Main.playerContent) {
				//1. 尝试切换用户
				Log.error("游戏帐户与聊天账户非绑定，请重新登录");
				MessageBox.Show("游戏帐户与聊天账户非绑定，请重新登录", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				main.restart();
				return;
			} else {
				Log.info("autoLogin sucess");
				allGroupsVisiable();
				main.startServiceProcess();
				FF14Chat_Main.loginUserResult = result;

			}
		}

		//注册界面
		public async void buttonRegister_Click(object sender, EventArgs e) {
			this.registerForm = await RegisterForm.CreateRegisterForm();
			registerForm.Show();
		}

		//登录成功，监听事件
		private void Form_ContentClosed(object sender, LoginUserResult result) {
			if(result != null && result.getToken() != null && !"".Equals(result.getToken()) && result.getServerId() != null && !"".Equals(result.getServerId())
				&& result.getAliasname() != null && !"".Equals(result.getAliasname()) && result.getPassword() != null && !"".Equals(result.getPassword())) {
				Log.info("Form_ContentClosed get token");

				if(FF14Chat_Main.playerContent != 0 && ulong.Parse(result.getcontent()) != FF14Chat_Main.playerContent) {
					//1. 尝试切换用户
					Log.error("游戏帐户与聊天账户非绑定，请重新登录");
					MessageBox.Show("游戏帐户与聊天账户非绑定，请重新登录", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
					main.restart();
					return;
				}

				allGroupsVisiable();
				FF14Chat_Main.loginUserResult = result;

				main.startServiceProcess();
			}
		}
		#endregion

		#region common
		public void allGroupsUnVisiable() {
			this.tabControl.Visible = false;
			this.groupBoxStatus.Visible = true;
			this.groupBoxRule.Visible = false;
			this.groupboxUser.Visible = false;
			this.groupBoxBind.Visible = false;
			this.buttonLogin.Visible = true;
			this.buttonRegister.Visible = true;
			this.groupBoxBlanklist.Visible = false;
		}

		public void allGroupsVisiable() {
			if(!this.tabControl.Visible)
				this.tabControl.Visible = true;
			if(!this.groupBoxStatus.Visible)
				this.groupBoxStatus.Visible = true;
			if(!this.groupBoxRule.Visible)
				this.groupBoxRule.Visible = true;

			if(!this.groupboxUser.Visible)
				this.groupboxUser.Visible = true;
			if(!this.groupBoxBind.Visible)
				this.groupBoxBind.Visible = true;
			if(this.buttonLogin.Visible)
				this.buttonLogin.Visible = false;
			if(this.buttonRegister.Visible)
				this.buttonRegister.Visible = false;
			if(!this.groupBoxBlanklist.Visible)
				this.groupBoxBlanklist.Visible = true;
		}
		#endregion

		#region setting
		public void setLabelLoginStatus(String name) {
			this.labelLoginStatus.Text = name;
		}
		public void setLabelGameProcessStatus(int processNum) {
			this.labelGameProcessStatus.Text = processNum.ToString();
		}
		public void setLabelFFXIVPluginStatusRun() {
			this.labelFFXIVPluginStatus.Text = "Running...";
		}

		public void dataGridUserAdd(DataGridViewRow row) {
			try {
				dataGridUser.Rows.Add(row);
			} catch(Exception ex) {
				Log.info("ex1: " + ex.Message);
			}
		}
		public void dataGridUserRemove(int index) {
			try {
				dataGridUser.Rows.RemoveAt(index);
			} catch(Exception ex) {
				Log.info("ex2: " + ex.Message);
			}
		}
		#endregion

		/*	this.textBoxRule.GotFocus += textBoxRule_GotFocus;
	this.textBoxRule.LostFocus += textBoxRule_LostFocus;*/
	}
}