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
using CrystalDecisions.CrystalReports.Engine;

namespace pos
{
    public partial class ReportK : Form
    {
        private SqlCommand cmd;
        private DataSet ds;
        //private SqlDataAdapter da;
        private SqlDataReader dr;

        koneksi konn = new koneksi();
        public ReportK()
        {
            InitializeComponent();
        }

        private void gunaButton1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            ReportDocument rd = new ReportDocument();
            SqlDataAdapter da = new SqlDataAdapter("select * from tb_kembali where judul like '%" + tbSearch.Text + "%' or tgl_kembali like '%" + tbSearch.Text + "%' ", conn);
            DataSet dst = new DataSet();
            da.Fill(dst, "tb_kembali");
            rd.Load(@"D:\Kuliah\Semester 5\Visual\UAS\Perpustakaan\ReportKembali.rpt");
            rd.SetDataSource(dst);
            crystalReportViewer1.ReportSource = rd;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            SqlConnection conn = konn.GetConn();
            conn.Open();
            ReportDocument rd = new ReportDocument();
            SqlDataAdapter da = new SqlDataAdapter("select * from tb_kembali", conn);
            DataSet dst = new DataSet();
            da.Fill(dst, "tb_kembali");
            rd.Load(@"D:\Kuliah\Semester 5\Visual\UAS\Perpustakaan\ReportKembali.rpt");
            rd.SetDataSource(dst);
            crystalReportViewer1.ReportSource = rd;
        }
    }
}
