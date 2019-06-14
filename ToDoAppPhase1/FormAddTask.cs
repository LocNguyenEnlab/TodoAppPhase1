using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoAppPhase1
{
    public partial class FormAddTask : Form
    {
        public FormAddTask()
        {
            InitializeComponent();
            btnUpdate.Visible = false;
        }
        public FormAddTask(Task t)
        {
            InitializeComponent();
            tbTitle.Text = t.Title;
            tbDescription.Text = t.Description;
            btnOk.Visible = false;
            taskEdit = t;
        }

        private Task taskEdit = new Task();

        public delegate void PassData(Task t);
        public delegate void ShowForm1();

        public PassData pd;
        public ShowForm1 show;

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Task task = new Task
            {
                Id = -1,
                Title = tbTitle.Text,
                Description = tbDescription.Text,
                TimeCreate = Convert.ToDateTime(DateTime.Now)
            };
            if(!task.IsEmpty())
            {
                pd(task);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Please fill in full info", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormAddTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            show();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            taskEdit.Title = tbTitle.Text;
            taskEdit.Description = tbDescription.Text;
            if (!taskEdit.IsEmpty())
            {
                pd(taskEdit);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Please fill in full info", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
