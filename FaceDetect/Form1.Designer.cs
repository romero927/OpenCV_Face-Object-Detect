namespace DetectionApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.modeSelector = new System.Windows.Forms.ComboBox();
            this.modeSelectorLabel = new System.Windows.Forms.Label();
            this.scaleFactorTrackBar = new System.Windows.Forms.TrackBar();
            this.minNeighborsTrackBar = new System.Windows.Forms.TrackBar();
            this.minSizeTrackBar = new System.Windows.Forms.TrackBar();
            this.scaleFactorLabel = new System.Windows.Forms.Label();
            this.minNeighborsLabel = new System.Windows.Forms.Label();
            this.minSizeLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.confidenceThresholdLabel = new System.Windows.Forms.Label();
            this.confidenceThresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.saveFrameButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scaleFactorTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minNeighborsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSizeTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.confidenceThresholdTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // modeSelector
            // 
            this.modeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeSelector.FormattingEnabled = true;
            this.modeSelector.Location = new System.Drawing.Point(12, 32);
            this.modeSelector.Name = "modeSelector";
            this.modeSelector.Size = new System.Drawing.Size(200, 23);
            this.modeSelector.TabIndex = 0;
            // 
            // modeSelectorLabel
            // 
            this.modeSelectorLabel.AutoSize = true;
            this.modeSelectorLabel.Location = new System.Drawing.Point(12, 14);
            this.modeSelectorLabel.Name = "modeSelectorLabel";
            this.modeSelectorLabel.Size = new System.Drawing.Size(89, 15);
            this.modeSelectorLabel.TabIndex = 1;
            this.modeSelectorLabel.Text = "Detection Mode";
            // 
            // scaleFactorTrackBar
            // 
            this.scaleFactorTrackBar.Location = new System.Drawing.Point(12, 86);
            this.scaleFactorTrackBar.Name = "scaleFactorTrackBar";
            this.scaleFactorTrackBar.Size = new System.Drawing.Size(200, 45);
            this.scaleFactorTrackBar.TabIndex = 2;
            // 
            // minNeighborsTrackBar
            // 
            this.minNeighborsTrackBar.Location = new System.Drawing.Point(12, 152);
            this.minNeighborsTrackBar.Name = "minNeighborsTrackBar";
            this.minNeighborsTrackBar.Size = new System.Drawing.Size(200, 45);
            this.minNeighborsTrackBar.TabIndex = 3;
            // 
            // minSizeTrackBar
            // 
            this.minSizeTrackBar.Location = new System.Drawing.Point(12, 218);
            this.minSizeTrackBar.Name = "minSizeTrackBar";
            this.minSizeTrackBar.Size = new System.Drawing.Size(200, 45);
            this.minSizeTrackBar.TabIndex = 4;
            // 
            // scaleFactorLabel
            // 
            this.scaleFactorLabel.AutoSize = true;
            this.scaleFactorLabel.Location = new System.Drawing.Point(12, 68);
            this.scaleFactorLabel.Name = "scaleFactorLabel";
            this.scaleFactorLabel.Size = new System.Drawing.Size(68, 15);
            this.scaleFactorLabel.TabIndex = 5;
            this.scaleFactorLabel.Text = "Scale Factor";
            // 
            // minNeighborsLabel
            // 
            this.minNeighborsLabel.AutoSize = true;
            this.minNeighborsLabel.Location = new System.Drawing.Point(12, 134);
            this.minNeighborsLabel.Name = "minNeighborsLabel";
            this.minNeighborsLabel.Size = new System.Drawing.Size(86, 15);
            this.minNeighborsLabel.TabIndex = 6;
            this.minNeighborsLabel.Text = "Min Neighbors";
            // 
            // minSizeLabel
            // 
            this.minSizeLabel.AutoSize = true;
            this.minSizeLabel.Location = new System.Drawing.Point(12, 200);
            this.minSizeLabel.Name = "minSizeLabel";
            this.minSizeLabel.Size = new System.Drawing.Size(52, 15);
            this.minSizeLabel.TabIndex = 7;
            this.minSizeLabel.Text = "Min Size";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(230, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(640, 480);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 8;
            this.pictureBox.TabStop = false;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 498);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 15);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "Status";
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(230, 498);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(26, 15);
            this.fpsLabel.TabIndex = 10;
            this.fpsLabel.Text = "FPS";
            // 
            // confidenceThresholdLabel
            // 
            this.confidenceThresholdLabel.AutoSize = true;
            this.confidenceThresholdLabel.Location = new System.Drawing.Point(12, 266);
            this.confidenceThresholdLabel.Name = "confidenceThresholdLabel";
            this.confidenceThresholdLabel.Size = new System.Drawing.Size(127, 15);
            this.confidenceThresholdLabel.TabIndex = 11;
            this.confidenceThresholdLabel.Text = "Confidence Threshold";
            // 
            // confidenceThresholdTrackBar
            // 
            this.confidenceThresholdTrackBar.Location = new System.Drawing.Point(12, 284);
            this.confidenceThresholdTrackBar.Name = "confidenceThresholdTrackBar";
            this.confidenceThresholdTrackBar.Size = new System.Drawing.Size(200, 45);
            this.confidenceThresholdTrackBar.TabIndex = 12;
            // 
            // saveFrameButton
            // 
            this.saveFrameButton.Location = new System.Drawing.Point(12, 335);
            this.saveFrameButton.Name = "saveFrameButton";
            this.saveFrameButton.Size = new System.Drawing.Size(200, 23);
            this.saveFrameButton.TabIndex = 13;
            this.saveFrameButton.Text = "Save Frame";
            this.saveFrameButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 521);
            this.Controls.Add(this.saveFrameButton);
            this.Controls.Add(this.confidenceThresholdTrackBar);
            this.Controls.Add(this.confidenceThresholdLabel);
            this.Controls.Add(this.fpsLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.minSizeLabel);
            this.Controls.Add(this.minNeighborsLabel);
            this.Controls.Add(this.scaleFactorLabel);
            this.Controls.Add(this.minSizeTrackBar);
            this.Controls.Add(this.minNeighborsTrackBar);
            this.Controls.Add(this.scaleFactorTrackBar);
            this.Controls.Add(this.modeSelectorLabel);
            this.Controls.Add(this.modeSelector);
            this.Name = "MainForm";
            this.Text = "Face and Object Detection";
            ((System.ComponentModel.ISupportInitialize)(this.scaleFactorTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minNeighborsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minSizeTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.confidenceThresholdTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox modeSelector;
        private System.Windows.Forms.Label modeSelectorLabel;
        private System.Windows.Forms.TrackBar scaleFactorTrackBar;
        private System.Windows.Forms.TrackBar minNeighborsTrackBar;
        private System.Windows.Forms.TrackBar minSizeTrackBar;
        private System.Windows.Forms.Label scaleFactorLabel;
        private System.Windows.Forms.Label minNeighborsLabel;
        private System.Windows.Forms.Label minSizeLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Label confidenceThresholdLabel;
        private System.Windows.Forms.TrackBar confidenceThresholdTrackBar;
        private System.Windows.Forms.Button saveFrameButton;
    }
}