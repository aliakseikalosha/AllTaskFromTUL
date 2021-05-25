
namespace VAPW08
{
    partial class Form1
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
            this.listOfStorage = new System.Windows.Forms.ListBox();
            this.listOfUsers = new System.Windows.Forms.ListBox();
            this.plus = new System.Windows.Forms.Button();
            this.minus = new System.Windows.Forms.Button();
            this.addUser = new System.Windows.Forms.Button();
            this.printTotal = new System.Windows.Forms.Button();
            this.order = new System.Windows.Forms.ListBox();
            this.total = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listOfStorage
            // 
            this.listOfStorage.FormattingEnabled = true;
            this.listOfStorage.ItemHeight = 15;
            this.listOfStorage.Location = new System.Drawing.Point(35, 77);
            this.listOfStorage.Name = "listOfStorage";
            this.listOfStorage.Size = new System.Drawing.Size(178, 94);
            this.listOfStorage.TabIndex = 0;
            this.listOfStorage.SelectedIndexChanged += new System.EventHandler(this.storage_SelectedIndexChanged);
            // 
            // listOfUsers
            // 
            this.listOfUsers.FormattingEnabled = true;
            this.listOfUsers.ItemHeight = 15;
            this.listOfUsers.Location = new System.Drawing.Point(219, 77);
            this.listOfUsers.Name = "listOfUsers";
            this.listOfUsers.Size = new System.Drawing.Size(171, 94);
            this.listOfUsers.TabIndex = 1;
            this.listOfUsers.SelectedIndexChanged += new System.EventHandler(this.listOfUsers_SelectedIndexChanged);
            // 
            // plus
            // 
            this.plus.Location = new System.Drawing.Point(78, 286);
            this.plus.Name = "plus";
            this.plus.Size = new System.Drawing.Size(75, 23);
            this.plus.TabIndex = 2;
            this.plus.Text = "+";
            this.plus.UseVisualStyleBackColor = true;
            this.plus.Click += new System.EventHandler(this.plus_Click);
            // 
            // minus
            // 
            this.minus.Location = new System.Drawing.Point(78, 326);
            this.minus.Name = "minus";
            this.minus.Size = new System.Drawing.Size(75, 23);
            this.minus.TabIndex = 3;
            this.minus.Text = "-";
            this.minus.UseVisualStyleBackColor = true;
            this.minus.Click += new System.EventHandler(this.minus_Click);
            // 
            // addUser
            // 
            this.addUser.Location = new System.Drawing.Point(219, 286);
            this.addUser.Name = "addUser";
            this.addUser.Size = new System.Drawing.Size(75, 23);
            this.addUser.TabIndex = 4;
            this.addUser.Text = "Add User";
            this.addUser.UseVisualStyleBackColor = true;
            this.addUser.Click += new System.EventHandler(this.addUser_Click);
            // 
            // printTotal
            // 
            this.printTotal.Location = new System.Drawing.Point(388, 285);
            this.printTotal.Name = "printTotal";
            this.printTotal.Size = new System.Drawing.Size(75, 23);
            this.printTotal.TabIndex = 6;
            this.printTotal.Text = "Print Total";
            this.printTotal.UseVisualStyleBackColor = true;
            this.printTotal.Click += new System.EventHandler(this.printTotal_Click);
            // 
            // order
            // 
            this.order.FormattingEnabled = true;
            this.order.ItemHeight = 15;
            this.order.Location = new System.Drawing.Point(396, 77);
            this.order.Name = "order";
            this.order.Size = new System.Drawing.Size(175, 94);
            this.order.TabIndex = 7;
            // 
            // total
            // 
            this.total.AutoSize = true;
            this.total.Location = new System.Drawing.Point(78, 403);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(31, 15);
            this.total.TabIndex = 8;
            this.total.Text = "total";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.total);
            this.Controls.Add(this.order);
            this.Controls.Add(this.printTotal);
            this.Controls.Add(this.addUser);
            this.Controls.Add(this.minus);
            this.Controls.Add(this.plus);
            this.Controls.Add(this.listOfUsers);
            this.Controls.Add(this.listOfStorage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listOfStorage;
        private System.Windows.Forms.ListBox listOfUsers;
        private System.Windows.Forms.Button plus;
        private System.Windows.Forms.Button minus;
        private System.Windows.Forms.Button addUser;
        private System.Windows.Forms.Button printTotal;
        private System.Windows.Forms.ListBox order;
        private System.Windows.Forms.Label total;
    }
}

