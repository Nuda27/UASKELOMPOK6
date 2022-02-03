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
    public partial class DataPengembalian : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;
        private SqlDataReader dr;

        int tambah, stokawal, stokakhir, counter;
        koneksi konn = new koneksi();
        public DataPengembalian()
        {
            InitializeComponent();
        }

        void tampildata()
        {
            SqlConnection conn = konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("select * from tb_kembali", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "tb_kembali");
                dataGrid.DataSource = ds;
                dataGrid.DataMember = "tb_kembali";
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
                SqlDataAdapter sqlInsert = new SqlDataAdapter("Exec SpCariKembali @judul", conn);
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
            //tbId.Text = "";
            dtpKembali.Text = "";
            cbJudul.Text = null;
            cbPetugas.Text = null;
            cbAnggota.Text = null;
            tbJumlah.Text = "";
        }

        void idOto()
        {
            long itung;
            string urut;
            SqlDataReader dr;
            SqlConnection conn = konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select id_pengembalian from tb_kembali where id_pengembalian in(select max(id_pengembalian) from tb_kembali) order by id_pengembalian desc", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                itung = Convert.ToInt64(dr[0].ToString().Substring(dr["id_pengembalian"].ToString().Length - 3, 3)) + 1;
                string idurut = "000" + itung;
                urut = "KB" + idurut.Substring(idurut.Length - 3, 3);
            }
            else
            {
                urut = "KB001";
            }
            dr.Close();
            tbId.Text = urut;
            conn.Close();
        }

        void petugas()
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select nama from tb_petugas", conn);
            conn.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbPetugas.Items.Add(dr[0].ToString());
            }
        }
        void anggota()
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select nama from tb_anggota", conn);
            conn.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbAnggota.Items.Add(dr[0].ToString());
            }
        }
        void buku()
        {
            SqlConnection conn = konn.GetConn();
            cmd = new SqlCommand("select judul from tb_buku", conn);
            conn.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbJudul.Items.Add(dr[0].ToString());
            }
        }

        private void DataPengembalian_Load(object sender, EventArgs e)
        {
            tampildata();
            petugas();
            anggota();
            buku();
            bersih();
            idOto();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            if (tbId.Text.Trim() == "" || dtpKembali.Text.Trim() == "" || cbJudul.Text.Trim() == "" || cbAnggota.Text.Trim() == "" || cbPetugas.Text.Trim() == "" || tbJumlah.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlInsert = new SqlCommand("Exec SpInsertKembali @id, @tglK, @judul, @namaA, @namaP, @jml", conn);
                    conn.Open();
                    sqlInsert.Parameters.AddWithValue("@id", tbId.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@tglK", dtpKembali.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@judul", cbJudul.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@namaA", cbAnggota.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@namaP", cbPetugas.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@jml", tbJumlah.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    tambah = Convert.ToInt32(tbJumlah.Text);
                    cmd = new SqlCommand("select judul,stok from tb_buku", conn);
                    //conn.Open
                    dr = cmd.ExecuteReader();
                    counter = 1;
                    while (dr.Read())
                    {
                        if (counter == 1 && dr[0].ToString() == cbJudul.Text)
                        {
                            stokawal = Convert.ToInt32(dr[1].ToString());
                        }
                        counter = +1;
                    }
                    stokakhir = stokawal + tambah;
                    conn.Close();
                    conn.Open();
                    SqlCommand sqlUpdateStokBuku = new SqlCommand("update tb_buku set stok='" + stokakhir + "' where judul='" + cbJudul.Text + "'", conn);

                    sqlUpdateStokBuku.ExecuteNonQuery();

                    MessageBox.Show("Data Tersimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    {
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_kembali Order By id_pengembalian Asc", conn);
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
            if (tbId.Text.Trim() == "" || dtpKembali.Text.Trim() == "" || cbJudul.Text.Trim() == "" || cbAnggota.Text.Trim() == "" || cbPetugas.Text.Trim() == "")
            {
                MessageBox.Show("Data Tidak Lengkap", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    SqlCommand sqlInsert = new SqlCommand("Exec SpUpdateKembali @id, @tglK, @judul, @namaA, @namaP, @jml", conn);
                    conn.Open();
                    sqlInsert.Parameters.AddWithValue("@id", tbId.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@tglK", dtpKembali.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@judul", cbJudul.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@namaA", cbAnggota.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@namaP", cbPetugas.Text.Trim());
                    sqlInsert.Parameters.AddWithValue("@jml", tbJumlah.Text.Trim());
                    sqlInsert.ExecuteNonQuery();
                    MessageBox.Show("Data Tersimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    {
                        SqlDataAdapter getData = new SqlDataAdapter("SELECT * FROM tb_kembali Order By id_pengembalian Asc", conn);
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
                tbId.Text = row.Cells["id_pengembalian"].Value.ToString();
                dtpKembali.Text = row.Cells["tgl_kembali"].Value.ToString();
                cbJudul.Text = row.Cells["judul"].Value.ToString();
                cbAnggota.Text = row.Cells["nama_anggota"].Value.ToString();
                cbPetugas.Text = row.Cells["nama_petugas"].Value.ToString();
                tbJumlah.Text = row.Cells["jumlah"].Value.ToString();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda Yakin Data Pengembalian Buku " + cbJudul.Text + " ?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = konn.GetConn();
                cmd = new SqlCommand(" EXEC SpDeleteKembali @id ", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@id", tbId.Text.Trim());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Hapus Data Berhasil", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tampildata();
                bersih();
                idOto();
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

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            ReportK rk = new ReportK();
            rk.Show();
        }
    }
}
