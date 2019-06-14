namespace ToDoAppPhase1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lvToDo = new System.Windows.Forms.ListView();
            this.lvDoing = new System.Windows.Forms.ListView();
            this.lvDone = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "To do";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(435, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Doing";
            // 
            // btnAddTask
            // 
            this.btnAddTask.AllowDrop = true;
            this.btnAddTask.Location = new System.Drawing.Point(67, 524);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(95, 23);
            this.btnAddTask.TabIndex = 2;
            this.btnAddTask.Text = "Add new Task";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.BtnAddTask_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(804, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Done";
            // 
            // lvToDo
            // 
            this.lvToDo.AllowDrop = true;
            this.lvToDo.Location = new System.Drawing.Point(67, 109);
            this.lvToDo.Name = "lvToDo";
            this.lvToDo.Size = new System.Drawing.Size(332, 393);
            this.lvToDo.TabIndex = 1;
            this.lvToDo.UseCompatibleStateImageBehavior = false;
            this.lvToDo.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvTodo_DragDrop);
            this.lvToDo.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvTodo_DragEnter);
            // 
            // lvDoing
            // 
            this.lvDoing.AllowDrop = true;
            this.lvDoing.Location = new System.Drawing.Point(438, 109);
            this.lvDoing.Name = "lvDoing";
            this.lvDoing.Size = new System.Drawing.Size(332, 393);
            this.lvDoing.TabIndex = 3;
            this.lvDoing.UseCompatibleStateImageBehavior = false;
            this.lvDoing.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvDoing_DragDrop);
            this.lvDoing.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvDoing_DragEnter);
            // 
            // lvDone
            // 
            this.lvDone.AllowDrop = true;
            this.lvDone.Location = new System.Drawing.Point(807, 109);
            this.lvDone.Name = "lvDone";
            this.lvDone.Size = new System.Drawing.Size(332, 393);
            this.lvDone.TabIndex = 4;
            this.lvDone.UseCompatibleStateImageBehavior = false;
            this.lvDone.DragDrop += new System.Windows.Forms.DragEventHandler(this.LvDone_DragDrop);
            this.lvDone.DragEnter += new System.Windows.Forms.DragEventHandler(this.LvDone_DragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 629);
            this.Controls.Add(this.lvDone);
            this.Controls.Add(this.lvDoing);
            this.Controls.Add(this.lvToDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddTask);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvToDo;
        private System.Windows.Forms.ListView lvDoing;
        private System.Windows.Forms.ListView lvDone;
    }
}

