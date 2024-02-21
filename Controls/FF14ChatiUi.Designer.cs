using System;
using System.Drawing;
using System.Windows.Forms;

namespace FF14Chat.Controls {
	partial class FF14ChatUi {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripMenuItem ToolStripMenu_Report1;
			System.Windows.Forms.ToolStripMenuItem ToolStripMenu_Report2;
			System.Windows.Forms.ToolStripMenuItem ToolStripMenu_Report3;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dataGridUser = new System.Windows.Forms.DataGridView();
			this.dataGridViewUserColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBoxStatus = new System.Windows.Forms.GroupBox();
			this.buttonRegister = new System.Windows.Forms.Button();
			this.buttonLogin = new System.Windows.Forms.Button();
			this.labelLoginStatus = new System.Windows.Forms.Label();
			this.labelGameProcessStatus = new System.Windows.Forms.Label();
			this.labelFFXIVPluginStatus = new System.Windows.Forms.Label();
			this.labelACTStatus = new System.Windows.Forms.Label();
			this.labelLogin = new System.Windows.Forms.Label();
			this.labelGameProcess = new System.Windows.Forms.Label();
			this.labelFFXIVPlugin = new System.Windows.Forms.Label();
			this.labelACT = new System.Windows.Forms.Label();
			this.groupBoxRule = new System.Windows.Forms.GroupBox();
			this.flowLayoutRule = new System.Windows.Forms.FlowLayoutPanel();
			this.checkBoxRuleSelect = new System.Windows.Forms.CheckBox();
			this.buttonRuleAdd = new System.Windows.Forms.Button();
			this.textBoxRule = new System.Windows.Forms.TextBox();
			this.groupboxUser = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageRegion = new System.Windows.Forms.TabPage();
			this.dataGridMessage1 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabPageTeamUp = new System.Windows.Forms.TabPage();
			this.tabPageTrade = new System.Windows.Forms.TabPage();
			this.dataGridMessage2 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridMessage3 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.groupBoxBind = new System.Windows.Forms.GroupBox();
			this.comboBox3 = new System.Windows.Forms.ComboBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBoxBlanklist = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridBlacklist = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuMsg1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenu_black1 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuUserlist = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItemBlank = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuBlanklist = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenuItemResume = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuMsg2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenu_black2 = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuMsg3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ToolStripMenu_black3 = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenu_Report1 = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenu_Report2 = new System.Windows.Forms.ToolStripMenuItem();
			ToolStripMenu_Report3 = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dataGridUser)).BeginInit();
			this.groupBoxStatus.SuspendLayout();
			this.groupBoxRule.SuspendLayout();
			this.groupboxUser.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.tabControl.SuspendLayout();
			this.tabPageRegion.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMessage1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMessage2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMessage3)).BeginInit();
			this.groupBoxBind.SuspendLayout();
			this.groupBoxBlanklist.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridBlacklist)).BeginInit();
			this.contextMenuMsg1.SuspendLayout();
			this.contextMenuUserlist.SuspendLayout();
			this.contextMenuBlanklist.SuspendLayout();
			this.contextMenuMsg2.SuspendLayout();
			this.contextMenuMsg3.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolStripMenu_Report1
			// 
			ToolStripMenu_Report1.Name = "ToolStripMenu_Report1";
			ToolStripMenu_Report1.Size = new System.Drawing.Size(136, 22);
			ToolStripMenu_Report1.Text = "举报";
			ToolStripMenu_Report1.Click += new System.EventHandler(this.ToolStripMenu_Report1_Click);
			// 
			// ToolStripMenu_Report2
			// 
			ToolStripMenu_Report2.Name = "ToolStripMenu_Report2";
			ToolStripMenu_Report2.Size = new System.Drawing.Size(136, 22);
			ToolStripMenu_Report2.Text = "举报";
			ToolStripMenu_Report2.Click += new System.EventHandler(this.ToolStripMenu_Report2_Click);
			// 
			// ToolStripMenu_Report3
			// 
			ToolStripMenu_Report3.Name = "ToolStripMenu_Report3";
			ToolStripMenu_Report3.Size = new System.Drawing.Size(136, 22);
			ToolStripMenu_Report3.Text = "举报";
			ToolStripMenu_Report3.Click += new System.EventHandler(this.ToolStripMenu_Report3_Click);
			// 
			// dataGridUser
			// 
			this.dataGridUser.AllowUserToAddRows = false;
			this.dataGridUser.AllowUserToDeleteRows = false;
			this.dataGridUser.AllowUserToResizeColumns = false;
			this.dataGridUser.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
			this.dataGridUser.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridUser.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridUser.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridUser.ColumnHeadersVisible = false;
			this.dataGridUser.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewUserColumn1});
			this.dataGridUser.Location = new System.Drawing.Point(6, 20);
			this.dataGridUser.MultiSelect = false;
			this.dataGridUser.Name = "dataGridUser";
			this.dataGridUser.RowHeadersVisible = false;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
			this.dataGridUser.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridUser.RowTemplate.Height = 20;
			this.dataGridUser.RowTemplate.ReadOnly = true;
			this.dataGridUser.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridUser.Size = new System.Drawing.Size(121, 438);
			this.dataGridUser.TabIndex = 1;
			this.dataGridUser.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridUser_CellMouseDown);
			// 
			// dataGridViewUserColumn1
			// 
			this.dataGridViewUserColumn1.Name = "dataGridViewUserColumn1";
			this.dataGridViewUserColumn1.ReadOnly = true;
			// 
			// groupBoxStatus
			// 
			this.groupBoxStatus.Controls.Add(this.buttonRegister);
			this.groupBoxStatus.Controls.Add(this.buttonLogin);
			this.groupBoxStatus.Controls.Add(this.labelLoginStatus);
			this.groupBoxStatus.Controls.Add(this.labelGameProcessStatus);
			this.groupBoxStatus.Controls.Add(this.labelFFXIVPluginStatus);
			this.groupBoxStatus.Controls.Add(this.labelACTStatus);
			this.groupBoxStatus.Controls.Add(this.labelLogin);
			this.groupBoxStatus.Controls.Add(this.labelGameProcess);
			this.groupBoxStatus.Controls.Add(this.labelFFXIVPlugin);
			this.groupBoxStatus.Controls.Add(this.labelACT);
			this.groupBoxStatus.Location = new System.Drawing.Point(12, 25);
			this.groupBoxStatus.Name = "groupBoxStatus";
			this.groupBoxStatus.Size = new System.Drawing.Size(283, 127);
			this.groupBoxStatus.TabIndex = 1;
			this.groupBoxStatus.TabStop = false;
			this.groupBoxStatus.Text = "状态";
			// 
			// buttonRegister
			// 
			this.buttonRegister.Location = new System.Drawing.Point(191, 71);
			this.buttonRegister.Name = "buttonRegister";
			this.buttonRegister.Size = new System.Drawing.Size(75, 23);
			this.buttonRegister.TabIndex = 1;
			this.buttonRegister.Text = "注册";
			this.buttonRegister.UseVisualStyleBackColor = true;
			this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
			// 
			// buttonLogin
			// 
			this.buttonLogin.Location = new System.Drawing.Point(191, 28);
			this.buttonLogin.Name = "buttonLogin";
			this.buttonLogin.Size = new System.Drawing.Size(75, 23);
			this.buttonLogin.TabIndex = 0;
			this.buttonLogin.Text = "登录";
			this.buttonLogin.UseVisualStyleBackColor = true;
			this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
			// 
			// labelLoginStatus
			// 
			this.labelLoginStatus.AutoSize = true;
			this.labelLoginStatus.Location = new System.Drawing.Point(90, 101);
			this.labelLoginStatus.Name = "labelLoginStatus";
			this.labelLoginStatus.Size = new System.Drawing.Size(71, 12);
			this.labelLoginStatus.TabIndex = 0;
			this.labelLoginStatus.Text = "No Process.";
			// 
			// labelGameProcessStatus
			// 
			this.labelGameProcessStatus.AutoSize = true;
			this.labelGameProcessStatus.Location = new System.Drawing.Point(90, 76);
			this.labelGameProcessStatus.Name = "labelGameProcessStatus";
			this.labelGameProcessStatus.Size = new System.Drawing.Size(71, 12);
			this.labelGameProcessStatus.TabIndex = 0;
			this.labelGameProcessStatus.Text = "No Process.";
			// 
			// labelFFXIVPluginStatus
			// 
			this.labelFFXIVPluginStatus.AutoSize = true;
			this.labelFFXIVPluginStatus.Location = new System.Drawing.Point(90, 52);
			this.labelFFXIVPluginStatus.Name = "labelFFXIVPluginStatus";
			this.labelFFXIVPluginStatus.Size = new System.Drawing.Size(71, 12);
			this.labelFFXIVPluginStatus.TabIndex = 0;
			this.labelFFXIVPluginStatus.Text = "No Process.";
			// 
			// labelACTStatus
			// 
			this.labelACTStatus.AutoSize = true;
			this.labelACTStatus.Location = new System.Drawing.Point(90, 28);
			this.labelACTStatus.Name = "labelACTStatus";
			this.labelACTStatus.Size = new System.Drawing.Size(65, 12);
			this.labelACTStatus.TabIndex = 0;
			this.labelACTStatus.Text = "Running...";
			// 
			// labelLogin
			// 
			this.labelLogin.AutoSize = true;
			this.labelLogin.Location = new System.Drawing.Point(6, 101);
			this.labelLogin.Name = "labelLogin";
			this.labelLogin.Size = new System.Drawing.Size(47, 12);
			this.labelLogin.TabIndex = 0;
			this.labelLogin.Text = "Login: ";
			// 
			// labelGameProcess
			// 
			this.labelGameProcess.AutoSize = true;
			this.labelGameProcess.Location = new System.Drawing.Point(6, 76);
			this.labelGameProcess.Name = "labelGameProcess";
			this.labelGameProcess.Size = new System.Drawing.Size(83, 12);
			this.labelGameProcess.TabIndex = 0;
			this.labelGameProcess.Text = "GameProcess: ";
			// 
			// labelFFXIVPlugin
			// 
			this.labelFFXIVPlugin.AutoSize = true;
			this.labelFFXIVPlugin.Location = new System.Drawing.Point(6, 52);
			this.labelFFXIVPlugin.Name = "labelFFXIVPlugin";
			this.labelFFXIVPlugin.Size = new System.Drawing.Size(83, 12);
			this.labelFFXIVPlugin.TabIndex = 0;
			this.labelFFXIVPlugin.Text = "FFXIVPlugin: ";
			// 
			// labelACT
			// 
			this.labelACT.AutoSize = true;
			this.labelACT.Location = new System.Drawing.Point(6, 28);
			this.labelACT.Name = "labelACT";
			this.labelACT.Size = new System.Drawing.Size(35, 12);
			this.labelACT.TabIndex = 0;
			this.labelACT.Text = "ACT: ";
			// 
			// groupBoxRule
			// 
			this.groupBoxRule.Controls.Add(this.flowLayoutRule);
			this.groupBoxRule.Controls.Add(this.checkBoxRuleSelect);
			this.groupBoxRule.Controls.Add(this.buttonRuleAdd);
			this.groupBoxRule.Controls.Add(this.textBoxRule);
			this.groupBoxRule.Location = new System.Drawing.Point(12, 245);
			this.groupBoxRule.Name = "groupBoxRule";
			this.groupBoxRule.Size = new System.Drawing.Size(283, 231);
			this.groupBoxRule.TabIndex = 3;
			this.groupBoxRule.TabStop = false;
			this.groupBoxRule.Text = "聊天过滤器";
			// 
			// flowLayoutRule
			// 
			this.flowLayoutRule.Location = new System.Drawing.Point(8, 77);
			this.flowLayoutRule.Name = "flowLayoutRule";
			this.flowLayoutRule.Size = new System.Drawing.Size(269, 148);
			this.flowLayoutRule.TabIndex = 3;
			// 
			// checkBoxRuleSelect
			// 
			this.checkBoxRuleSelect.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxRuleSelect.AutoSize = true;
			this.checkBoxRuleSelect.Location = new System.Drawing.Point(8, 48);
			this.checkBoxRuleSelect.Name = "checkBoxRuleSelect";
			this.checkBoxRuleSelect.Size = new System.Drawing.Size(51, 22);
			this.checkBoxRuleSelect.TabIndex = 2;
			this.checkBoxRuleSelect.Text = "包括项";
			this.checkBoxRuleSelect.UseVisualStyleBackColor = true;
			this.checkBoxRuleSelect.Click += new System.EventHandler(this.checkBoxRuleSelect_Click);
			// 
			// buttonRuleAdd
			// 
			this.buttonRuleAdd.Location = new System.Drawing.Point(202, 19);
			this.buttonRuleAdd.Name = "buttonRuleAdd";
			this.buttonRuleAdd.Size = new System.Drawing.Size(75, 23);
			this.buttonRuleAdd.TabIndex = 1;
			this.buttonRuleAdd.Text = "添加";
			this.buttonRuleAdd.UseVisualStyleBackColor = true;
			this.buttonRuleAdd.Click += new System.EventHandler(this.buttonRuleAdd_Click);
			// 
			// textBoxRule
			// 
			this.textBoxRule.Location = new System.Drawing.Point(8, 21);
			this.textBoxRule.Name = "textBoxRule";
			this.textBoxRule.Size = new System.Drawing.Size(188, 21);
			this.textBoxRule.TabIndex = 0;
			this.textBoxRule.Enter += new System.EventHandler(this.textBoxRule_GotFocus);
			this.textBoxRule.Leave += new System.EventHandler(this.textBoxRule_LostFocus);
			// 
			// groupboxUser
			// 
			this.groupboxUser.Controls.Add(this.groupBox1);
			this.groupboxUser.Controls.Add(this.dataGridUser);
			this.groupboxUser.Location = new System.Drawing.Point(605, 14);
			this.groupboxUser.Name = "groupboxUser";
			this.groupboxUser.Size = new System.Drawing.Size(132, 462);
			this.groupboxUser.TabIndex = 5;
			this.groupboxUser.TabStop = false;
			this.groupboxUser.Text = "服务器当前用户";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dataGridView1);
			this.groupBox1.Location = new System.Drawing.Point(149, 1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(149, 462);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "服务器当前用户";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
			this.dataGridView1.Location = new System.Drawing.Point(6, 20);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
			this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridView1.RowTemplate.Height = 20;
			this.dataGridView1.RowTemplate.ReadOnly = true;
			this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.Size = new System.Drawing.Size(137, 438);
			this.dataGridView1.TabIndex = 1;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPageRegion);
			this.tabControl.Controls.Add(this.tabPageTeamUp);
			this.tabControl.Controls.Add(this.tabPageTrade);
			this.tabControl.Location = new System.Drawing.Point(301, 14);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(304, 462);
			this.tabControl.TabIndex = 4;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
			// 
			// tabPageRegion
			// 
			this.tabPageRegion.Controls.Add(this.dataGridMessage1);
			this.tabPageRegion.Location = new System.Drawing.Point(4, 22);
			this.tabPageRegion.Name = "tabPageRegion";
			this.tabPageRegion.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageRegion.Size = new System.Drawing.Size(296, 436);
			this.tabPageRegion.TabIndex = 0;
			this.tabPageRegion.Text = "区域频道";
			this.tabPageRegion.UseVisualStyleBackColor = true;
			// 
			// dataGridMessage1
			// 
			this.dataGridMessage1.AllowUserToAddRows = false;
			this.dataGridMessage1.AllowUserToDeleteRows = false;
			this.dataGridMessage1.AllowUserToResizeColumns = false;
			this.dataGridMessage1.AllowUserToResizeRows = false;
			this.dataGridMessage1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridMessage1.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridMessage1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridMessage1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridMessage1.ColumnHeadersVisible = false;
			this.dataGridMessage1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12});
			this.dataGridMessage1.Location = new System.Drawing.Point(0, 0);
			this.dataGridMessage1.MultiSelect = false;
			this.dataGridMessage1.Name = "dataGridMessage1";
			this.dataGridMessage1.ReadOnly = true;
			this.dataGridMessage1.RowHeadersVisible = false;
			this.dataGridMessage1.RowTemplate.Height = 17;
			this.dataGridMessage1.RowTemplate.ReadOnly = true;
			this.dataGridMessage1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridMessage1.Size = new System.Drawing.Size(293, 434);
			this.dataGridMessage1.TabIndex = 0;
			this.dataGridMessage1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid1Message_CellMouseDown);
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn11.FillWeight = 50.56817F;
			this.dataGridViewTextBoxColumn11.HeaderText = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.ReadOnly = true;
			this.dataGridViewTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn11.Width = 70;
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.FillWeight = 148.6704F;
			this.dataGridViewTextBoxColumn12.HeaderText = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			this.dataGridViewTextBoxColumn12.ReadOnly = true;
			this.dataGridViewTextBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// tabPageTeamUp
			// 
			this.tabPageTeamUp.Location = new System.Drawing.Point(4, 22);
			this.tabPageTeamUp.Name = "tabPageTeamUp";
			this.tabPageTeamUp.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageTeamUp.Size = new System.Drawing.Size(296, 436);
			this.tabPageTeamUp.TabIndex = 1;
			this.tabPageTeamUp.Text = "组队频道";
			this.tabPageTeamUp.UseVisualStyleBackColor = true;
			// 
			// tabPageTrade
			// 
			this.tabPageTrade.Location = new System.Drawing.Point(4, 22);
			this.tabPageTrade.Name = "tabPageTrade";
			this.tabPageTrade.Size = new System.Drawing.Size(296, 436);
			this.tabPageTrade.TabIndex = 2;
			this.tabPageTrade.Text = "交易频道";
			this.tabPageTrade.UseVisualStyleBackColor = true;
			// 
			// dataGridMessage2
			// 
			this.dataGridMessage2.AllowUserToAddRows = false;
			this.dataGridMessage2.AllowUserToDeleteRows = false;
			this.dataGridMessage2.AllowUserToResizeColumns = false;
			this.dataGridMessage2.AllowUserToResizeRows = false;
			this.dataGridMessage2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridMessage2.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridMessage2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridMessage2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridMessage2.ColumnHeadersVisible = false;
			this.dataGridMessage2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn21,
            this.dataGridViewTextBoxColumn22});
			this.dataGridMessage2.Location = new System.Drawing.Point(0, 0);
			this.dataGridMessage2.MultiSelect = false;
			this.dataGridMessage2.Name = "dataGridMessage2";
			this.dataGridMessage2.ReadOnly = true;
			this.dataGridMessage2.RowHeadersVisible = false;
			this.dataGridMessage2.RowTemplate.Height = 17;
			this.dataGridMessage2.RowTemplate.ReadOnly = true;
			this.dataGridMessage2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridMessage2.Size = new System.Drawing.Size(293, 434);
			this.dataGridMessage2.TabIndex = 0;
			this.dataGridMessage2.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid2Message_CellMouseDown);
			// 
			// dataGridViewTextBoxColumn21
			// 
			this.dataGridViewTextBoxColumn21.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn21.FillWeight = 76.14214F;
			this.dataGridViewTextBoxColumn21.HeaderText = "";
			this.dataGridViewTextBoxColumn21.Name = "dataGridViewTextBoxColumn21";
			this.dataGridViewTextBoxColumn21.ReadOnly = true;
			this.dataGridViewTextBoxColumn21.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn21.Width = 70;
			// 
			// dataGridViewTextBoxColumn22
			// 
			this.dataGridViewTextBoxColumn22.FillWeight = 123.8579F;
			this.dataGridViewTextBoxColumn22.HeaderText = "";
			this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
			this.dataGridViewTextBoxColumn22.ReadOnly = true;
			this.dataGridViewTextBoxColumn22.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// dataGridMessage3
			// 
			this.dataGridMessage3.AllowUserToAddRows = false;
			this.dataGridMessage3.AllowUserToDeleteRows = false;
			this.dataGridMessage3.AllowUserToResizeColumns = false;
			this.dataGridMessage3.AllowUserToResizeRows = false;
			this.dataGridMessage3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridMessage3.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridMessage3.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridMessage3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridMessage3.ColumnHeadersVisible = false;
			this.dataGridMessage3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn32});
			this.dataGridMessage3.Location = new System.Drawing.Point(0, 0);
			this.dataGridMessage3.MultiSelect = false;
			this.dataGridMessage3.Name = "dataGridMessage3";
			this.dataGridMessage3.ReadOnly = true;
			this.dataGridMessage3.RowHeadersVisible = false;
			this.dataGridMessage3.RowTemplate.Height = 17;
			this.dataGridMessage3.RowTemplate.ReadOnly = true;
			this.dataGridMessage3.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridMessage3.Size = new System.Drawing.Size(293, 434);
			this.dataGridMessage3.TabIndex = 0;
			this.dataGridMessage3.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid3Message_CellMouseDown);
			// 
			// dataGridViewTextBoxColumn31
			// 
			this.dataGridViewTextBoxColumn31.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.dataGridViewTextBoxColumn31.FillWeight = 76.14214F;
			this.dataGridViewTextBoxColumn31.HeaderText = "dataGridViewTextBoxColumn31";
			this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
			this.dataGridViewTextBoxColumn31.ReadOnly = true;
			this.dataGridViewTextBoxColumn31.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewTextBoxColumn31.Width = 70;
			// 
			// dataGridViewTextBoxColumn32
			// 
			this.dataGridViewTextBoxColumn32.FillWeight = 123.8579F;
			this.dataGridViewTextBoxColumn32.HeaderText = "dataGridViewTextBoxColumn32";
			this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
			this.dataGridViewTextBoxColumn32.ReadOnly = true;
			this.dataGridViewTextBoxColumn32.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			// 
			// groupBoxBind
			// 
			this.groupBoxBind.Controls.Add(this.comboBox3);
			this.groupBoxBind.Controls.Add(this.comboBox2);
			this.groupBoxBind.Controls.Add(this.comboBox1);
			this.groupBoxBind.Controls.Add(this.label4);
			this.groupBoxBind.Controls.Add(this.label3);
			this.groupBoxBind.Controls.Add(this.label2);
			this.groupBoxBind.Location = new System.Drawing.Point(12, 159);
			this.groupBoxBind.Name = "groupBoxBind";
			this.groupBoxBind.Size = new System.Drawing.Size(283, 80);
			this.groupBoxBind.TabIndex = 2;
			this.groupBoxBind.TabStop = false;
			this.groupBoxBind.Text = "通讯贝";
			// 
			// comboBox3
			// 
			this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.comboBox3.FormattingEnabled = true;
			this.comboBox3.Location = new System.Drawing.Point(92, 57);
			this.comboBox3.Name = "comboBox3";
			this.comboBox3.Size = new System.Drawing.Size(121, 20);
			this.comboBox3.TabIndex = 3;
			this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.combobox3SelectedIndexChanged);
			// 
			// comboBox2
			// 
			this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.comboBox2.FormattingEnabled = true;
			this.comboBox2.Location = new System.Drawing.Point(92, 34);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(121, 20);
			this.comboBox2.TabIndex = 2;
			this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.combobox2SelectedIndexChanged);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(92, 9);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 1;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.combobox1SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(33, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "交易频道";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(33, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "组队频道";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(33, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "区域频道";
			// 
			// groupBoxBlanklist
			// 
			this.groupBoxBlanklist.Controls.Add(this.groupBox3);
			this.groupBoxBlanklist.Controls.Add(this.dataGridBlacklist);
			this.groupBoxBlanklist.Location = new System.Drawing.Point(743, 14);
			this.groupBoxBlanklist.Name = "groupBoxBlanklist";
			this.groupBoxBlanklist.Size = new System.Drawing.Size(132, 462);
			this.groupBoxBlanklist.TabIndex = 3;
			this.groupBoxBlanklist.TabStop = false;
			this.groupBoxBlanklist.Text = "黑名单";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.dataGridView2);
			this.groupBox3.Location = new System.Drawing.Point(149, 1);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(149, 462);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "黑名单";
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToResizeColumns = false;
			this.dataGridView2.AllowUserToResizeRows = false;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
			this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
			this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridView2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.ColumnHeadersVisible = false;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2});
			this.dataGridView2.Location = new System.Drawing.Point(6, 20);
			this.dataGridView2.MultiSelect = false;
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowHeadersVisible = false;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
			this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle6;
			this.dataGridView2.RowTemplate.Height = 20;
			this.dataGridView2.RowTemplate.ReadOnly = true;
			this.dataGridView2.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView2.Size = new System.Drawing.Size(137, 438);
			this.dataGridView2.TabIndex = 1;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			// 
			// dataGridBlacklist
			// 
			this.dataGridBlacklist.AllowUserToAddRows = false;
			this.dataGridBlacklist.AllowUserToDeleteRows = false;
			this.dataGridBlacklist.AllowUserToResizeColumns = false;
			this.dataGridBlacklist.AllowUserToResizeRows = false;
			dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
			this.dataGridBlacklist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
			this.dataGridBlacklist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridBlacklist.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dataGridBlacklist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.dataGridBlacklist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridBlacklist.ColumnHeadersVisible = false;
			this.dataGridBlacklist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3});
			this.dataGridBlacklist.Location = new System.Drawing.Point(6, 20);
			this.dataGridBlacklist.MultiSelect = false;
			this.dataGridBlacklist.Name = "dataGridBlacklist";
			this.dataGridBlacklist.RowHeadersVisible = false;
			dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
			this.dataGridBlacklist.RowsDefaultCellStyle = dataGridViewCellStyle8;
			this.dataGridBlacklist.RowTemplate.Height = 20;
			this.dataGridBlacklist.RowTemplate.ReadOnly = true;
			this.dataGridBlacklist.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridBlacklist.Size = new System.Drawing.Size(121, 438);
			this.dataGridBlacklist.TabIndex = 1;
			this.dataGridBlacklist.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridBlacklist_CellMouseDown);
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			// 
			// contextMenuMsg1
			// 
			this.contextMenuMsg1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ToolStripMenu_Report1,
            this.ToolStripMenu_black1});
			this.contextMenuMsg1.Name = "contextMenuStrip1";
			this.contextMenuMsg1.Size = new System.Drawing.Size(137, 48);
			// 
			// ToolStripMenu_black1
			// 
			this.ToolStripMenu_black1.Name = "ToolStripMenu_black1";
			this.ToolStripMenu_black1.Size = new System.Drawing.Size(136, 22);
			this.ToolStripMenu_black1.Text = "屏蔽发送者";
			this.ToolStripMenu_black1.Click += new System.EventHandler(this.ToolStripMenu_black1_Click);
			// 
			// contextMenuUserlist
			// 
			this.contextMenuUserlist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemBlank});
			this.contextMenuUserlist.Name = "contextMenuUserlist";
			this.contextMenuUserlist.Size = new System.Drawing.Size(137, 26);
			// 
			// ToolStripMenuItemBlank
			// 
			this.ToolStripMenuItemBlank.Name = "ToolStripMenuItemBlank";
			this.ToolStripMenuItemBlank.Size = new System.Drawing.Size(136, 22);
			this.ToolStripMenuItemBlank.Text = "屏蔽该用户";
			this.ToolStripMenuItemBlank.Click += new System.EventHandler(this.ToolStripMenuItemBlank_Click);
			// 
			// contextMenuBlanklist
			// 
			this.contextMenuBlanklist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemResume});
			this.contextMenuBlanklist.Name = "contextMenuBlanklist";
			this.contextMenuBlanklist.Size = new System.Drawing.Size(137, 26);
			// 
			// ToolStripMenuItemResume
			// 
			this.ToolStripMenuItemResume.AccessibleRole = System.Windows.Forms.AccessibleRole.Cell;
			this.ToolStripMenuItemResume.Name = "ToolStripMenuItemResume";
			this.ToolStripMenuItemResume.Size = new System.Drawing.Size(136, 22);
			this.ToolStripMenuItemResume.Text = "恢复该用户";
			this.ToolStripMenuItemResume.Click += new System.EventHandler(this.ToolStripMenuItemResume_Click);
			// 
			// contextMenuMsg2
			// 
			this.contextMenuMsg2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ToolStripMenu_Report2,
            this.ToolStripMenu_black2});
			this.contextMenuMsg2.Name = "contextMenuStrip1";
			this.contextMenuMsg2.Size = new System.Drawing.Size(137, 48);
			// 
			// ToolStripMenu_black2
			// 
			this.ToolStripMenu_black2.Name = "ToolStripMenu_black2";
			this.ToolStripMenu_black2.Size = new System.Drawing.Size(136, 22);
			this.ToolStripMenu_black2.Text = "屏蔽发送者";
			this.ToolStripMenu_black2.Click += new System.EventHandler(this.ToolStripMenu_black2_Click);
			// 
			// contextMenuMsg3
			// 
			this.contextMenuMsg3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ToolStripMenu_Report3,
            this.ToolStripMenu_black3});
			this.contextMenuMsg3.Name = "contextMenuStrip1";
			this.contextMenuMsg3.Size = new System.Drawing.Size(137, 48);
			// 
			// ToolStripMenu_black3
			// 
			this.ToolStripMenu_black3.Name = "ToolStripMenu_black3";
			this.ToolStripMenu_black3.Size = new System.Drawing.Size(136, 22);
			this.ToolStripMenu_black3.Text = "屏蔽发送者";
			this.ToolStripMenu_black3.Click += new System.EventHandler(this.ToolStripMenu_black3_Click);
			// 
			// FF14ChatUi
			// 
			this.Controls.Add(this.groupBoxBlanklist);
			this.Controls.Add(this.groupBoxBind);
			this.Controls.Add(this.groupboxUser);
			this.Controls.Add(this.groupBoxRule);
			this.Controls.Add(this.groupBoxStatus);
			this.Controls.Add(this.tabControl);
			this.Name = "FF14ChatUi";
			this.Size = new System.Drawing.Size(897, 480);
			((System.ComponentModel.ISupportInitialize)(this.dataGridUser)).EndInit();
			this.groupBoxStatus.ResumeLayout(false);
			this.groupBoxStatus.PerformLayout();
			this.groupBoxRule.ResumeLayout(false);
			this.groupBoxRule.PerformLayout();
			this.groupboxUser.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.tabControl.ResumeLayout(false);
			this.tabPageRegion.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridMessage1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMessage2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMessage3)).EndInit();
			this.groupBoxBind.ResumeLayout(false);
			this.groupBoxBind.PerformLayout();
			this.groupBoxBlanklist.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridBlacklist)).EndInit();
			this.contextMenuMsg1.ResumeLayout(false);
			this.contextMenuUserlist.ResumeLayout(false);
			this.contextMenuBlanklist.ResumeLayout(false);
			this.contextMenuMsg2.ResumeLayout(false);
			this.contextMenuMsg3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private void TextBoxRule_LostFocus(object sender, EventArgs e) {
			throw new NotImplementedException();
		}

		#endregion
		private System.Windows.Forms.GroupBox groupBoxStatus;
		private System.Windows.Forms.Label labelFFXIVPlugin;
		private System.Windows.Forms.Label labelACT;
		private System.Windows.Forms.GroupBox groupBoxRule;
		private System.Windows.Forms.Label labelLoginStatus;
		private System.Windows.Forms.Label labelGameProcessStatus;
		private System.Windows.Forms.Label labelFFXIVPluginStatus;
		private System.Windows.Forms.Label labelACTStatus;
		private System.Windows.Forms.Label labelLogin;
		private System.Windows.Forms.Label labelGameProcess;
		private System.Windows.Forms.CheckBox checkBoxRuleSelect;
		private System.Windows.Forms.Button buttonRuleAdd;
		private System.Windows.Forms.TextBox textBoxRule;
		private System.Windows.Forms.DataGridView dataGridUser;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewUserColumn1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutRule;
		private Button buttonRegister;
		private Button buttonLogin;
		public LoginForm loginForm;
		public RegisterForm registerForm;
		private GroupBox groupboxUser;
		private TabControl tabControl;
		private TabPage tabPageRegion;
		private DataGridView dataGridMessage2;
		private DataGridView dataGridMessage3;
		private DataGridView dataGridMessage1;
		private TabPage tabPageTeamUp;
		private TabPage tabPageTrade;
		private GroupBox groupBoxBind;
		private Label label4;
		private Label label3;
		private Label label2;
		public ComboBox comboBox3;
		public ComboBox comboBox2;
		public ComboBox comboBox1;
		private GroupBox groupBox1;
		private DataGridView dataGridView1;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private GroupBox groupBoxBlanklist;
		private GroupBox groupBox3;
		private DataGridView dataGridView2;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private DataGridView dataGridBlacklist;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private ContextMenuStrip contextMenuMsg1;
		private ContextMenuStrip contextMenuUserlist;
		private ContextMenuStrip contextMenuBlanklist;
		private ToolStripMenuItem ToolStripMenu_black1;
		private ToolStripMenuItem ToolStripMenuItemBlank;
		private ToolStripMenuItem ToolStripMenuItemResume;
		private ContextMenuStrip contextMenuMsg2;
		private ToolStripMenuItem ToolStripMenu_black2;
		private ContextMenuStrip contextMenuMsg3;
		private ToolStripMenuItem ToolStripMenu_black3;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
	}
}
