using System.Drawing;

namespace BeneathTheSurface
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.play = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.credits = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // play
            // 
            this.play.BackColor = System.Drawing.Color.Black;
            this.play.Font = new System.Drawing.Font("Impact", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.play.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.play.Location = new System.Drawing.Point(375, 237);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(296, 77);
            this.play.TabIndex = 0;
            this.play.Text = "PLAY";
            this.play.UseVisualStyleBackColor = false;
            this.play.Click += new System.EventHandler(this.button1_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.BackColor = System.Drawing.Color.Black;
            this.Title.Font = new System.Drawing.Font("Impact", 54F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Title.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Title.Location = new System.Drawing.Point(171, 147);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(680, 87);
            this.Title.TabIndex = 1;
            this.Title.Text = "BENEATH THE SURFACE";
            this.Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Title.Click += new System.EventHandler(this.label1_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Black;
            this.exit.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.exit.Location = new System.Drawing.Point(409, 372);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(223, 53);
            this.exit.TabIndex = 2;
            this.exit.Text = " EXIT";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.button2_Click);
            // 
            // credits
            // 
            this.credits.BackColor = System.Drawing.Color.Black;
            this.credits.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.credits.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.credits.Location = new System.Drawing.Point(429, 320);
            this.credits.Name = "credits";
            this.credits.Size = new System.Drawing.Size(180, 46);
            this.credits.TabIndex = 3;
            this.credits.Text = "CREDITS";
            this.credits.UseVisualStyleBackColor = false;
            this.credits.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1014, 510);
            this.Controls.Add(this.credits);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.play);
            this.Name = "MainMenu";
            this.Text = "Beneath The Surface";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button credits;
    }
}
