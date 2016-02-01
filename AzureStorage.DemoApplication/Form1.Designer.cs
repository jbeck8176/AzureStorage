namespace AzureStorage.DemoApplication
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbRowKey = new System.Windows.Forms.TextBox();
            this.tbPartitionKey = new System.Windows.Forms.TextBox();
            this.btnRetStorageTableData = new System.Windows.Forms.Button();
            this.btnStInsert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbStResView = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbQRowKey = new System.Windows.Forms.TextBox();
            this.tbQPartitionKey = new System.Windows.Forms.TextBox();
            this.btnProcTestQueue = new System.Windows.Forms.Button();
            this.rtbQueueRes = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbStResView);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbRowKey);
            this.groupBox1.Controls.Add(this.tbPartitionKey);
            this.groupBox1.Controls.Add(this.btnRetStorageTableData);
            this.groupBox1.Controls.Add(this.btnStInsert);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 257);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "StorageTables";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Row Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Partition Key";
            // 
            // tbRowKey
            // 
            this.tbRowKey.Location = new System.Drawing.Point(7, 83);
            this.tbRowKey.Name = "tbRowKey";
            this.tbRowKey.Size = new System.Drawing.Size(295, 20);
            this.tbRowKey.TabIndex = 3;
            // 
            // tbPartitionKey
            // 
            this.tbPartitionKey.Location = new System.Drawing.Point(7, 38);
            this.tbPartitionKey.Name = "tbPartitionKey";
            this.tbPartitionKey.Size = new System.Drawing.Size(295, 20);
            this.tbPartitionKey.TabIndex = 2;
            // 
            // btnRetStorageTableData
            // 
            this.btnRetStorageTableData.Location = new System.Drawing.Point(7, 154);
            this.btnRetStorageTableData.Name = "btnRetStorageTableData";
            this.btnRetStorageTableData.Size = new System.Drawing.Size(151, 35);
            this.btnRetStorageTableData.TabIndex = 1;
            this.btnRetStorageTableData.Text = "Retreive Storage Table Data";
            this.btnRetStorageTableData.UseVisualStyleBackColor = true;
            this.btnRetStorageTableData.Click += new System.EventHandler(this.btnRetStorageTableData_Click);
            // 
            // btnStInsert
            // 
            this.btnStInsert.Location = new System.Drawing.Point(7, 113);
            this.btnStInsert.Name = "btnStInsert";
            this.btnStInsert.Size = new System.Drawing.Size(151, 35);
            this.btnStInsert.TabIndex = 0;
            this.btnStInsert.Text = "Insert Storage Table Data";
            this.btnStInsert.UseVisualStyleBackColor = true;
            this.btnStInsert.Click += new System.EventHandler(this.btnStInsert_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtbQueueRes);
            this.groupBox2.Controls.Add(this.btnProcTestQueue);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbQRowKey);
            this.groupBox2.Controls.Add(this.tbQPartitionKey);
            this.groupBox2.Location = new System.Drawing.Point(13, 276);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 359);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Queues";
            // 
            // tbStResView
            // 
            this.tbStResView.Location = new System.Drawing.Point(10, 196);
            this.tbStResView.Multiline = true;
            this.tbStResView.Name = "tbStResView";
            this.tbStResView.Size = new System.Drawing.Size(389, 55);
            this.tbStResView.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Row Key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Partition Key";
            // 
            // tbQRowKey
            // 
            this.tbQRowKey.Location = new System.Drawing.Point(10, 82);
            this.tbQRowKey.Name = "tbQRowKey";
            this.tbQRowKey.Size = new System.Drawing.Size(295, 20);
            this.tbQRowKey.TabIndex = 7;
            // 
            // tbQPartitionKey
            // 
            this.tbQPartitionKey.Location = new System.Drawing.Point(10, 37);
            this.tbQPartitionKey.Name = "tbQPartitionKey";
            this.tbQPartitionKey.Size = new System.Drawing.Size(295, 20);
            this.tbQPartitionKey.TabIndex = 6;
            // 
            // btnProcTestQueue
            // 
            this.btnProcTestQueue.Location = new System.Drawing.Point(10, 109);
            this.btnProcTestQueue.Name = "btnProcTestQueue";
            this.btnProcTestQueue.Size = new System.Drawing.Size(148, 35);
            this.btnProcTestQueue.TabIndex = 10;
            this.btnProcTestQueue.Text = "Process Test Queue";
            this.btnProcTestQueue.UseVisualStyleBackColor = true;
            this.btnProcTestQueue.Click += new System.EventHandler(this.btnProcTestQueue_Click);
            // 
            // rtbQueueRes
            // 
            this.rtbQueueRes.Location = new System.Drawing.Point(10, 150);
            this.rtbQueueRes.Name = "rtbQueueRes";
            this.rtbQueueRes.Size = new System.Drawing.Size(389, 203);
            this.rtbQueueRes.TabIndex = 11;
            this.rtbQueueRes.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 647);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbRowKey;
        private System.Windows.Forms.TextBox tbPartitionKey;
        private System.Windows.Forms.Button btnRetStorageTableData;
        private System.Windows.Forms.Button btnStInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbStResView;
        private System.Windows.Forms.Button btnProcTestQueue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbQRowKey;
        private System.Windows.Forms.TextBox tbQPartitionKey;
        private System.Windows.Forms.RichTextBox rtbQueueRes;
    }
}

