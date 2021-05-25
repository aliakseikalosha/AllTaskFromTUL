
namespace VAPW06
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
            this.person = new System.Windows.Forms.RadioButton();
            this.worker = new System.Windows.Forms.RadioButton();
            this.Create = new System.Windows.Forms.Button();
            this.listOfPerson = new System.Windows.Forms.ListBox();
            this.prev = new System.Windows.Forms.Button();
            this.next = new System.Windows.Forms.Button();
            this.payment = new System.Windows.Forms.NumericUpDown();
            this.name = new System.Windows.Forms.TextBox();
            this.description = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.payment)).BeginInit();
            this.SuspendLayout();
            // 
            // person
            // 
            this.person.AutoSize = true;
            this.person.Location = new System.Drawing.Point(51, 199);
            this.person.Name = "person";
            this.person.Size = new System.Drawing.Size(61, 19);
            this.person.TabIndex = 0;
            this.person.TabStop = true;
            this.person.Text = "person";
            this.person.UseVisualStyleBackColor = true;
            // 
            // worker
            // 
            this.worker.AutoSize = true;
            this.worker.Location = new System.Drawing.Point(51, 224);
            this.worker.Name = "worker";
            this.worker.Size = new System.Drawing.Size(61, 19);
            this.worker.TabIndex = 1;
            this.worker.TabStop = true;
            this.worker.Text = "worker";
            this.worker.UseVisualStyleBackColor = true;
            // 
            // Create
            // 
            this.Create.Location = new System.Drawing.Point(51, 266);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(75, 23);
            this.Create.TabIndex = 2;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // listOfPerson
            // 
            this.listOfPerson.FormattingEnabled = true;
            this.listOfPerson.ItemHeight = 15;
            this.listOfPerson.Location = new System.Drawing.Point(309, 142);
            this.listOfPerson.Name = "listOfPerson";
            this.listOfPerson.Size = new System.Drawing.Size(120, 94);
            this.listOfPerson.TabIndex = 3;
            // 
            // prev
            // 
            this.prev.Location = new System.Drawing.Point(280, 242);
            this.prev.Name = "prev";
            this.prev.Size = new System.Drawing.Size(75, 23);
            this.prev.TabIndex = 4;
            this.prev.Text = "prev";
            this.prev.UseVisualStyleBackColor = true;
            this.prev.Click += new System.EventHandler(this.prev_Click);
            // 
            // next
            // 
            this.next.Location = new System.Drawing.Point(385, 242);
            this.next.Name = "next";
            this.next.Size = new System.Drawing.Size(75, 23);
            this.next.TabIndex = 5;
            this.next.Text = "next";
            this.next.UseVisualStyleBackColor = true;
            this.next.Click += new System.EventHandler(this.next_Click);
            // 
            // payment
            // 
            this.payment.Location = new System.Drawing.Point(51, 171);
            this.payment.Name = "payment";
            this.payment.Size = new System.Drawing.Size(120, 23);
            this.payment.TabIndex = 6;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(51, 142);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(120, 23);
            this.name.TabIndex = 7;
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Location = new System.Drawing.Point(517, 142);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(0, 15);
            this.description.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.description);
            this.Controls.Add(this.name);
            this.Controls.Add(this.payment);
            this.Controls.Add(this.next);
            this.Controls.Add(this.prev);
            this.Controls.Add(this.listOfPerson);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.worker);
            this.Controls.Add(this.person);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.payment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton person;
        private System.Windows.Forms.RadioButton worker;
        private System.Windows.Forms.Button Create;
        private System.Windows.Forms.ListBox listOfPerson;
        private System.Windows.Forms.Button prev;
        private System.Windows.Forms.Button next;
        private System.Windows.Forms.NumericUpDown payment;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label description;
    }
}

