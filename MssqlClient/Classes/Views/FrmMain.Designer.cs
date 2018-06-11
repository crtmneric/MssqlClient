using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace MssqlClient.Classes.Views
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement3 = new DevExpress.XtraEditors.TileItemElement();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileDayToDo = new DevExpress.XtraEditors.TileItem();
            this.tileTicket = new DevExpress.XtraEditors.TileItem();
            this.tileClose = new DevExpress.XtraEditors.TileItem();
            this.pctBack = new System.Windows.Forms.PictureBox();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pctBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AllowDragTilesBetweenGroups = false;
            this.tileControl1.BackColor = System.Drawing.Color.Transparent;
            this.tileControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.DragSize = new System.Drawing.Size(0, 0);
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.ItemSize = 250;
            this.tileControl1.Location = new System.Drawing.Point(0, 197);
            this.tileControl1.MaxId = 20;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(1920, 883);
            this.tileControl1.TabIndex = 1;
            this.tileControl1.Text = "tileControl1";
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileDayToDo);
            this.tileGroup2.Items.Add(this.tileTicket);
            this.tileGroup2.Items.Add(this.tileClose);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileDayToDo
            // 
            this.tileDayToDo.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkCyan;
            this.tileDayToDo.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.SteelBlue;
            this.tileDayToDo.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.tileDayToDo.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tileDayToDo.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileDayToDo.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileDayToDo.AppearanceItem.Normal.Options.UseFont = true;
            this.tileDayToDo.AppearanceItem.Pressed.BackColor = System.Drawing.Color.Turquoise;
            this.tileDayToDo.AppearanceItem.Pressed.BackColor2 = System.Drawing.Color.SteelBlue;
            this.tileDayToDo.AppearanceItem.Pressed.Options.UseBackColor = true;
            this.tileDayToDo.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Always;
            tileItemElement1.Image = global::MssqlClient.Properties.Resources.add;
            tileItemElement1.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement1.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement1.ImageToTextIndent = 60;
            tileItemElement1.Text = "CREATE / DELETE DATABASE";
            this.tileDayToDo.Elements.Add(tileItemElement1);
            this.tileDayToDo.Id = 10;
            this.tileDayToDo.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileDayToDo.Name = "tileDayToDo";
            this.tileDayToDo.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileDayToDo_ItemClick);
            // 
            // tileTicket
            // 
            this.tileTicket.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkCyan;
            this.tileTicket.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.DeepSkyBlue;
            this.tileTicket.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.tileTicket.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tileTicket.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileTicket.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileTicket.AppearanceItem.Normal.Options.UseFont = true;
            this.tileTicket.AppearanceItem.Pressed.BackColor = System.Drawing.Color.Turquoise;
            this.tileTicket.AppearanceItem.Pressed.BackColor2 = System.Drawing.Color.SteelBlue;
            this.tileTicket.AppearanceItem.Pressed.Options.UseBackColor = true;
            this.tileTicket.BorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Always;
            tileItemElement2.Image = global::MssqlClient.Properties.Resources.edit;
            tileItemElement2.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement2.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement2.ImageToTextIndent = 60;
            tileItemElement2.Text = "EDIT DATABASE";
            this.tileTicket.Elements.Add(tileItemElement2);
            this.tileTicket.Id = 14;
            this.tileTicket.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileTicket.Name = "tileTicket";
            this.tileTicket.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileTicket_ItemClick);
            // 
            // tileClose
            // 
            this.tileClose.AppearanceItem.Normal.BackColor = System.Drawing.Color.DarkCyan;
            this.tileClose.AppearanceItem.Normal.BackColor2 = System.Drawing.Color.Firebrick;
            this.tileClose.AppearanceItem.Normal.BorderColor = System.Drawing.Color.Black;
            this.tileClose.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tileClose.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileClose.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileClose.AppearanceItem.Normal.Options.UseFont = true;
            this.tileClose.AppearanceItem.Pressed.BackColor = System.Drawing.Color.Turquoise;
            this.tileClose.AppearanceItem.Pressed.BackColor2 = System.Drawing.Color.Firebrick;
            this.tileClose.AppearanceItem.Pressed.BorderColor = System.Drawing.Color.Black;
            this.tileClose.AppearanceItem.Pressed.Options.UseBackColor = true;
            this.tileClose.AppearanceItem.Pressed.Options.UseBorderColor = true;
            tileItemElement3.Image = global::MssqlClient.Properties.Resources.rsz_close_icon_dark_256;
            tileItemElement3.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileItemElement3.ImageToTextAlignment = DevExpress.XtraEditors.TileControlImageToTextAlignment.Left;
            tileItemElement3.Text = "         CLOSE";
            this.tileClose.Elements.Add(tileItemElement3);
            this.tileClose.Id = 19;
            this.tileClose.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileClose.Name = "tileClose";
            this.tileClose.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileClose_ItemClick);
            // 
            // pctBack
            // 
            this.pctBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pctBack.Location = new System.Drawing.Point(0, 197);
            this.pctBack.Name = "pctBack";
            this.pctBack.Size = new System.Drawing.Size(1920, 883);
            this.pctBack.TabIndex = 0;
            this.pctBack.TabStop = false;
            // 
            // pctLogo
            // 
            this.pctLogo.BackColor = System.Drawing.Color.Transparent;
            this.pctLogo.BackgroundImage = global::MssqlClient.Properties.Resources.database_logo_icon;
            this.pctLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pctLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pctLogo.Location = new System.Drawing.Point(0, 0);
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.Size = new System.Drawing.Size(1920, 197);
            this.pctLogo.TabIndex = 3;
            this.pctLogo.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MssqlClient.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.tileControl1);
            this.Controls.Add(this.pctBack);
            this.Controls.Add(this.pctLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MssqlClient-Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pctBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pctBack;
        private TileControl tileControl1;
        private TileGroup tileGroup2;
        private TileItem tileDayToDo;
        private TileItem tileTicket;
        private PictureBox pctLogo;
        private TileItem tileClose;
    }
}

