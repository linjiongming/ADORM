using System;

namespace ADORM
{
    partial class CodeGeneratorForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ConnectionStringTextBox = new System.Windows.Forms.TextBox();
            this.GenerateButton = new System.Windows.Forms.Button();
            this.MessageListBox = new System.Windows.Forms.ListBox();
            this.GenBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.NameSpaceGroupBox = new System.Windows.Forms.GroupBox();
            this.NameSpaceTextBox = new System.Windows.Forms.TextBox();
            this.SchemaProviderGroupBox = new System.Windows.Forms.GroupBox();
            this.SchemaProviderCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.ConnectionStringGroupBox = new System.Windows.Forms.GroupBox();
            this.TestConnectionLinkLabel = new System.Windows.Forms.LinkLabel();
            this.NextButton1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TablesGroupBox = new System.Windows.Forms.GroupBox();
            this.TablesTreeView = new System.Windows.Forms.TreeView();
            this.PrevButton2 = new System.Windows.Forms.Button();
            this.NextButton2 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.MessageGroupBox = new System.Windows.Forms.GroupBox();
            this.ActionsGroupBox = new System.Windows.Forms.GroupBox();
            this.ActionsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.PrevButton3 = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.NameSpaceGroupBox.SuspendLayout();
            this.SchemaProviderGroupBox.SuspendLayout();
            this.ConnectionStringGroupBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.TablesGroupBox.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.MessageGroupBox.SuspendLayout();
            this.ActionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectionStringTextBox
            // 
            this.ConnectionStringTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionStringTextBox.Location = new System.Drawing.Point(3, 17);
            this.ConnectionStringTextBox.Multiline = true;
            this.ConnectionStringTextBox.Name = "ConnectionStringTextBox";
            this.ConnectionStringTextBox.Size = new System.Drawing.Size(554, 80);
            this.ConnectionStringTextBox.TabIndex = 2;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateButton.Location = new System.Drawing.Point(493, 504);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 5;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // MessageListBox
            // 
            this.MessageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageListBox.FormattingEnabled = true;
            this.MessageListBox.ItemHeight = 12;
            this.MessageListBox.Location = new System.Drawing.Point(3, 17);
            this.MessageListBox.Name = "MessageListBox";
            this.MessageListBox.Size = new System.Drawing.Size(554, 366);
            this.MessageListBox.TabIndex = 3;
            // 
            // GenBackgroundWorker
            // 
            this.GenBackgroundWorker.WorkerReportsProgress = true;
            this.GenBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GenBackgroundWorker_DoWork);
            this.GenBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.GenBackgroundWorker_ProgressChanged);
            this.GenBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GenBackgroundWorker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(8, 504);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(396, 23);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.tabPage1);
            this.MainTabControl.Controls.Add(this.tabPage2);
            this.MainTabControl.Controls.Add(this.tabPage3);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(584, 561);
            this.MainTabControl.TabIndex = 0;
            this.MainTabControl.TabStop = false;
            this.MainTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTabControl_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.NameSpaceGroupBox);
            this.tabPage1.Controls.Add(this.SchemaProviderGroupBox);
            this.tabPage1.Controls.Add(this.ConnectionStringGroupBox);
            this.tabPage1.Controls.Add(this.TestConnectionLinkLabel);
            this.tabPage1.Controls.Add(this.NextButton1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(576, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Setting";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // NameSpaceGroupBox
            // 
            this.NameSpaceGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameSpaceGroupBox.Controls.Add(this.NameSpaceTextBox);
            this.NameSpaceGroupBox.Location = new System.Drawing.Point(8, 6);
            this.NameSpaceGroupBox.Name = "NameSpaceGroupBox";
            this.NameSpaceGroupBox.Size = new System.Drawing.Size(560, 49);
            this.NameSpaceGroupBox.TabIndex = 3;
            this.NameSpaceGroupBox.TabStop = false;
            this.NameSpaceGroupBox.Text = "Name Space";
            // 
            // NameSpaceTextBox
            // 
            this.NameSpaceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameSpaceTextBox.Location = new System.Drawing.Point(3, 17);
            this.NameSpaceTextBox.Name = "NameSpaceTextBox";
            this.NameSpaceTextBox.Size = new System.Drawing.Size(554, 21);
            this.NameSpaceTextBox.TabIndex = 1;
            // 
            // SchemaProviderGroupBox
            // 
            this.SchemaProviderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SchemaProviderGroupBox.Controls.Add(this.SchemaProviderCheckedListBox);
            this.SchemaProviderGroupBox.Location = new System.Drawing.Point(8, 167);
            this.SchemaProviderGroupBox.Name = "SchemaProviderGroupBox";
            this.SchemaProviderGroupBox.Size = new System.Drawing.Size(560, 331);
            this.SchemaProviderGroupBox.TabIndex = 7;
            this.SchemaProviderGroupBox.TabStop = false;
            this.SchemaProviderGroupBox.Text = "Schema Provider";
            // 
            // SchemaProviderCheckedListBox
            // 
            this.SchemaProviderCheckedListBox.CheckOnClick = true;
            this.SchemaProviderCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SchemaProviderCheckedListBox.FormattingEnabled = true;
            this.SchemaProviderCheckedListBox.Location = new System.Drawing.Point(3, 17);
            this.SchemaProviderCheckedListBox.Name = "SchemaProviderCheckedListBox";
            this.SchemaProviderCheckedListBox.Size = new System.Drawing.Size(554, 311);
            this.SchemaProviderCheckedListBox.TabIndex = 3;
            this.SchemaProviderCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.SchemaProviderCheckedListBox_ItemCheck);
            // 
            // ConnectionStringGroupBox
            // 
            this.ConnectionStringGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectionStringGroupBox.Controls.Add(this.ConnectionStringTextBox);
            this.ConnectionStringGroupBox.Location = new System.Drawing.Point(8, 61);
            this.ConnectionStringGroupBox.Name = "ConnectionStringGroupBox";
            this.ConnectionStringGroupBox.Size = new System.Drawing.Size(560, 100);
            this.ConnectionStringGroupBox.TabIndex = 7;
            this.ConnectionStringGroupBox.TabStop = false;
            this.ConnectionStringGroupBox.Text = "Connection String";
            // 
            // TestConnectionLinkLabel
            // 
            this.TestConnectionLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TestConnectionLinkLabel.AutoSize = true;
            this.TestConnectionLinkLabel.Location = new System.Drawing.Point(6, 509);
            this.TestConnectionLinkLabel.Name = "TestConnectionLinkLabel";
            this.TestConnectionLinkLabel.Size = new System.Drawing.Size(95, 12);
            this.TestConnectionLinkLabel.TabIndex = 4;
            this.TestConnectionLinkLabel.TabStop = true;
            this.TestConnectionLinkLabel.Text = "Test Connection";
            this.TestConnectionLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TestConnectionLinkLabel_LinkClicked);
            // 
            // NextButton1
            // 
            this.NextButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton1.Location = new System.Drawing.Point(493, 504);
            this.NextButton1.Name = "NextButton1";
            this.NextButton1.Size = new System.Drawing.Size(75, 23);
            this.NextButton1.TabIndex = 5;
            this.NextButton1.Text = "Next >";
            this.NextButton1.UseVisualStyleBackColor = true;
            this.NextButton1.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TablesGroupBox);
            this.tabPage2.Controls.Add(this.PrevButton2);
            this.tabPage2.Controls.Add(this.NextButton2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(576, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Select Tables";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TablesGroupBox
            // 
            this.TablesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablesGroupBox.Controls.Add(this.TablesTreeView);
            this.TablesGroupBox.Location = new System.Drawing.Point(8, 6);
            this.TablesGroupBox.Name = "TablesGroupBox";
            this.TablesGroupBox.Size = new System.Drawing.Size(560, 492);
            this.TablesGroupBox.TabIndex = 8;
            this.TablesGroupBox.TabStop = false;
            this.TablesGroupBox.Text = "Tables";
            // 
            // TablesTreeView
            // 
            this.TablesTreeView.CheckBoxes = true;
            this.TablesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablesTreeView.Location = new System.Drawing.Point(3, 17);
            this.TablesTreeView.Name = "TablesTreeView";
            this.TablesTreeView.Size = new System.Drawing.Size(554, 472);
            this.TablesTreeView.TabIndex = 0;
            this.TablesTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TablesTreeView_AfterCheck);
            // 
            // PrevButton2
            // 
            this.PrevButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrevButton2.Location = new System.Drawing.Point(412, 504);
            this.PrevButton2.Name = "PrevButton2";
            this.PrevButton2.Size = new System.Drawing.Size(75, 23);
            this.PrevButton2.TabIndex = 6;
            this.PrevButton2.Text = "< Prev";
            this.PrevButton2.UseVisualStyleBackColor = true;
            this.PrevButton2.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // NextButton2
            // 
            this.NextButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NextButton2.Location = new System.Drawing.Point(493, 504);
            this.NextButton2.Name = "NextButton2";
            this.NextButton2.Size = new System.Drawing.Size(75, 23);
            this.NextButton2.TabIndex = 7;
            this.NextButton2.Text = "Next >";
            this.NextButton2.UseVisualStyleBackColor = true;
            this.NextButton2.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.MessageGroupBox);
            this.tabPage3.Controls.Add(this.ActionsGroupBox);
            this.tabPage3.Controls.Add(this.PrevButton3);
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.GenerateButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(576, 535);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Generating";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MessageGroupBox
            // 
            this.MessageGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageGroupBox.Controls.Add(this.MessageListBox);
            this.MessageGroupBox.Location = new System.Drawing.Point(8, 112);
            this.MessageGroupBox.Name = "MessageGroupBox";
            this.MessageGroupBox.Size = new System.Drawing.Size(560, 386);
            this.MessageGroupBox.TabIndex = 13;
            this.MessageGroupBox.TabStop = false;
            this.MessageGroupBox.Text = "Message";
            // 
            // ActionsGroupBox
            // 
            this.ActionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActionsGroupBox.Controls.Add(this.ActionsCheckedListBox);
            this.ActionsGroupBox.Location = new System.Drawing.Point(8, 6);
            this.ActionsGroupBox.Name = "ActionsGroupBox";
            this.ActionsGroupBox.Size = new System.Drawing.Size(560, 100);
            this.ActionsGroupBox.TabIndex = 12;
            this.ActionsGroupBox.TabStop = false;
            this.ActionsGroupBox.Text = "Actions";
            // 
            // ActionsCheckedListBox
            // 
            this.ActionsCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ActionsCheckedListBox.FormattingEnabled = true;
            this.ActionsCheckedListBox.Items.AddRange(new object[] {
            "Generate Entities",
            "Generate Repositories"});
            this.ActionsCheckedListBox.Location = new System.Drawing.Point(3, 17);
            this.ActionsCheckedListBox.Name = "ActionsCheckedListBox";
            this.ActionsCheckedListBox.Size = new System.Drawing.Size(554, 80);
            this.ActionsCheckedListBox.TabIndex = 2;
            // 
            // PrevButton3
            // 
            this.PrevButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PrevButton3.Location = new System.Drawing.Point(412, 504);
            this.PrevButton3.Name = "PrevButton3";
            this.PrevButton3.Size = new System.Drawing.Size(75, 23);
            this.PrevButton3.TabIndex = 4;
            this.PrevButton3.Text = "< Prev";
            this.PrevButton3.UseVisualStyleBackColor = true;
            this.PrevButton3.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // CodeGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.MainTabControl);
            this.Name = "CodeGeneratorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Generator";
            this.MainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.NameSpaceGroupBox.ResumeLayout(false);
            this.NameSpaceGroupBox.PerformLayout();
            this.SchemaProviderGroupBox.ResumeLayout(false);
            this.ConnectionStringGroupBox.ResumeLayout(false);
            this.ConnectionStringGroupBox.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.TablesGroupBox.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.MessageGroupBox.ResumeLayout(false);
            this.ActionsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox ConnectionStringTextBox;
        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.ListBox MessageListBox;
        private System.ComponentModel.BackgroundWorker GenBackgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button NextButton1;
        private System.Windows.Forms.LinkLabel TestConnectionLinkLabel;
        private System.Windows.Forms.Button PrevButton2;
        private System.Windows.Forms.Button NextButton2;
        private System.Windows.Forms.Button PrevButton3;
        private System.Windows.Forms.GroupBox SchemaProviderGroupBox;
        private System.Windows.Forms.CheckedListBox SchemaProviderCheckedListBox;
        private System.Windows.Forms.GroupBox ConnectionStringGroupBox;
        private System.Windows.Forms.GroupBox MessageGroupBox;
        private System.Windows.Forms.GroupBox ActionsGroupBox;
        private System.Windows.Forms.CheckedListBox ActionsCheckedListBox;
        private System.Windows.Forms.GroupBox TablesGroupBox;
        private System.Windows.Forms.TreeView TablesTreeView;
        private System.Windows.Forms.GroupBox NameSpaceGroupBox;
        private System.Windows.Forms.TextBox NameSpaceTextBox;
    }
}

