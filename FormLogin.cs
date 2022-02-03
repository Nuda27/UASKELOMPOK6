using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace pos
{
    public partial class FormLogin : Form
    {
        public static string user;


        koneksi konn = new koneksi();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            user = tbUser.Text;
            try
            {
                if (tbUser.Text.Length == 0 || tbPass.Text.Length == 0)
                {
                    MessageBox.Show("Silahkan masukkan nama pengguna dan kata sandi anda.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    conn.Open();
                    SqlCommand login = new SqlCommand("SELECT nama, password from tb_petugas where nama ='" + tbUser.Text + "' and password = '" + tbPass.Text + "'", conn);
                    SqlDataReader dr;
                    dr = login.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Login Sukses !!");
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Nama pengguna atau kata sandi anda salah.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
    }
}
