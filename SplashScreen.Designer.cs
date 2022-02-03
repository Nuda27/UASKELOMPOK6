namespace pos
{
    partial class SplashScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.gunaCircleProgressBar2 = new Guna.UI.WinForms.GunaCircleProgressBar();
            this.gunaElipse1 = new Guna.UI.WinForms.GunaElipse(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gunaCircleProgressBar2
            // 
            this.gunaCircleProgressBar2.Animated = true;
            this.gunaCircleProgressBar2.AnimationSpeed = 0.6F;
            this.gunaCircleProgressBar2.BackColor = System.Drawing.Color.Transparent;
            this.gunaCircleProgressBar2.BaseColor = System.Drawing.Color.White;
            this.gunaCircleProgressBar2.ColorStyle = Guna.UI.WinForms.ColorStyle.Transition;
            this.gunaCircleProgressBar2.IdleColor = System.Drawing.Color.Gainsboro;
            this.gunaCircleProgressBar2.IdleOffset = 20;
            this.gunaCircleProgressBar2.Image = null;
            this.gunaCircleProgressBar2.ImageSize = new System.Drawing.Size(52, 52);
            this.gunaCircleProgressBar2.LineEndCap = System.Drawing.Drawing2D.LineCap.Round;
            this.gunaCircleProgressBar2.LineStartCap = System.Drawing.Drawing2D.LineCap.Round;
            this.gunaCircleProgressBar2.Location = new System.Drawing.Point(114, 260);
            this.gunaCircleProgressBar2.Name = "gunaCircleProgressBar2";
            this.gunaCircleProgressBar2.ProgressMaxColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(151)))), ((int)(((byte)(114)))));
            this.gunaCircleProgressBar2.ProgressMinColor = System.Drawing.Color.Gainsboro;
            this.gunaCircleProgressBar2.ProgressOffset = 20;
            this.gunaCircleProgressBar2.Size = new System.Drawing.Size(167, 155);
            this.gunaCircleProgressBar2.TabIndex = 2;
            this.gunaCircleProgressBar2.UseProgressPercentText = true;
            // 
            // gunaElipse1
            // 
            this.gunaElipse1.TargetControl = this;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::pos.Properties.Resources.SIP_AE_LIBRARY__2_;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(846, 477);
            this.Controls.Add(this.gunaCircleProgressBar2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI.WinForms.GunaCircleProgressBar gunaCircleProgressBar2;
        private Guna.UI.WinForms.GunaElipse gunaElipse1;
        private System.Windows.Forms.Timer timer1;
    }
}