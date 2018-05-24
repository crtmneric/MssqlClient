using System;
using System.Windows.Forms;
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

        private void tileDayToDo_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            FrmDbConnection dbCreate = new FrmDbConnection();
            dbCreate.ShowDialog();
        }

        private void tileTicket_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                FrmEditDB dbEdit = new FrmEditDB();
                dbEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                Log.Error("login credentials cannot be found",ex);
             }
          

        }

     }
}
