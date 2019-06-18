using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoAppPhase1
{
    public partial class Form1 : Form
    {
        private int _pointYTodo;
        private int _pointYDoing;
        private int _pointYDone;
        private List<Task> _listTask;
        private List<TextBox> _listTbTodo;
        private List<TextBox> _listTbDoing;
        private List<TextBox> _listTbDone;


        public Form1()
        {
            InitializeComponent();
            _pointYTodo = 0;
            _pointYDoing = 0;
            _pointYDone = 0;
            _listTask = new List<Task>();
            _listTbTodo = new List<TextBox>();
            _listTbDoing = new List<TextBox>();
            _listTbDone = new List<TextBox>();
        }

        private void CreateNewTextBox(Task t, ListView lv, ref int pointY, List<TextBox> listTb)
        {
            var tb = new TextBox();
            tb.Location = new Point(0, pointY);
            tb.Text = t.Title;
            tb.Name = "tb" + t.Id;
            tb.ReadOnly = true;
            tb.Size = new Size(325, 20);
            tb.MouseDown += TextBox_MouseDown;
            tb.Show();
            listTb.Add(tb);
            lv.Controls.Add(tb);
            pointY += 25;
        }

        private bool IsInListTextbox(string nameTb, List<TextBox> list)
        {
            foreach (var item in list)
            {
                if (item.Name == nameTb)
                {
                    return true;
                }
            }
            return false;
        }

        private TextBox FindTextbox(string nameTb, List<TextBox> list)
        {
            foreach (var item in list)
            {
                if (item.Name == nameTb)
                {
                    return item;
                }
            }
            return null;
        }

        private int FindIndexTextboxInList(List<TextBox> list, TextBox tb)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == tb)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindIndexTask(List<Task> list, string title)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Title == title)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Check duplicate task
        /// </summary>
        /// <param name="list"></param>
        /// <param name="t"></param>
        /// <returns>true if t already exists in list, otherwise return false</returns>
        private bool IsDuplicateTask(List<Task> list, Task t)
        {
            foreach (var item in list)
            {
                if (item.Compare(t))
                    return true;
            }
            return false;
        }

        private void RemoveTextBox(TextBox tb, ListView lv, List<TextBox> list, ref int pointY)
        {
            int indexTb = FindIndexTextboxInList(list, tb);

            if (indexTb == list.Count - 1)
            {
                pointY -= 25;
            }
            else
            {
                for (int i = indexTb + 1; i < list.Count; i++)
                {
                    list[i].Location = new Point(0, list[i].Location.Y - 25);
                }
                pointY -= 25;
            }

            lv.Controls.Remove(tb);
            list.Remove(tb);
        }

        private void EditTask(Task t)
        {
            var addTaskForm = new AddTaskForm(t)
            {
                passData = new AddTaskForm.PassData(PassData),
                showMainForm = new AddTaskForm.ShowMainForm(Show),
                Text = "Edit task"
            };
            addTaskForm.Show();
            this.Hide();
        }

        private void DeleteTask(Task t)
        {
            int idTb = t.Id;
            if (IsInListTextbox("tb" + idTb, _listTbTodo))
            {
                TextBox tb = FindTextbox("tb" + idTb, _listTbTodo);
                RemoveTextBox(tb, lvToDo, _listTbTodo, ref _pointYTodo);
            }
            else if (IsInListTextbox("tb" + idTb, _listTbDoing))
            {
                TextBox tb = FindTextbox("tb" + idTb, _listTbDoing);
                RemoveTextBox(tb, lvDoing, _listTbDoing, ref _pointYDoing);
            }
            else if (IsInListTextbox("tb" + idTb, _listTbDone))
            {
                TextBox tb = FindTextbox("tb" + idTb, _listTbDone);
                RemoveTextBox(tb, lvDone, _listTbDone, ref _pointYDone);
            }
            _listTask.Remove(t);
        }

        private void ShowTaskDetail(Task t)
        {
            MessageBoxManager.Yes = "Edit";
            MessageBoxManager.No = "Delete";
            MessageBoxManager.Cancel = "Close";
            MessageBoxManager.Register();

            DialogResult dialogResult = MessageBox.Show(string.Format("Title: {0}\nDescription: {1}\nTime create: {2}",
                t.Title, t.Description, Convert.ToDateTime(t.TimeCreate)),
                "Task Detail", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            MessageBoxManager.Unregister();
            if (dialogResult == DialogResult.Yes)
            {
                EditTask(t);
            }
            else if (dialogResult == DialogResult.No)
            {
                DialogResult resultConfirm = MessageBox.Show("Do you want to delete this task?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultConfirm == DialogResult.Yes)
                {
                    DeleteTask(t);
                }
            }
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            var formAddTask = new AddTaskForm
            {
                passData = new AddTaskForm.PassData(PassData),
                showMainForm = new AddTaskForm.ShowMainForm(Show)
            };
            formAddTask.Show();
            this.Hide();
        }

        public void PassData(Task t)
        {
            if (t.Id == -1) //add new task
            {
                if (!IsDuplicateTask(_listTask, t))
                {
                    this.Show();
                    t.Id = _listTask.Count;
                    _listTask.Add(t);
                    CreateNewTextBox(t, lvToDo, ref _pointYTodo, _listTbTodo);
                }
                else
                {
                    MessageBox.Show("This task is already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Show();
                }
            }
            else //update an exists task
            {
                _listTask[t.Id].Title = t.Title;
                _listTask[t.Id].Description = t.Description;
                UpdateTextBox(t);
                this.Show();
            }
        }

        public void UpdateTextBox(Task t)
        {
            TextBox tb;

            if (FindTextbox("tb" + t.Id, _listTbTodo) != null)
            {
                tb = FindTextbox("tb" + t.Id, _listTbTodo);
                _listTbTodo[FindIndexTextboxInList(_listTbTodo, tb)].Text = t.Title;
            }
            else if (FindTextbox("tb" + t.Id, _listTbDoing) != null)
            {
                tb = FindTextbox("tb" + t.Id, _listTbDoing);
                _listTbDoing[FindIndexTextboxInList(_listTbDoing, tb)].Text = t.Title;
            }
            else if (FindTextbox("tb" + t.Id, _listTbDone) != null)
            {
                tb = FindTextbox("tb" + t.Id, _listTbDone);
                _listTbDone[FindIndexTextboxInList(_listTbDone, tb)].Text = t.Title;
            }
        }

        private void LvDoing_DragEnter(object sender, DragEventArgs e)
        {
            int idTb = ((Task)e.Data.GetData(e.Data.GetFormats()[0])).Id;

            if (!IsInListTextbox("tb" + idTb, _listTbDoing))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void LvDone_DragEnter(object sender, DragEventArgs e)
        {
            int idTb = ((Task)e.Data.GetData(e.Data.GetFormats()[0])).Id;

            if (!IsInListTextbox("tb" + idTb, _listTbDone))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void LvTodo_DragEnter(object sender, DragEventArgs e)
        {
            int idTb = ((Task)e.Data.GetData(e.Data.GetFormats()[0])).Id;

            if (!IsInListTextbox("tb" + idTb, _listTbTodo))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void LvDoing_DragDrop(object sender, DragEventArgs e)
        {
            var lv2 = sender as ListView;
            Task t = (Task)e.Data.GetData(e.Data.GetFormats()[0]);
            CreateNewTextBox(t, lv2, ref _pointYDoing, _listTbDoing);

            if (IsInListTextbox("tb" + t.Id, _listTbTodo))
            {
                var item = FindTextbox("tb" + t.Id, _listTbTodo);
                RemoveTextBox(item, lvToDo, _listTbTodo, ref _pointYTodo);
            }

            if (IsInListTextbox("tb" + t.Id, _listTbDone))
            {
                var item = FindTextbox("tb" + t.Id, _listTbDone);
                RemoveTextBox(item, lvDone, _listTbDone, ref _pointYDone);
            }
        }

        private void LvDone_DragDrop(object sender, DragEventArgs e)
        {
            var lv2 = sender as ListView;
            Task t = (Task)e.Data.GetData(e.Data.GetFormats()[0]);
            CreateNewTextBox(t, lv2, ref _pointYDone, _listTbDone);

            if (IsInListTextbox("tb" + t.Id, _listTbTodo))
            {
                var item = FindTextbox("tb" + t.Id, _listTbTodo);
                RemoveTextBox(item, lvToDo, _listTbTodo, ref _pointYTodo);
            }

            if (IsInListTextbox("tb" + t.Id, _listTbDoing))
            {
                var item = FindTextbox("tb" + t.Id, _listTbDoing);
                RemoveTextBox(item, lvDoing, _listTbDoing, ref _pointYDoing);
            }
        }

        private void LvTodo_DragDrop(object sender, DragEventArgs e)
        {
            var lv2 = sender as ListView;
            Task t = (Task)e.Data.GetData(e.Data.GetFormats()[0]);
            CreateNewTextBox(t, lv2, ref _pointYTodo, _listTbTodo);

            if (IsInListTextbox("tb" + t.Id, _listTbDone))
            {
                var item = FindTextbox("tb" + t.Id, _listTbDone);
                RemoveTextBox(item, lvDone, _listTbDone, ref _pointYDone);
            }

            if (IsInListTextbox("tb" + t.Id, _listTbDoing))
            {
                var item = FindTextbox("tb" + t.Id, _listTbDoing);
                RemoveTextBox(item, lvDoing, _listTbDoing, ref _pointYDoing);
            }
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            var tb = sender as TextBox;
            string title = tb.Text;
            int indexTask = FindIndexTask(_listTask, title);

            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                tb.DoDragDrop(_listTask[indexTask], DragDropEffects.Move);
            }
            else
            {
                ShowTaskDetail(_listTask[indexTask]);
            }
        }
    }
}
