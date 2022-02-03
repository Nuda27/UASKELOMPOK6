using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pos
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            customsub();
        }
        private void customsub()
        {
            panelSubMenu.Visible = false;
        }
        private void hidesub()
        {
            if (panelSubMenu.Visible == true)
                panelSubMenu.Visible = false;
        }

        private void showsub(Panel sub)
        {
            if (sub.Visible == false)
            {
                hidesub();
                sub.Visible = true;
            }
            else
                sub.Visible = false;
        }

        private void btnDataMaster_Click(object sender, EventArgs e)
        {
          
        }
        private Form activeForm = null;
        private void tampilan(Form tampil)
        {
            if (activeForm != null)
            activeForm.Close();
            activeForm = tampil;
            tampil.TopLevel = false;
            tampil.FormBorderStyle = FormBorderStyle.None;
            tampil.Dock = DockStyle.Fill;
            panelTampil.Controls.Add(tampil);
            panelTampil.Tag = tampil;
            tampil.BringToFront();
            tampil.Show();
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            lblUser.Text = FormLogin.user;
            //lblLu.Text = ;
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDataMaster_Click_1(object sender, EventArgs e)
        {
            showsub(panelSubMenu);
        }

        private void btnUser_Click_1(object sender, EventArgs e)
        {
            tampilan(new DataUser());
            hidesub();
        }

        private void btnAb_Click(object sender, EventArgs e)
        {
            tampilan(new DataAnggota());
            hidesub();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tampilan(new DataPeminjaman());
            hidesub();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tampilan(new DataPengembalian());
            hidesub();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tampilan(new DataBuku());
            hidesub();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLogin fl = new FormLogin();
            fl.Show();
            this.Close();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
