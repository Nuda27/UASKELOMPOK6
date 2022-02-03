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
    public partial class DataUser : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        koneksi konn = new koneksi();
        public DataUser()
        {
            InitializeComponent();
        }

        void tampildata()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select * from tb_petugas", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tb_petugas");
                dataGrid.DataSource = ds;
                dataGrid.DataMember = "tb_petugas";
                dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        void caridata()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                    conn.Open();
                    SqlDataAdapter sqlInsert = new SqlDataAdapter("Exec SpCariPetugas @nama", conn);
                    sqlInsert.SelectCommand.Parameters.AddWithValue("@nama", tbSearch.Text.Trim());
                    sqlInsert.SelectCommand.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    sqlInsert.Fill(dt);
                    dataGrid.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void bersih()
        {
            //tbKd.Text = "";
            tbNama.Text = "";
            tbNo.Text = "";
            tbPass.Text = "";
            cbJabatan.Text = null;
        }

        void idOto()
        {
            long itung;
            string urut;
            SqlDataReader dr;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("Exec SpIdPetugas", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                itung = Convert.ToInt64(dr[0].ToString().Substring(dr["id_petugas"].ToString().Length - 3, 3)) +1;
                string idurut = "000" + itung;
                urut = "USR" + idurut.Substring(idurut.Length - 3, 3);
            }
            else
            {
                urut = "USR001";
            }
            dr.Close();
            //tbKd.Enabled = false;
            tbKd.Text = urut;
            conn.Close();

        }
        void combo()
        {
            cbJabatan.Items.Add("Administrator");
            cbJabatan.Items.Add("User");
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void DataUser_Load(object sender, EventArgs e)
        {
            tampildata();
            combo();
            bersih();
            idOto();
            //tbKd.Enabled = false;
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbKd.Text.Trim() == "" || tbNama.Text.Trim() == "" || tbNo.Text.Trim() == "" || tbPass.Text.Trim() == "" || cbJabatan.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                        SqlCommand sqlInsert = new SqlCommand("Exec SpInsertPetugas @id, @nama, @jabatan, @nohp, @pass", conn);
                        conn.Open();
                        sqlInsert.Parameters.AddWithValue("@id", tbKd.Text.Trim());
                        sqlInsert.Parameters.AddWithValue("@nama", tbNama.Text.Trim());
                        sqlInsert.Parameters.AddWithValue("@jabatan", cbJabatan.Text.Trim());
                        sqlInsert.Parameters.AddWithValue("@nohp", tbNo.Text.Trim());
                        sqlInsert.Parameters.AddWithValue("@pass", tbPass.Text.Trim());
                        sqlInsert.ExecuteNonQuery();
                        MessageBox.Show("Data Tersimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        {
                            SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_petugas Order By id_petugas Asc", conn);
                            getData.SelectCommand.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            getData.Fill(dt);
                            dataGrid.DataSource = dt;
                        }
                    tampildata();
                    bersih();
                    idOto();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGrid.Rows[e.RowIndex];
                tbKd.Text = row.Cells["id_petugas"].Value.ToString();
                tbNama.Text = row.Cells["nama"].Value.ToString();
                tbNo.Text = row.Cells["nohp"].Value.ToString();
                tbPass.Text = row.Cells["password"].Value.ToString();
                cbJabatan.Text = row.Cells["jabatan"].Value.ToString();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbKd.Text.Trim() == "" || tbNama.Text.Trim() == "" || tbNo.Text.Trim() == "" || tbPass.Text.Trim() == "" || cbJabatan.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlUpdate = new SqlCommand("Exec SpUpdatePetugas @id, @nama, @jabatan, @nohp, @pass", conn);
                    conn.Open();
                    sqlUpdate.Parameters.AddWithValue("@id", tbKd.Text.Trim());
                    sqlUpdate.Parameters.AddWithValue("@nama", tbNama.Text.Trim());
                    sqlUpdate.Parameters.AddWithValue("@jabatan", cbJabatan.Text.Trim());
                    sqlUpdate.Parameters.AddWithValue("@nohp", tbNo.Text.Trim());
                    sqlUpdate.Parameters.AddWithValue("@pass", tbPass.Text.Trim());
                    sqlUpdate.ExecuteNonQuery();
                    MessageBox.Show("Ubah Data Berhasil", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tampildata();
                    bersih();
                    idOto();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda Yakin Data Petugas "+tbNama.Text+ " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = konn.GetConn();
                cmd = new SqlCommand("SpDeletePetugas @id ", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", tbKd.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Hapus Data Berhasil", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampildata();
                bersih();
                idOto();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bersih();
            idOto();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            caridata();
        }

        private void tbId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
