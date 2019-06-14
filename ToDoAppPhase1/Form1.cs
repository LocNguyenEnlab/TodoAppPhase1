using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoAppPhase1
{
    public partial class Form1 : Form
    {
        public int _pointYTodo = 0;
        public int _pointYDoing = 0;
        public int _pointYDone = 0;
        public List<Task> _listTask;
        public List<TextBox> _listTbTodo;
        public List<TextBox> _listTbDoing;
        public List<TextBox> _listTbDone;
        public int _index = 0;

        public Form1()
        {
            InitializeComponent();
            _listTask = new List<Task>();
            _listTbTodo = new List<TextBox>();
            _listTbDoing = new List<TextBox>();
            _listTbDone = new List<TextBox>();
        }

        private void CreateNewTextBox(Task t)
        {
            TextBox tb = new TextBox();
            tb.Location = new Point(0, _pointYTodo);
            tb.Text = t.Title;
            tb.Name = "tb" + _listTask.Count;
            tb.ReadOnly = true;
            tb.Size = new Size(325, 20);
            _listTbTodo.Add(tb);
            tb.MouseDown += TextBox_MouseDown;            

            tb.Show();
            lvToDo.Controls.Add(tb);
            _pointYTodo += 25;
            t.Id = _listTask.Count;
            _listTask.Add(t);
        }

        private void CreateAgainTextBox(Task t, ListView lv, ref int pointY, List<TextBox> listTb)
        {
            TextBox tb = new TextBox();
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

        private bool IsInListTbByName(string nameTb, List<TextBox> list)
        {
            foreach(var item in list)
            {
                if (item.Name == nameTb)
                {
                    return true;
                }
            }
            return false;
        }

        private TextBox FindTbByName(string name, List<TextBox> list)
        {
            foreach (var item in list)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }

        private int FindIndexTbInList(List<TextBox> list, TextBox tb)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i] == tb)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindIndexTaskByTitle(List<Task> list, string title)
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

        private void RemoveTextBoxFromListView(TextBox tb, ListView lv, List<TextBox> list, ref int pointY)
        {
            int indexTb = FindIndexTbInList(list, tb);

            if (indexTb == list.Count - 1)
            {
                pointY -= 25;
            } else
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
            FormAddTask f = new FormAddTask(t);
            f.Text = "Edit task";
            f.pd = new FormAddTask.PassData(PassData);
            f.show = new FormAddTask.ShowForm1(Show);
            f.Show();
            this.Hide();
        }
        
        private void DeleteTask(Task t)
        {
            int idTb = t.Id;
            if (IsInListTbByName("tb" + idTb, _listTbTodo))
            {
                TextBox tb = FindTbByName("tb" + idTb, _listTbTodo);
                RemoveTextBoxFromListView(tb, lvToDo, _listTbTodo, ref _pointYTodo);
                _listTask.Remove(t);
            }
            else if (IsInListTbByName("tb" + idTb, _listTbDoing)) {
                TextBox tb = FindTbByName("tb" + idTb, _listTbDoing);
                RemoveTextBoxFromListView(tb, lvDoing, _listTbDoing, ref _pointYDoing);
                _listTask.Remove(t);
            } 
            else if (IsInListTbByName("tb" + idTb, _listTbDone)) {
                TextBox tb = FindTbByName("tb" + idTb, _listTbDone);
                RemoveTextBoxFromListView(tb, lvDone, _listTbDone, ref _pointYDone);
                _listTask.Remove(t);
            }
        }

        private void ShowTaskDetail(Task t)
        {
            MessageBoxManager.Yes = "Edit";
            MessageBoxManager.No = "Delete";
            MessageBoxManager.Cancel = "Close";
            MessageBoxManager.Register();

            DialogResult dialogResult = MessageBox.Show(string.Format("Title: {0}\nDescription: {1}\nTime create: {2}", t.Title, t.Description, Convert.ToDateTime(t.TimeCreate)),
                "Task Detail", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            MessageBoxManager.Unregister();
            if (dialogResult == DialogResult.Yes)
            {
                EditTask(t);
            }
            else if (dialogResult == DialogResult.No)
            {                
                DialogResult resultConfirm = MessageBox.Show("Do you want to delete this task?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultConfirm == DialogResult.Yes)
                {
                    DeleteTask(t);
                }
            }
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            FormAddTask form2 = new FormAddTask();
            form2.pd = new FormAddTask.PassData(PassData);
            form2.show = new FormAddTask.ShowForm1(Show);
            form2.Show();
            this.Hide();
        }

        public void PassData(Task t)
        {
            if (t.Id == -1) //add new task
            {
                if (!IsDuplicateTask(_listTask, t))
                {
                    this.Show();
                    CreateNewTextBox(t);
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

        public void ShowForm1()
        {
            this.Show();
        }

        public void UpdateTextBox(Task t)
        {
            TextBox tb = new TextBox();

            if (FindTbByName("tb" + t.Id, _listTbTodo) != null)
            {
                tb = FindTbByName("tb" + t.Id, _listTbTodo);
                _listTbTodo[FindIndexTbInList(_listTbTodo, tb)].Text = t.Title;
            }
            else if (FindTbByName("tb" + t.Id, _listTbDoing) != null)
            {
                tb = FindTbByName("tb" + t.Id, _listTbDoing);
                _listTbDoing[FindIndexTbInList(_listTbDoing, tb)].Text = t.Title;
            }
            else if (FindTbByName("tb" + t.Id, _listTbDone) != null)
            {
                tb = FindTbByName("tb" + t.Id, _listTbDone);
                _listTbDone[FindIndexTbInList(_listTbDone, tb)].Text = t.Title;
            }
        }

        private void LvDoing_DragEnter(object sender, DragEventArgs e)
        {
            int idTb = ((Task)e.Data.GetData(e.Data.GetFormats()[0])).Id;

            if (!IsInListTbByName("tb" + idTb, _listTbDoing))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void LvDone_DragEnter(object sender, DragEventArgs e)
        {
            int idTb = ((Task)e.Data.GetData(e.Data.GetFormats()[0])).Id;

            if (!IsInListTbByName("tb" + idTb, _listTbDone))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void LvTodo_DragEnter(object sender, DragEventArgs e)
        {
            int idTb = ((Task)e.Data.GetData(e.Data.GetFormats()[0])).Id;
            
            if(!IsInListTbByName("tb" + idTb, _listTbTodo))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void LvDoing_DragDrop(object sender, DragEventArgs e)
        {
            ListView lv2 = sender as ListView;
            TextBox tb = new TextBox();
            Task t = new Task();
            t = (Task)e.Data.GetData(e.Data.GetFormats()[0]);
            CreateAgainTextBox(t, lv2, ref _pointYDoing, _listTbDoing);  
            
            if (IsInListTbByName("tb" + t.Id, _listTbTodo))
            {
                var item = FindTbByName("tb" + t.Id, _listTbTodo);
                RemoveTextBoxFromListView(item, lvToDo, _listTbTodo, ref _pointYTodo);
            }

            if (IsInListTbByName("tb" + t.Id, _listTbDone))
            {
                var item = FindTbByName("tb" + t.Id, _listTbDone);
                RemoveTextBoxFromListView(item, lvDone, _listTbDone, ref _pointYDone);
            }
        }

        private void LvDone_DragDrop(object sender, DragEventArgs e)
        {
            ListView lv2 = sender as ListView;
            TextBox tb = new TextBox();
            Task t = new Task();
            t = (Task)e.Data.GetData(e.Data.GetFormats()[0]);
            CreateAgainTextBox(t, lv2, ref _pointYDone, _listTbDone);
            
            if (IsInListTbByName("tb" + t.Id, _listTbTodo))
            {
                var item = FindTbByName("tb" + t.Id, _listTbTodo);
                RemoveTextBoxFromListView(item, lvToDo, _listTbTodo, ref _pointYTodo);
            }

            if (IsInListTbByName("tb" + t.Id, _listTbDoing))
            {
                var item = FindTbByName("tb" + t.Id, _listTbDoing);
                RemoveTextBoxFromListView(item, lvDoing, _listTbDoing, ref _pointYDoing);
            }
        }

        private void LvTodo_DragDrop(object sender, DragEventArgs e)
        {
            ListView lv2 = sender as ListView;
            TextBox tb = new TextBox();
            Task t = new Task();
            t = (Task)e.Data.GetData(e.Data.GetFormats()[0]);
            CreateAgainTextBox(t, lv2, ref _pointYTodo, _listTbTodo);

            if (IsInListTbByName("tb" + t.Id, _listTbDone))
            {
                var item = FindTbByName("tb" + t.Id, _listTbDone);
                RemoveTextBoxFromListView(item, lvDone, _listTbDone, ref _pointYDone);
            }

            if (IsInListTbByName("tb" + t.Id, _listTbDoing))
            {
                var item = FindTbByName("tb" + t.Id, _listTbDoing);
                RemoveTextBoxFromListView(item, lvDoing, _listTbDoing, ref _pointYDoing);
            }
        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            string nameTb = tb.Name;
            int idTb = Convert.ToInt32(nameTb.Replace("tb", ""));
            string title = tb.Text;
            int indexTask = FindIndexTaskByTitle(_listTask, title);

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
