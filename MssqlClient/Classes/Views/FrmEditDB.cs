using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using MssqlClient.Properties;

namespace MssqlClient.Classes.Views
{
    public partial class FrmEditDB : Form
    {
        private string connectionString;
        private string tableConnectionString;
        private string _globalDatabaseName;
        private DataTable dataTable ;
        private bool act1;
        private bool act2;

        private void dataCount()
        {
            lblCount.Text = "Total Data Count:" +gridView1.DataRowCount;
        }
        public FrmEditDB()
        {
            InitializeComponent();
            btnBack.Hide();
          if (InitializeOptions())
            {
                ListDatabases();
                gridView1.BestFitColumns();
                dataCount();
                InitializeGrid();
            }
            else
            {
                MessageBox.Show("Save login credantials from create database!", "FAİLED!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                this.Close();

            }
           
        }
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
            gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            gridView1.Columns[0].OptionsColumn.ReadOnly = true;

        }

        private void InitializeTableValue()
        {
            foreach (GridColumn column in gridView1.Columns)
            {
                column.AppearanceCell.Font = new Font("Segoe UI", 16);
                column.AppearanceHeader.Font = new Font("Segoe UI", 18);
                RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit(); gridControl1.RepositoryItems.Add(repoMemo);
                  column.ColumnEdit = repoMemo;
                gridView1.OptionsView.RowAutoHeight = true;
                gridView1.OptionsView.ColumnAutoWidth = true;
                gridView1.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
                gridView1.Appearance.FooterPanel.Font = new Font("Segoe UI", 18);
                column.BestFit();
                column.Image = Resources.database_32x32;
               column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
                dataCount();
            }
        }
      
        private bool InitializeOptions()
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
                    connectionString = string.Format("Data Source={0};User ID={1};Password={2};", optList[0].Split(':')[1].Replace(":", String.Empty),
                        optList[2].Split(':')[1].Replace(":", String.Empty), optList[3].Split(':')[1].Replace(":", String.Empty));
                    return true;
                }
                else
                {
                    return false;
                }

               
            }

            else
            {
                return false;

            }





        }
        private void ListDatabases()
        {
            gridView1.Columns.Clear();

            DBNameBindingSource1.DataSource = null;
         
            List<DatabaseNames> databaseNamesListBindingList = new List<DatabaseNames>();
            List<string> databaseNamesList = new List<string>();
            databaseNamesList = GetDatabaseList(connectionString);
            foreach (string db in databaseNamesList)
            {
                DatabaseNames name = new DatabaseNames();
                name.Database_Name = db;
                databaseNamesListBindingList.Add(name);

            }         
            DBNameBindingSource1.DataSource = databaseNamesListBindingList;
            gridControl1.RefreshDataSource();
         }

        private void GetAllValues(String tableNamecik,String connString)
        {
            gridView1.Columns.Clear();
            DBNameBindingSource1.DataSource = null;        
           
            ListTableValues(tableNamecik,connString);      
            DBNameBindingSource1.DataSource = dataTable;
            gridControl1.RefreshDataSource();
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

        private class DatabaseNames
        {
            private String _databaseName;
            public String Database_Name
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

        private class TableNames
        {
            private String tableName;
            public String Table_Name
            {
                get
                {
                    return tableName;
                }
                set
                {
                    tableName = value;
                }
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            SelectOption();
        }

        private void SelectOption()
        {
            if (DBNameBindingSource1.Current != null && !act1)
            {
                ListTablesQuickly();
                lblDB.Show();
                lblDB.Text ="Database Name:"+ _globalDatabaseName;
                dataCount();
                
            }
            else if (DBNameBindingSource1.Current != null && act1)
            {

                lblTable.Text="Table Name:"+((TableNames)DBNameBindingSource1.Current).Table_Name;
                lblTable.Show();
                
                act2 = true;
                GetAllValues(((TableNames)DBNameBindingSource1.Current).Table_Name, string.Format("Initial Catalog={0};{1}", _globalDatabaseName, connectionString));
                lblCount.Text = "Total Data Count:" + dataTable.Rows.Count;
                InitializeTableValue();
                dataCount();}
        }

        private void ListTablesQuickly()
        {
            this.Cursor = Cursors.WaitCursor;
            if (!act2)
            {
                _globalDatabaseName = ((DatabaseNames)DBNameBindingSource1.Current).Database_Name;

            }
         
            DBNameBindingSource1.DataSource = null;
            gridView1.Columns.Clear();
            DBNameBindingSource1.DataSource = ListTables("Initial Catalog=" + _globalDatabaseName + ";" + connectionString);
            InitializeGrid();
            label1.Hide();
            act1 = true;
            btnBack.Show();
            this.Cursor = Cursors.Default;
        }

        private IList<TableNames> ListTables(String conString)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();
               
                  
                    List<TableNames> tableNames = new List<TableNames>();
                    DataTable dt =con.GetSchema("Tables");
                    foreach (DataRow row in dt.Rows)
                    {
                        TableNames name = new TableNames() { Table_Name = (string)row[2] };
                       tableNames.Add(name);
                    }

                return tableNames;

            }
            
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (DBNameBindingSource1.Current != null&&!act1)
            {
                label1.Text = ((DatabaseNames) DBNameBindingSource1.Current).Database_Name + " has been selected!";
            }
            else if(DBNameBindingSource1.Current!=null&&!act2)
            {
                label1.Show();
                label1.Text = ((TableNames) DBNameBindingSource1.Current).Table_Name+ " has been selected!";
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (act1)
            {
                btnBack.Hide();
                ListDatabases();
                act1 = false;
                InitializeGrid();
                dataCount();
            }

            if (act2)
            {
                
                ListTablesQuickly();
                lblTable.Hide();
                act2 = false;
                dataCount();

            }
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            SelectOption();

        }

        private void ListTableValues(String tableName, String conString)
        {
            SqlDataAdapter da;
            dataTable = new DataTable();using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * from "+tableName, con))
                {
                     da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                    da.Dispose();

                }
            }          
        }
           
    }
}
