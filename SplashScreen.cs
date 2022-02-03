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
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (gunaCircleProgressBar2.Value == 100)
            {
                timer1.Stop();
                FormLogin fl = new FormLogin();
                fl.Show();
                this.Hide();
            }
            else
            {
                gunaCircleProgressBar2.Value += 1;
                //lblPersen.Text = gunaCircleProgressBar1.Value.ToString() + "%";
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
