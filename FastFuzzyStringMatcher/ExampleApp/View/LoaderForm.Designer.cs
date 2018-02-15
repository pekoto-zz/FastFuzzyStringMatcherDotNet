namespace ExampleApp.View
{
    partial class LoaderForm
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
            this.cancel_btn = new System.Windows.Forms.Button();
            this.status_lbl = new System.Windows.Forms.Label();
            this.loading_progressbar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // cancel_btn
            // 
            this.cancel_btn.Location = new System.Drawing.Point(213, 115);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 35);
            this.cancel_btn.TabIndex = 0;
            this.cancel_btn.Text = "Cancel";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // status_lbl
            // 
            this.status_lbl.Location = new System.Drawing.Point(12, 63);
            this.status_lbl.Name = "status_lbl";
            this.status_lbl.Size = new System.Drawing.Size(476, 23);
            this.status_lbl.TabIndex = 1;
            this.status_lbl.Text = "Loading: XXXXX/XXXXX";
            this.status_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loading_progressbar
            // 
            this.loading_progressbar.Location = new System.Drawing.Point(15, 30);
            this.loading_progressbar.Name = "loading_progressbar";
            this.loading_progressbar.Size = new System.Drawing.Size(473, 23);
            this.loading_progressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.loading_progressbar.TabIndex = 2;
            // 
            // LoaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 170);
            this.Controls.Add(this.loading_progressbar);
            this.Controls.Add(this.status_lbl);
            this.Controls.Add(this.cancel_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoaderForm";
            this.Text = "Loading...";
            this.Load += new System.EventHandler(this.LoaderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Label status_lbl;
        private System.Windows.Forms.ProgressBar loading_progressbar;
    }
}