using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using MssqlClient.Classes.Views;
using MssqlClient.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MssqlClient.Classes.Beans
{
    public class Initialize
    {
        private FrmEditDb _frmEditDb;

        public Initialize(FrmEditDb frmEditDb)
        {
            _frmEditDb = frmEditDb;
        }

        public void InitializeGrid()
        {
            _frmEditDb.Gridview2.Columns[0].AppearanceCell.Font = new Font("Segoe UI", 16);
            _frmEditDb.Gridview2.Columns[0].AppearanceHeader.Font = new Font("Segoe UI", 18);
            RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();
            _frmEditDb.Gridcontrol2.RepositoryItems.Add(repoMemo);
            _frmEditDb.Gridview2.Columns[0].ColumnEdit = repoMemo;
            _frmEditDb.Gridview2.OptionsView.RowAutoHeight = true;
            _frmEditDb.Gridview2.OptionsView.ColumnAutoWidth = true;
            _frmEditDb.Gridview2.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
            _frmEditDb.Gridview2.Appearance.FooterPanel.Font = new Font("Segoe UI", 18);
            _frmEditDb.Gridview2.Columns[0].BestFit();
            _frmEditDb.Gridview2.Columns[0].Image = Resources.database_32x32;
            _frmEditDb.Gridview2.Columns[0].OptionsColumn.AllowEdit = false;
            _frmEditDb.Gridview2.Columns[0].OptionsColumn.ReadOnly = true;
            _frmEditDb.Gridview2.BestFitColumns();
            _frmEditDb.Gridcontrol2.UseEmbeddedNavigator = true;

        }

        public void InitializeTableValue()
        {
            foreach (GridColumn column in _frmEditDb.Gridview2.Columns)
            {
                column.AppearanceCell.Font = new Font("Segoe UI", 16);
                column.AppearanceHeader.Font = new Font("Segoe UI", 18);
                RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();
                _frmEditDb.Gridcontrol2.RepositoryItems.Add(repoMemo);
                column.ColumnEdit = repoMemo;
                _frmEditDb.Gridview2.OptionsView.RowAutoHeight = true;
                _frmEditDb.Gridview2.OptionsView.ColumnAutoWidth = true;
                _frmEditDb.Gridview2.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;
                _frmEditDb.Gridview2.Appearance.FooterPanel.Font = new Font("Segoe UI", 18);
                column.BestFit();
                column.Image = Resources.database_32x32;
                column.OptionsColumn.AllowEdit = false;
                column.OptionsColumn.ReadOnly = true;
                _frmEditDb.DataCount();
            }

            _frmEditDb.Gridview2.BestFitColumns();
        }

        public bool InitializeOptions()
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
                    _frmEditDb.ConnectionString = string.Format("Data Source={0};User ID={1};Password={2};", optList[0].Split(':')[1].Replace(":", String.Empty),
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
    }
}