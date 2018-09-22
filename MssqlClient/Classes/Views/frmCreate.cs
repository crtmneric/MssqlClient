using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ComponentModel.DataAnnotations;
using System.IO;
using DevExpress.XtraLayout.Helpers;
using DevExpress.XtraLayout;

namespace MssqlClient.Classes.Views
{
    public partial class frmCreate : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmCreate(bool table)
        {
            InitializeComponent();
            if (table)
            {
                lblName.Text = "Creating Table...";
                listBoxControl1.Enabled = false;
            }
            else
            {
                lblName.Text = "Creating Column...";
            }
        }
       
    }
}
