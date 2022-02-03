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
    public partial class DataAnggota : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        koneksi konn = new koneksi();
        public DataAnggota()
        {
            InitializeComponent();
        }
        void tampildata()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select * from tb_anggota", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tb_anggota");
                dataGrid.DataSource = ds;
                dataGrid.DataMember = "tb_anggota";
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
                SqlDataAdapter sqlInsert = new SqlDataAdapter("Exec SpCariAnggota @nama", conn);
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
            tbId.Text = "";
            tbNama.Text = "";
            tbNo.Text = "";
            cbJk.Text = null;
            tbAlamat.Text = "";
        }

        void idOto()
        {
            long itung;
            string urut;
            SqlDataReader dr;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("EXECUTE spIdAnggota", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                itung = Convert.ToInt64(dr[0].ToString().Substring(dr["id_anggota"].ToString().Length - 3, 3)) + 1;
                string idurut = "000" + itung;
                urut = "AG" + idurut.Substring(idurut.Length - 3, 3);
            }
            else
            {
                urut = "AG001";
            }
            dr.Close();
            tbId.Text = urut;
            conn.Close();
        }

        void jk()
        {
            cbJk.Items.Add("Laki - Laki");
            cbJk.Items.Add("Perempuan");
        }

        private void DataAnggota_Load(object sender, EventArgs e)
        {
            tampildata();
            jk();
            bersih();
            idOto();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbId.Text.Trim() == "" || tbNama.Text.Trim() == "" || tbNo.Text.Trim() == "" || cbJk.Text.Trim() == "" || tbAlamat.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlInsert = new SqlCommand("Exec SpInsertAnggota @id, @nama, @jenis, @no, @alamat", conn);
                    conn.Open();
                    sqlInsert.Parameters.AddWithValue("@id", tbId.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@nama", tbNama.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@jenis", cbJk.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@no", tbNo.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@alamat", tbAlamat.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    MessageBox.Show("Data Tersimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    {
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_anggota Order By id_anggota Asc", conn);
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

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbId.Text.Trim() == "" || tbNama.Text.Trim() == "" || tbNo.Text.Trim() == "" || cbJk.Text.Trim() == "" || tbAlamat.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlInsert = new SqlCommand("Exec SpUpdateAnggota @id, @nama, @jenis, @no, @alamat", conn);
                    conn.Open();
                    sqlInsert.Parameters.AddWithValue("@id", tbId.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@nama", tbNama.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@jenis", cbJk.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@no", tbNo.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@alamat", tbAlamat.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Dirubah", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    {
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_anggota Order By id_anggota Asc", conn);
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
                tbId.Text = row.Cells["id_anggota"].Value.ToString();
                tbNama.Text = row.Cells["nama"].Value.ToString();
                tbNo.Text = row.Cells["nohp"].Value.ToString();
                tbAlamat.Text = row.Cells["alamat"].Value.ToString();
                cbJk.Text = row.Cells["jenis_kelamin"].Value.ToString();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            caridata();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda Yakin Data Anggota " + tbNama.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = konn.GetConn();
                cmd = new SqlCommand("SpDeleteAnggota @id ", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", tbId.Text.Trim());
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
        }
    }
}
