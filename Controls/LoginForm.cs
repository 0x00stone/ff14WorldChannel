
using FF14Chat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using FF14Chat.Common;
using FF14Chat_c.Models;

namespace FF14Chat.Controls {

	public partial class LoginForm : Form {
		
		private Dictionary<string, Dictionary<string, string>> resultMap;
		private LoginUserResult result;

		public event EventHandler<LoginUserResult> ContentClosed;
		private void LoginForm_FormClosed(object sender, FormClosedEventArgs e) {
			ContentClosed?.Invoke(this, result);
		}

		public LoginForm() {
			InitializeComponent();
			this.FormClosed += LoginForm_FormClosed;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ControlBox = false;
			this.ClientSize = new System.Drawing.Size(300, 180);
			this.Location = new System.Drawing.Point(300, 180);
			this.resultMap = new Dictionary<string, Dictionary<string, string>>();
		}

		private async Task InitializeAsync() {
			await NetworkUtil.getServer(resultMap);
			XmlUtils.LoadUserSettings(this);
		}

		public static async Task<LoginForm> CreateLoginForm() {
			var result = new LoginForm();
			await result.InitializeAsync();
			return result;
		}


		private bool isLoad1 = false;
		private void ComboBox1_DropDown(object sender, EventArgs e) {
			if(!isLoad1) {
				foreach(var entry in resultMap) {
					comboBox1.Items.Add(entry.Key);
				}
				isLoad1 = true;
			}
		}
		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
			comboBox2.Items.Clear();
			comboBox2.Text = string.Empty;
			isLoad2 = false;
		}

		private bool isLoad2 = false;

		private void ComboBox2_DropDown(object sender, EventArgs e) {
			try {
				if(!isLoad2) {
					string key = (string)comboBox1.SelectedItem;
					if(resultMap.ContainsKey(key)) {
						var innerMap = resultMap[key];
						foreach(var kvp in innerMap) {
							comboBox2.Items.Add(kvp.Key);
						}
					} else {
						Log.error("ComboBox2_DropDown Key not found in resultMap.");
					}
					isLoad2 = true;
				}
			} catch { }
		}


		private async void loginButton_Click(object sender, EventArgs e) {
			string username = textBox1.Text;
			string password = textBox2.Text;
			string partition = (string)comboBox1.SelectedItem;
			string server = (string)comboBox2.SelectedItem;

			if(username.Length >= 10) {
				MessageBox.Show("用户名称大于10个字符", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(System.Text.RegularExpressions.Regex.IsMatch(username, @"[^a-zA-Z0-9_\-\u4e00-\u9fa5]")) {
				MessageBox.Show("用户名只可以使用字母，数字，中文和_-", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(username == null || "".Equals(username) || password == null || "".Equals(password)) {
				MessageBox.Show("用户名或密码为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(partition == null || partition == "" || server == null || server == "") {
				MessageBox.Show("未选择服务器", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var jsonObject = new {
				aliasName = username.Trim(),
				password = password.Trim().GetHashCode(),
				serverId = resultMap[partition][server]
			};
			string json = SerializeUtil.ToJson(jsonObject);

			this.result = await NetworkUtil.Login(json);
			if("".Equals(result.getToken())) {

			} else {
				result.setServerId(resultMap[partition][server]);
				result.setAliasName(username.Trim());
				result.setPassword(password.Trim());
				XmlUtils.SaveUserSettings(checkBox1.Checked, checkBox2.Checked, username.Trim(), partition + ":" + server, password.Trim());
			}

			FF14Chat_Main.isLogin = true;
			this.Close();
		}

		public async void autoLogin(LoginUser loginUser) {
			Log.info($"try to autoLogin");

			string username = loginUser.Name;
			string password = loginUser.Password;
			string partition = loginUser.ServerId.Split(':')[0];
			string server = loginUser.ServerId.Split(':')[1];

			if(username.Length >= 10) {
				MessageBox.Show("用户名称大于10个字符", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(System.Text.RegularExpressions.Regex.IsMatch(username, @"[^a-zA-Z0-9_\-\u4e00-\u9fa5]")) {
				MessageBox.Show("用户名只可以使用字母，数字，中文和_-", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(username == null || "".Equals(username) || password == null || "".Equals(password)) {
				MessageBox.Show("用户名或密码为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(partition == null || partition == "" || server == null || server == "") {
				MessageBox.Show("未选择服务器", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var jsonObject = new {
				aliasName = username.Trim(),
				password = password.Trim().GetHashCode(),
				serverId = resultMap[partition][server]
			};
			string json = SerializeUtil.ToJson(jsonObject);

			this.result = await NetworkUtil.Login(json);
			FF14Chat_Main.isLogin = true;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.Close();
		}

		public void setAliasName(string aliasname) {
			this.textBox1.Text = aliasname;
		}

		public void setPassword(string password) { 
			this.textBox2.Text = password;
		}

		public void setServerId(string serverId) { 
			string[] temp = serverId.Split(':');

			foreach(var entry in resultMap) {
				comboBox1.Items.Add(entry.Key);
			}
		
			comboBox1.SelectedItem = temp[0];

			if(resultMap.ContainsKey(temp[0])) {
				var innerMap = resultMap[temp[0]];
				foreach(var kvp in innerMap) {
					comboBox2.Items.Add(kvp.Key);
				}
			} else {
				Log.error("setServerId Key not found in resultMap. ");
			}

			comboBox2.SelectedItem = temp[1];

		/*	string partition = (string)comboBox1.SelectedItem;
			string server = (string)comboBox2.SelectedItem;*/
		}

		public void setRememberaliasname(bool isRememberaliasname) { 
			this.checkBox1.Checked = isRememberaliasname;
		}

		public void setAutoLogin(bool isAutoLogin) {
			this.checkBox2.Checked = isAutoLogin;
		}

		private async void button3_Click(object sender, EventArgs e) {
			string username = textBox1.Text;
			string password = textBox2.Text;
			string partition = (string)comboBox1.SelectedItem;
			string server = (string)comboBox2.SelectedItem;

			if(username.Length >= 10) {
				MessageBox.Show("用户名称大于10个字符", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(System.Text.RegularExpressions.Regex.IsMatch(username, @"[^a-zA-Z0-9_\-\u4e00-\u9fa5]")) {
				MessageBox.Show("用户名只可以使用字母，数字，中文和_-", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(username == null || "".Equals(username) || password == null || "".Equals(password)) {
				MessageBox.Show("用户名或密码为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(partition == null || partition == "" || server == null || server == "") {
				MessageBox.Show("未选择服务器", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var jsonObject = new {
				aliasName = username.Trim(),
				password = password.Trim(),
				serverId = resultMap[partition][server]
			};
			string json = SerializeUtil.ToJson(jsonObject);

			this.result = await NetworkUtil.Login(json);
			if("".Equals(result.getToken())) {

			} else {
				result.setServerId(resultMap[partition][server]);
				result.setAliasName(username.Trim());
				result.setPassword(password.Trim());
				XmlUtils.SaveUserSettings(checkBox1.Checked, checkBox2.Checked, username.Trim(), partition + ":" + server, password.Trim());
			}

			FF14Chat_Main.isLogin = true;
			this.Close();
		}
	}
}
