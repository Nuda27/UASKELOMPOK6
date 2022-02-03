using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

namespace pos
{
    public partial class ReportP : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        //private SqlDataAdapter da;
        private SqlDataReader dr;

        koneksi konn = new koneksi();
        public ReportP()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            ReportDocument rd = new ReportDocument();
            SqlDataAdapter da = new SqlDataAdapter("select * from tb_pinjam where judul like '%" + tbSearch.Text + "%' or tgl_pinjam like '%" + tbSearch.Text + "%' ", conn);
            DataSet dst = new DataSet();
            da.Fill(dst, "tb_pinjam");
            rd.Load(@"D:\Kuliah\Semester 5\Visual\UAS\Perpustakaan\ReportPinjam.rpt");
            rd.SetDataSource(dst);
            crystalReportViewer1.ReportSource = rd;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            ReportDocument rd = new ReportDocument();
            SqlDataAdapter da = new SqlDataAdapter("select * from tb_pinjam", conn);
            DataSet dst = new DataSet();
            da.Fill(dst, "tb_pinjam");
            rd.Load(@"D:\Kuliah\Semester 5\Visual\UAS\Perpustakaan\ReportPinjam.rpt");
            rd.SetDataSource(dst);
            crystalReportViewer1.ReportSource = rd;
        }
    }
}
