
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using FF14Chat.Common;
using FF14Chat.Network;

namespace FF14Chat.Controls {
	public partial class RegisterForm : Form {

		private Dictionary<string, Dictionary<string, string>> resultMap;

		public RegisterForm() {
			InitializeComponent();
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.ControlBox = false;
			this.ClientSize = new System.Drawing.Size(300, 180);
			this.Location = new System.Drawing.Point(300, 180);
			this.Text = "注册信息";
			this.resultMap = new Dictionary<string, Dictionary<string, string>>();
		}

		private async Task InitializeAsync() {
			await NetworkUtil.getServer(resultMap);
		}

		public static async Task<RegisterForm> CreateRegisterForm() {
			var result = new RegisterForm();
			result.InitializeAsync();
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

		private async void registerButton_Click(object sender, EventArgs e) {
			string username = textBox1.Text;
			string password1 = textBox2.Text;
			string password2 = textBox3.Text;
			string partition = (string)comboBox1.SelectedItem;
			string server = (string)comboBox2.SelectedItem;
			if(!password1.Equals(password2)) {
				MessageBox.Show("两次密码不相同！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(FF14Chat_Main.playerContent != 0) {
				MessageBox.Show("请先登录游戏角色再注册账号", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(username.Length >= 10) {
				MessageBox.Show("用户名称大于10个字符", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(System.Text.RegularExpressions.Regex.IsMatch(username, @"[^a-zA-Z0-9_\-\u4e00-\u9fa5]")) {
				MessageBox.Show("用户名只可以使用字母，数字，中文和_-", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(username == null || "".Equals(username) || password1 == null || "".Equals(password1)) {
				MessageBox.Show("用户名或密码为空", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if(partition == null || partition == "" || server == null || server == "") {
				MessageBox.Show("未选择服务器", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var jsonObject = new {
				aliasName = username.Trim(),
				password = password1.Trim().GetHashCode(),
				serverId = resultMap[partition][server],
				contentId = FF14Chat_Main.playerContent
			};

			NetworkUtil.register(SerializeUtil.ToJson(jsonObject));
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			this.Close();
		}
	}
}
