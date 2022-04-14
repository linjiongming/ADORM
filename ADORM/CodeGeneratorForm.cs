using Microsoft.VisualBasic;
using ADORM.Domain;
using System;
using System.Linq;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using ADORM.Domain.Objects.Generator;
using System.Data.Common;
using System.Collections.Generic;

namespace ADORM
{
    public partial class CodeGeneratorForm : Form
    {
        private static readonly object _locker = new object();

        public CodeGeneratorForm()
        {
            InitializeComponent();
            DefaultSetting();
            NextButton1.Click += NextButton1_Click;
            NextButton2.Click += NextButton2_Click;
        }

        private bool DisableSelectTab { get; set; } = true;

        private AdoCodeGenerator Generator { get; set; }

        private DbProviderFactory Provider => Const.Schema.Factories.Values.ElementAt(SchemaProviderCheckedListBox.SelectedIndex);

        private GeneratorOption Option => new GeneratorOption
        {
            NameSpace = NameSpaceTextBox.Text,
            ConnectionString = ConnectionStringTextBox.Text,
            DbProviderFactory = Provider,
            GenerateEntities = ActionsCheckedListBox.GetItemChecked(0),
            GenerateRepositories = ActionsCheckedListBox.GetItemChecked(1)
        };

        private void ShowInfo(string info)
        {
            MessageListBox.Items.Add($"{DateTime.Now:HH:mm:ss}: {info}");
            MessageListBox.SelectedIndex = MessageListBox.Items.Count - 1;
        }

        private void ShowError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DefaultSetting()
        {
            //NameSpaceTextBox.Text = "My";
            //ConnectionStringTextBox.Text = "data source=.\\SQLEXPRESS;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|aspnetdb.mdf;User Instance=true";
            NameSpaceTextBox.Text = "SE";
            ConnectionStringTextBox.Text = "data source=RD-VM-T004,2064;initial catalog=iTrackApp;persist security info=True;user id=timetrackpro;password=tp2000;MultipleActiveResultSets=True;App=EntityFramework";

            //SchemaProviderCheckedListBox.DataSource = Const.Schema.Factories.ToDictionary(x => x.Key, x => x.Value.GetType().Namespace);
            //SchemaProviderCheckedListBox.DisplayMember = "Value";
            SchemaProviderCheckedListBox.Items.Clear();
            foreach (var key in Const.Schema.Factories.Keys)
            {
                SchemaProviderCheckedListBox.Items.Add(Const.Schema.Factories[key].GetType().Namespace);
            }

            var providerIndex = Const.Schema.Factories.Keys.ToList().IndexOf("Sql");
            SchemaProviderCheckedListBox.SelectedIndex = providerIndex;
            SchemaProviderCheckedListBox.SetItemChecked(providerIndex, true);

            for (int i = 0; i < ActionsCheckedListBox.Items.Count; i++)
            {
                ActionsCheckedListBox.SetItemChecked(i, true);
            }
        }

        private void MainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = DisableSelectTab;
        }

        private void SchemaProviderCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            foreach (int index in SchemaProviderCheckedListBox.CheckedIndices)
            {
                if (index != e.Index)
                {
                    SchemaProviderCheckedListBox.SetItemChecked(index, false);
                }
            }
        }

        private void TestConnectionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (var conn = Provider.CreateConnection())
                {
                    conn.ConnectionString = ConnectionStringTextBox.Text;
                    conn.Open();
                    var dtSchema = conn.GetSchema("Tables");
                    if (dtSchema.Rows.Count > 0)
                        MessageBox.Show($"The database contains {dtSchema.Rows.Count} tables.", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("There are no tables in the database.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            DisableSelectTab = false;
            if (MainTabControl.SelectedIndex > 0)
            {
                MainTabControl.SelectedIndex--;
            }
            DisableSelectTab = true;
            Application.DoEvents();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            DisableSelectTab = false;
            if (MainTabControl.SelectedIndex < MainTabControl.TabCount - 1)
            {
                MainTabControl.SelectedIndex++;
            }
            DisableSelectTab = true;
            Application.DoEvents();
        }

        private void NextButton1_Click(object sender, EventArgs e)
        {
            NextButton2.Enabled = false;
            try
            {
                TablesTreeView.Nodes.Clear();
                var root = TablesTreeView.Nodes.Add("All");
                Generator = new AdoCodeGenerator(Option);
                Generator.OnReportProgress += GenBackgroundWorker.ReportProgress;
                foreach (var tableName in Generator.GetAllTableNames().OrderBy(x => x))
                {
                    root.Nodes.Add(tableName);
                    root.Expand();
                    Application.DoEvents();
                }
                root.Checked = true;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                NextButton2.Enabled = false;
            }
            NextButton2.Enabled = true;
        }

        private void NextButton2_Click(object sender, EventArgs e)
        {
            GenerateButton.Enabled = false;
            Generator.SelectTables(TablesTreeView.Nodes[0].Nodes.Cast<TreeNode>().Where(x => x.Checked).Select(x => x.Text));
            GenerateButton.Enabled = true;
        }

        private void TablesTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes != null && e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            lock (_locker)
            {
                if (Generator.IsRunning)
                {
                    PrevButton3.Enabled = true;
                    Generator.Cancel();
                    GenerateButton.Text = "Generate";
                }
                else
                {
                    PrevButton3.Enabled = false;
                    MessageListBox.Items.Clear();
                    Generator.Ready();
                    {
                        progressBar1.Maximum = Generator.TaskCount;
                    }
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    GenBackgroundWorker.RunWorkerAsync();
                    GenerateButton.Text = "Cancel";
                }
            }
        }

        private void GenBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Generator.Go();
        }

        private void GenBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.UserState is Exception ex)
            {
                ShowError(ex.Message);
            }
            else
            {
                ShowInfo(e.UserState.ToString());
            }
        }

        private void GenBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            GenerateButton.Text = "Generate";
            progressBar1.Visible = false;
            PrevButton3.Enabled = true;
        }
    }
}
