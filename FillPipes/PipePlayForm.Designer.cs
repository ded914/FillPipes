namespace FillPipes
{
    partial class PipePlayForm
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
            this.pictureBoxPipeGrid = new System.Windows.Forms.PictureBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.pictureBoxPixelField = new System.Windows.Forms.PictureBox();
            this.buttonTransferToPixels = new System.Windows.Forms.Button();
            this.buttonResetWater = new System.Windows.Forms.Button();
            this.buttonSimulate = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSimulateD = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.checkBoxSimulateOn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPipeGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPixelField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxPipeGrid
            // 
            this.pictureBoxPipeGrid.BackColor = System.Drawing.Color.White;
            this.pictureBoxPipeGrid.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxPipeGrid.Name = "pictureBoxPipeGrid";
            this.pictureBoxPipeGrid.Size = new System.Drawing.Size(731, 539);
            this.pictureBoxPipeGrid.TabIndex = 0;
            this.pictureBoxPipeGrid.TabStop = false;
            this.pictureBoxPipeGrid.Click += new System.EventHandler(this.pictureBoxPipeGrid_Click);
            this.pictureBoxPipeGrid.DoubleClick += new System.EventHandler(this.pictureBoxPipeGrid_DoubleClick);
            this.pictureBoxPipeGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPipeGrid_MouseMove);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(12, 574);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // pictureBoxPixelField
            // 
            this.pictureBoxPixelField.BackColor = System.Drawing.Color.White;
            this.pictureBoxPixelField.Location = new System.Drawing.Point(1382, 12);
            this.pictureBoxPixelField.Name = "pictureBoxPixelField";
            this.pictureBoxPixelField.Size = new System.Drawing.Size(746, 539);
            this.pictureBoxPixelField.TabIndex = 2;
            this.pictureBoxPixelField.TabStop = false;
            // 
            // buttonTransferToPixels
            // 
            this.buttonTransferToPixels.Location = new System.Drawing.Point(217, 574);
            this.buttonTransferToPixels.Name = "buttonTransferToPixels";
            this.buttonTransferToPixels.Size = new System.Drawing.Size(129, 23);
            this.buttonTransferToPixels.TabIndex = 3;
            this.buttonTransferToPixels.Text = "Transfer to Pixels";
            this.buttonTransferToPixels.UseVisualStyleBackColor = true;
            this.buttonTransferToPixels.Click += new System.EventHandler(this.buttonTransferToPixels_Click);
            // 
            // buttonResetWater
            // 
            this.buttonResetWater.Location = new System.Drawing.Point(93, 574);
            this.buttonResetWater.Name = "buttonResetWater";
            this.buttonResetWater.Size = new System.Drawing.Size(118, 23);
            this.buttonResetWater.TabIndex = 4;
            this.buttonResetWater.Text = "Reset Water";
            this.buttonResetWater.UseVisualStyleBackColor = true;
            this.buttonResetWater.Click += new System.EventHandler(this.buttonResetWater_Click);
            // 
            // buttonSimulate
            // 
            this.buttonSimulate.Location = new System.Drawing.Point(352, 574);
            this.buttonSimulate.Name = "buttonSimulate";
            this.buttonSimulate.Size = new System.Drawing.Size(75, 23);
            this.buttonSimulate.TabIndex = 5;
            this.buttonSimulate.Text = "Simulate";
            this.buttonSimulate.UseVisualStyleBackColor = true;
            this.buttonSimulate.Click += new System.EventHandler(this.buttonSimulate_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(433, 574);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(514, 574);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 7;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSimulateD
            // 
            this.buttonSimulateD.Location = new System.Drawing.Point(654, 574);
            this.buttonSimulateD.Name = "buttonSimulateD";
            this.buttonSimulateD.Size = new System.Drawing.Size(75, 23);
            this.buttonSimulateD.TabIndex = 8;
            this.buttonSimulateD.Text = "Simulate D";
            this.buttonSimulateD.UseVisualStyleBackColor = true;
            this.buttonSimulateD.Click += new System.EventHandler(this.buttonSimulateD_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(776, 13);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(539, 538);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // checkBoxSimulateOn
            // 
            this.checkBoxSimulateOn.AutoSize = true;
            this.checkBoxSimulateOn.Location = new System.Drawing.Point(756, 579);
            this.checkBoxSimulateOn.Name = "checkBoxSimulateOn";
            this.checkBoxSimulateOn.Size = new System.Drawing.Size(85, 17);
            this.checkBoxSimulateOn.TabIndex = 10;
            this.checkBoxSimulateOn.Text = "Simulate ON";
            this.checkBoxSimulateOn.UseVisualStyleBackColor = true;
            // 
            // PipePlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1534, 609);
            this.Controls.Add(this.checkBoxSimulateOn);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonSimulateD);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSimulate);
            this.Controls.Add(this.buttonResetWater);
            this.Controls.Add(this.buttonTransferToPixels);
            this.Controls.Add(this.pictureBoxPixelField);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.pictureBoxPipeGrid);
            this.Name = "PipePlayForm";
            this.Text = "Pipe Playground";
            this.Load += new System.EventHandler(this.PipePlayForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPipeGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPixelField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPipeGrid;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.PictureBox pictureBoxPixelField;
        private System.Windows.Forms.Button buttonTransferToPixels;
        private System.Windows.Forms.Button buttonResetWater;
        private System.Windows.Forms.Button buttonSimulate;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSimulateD;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox checkBoxSimulateOn;
    }
}

