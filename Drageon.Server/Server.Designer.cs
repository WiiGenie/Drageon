namespace Drageon.Server
{
    partial class Server
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
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.memberList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // logBox
            // 
            this.logBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logBox.Location = new System.Drawing.Point(12, 12);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(315, 381);
            this.logBox.TabIndex = 0;
            this.logBox.Text = "[Basic]欢迎使用Drageon，当前版本: alpha 0.0.1.2";
            // 
            // inputBox
            // 
            this.inputBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputBox.Location = new System.Drawing.Point(12, 397);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(315, 23);
            this.inputBox.TabIndex = 1;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyDown);
            // 
            // memberList
            // 
            this.memberList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.memberList.FormattingEnabled = true;
            this.memberList.ItemHeight = 14;
            this.memberList.Items.AddRange(new object[] {
            ""});
            this.memberList.Location = new System.Drawing.Point(333, 12);
            this.memberList.Name = "memberList";
            this.memberList.Size = new System.Drawing.Size(199, 408);
            this.memberList.TabIndex = 2;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 433);
            this.Controls.Add(this.memberList);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.logBox);
            this.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Server";
            this.Text = "Drageon Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.ListBox memberList;
    }
}