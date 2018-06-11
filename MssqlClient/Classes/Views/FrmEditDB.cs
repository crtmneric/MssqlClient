using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.Data.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using MssqlClient.Classes.Beans;

namespace MssqlClient.Classes.Views
{
    public partial class FrmEditDb : Form
    {
        public string ConnectionString;
        private string _globalDatabaseName;
        private DataTable _dataTable ;
        private bool _act1;
        private bool _act2;
        private bool _deleteTable;
        private bool _deleteValue;
        private readonly Initialize _initialize;
        public GridView Gridview2;
        public GridControl Gridcontrol2;
        private String _idValue;
        public Initialize Initialize
        {
            get { return _initialize; }
        }

        public void DataCount()
        {
            lblCount.Text = "Total Data Count:" +gridView1.DataRowCount;
        }
        public FrmEditDb()
        {
            InitializeComponent();
            Gridview2 = gridView1;
            Gridcontrol2 = gridControl1;
            btnBack.Hide();
            _initialize = new Initialize(this);
            if (Initialize.InitializeOptions())
            {
                ListDatabases();
                gridView1.BestFitColumns();
                DataCount();
                Initialize.InitializeGrid();
            }
            else
            {
                MessageBox.Show("Save login credantials from create database!", "FAİLED!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Close();

            }
           
        }

        private void ListDatabases()
        {
            gridView1.Columns.Clear();

            DBNameBindingSource1.DataSource = null;
         
            List<DatabaseNames> databaseNamesListBindingList = new List<DatabaseNames>();
            List<string> databaseNamesList = new List<string>();
            databaseNamesList = GetDatabaseList(ConnectionString);
            foreach (string db in databaseNamesList)
            {
                DatabaseNames name = new DatabaseNames();
                name.DatabaseName = db;
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
            DBNameBindingSource1.DataSource = _dataTable;
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

        private class TableNames
        {
            private String _tableName;
            public String TableName
            {
                get
                {
                    return _tableName;
                }
                set
                {
                    _tableName = value;
                }
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            SelectOption();
        }

        private void SelectOption()
        {
            if (DBNameBindingSource1.Current != null && !_act1)
            {
                ListTablesQuickly();
                lblDB.Show();
                lblDB.Text ="Database Name:"+ _globalDatabaseName;
                DataCount();
                
            }
            else if (DBNameBindingSource1.Current != null && _act1 && !_act2)
            {

                lblTable.Text="Table Name:"+((TableNames)DBNameBindingSource1.Current).TableName;
                lblTable.Show();
                _act2 = true;
                GetAllValues(((TableNames)DBNameBindingSource1.Current).TableName, string.Format("Initial Catalog={0};{1}", _globalDatabaseName, ConnectionString));
                lblCount.Text = "Total Data Count:" + _dataTable.Rows.Count;
                Initialize.InitializeTableValue();
                DataCount();

            }
            else if (DBNameBindingSource1.Current != null && _act1 && _deleteValue)
            {

                GetAllValues(lblTable.Text.Split(':')[1], string.Format("Initial Catalog={0};{1}", _globalDatabaseName, ConnectionString));
                lblCount.Text = "Total Data Count:" + _dataTable.Rows.Count;
                Initialize.InitializeTableValue();
                DataCount();
            }
        }

        private void ListTablesQuickly()
        {
            Cursor = Cursors.WaitCursor;
            if (!_act2&&!_deleteTable)
            {
                _globalDatabaseName = ((DatabaseNames)DBNameBindingSource1.Current).DatabaseName;

            }
         
            DBNameBindingSource1.DataSource = null;
            gridView1.Columns.Clear();
            DBNameBindingSource1.DataSource = ListTables("Initial Catalog=" + _globalDatabaseName + ";" + ConnectionString);
            Initialize.InitializeGrid();
            label1.Hide();
            _act1 = true;
            btnBack.Show();
            Cursor = Cursors.Default;
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
                        TableNames name = new TableNames { TableName = (string)row[2] };
                       tableNames.Add(name);
                    }

                return tableNames;

            }
            
        }
        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            if (DBNameBindingSource1.Current != null&&!_act1)
            {
                label1.Text = ((DatabaseNames) DBNameBindingSource1.Current).DatabaseName + " has been selected!";
            }
            else if(DBNameBindingSource1.Current!=null&&!_act2)
            {
                label1.Show();
                label1.Text = ((TableNames) DBNameBindingSource1.Current).TableName+ " has been selected!";
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_act1)
            {
                btnBack.Hide();
                ListDatabases();
                _act1 = false;
                Initialize.InitializeGrid();
                DataCount();
            }

            if (_act2)
            {
                
                ListTablesQuickly();
                lblTable.Hide();
                _act2 = false;
                DataCount();

            }
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            SelectOption();

        }

        private void ListTableValues(String tableName, String conString)
        {
            SqlDataAdapter da;
            _dataTable = new DataTable();using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * from "+tableName, con))
                {
                     da = new SqlDataAdapter(cmd);
                    da.Fill(_dataTable);
                    da.Dispose();

                }
            }          
        }

        public List<string> ExecuteSqlCommand(string sqlCommand,string conString)
        {
            List<string> list = new List<string>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(sqlCommand, con))
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

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DBNameBindingSource1.Current != null&&_act1&&!_act2)
                {
                if (MessageBox.Show(" are u sure to delete this Table: " + ((TableNames)DBNameBindingSource1.Current).TableName  +"?", "Warning!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                    
                        List<string> cevap = new List<string>();
                        cevap = ExecuteSqlCommand("DROP TABLE " + ((TableNames)DBNameBindingSource1.Current).TableName, "Initial Catalog=" + _globalDatabaseName + ";" + ConnectionString);
                        if (cevap.Count == 0)
                        {
                            MessageBox.Show("Server's answer: Success", "Succeed!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            _deleteTable = true;
                            ListTablesQuickly();
                            DataCount();
                            _deleteTable = false;
                        }
                        else
                        {
                            MessageBox.Show("Server's answer: Failed!", "Failed!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                                           
                    }

                }
                else if (DBNameBindingSource1.Current != null && _act2)
                {
                  
                    DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
                    int counter = 0;
                    String value = row[0].ToString();
                    
                    foreach (DataColumn column in row.Table.Columns)
                    {
                        if (counter ==0)
                        {
                            _idValue = column.ColumnName;
                        }
                        counter++;
                    }

                    if (MessageBox.Show(" are u sure to delete this " + _idValue+":"+value + " valued row?", "Warning!",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes){
                                List<string> cevap = ExecuteSqlCommand("DELETE FROM " + lblTable.Text.Split(':')[1] + " WHERE " + _idValue + "=" + value, "Initial Catalog=" + _globalDatabaseName + ";" + ConnectionString);
                        if (cevap.Count == 0)
                        {
                            MessageBox.Show("Server's answer: Success", "Succeed!", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            _deleteValue = true;
                            SelectOption();_deleteValue = false;
                        }
                        else
                        {

                            MessageBox.Show("Server's answer: Failed!", "Failed!", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }

                    }
                }
            }
          
        }
           
    }
}
