using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using MssqlClient.Classes.Beans;
using MssqlClient.Properties;

namespace MssqlClient.Classes.Views
{
    public partial class FrmDbConnection : Form
    {
        public FrmDbConnection()
        {
          
            InitializeComponent();

            InitializeOptions();
            if (!String.IsNullOrEmpty(txtUsrname.Text))
            {
                ListDatabases();
                gridView1.BestFitColumns();
                gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                InitializeGrid();
            }
         progressPanel1.Hide();}

      
        private void InitializeGrid()
        {
          
                
            gridView1.Columns[0].AppearanceCell.Font = new Font("Segoe UI", 16);
            gridView1.Columns[0].AppearanceHeader.Font = new Font("Segoe UI", 18);
            RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit(); gridControl1.RepositoryItems.Add(repoMemo);
            gridView1.Columns[0].ColumnEdit = repoMemo;
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.OptionsView.ColumnAutoWidth = true;
            gridView1.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            gridView1.Appearance.FooterPanel.Font = new Font("Segoe UI", 18);
            gridView1.Columns[0].BestFit();
            gridView1.Columns[0].Image = Resources.database_32x32;

        }

        private void ListDatabases()
        {
           
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};", cmbServer.Text,
                txtUsrname.Text, txtPass.Text);
            List<DatabaseNames> databaseNamesListBindingList = new List<DatabaseNames>();
            List<string> databaseNamesList = new List<string>();
            databaseNamesList = GetDatabaseList(connectionString);
            foreach (string db in databaseNamesList)
            {
                DatabaseNames name = new DatabaseNames();
                name.DatabaseName = db;
                databaseNamesListBindingList.Add(name);
            }

            DBNameBindingSource1.DataSource = databaseNamesListBindingList;
        }

        private class DatabaseNames
        {
            private String _databaseName;
            public String DatabaseName
            {
                get
                {
                    return _databaseName;
                }
                set
                {
                    _databaseName = value;
                }
            }
        }
        private void InitializeOptions()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cookies.client";
            if (File.Exists(path))
            {
                StreamReader file = new StreamReader(path);
                List<string> optList = new List<string>();
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    optList.Add(line);
                }

                if (optList.Count == 4)
                {
                    cmbServer.Text = optList[0].Split(':')[1].Replace(":", String.Empty);
                    txtUsrname.Text = optList[2].Split(':')[1].Replace(":", String.Empty);
                    txtPass.Text = optList[3].Split(':')[1].Replace(":", String.Empty);
                }
            }
            else
            {
                return;
            }


            progressPanel1.Hide();
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
         
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};", cmbServer.Text,  txtUsrname.Text, txtPass.Text);
            try
            {
                Cursor = Cursors.WaitCursor;
                SqlHelper helper = new SqlHelper(connectionString);
                if (helper.IsConnection())
                {
                    btnSave.Enabled = true;
                    MessageBox.Show("Connection is Successed", "Succeed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListDatabases();
                    gridView1.BestFitColumns();
                    gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[0].OptionsColumn.ReadOnly = true;
                    InitializeGrid();
                    SaveToFile();
                }
                else
                {

                    MessageBox.Show("Connection Failure", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                FrmMain.Log.Error("Test failed", ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            progressPanel1.Show();
            progressPanel1.Description = "Creating database for you...";
            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};", cmbServer.Text, txtUsrname.Text, txtPass.Text);
            try
            {
               CreateDatabase(connectionString);
                
                ListDatabases();
                InitializeGrid();
                FrmMain.Log.Info("Database Created Succesfully!");   
                
    
            }
            catch (Exception ex)
            {
                MessageBox.Show("Somethink went wrong :(", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FrmMain.Log.Error("DB CREATİON FAİLED!", ex);
            }
           
            progressPanel1.Hide();}

        private void CreateDatabase(string connectionString)
        {
            if (!String.IsNullOrEmpty(txtDatabase.Text))
            {
                List<string> databaseNamesList = new List<string>();
                databaseNamesList = GetDatabaseList(connectionString);
                if (databaseNamesList.Contains(txtDatabase.Text))
                {
                    MessageBox.Show(txtDatabase.Text + " name with a different database please!", "Excuse us!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    progressPanel1.Hide();
                    return;
                }
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "CREATE DATABASE " + txtDatabase.Text;
                    command.ExecuteNonQuery();
                }
               
                MessageBox.Show("Database created successfully!", "Success!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Need a valid database name!", "FAİLED!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
           
          
        }

        private void SaveToFile()
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\cookies.client";
                StreamWriter file2 = new StreamWriter(path);
                file2.WriteLine("Datasource:" + cmbServer.Text);
                file2.WriteLine("InitialCatalog:" + txtDatabase.Text);
                file2.WriteLine("Username:" + txtUsrname.Text);
                file2.WriteLine("Pass:" + txtPass.Text);
               

                file2.Close();
            }
            catch (Exception e)
            {
              FrmMain.Log.Error("Cannot save connection string",e);
            }
           
        }

        private void frmDBConnection_Load(object sender, EventArgs e)
        {

            cmbServer.Items.Add(Environment.MachineName);
            cmbServer.Items.Add(".");
            cmbServer.Items.Add("(local)");
            cmbServer.Items.Add(@".\SQLEXPRESS");
            cmbServer.Items.Add(@"(LocalDB)\MSSQLLocalDB");
            cmbServer.Items.Add(string.Format(@"{0}\SQLEXPRESS", Environment.MachineName));
            cmbServer.Items.Add(string.Format(@"{0}(LocalDB)\MSSQLLocalDB", Environment.MachineName));
            cmbServer.SelectedIndex = 0;
        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public List<string> GetDatabaseList(string conString)
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }
            
            return list;
        }

        private List<string> ExecuteQuery(string conString, string query)
        {
            try
            {
                List<string> list = new List<string>();
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                list.Add(dr[0].ToString());
                            }
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
               FrmMain.Log.Error("Cannot delete database;",ex);
                throw;
            }
        }


        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
               progressPanel1.Show();
                progressPanel1.Description = "Deleting now...";
                if (DBNameBindingSource1.Current != null)
                {
                    String rowToDelete = (DBNameBindingSource1.Current as DatabaseNames).DatabaseName;
                    if (MessageBox.Show(" are u sure to delete this database:"+rowToDelete+"?", "Uyarı!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {


                            string connectionString = string.Format("Data Source={0};User ID={1};Password={2};Initial Catalog=master; Integrated Security=True;", cmbServer.Text, txtUsrname.Text, txtPass.Text);
                            List<string> cevap = new List<string>();
                            cevap = ExecuteQuery(connectionString, "DROP DATABASE [" + rowToDelete+ "] ;");
                             MessageBox.Show("Server's answer: Success", "Succeed!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            ListDatabases();
                            gridView1.BestFitColumns();
                        } 
                    
                }
            }
            progressPanel1.Hide();
        }

      
    }
}
