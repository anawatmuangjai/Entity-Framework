using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace BasicEntityFramework
{
    public partial class FormMain : Form
    {
        private int _mode;
        private bool _isAvailable;

        public FormMain()
        {
            InitializeComponent();
            InitializeData();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            Clear();
            SetMode(Mode.Add);
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            Clear();
            EditData();
            SetMode(Mode.Edit);
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (_mode == (int)Mode.Add)
                Create();
            else
                UpdateData();
            
            ReadData();
            Clear();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (DgvUser.Rows.Count == 0) return;

            var userId = DgvUser.CurrentRow.Cells[0].Value.ToString();

            DeleteData(userId);
            ReadData();
            Clear();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void ButtonView_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            ReadData();

            Cursor = Cursors.Default;
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            ReadData(TextBoxSearch.Text);
        }

        private void ComboBoxAvailable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxAvailable.SelectedIndex > 0)
            {
                var item = (KeyValuePair<bool, string>)ComboBoxAvailable.SelectedItem;
                _isAvailable = item.Key;
            }
        }

        private void InitializeData()
        {
            ComboBoxAvailable.Items.Clear();
            ComboBoxAvailable.Items.Add(new KeyValuePair<bool, string>(false, "Please select"));
            ComboBoxAvailable.Items.Add(new KeyValuePair<bool, string>(false,"Yes"));
            ComboBoxAvailable.Items.Add(new KeyValuePair<bool, string>(false,"No"));
            ComboBoxAvailable.ValueMember = "Key";
            ComboBoxAvailable.DisplayMember = "Value";
            ComboBoxAvailable.SelectedIndex = 0;
        }

        private void Create()
        {
            using (var context = new PARTSEntities())
            {
                var userDetail = new SP_UserDetail
                {
                    UserID = TextBoxUserId.Text,
                    UserName = TextBoxUserName.Text,
                    Password = TextBoxPassword.Text,
                    UserFunc = TextBoxFunction.Text,
                    Show = _isAvailable
                };

                context.SP_UserDetail.Add(userDetail);
                context.SaveChanges();
            }
        }

        private void ReadData()
        {
            using (var context = new PARTSEntities())
            {
                var users = context.SP_UserDetail
                    .Select(u => new
                    {
                        u.UserID,
                        u.UserName,
                        u.Password,
                        u.UserFunc
                    }).ToList();

                DgvUser.DataSource = users;
            }
        }

        private void ReadData(string userId)
        {
            using (var context = new PARTSEntities())
            {
                var users = context.SP_UserDetail
                    .Where(u => u.UserID == userId)
                    .Select(u => new
                    {
                        u.UserID,
                        u.UserName,
                        u.Password,
                        u.UserFunc
                    }).ToList();

                DgvUser.DataSource = users;
            }
        }

        private void UpdateData()
        {
            using (var context = new PARTSEntities())
            {
                var users = context.SP_UserDetail
                    .Where(u => u.UserID == TextBoxUserId.Text).FirstOrDefault();

                users.UserName = TextBoxUserName.Text;
                users.Password = TextBoxPassword.Text;
                users.UserFunc = TextBoxFunction.Text;
                users.Show = _isAvailable;

                context.SaveChanges();
            }
        }

        private void DeleteData(string userId)
        {
            using (var context = new PARTSEntities())
            {
                var users = context.SP_UserDetail
                    .Where(u => u.UserID == userId).FirstOrDefault();

                if (users != null)
                {
                    context.SP_UserDetail.Remove(users);
                    context.SaveChanges();
                }
            }
        }

        private void Clear()
        {
            TextBoxUserId.Clear();
            TextBoxUserName.Clear();
            TextBoxPassword.Clear();
            TextBoxFunction.Clear();

            ComboBoxAvailable.SelectedIndex = 0;

            PanelLeft.Enabled = false;
            _mode = -1;
        }

        private void SetMode(Mode mode)
        {
            _mode = (int)mode;
            PanelLeft.Enabled = true;
        }

        private void EditData()
        {
            if (DgvUser.Rows.Count == 0) return;

            TextBoxUserId.Text = DgvUser.CurrentRow.Cells[0].Value.ToString();
            TextBoxUserName.Text = DgvUser.CurrentRow.Cells[1].Value.ToString();
            TextBoxPassword.Text = DgvUser.CurrentRow.Cells[2].Value.ToString();
            TextBoxFunction.Text = DgvUser.CurrentRow.Cells[3].Value.ToString();

            TextBoxUserId.Enabled = false;
        }


    }
}
