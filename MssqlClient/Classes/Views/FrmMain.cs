using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using log4net;
using log4net.Appender;
using MssqlClient.Classes.Beans;

namespace MssqlClient.Classes.Views{
    public partial class FrmMain : Form
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(RollingFileAppender));
        public FrmMain()
        {
            InitializeComponent();
            Logger.Setup();

        }

        private void tileDayToDo_ItemClick(object sender, TileItemEventArgs e)
        {
            FrmDbConnection dbCreate = new FrmDbConnection();
            dbCreate.ShowDialog();
        }

        private void tileTicket_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                FrmEditDb dbEdit = new FrmEditDb();
                dbEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.Error("login credentials cannot be found",ex);
             }
          

        }

        private void tileClose_ItemClick(object sender, TileItemEventArgs e)
        {
            Application.Exit();
        }

     }
}
