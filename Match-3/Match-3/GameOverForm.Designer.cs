
namespace Match_3
{
    partial class GameOverForm
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
            this.labelGameOver = new System.Windows.Forms.Label();
            this.buttonGameOver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGameOver
            // 
            this.labelGameOver.AutoSize = true;
            this.labelGameOver.BackColor = System.Drawing.Color.Transparent;
            this.labelGameOver.Font = new System.Drawing.Font("Haettenschweiler", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGameOver.ForeColor = System.Drawing.Color.Red;
            this.labelGameOver.Location = new System.Drawing.Point(78, 28);
            this.labelGameOver.Name = "labelGameOver";
            this.labelGameOver.Size = new System.Drawing.Size(186, 50);
            this.labelGameOver.TabIndex = 0;
            this.labelGameOver.Text = "GAME OVER";
            // 
            // buttonGameOver
            // 
            this.buttonGameOver.BackColor = System.Drawing.Color.MistyRose;
            this.buttonGameOver.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonGameOver.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGameOver.ForeColor = System.Drawing.Color.LightCoral;
            this.buttonGameOver.Location = new System.Drawing.Point(111, 100);
            this.buttonGameOver.Name = "buttonGameOver";
            this.buttonGameOver.Size = new System.Drawing.Size(123, 42);
            this.buttonGameOver.TabIndex = 1;
            this.buttonGameOver.Text = "OK";
            this.buttonGameOver.UseVisualStyleBackColor = false;
            this.buttonGameOver.Click += new System.EventHandler(this.buttonGameOver_Click);
            // 
            // GameOverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Match_3.Properties.Resources.gradient_color_abstract_1t_2560x1080;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(355, 184);
            this.Controls.Add(this.buttonGameOver);
            this.Controls.Add(this.labelGameOver);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(371, 223);
            this.MinimumSize = new System.Drawing.Size(371, 223);
            this.Name = "GameOverForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameOverForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGameOver;
        private System.Windows.Forms.Button buttonGameOver;
    }
}