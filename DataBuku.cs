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
    public partial class DataBuku : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        koneksi konn = new koneksi();
        public DataBuku()
        {
            InitializeComponent();
        }

        void tampildata()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select * from tb_buku", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tb_buku");
                dataGrid.DataSource = ds;
                dataGrid.DataMember = "tb_buku";
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
                SqlDataAdapter sqlInsert = new SqlDataAdapter("Exec SpCariBuku @judul", conn);
                sqlInsert.SelectCommand.Parameters.AddWithValue("@judul", tbSearch.Text.Trim());
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
            tbJudul.Text = "";
            tbPenulis.Text = "";
            tbPenerbit.Text = "";
            tbTahun.Text = "";
            tbStok.Text = "";
        }

        void idOto()
        {
            long itung;
            string urut;
            SqlDataReader dr;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select id_buku from tb_buku where id_buku in(select max(id_buku) from tb_buku) order by id_buku desc", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                itung = Convert.ToInt64(dr[0].ToString().Substring(dr["id_buku"].ToString().Length - 3, 3)) + 1;
                string idurut = "000" + itung;
                urut = "BK" + idurut.Substring(idurut.Length - 3, 3);
            }
            else
            {
                urut = "BK001";
            }
            dr.Close();
            tbId.Text = urut;
            conn.Close();
        }

        private void DataBuku_Load(object sender, EventArgs e)
        {
            tampildata();
            bersih();
            idOto();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbId.Text.Trim() == "" || tbJudul.Text.Trim() == "" || tbPenulis.Text.Trim() == "" || tbPenerbit.Text.Trim() == "" || tbTahun.Text.Trim() == "" || tbStok.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlInsert = new SqlCommand("Exec SpInsertBuku @id, @judul, @penulis, @penerbit, @thn, @stok", conn);
                    conn.Open();
                    sqlInsert.Parameters.AddWithValue("@id", tbId.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@judul", tbJudul.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@penulis", tbPenulis.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@penerbit", tbPenerbit.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@thn", tbTahun.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@stok", tbStok.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    MessageBox.Show("Data Tersimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    {
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_buku Order By id_buku Asc", conn);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbId.Text.Trim() == "" || tbJudul.Text.Trim() == "" || tbPenulis.Text.Trim() == "" || tbPenerbit.Text.Trim() == "" || tbTahun.Text.Trim() == "" || tbStok.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlInsert = new SqlCommand("Exec SpUpdateBuku @id, @judul, @penulis, @penerbit, @thn, @stok", conn);
                    conn.Open();
                    sqlInsert.Parameters.AddWithValue("@id", tbId.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@judul", tbJudul.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@penulis", tbPenulis.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@penerbit", tbPenerbit.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@thn", tbTahun.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@stok", tbStok.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    MessageBox.Show("Data Berhasil Diubah", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    {
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_buku Order By id_buku Asc", conn);
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

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Apakah Anda Yakin Data Buku " + tbJudul.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = konn.GetConn();
                cmd = new SqlCommand(" EXEC SpDeleteBuku @id ", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", tbId.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Hapus Data Berhasil", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampildata();
                bersih();
                idOto();
            }
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGrid.Rows[e.RowIndex];
                tbId.Text = row.Cells["id_buku"].Value.ToString();
                tbJudul.Text = row.Cells["judul"].Value.ToString();
                tbPenulis.Text = row.Cells["penulis"].Value.ToString();
                tbPenerbit.Text = row.Cells["penerbit"].Value.ToString();
                tbTahun.Text = row.Cells["tahun"].Value.ToString();
                tbStok.Text = row.Cells["stok"].Value.ToString();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            bersih();
        }
    }
}
