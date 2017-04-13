using KtSoft.ScrumControls;
namespace KtSoft.ScrumControls.Test
{
    partial class Form1
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
            this.scrumScheduleControl1 = new KtSoft.ScrumControls.ScrumBoardControl();
            ((System.ComponentModel.ISupportInitialize)(this.scrumScheduleControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // scrumScheduleControl1
            // 
            this.scrumScheduleControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrumScheduleControl1.BackColor = System.Drawing.SystemColors.Window;
            this.scrumScheduleControl1.HeaderTextAlignment = KtSoft.ScrumControls.Renderer.TextAlignment.Center;
            this.scrumScheduleControl1.Location = new System.Drawing.Point(12, 12);
            this.scrumScheduleControl1.Name = "scrumScheduleControl1";
            this.scrumScheduleControl1.Size = new System.Drawing.Size(813, 556);
            this.scrumScheduleControl1.TabIndex = 0;
            this.scrumScheduleControl1.Text = "scrumScheduleControl1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 580);
            this.Controls.Add(this.scrumScheduleControl1);
            this.Name = "Form1";
            this.Text = "Calendar demo";
            ((System.ComponentModel.ISupportInitialize)(this.scrumScheduleControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
		private ScrumBoardControl scrumScheduleControl1;



	}
}

