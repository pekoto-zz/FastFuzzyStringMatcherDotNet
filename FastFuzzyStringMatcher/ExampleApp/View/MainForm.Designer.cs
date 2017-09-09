namespace ExampleApp
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.search_btn = new System.Windows.Forms.Button();
            this.searchTerm_tbx = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.english = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.japanese = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matchPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchTerm_lbl = new System.Windows.Forms.Label();
            this.matchPercent_lbl = new System.Windows.Forms.Label();
            this.searchResults_gbx = new System.Windows.Forms.GroupBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.matchPercentage_nud = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.searchResults_gbx.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matchPercentage_nud)).BeginInit();
            this.SuspendLayout();
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(397, 15);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(100, 25);
            this.search_btn.TabIndex = 3;
            this.search_btn.Text = "Search";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // searchTerm_tbx
            // 
            this.searchTerm_tbx.Location = new System.Drawing.Point(93, 18);
            this.searchTerm_tbx.Name = "searchTerm_tbx";
            this.searchTerm_tbx.Size = new System.Drawing.Size(100, 20);
            this.searchTerm_tbx.TabIndex = 1;
            this.searchTerm_tbx.Text = "Diplomat";
            // 
            // dataGridView
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.english,
            this.japanese,
            this.matchPercent});
            this.dataGridView.Location = new System.Drawing.Point(6, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(621, 275);
            this.dataGridView.TabIndex = 2;
            // 
            // english
            // 
            this.english.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.english.HeaderText = "English";
            this.english.Name = "english";
            // 
            // japanese
            // 
            this.japanese.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.japanese.HeaderText = "Japanese";
            this.japanese.Name = "japanese";
            // 
            // matchPercent
            // 
            this.matchPercent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.matchPercent.HeaderText = "%";
            this.matchPercent.Name = "matchPercent";
            this.matchPercent.Width = 70;
            // 
            // searchTerm_lbl
            // 
            this.searchTerm_lbl.AutoSize = true;
            this.searchTerm_lbl.Location = new System.Drawing.Point(20, 22);
            this.searchTerm_lbl.Name = "searchTerm_lbl";
            this.searchTerm_lbl.Size = new System.Drawing.Size(67, 13);
            this.searchTerm_lbl.TabIndex = 4;
            this.searchTerm_lbl.Text = "Search term:";
            // 
            // matchPercent_lbl
            // 
            this.matchPercent_lbl.AutoSize = true;
            this.matchPercent_lbl.Location = new System.Drawing.Point(208, 22);
            this.matchPercent_lbl.Name = "matchPercent_lbl";
            this.matchPercent_lbl.Size = new System.Drawing.Size(67, 13);
            this.matchPercent_lbl.TabIndex = 5;
            this.matchPercent_lbl.Text = "Min match %";
            // 
            // searchResults_gbx
            // 
            this.searchResults_gbx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchResults_gbx.Controls.Add(this.dataGridView);
            this.searchResults_gbx.Location = new System.Drawing.Point(12, 55);
            this.searchResults_gbx.Name = "searchResults_gbx";
            this.searchResults_gbx.Size = new System.Drawing.Size(636, 300);
            this.searchResults_gbx.TabIndex = 6;
            this.searchResults_gbx.TabStop = false;
            this.searchResults_gbx.Text = "Search Results";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 368);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(660, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // matchPercentage_nud
            // 
            this.matchPercentage_nud.Location = new System.Drawing.Point(281, 18);
            this.matchPercentage_nud.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.matchPercentage_nud.Name = "matchPercentage_nud";
            this.matchPercentage_nud.Size = new System.Drawing.Size(100, 20);
            this.matchPercentage_nud.TabIndex = 2;
            this.matchPercentage_nud.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 390);
            this.Controls.Add(this.matchPercentage_nud);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.searchResults_gbx);
            this.Controls.Add(this.matchPercent_lbl);
            this.Controls.Add(this.searchTerm_lbl);
            this.Controls.Add(this.searchTerm_tbx);
            this.Controls.Add(this.search_btn);
            this.MinimumSize = new System.Drawing.Size(676, 429);
            this.Name = "MainForm";
            this.Text = "Translation Memory Searcher";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.searchResults_gbx.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.matchPercentage_nud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.TextBox searchTerm_tbx;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label searchTerm_lbl;
        private System.Windows.Forms.Label matchPercent_lbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn english;
        private System.Windows.Forms.DataGridViewTextBoxColumn japanese;
        private System.Windows.Forms.DataGridViewTextBoxColumn matchPercent;
        private System.Windows.Forms.GroupBox searchResults_gbx;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.NumericUpDown matchPercentage_nud;
    }
}

