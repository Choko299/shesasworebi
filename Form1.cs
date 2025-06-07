using System;
using System.Collections.Generic;
using System.Drawing; // საჭიროა Point, Size
using System.Windows.Forms; // საჭიროა Form, ListBox, Button და ა.შ.

namespace MyProject // ეს არის შენი პროექტის ნაგულისხმევი namespace
{
    public partial class Form1 : Form
    {
        private DatabaseHelper _dbHelper;
        private string _tableName;
        private ListBox _objectListBox; // ListBox-ს კოდით ვქმნით
        private Button _addButton;
        private Button _deleteButton;

        public Form1()
        {
            // InitializeComponent(); // ეს ხაზი უნდა იყოს წაშლილი (რადგან Designer.cs-ს ვშლით)

            // !!! გამოცდაზე: აქ შეცვალე ცხრილის სახელი ზუსტად იმით, რაც SQL-ში მიუთითე (Table1-ის ნაცვლად) !!!
            _tableName = "Table1"; // მაგალითად: "Cars", "Movies", "Books"

            _dbHelper = new DatabaseHelper(_tableName);
            InitializeUI(); // UI ელემენტების ინიციალიზაცია კოდით
            LoadObjects(); // ობიექტების ჩატვირთვა გაშვებისას
        }

        private void InitializeUI()
        {
            this.Text = "ზოგადი მენეჯერი"; // !!! გამოცდაზე: შეცვალე, მაგ. "მანქანების მენეჯერი"
            this.Size = new Size(800, 450);
            this.MinimumSize = new Size(400, 300); // მინიმალური ზომა, რომ არ გაქრეს ელემენტები
            this.StartPosition = FormStartPosition.CenterScreen;

            // ListBox-ის შექმნა
            _objectListBox = new ListBox();
            _objectListBox.Name = "ObjectListBox"; // სახელი, რომელსაც შემდგომში ვიპოვით, თუ საჭიროა
            _objectListBox.Dock = DockStyle.Fill;
            _objectListBox.Margin = new Padding(10);
            this.Controls.Add(_objectListBox);

            // ღილაკების პანელი
            Panel buttonPanel = new Panel();
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Height = 50;
            buttonPanel.Padding = new Padding(10);
            this.Controls.Add(buttonPanel);

            // დამატების ღილაკი
            _addButton = new Button();
            _addButton.Text = "ობიექტის დამატება"; // !!! გამოცდაზე: შეცვალე, მაგ. "მანქანის დამატება"
            _addButton.Size = new Size(150, 30);
            _addButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            _addButton.Click += AddObject_Click; // Event Handler-ის მიბმა
            buttonPanel.Controls.Add(_addButton);

            // წაშლის ღილაკი
            _deleteButton = new Button();
            _deleteButton.Text = "ობიექტის წაშლა"; // !!! გამოცდაზე: შეცვალე, მაგ. "მანქანის წაშლა"
            _deleteButton.Size = new Size(150, 30);
            _deleteButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            _deleteButton.Click += DeleteObject_Click; // Event Handler-ის მიბმა
            buttonPanel.Controls.Add(_deleteButton);

            // ღილაკების პოზიციონირება (მარჯვნივ გასწორება)
            // ეს პოზიციები დამოკიდებულია buttonPanel-ის ზომაზე.
            // უზრუნველყოფს, რომ ღილაკები ყოველთვის მარჯვნივ იყოს.
            buttonPanel.SizeChanged += (sender, e) =>
            {
                _deleteButton.Location = new Point(buttonPanel.Width - _deleteButton.Width - 10, 10);
                _addButton.Location = new Point(_deleteButton.Left - _addButton.Width - 10, 10);
            };
            // პირველადი განლაგება
            _deleteButton.Location = new Point(buttonPanel.Width - _deleteButton.Width - 10, 10);
            _addButton.Location = new Point(_deleteButton.Left - _addButton.Width - 10, 10);


            _objectListBox.BringToFront(); // დარწმუნდით, რომ ListBox ჩანს
        }

        private void LoadObjects()
        {
            if (_objectListBox != null)
            {
                _objectListBox.DataSource = null; // Clear previous data source
                List<MyObject> objects = _dbHelper.GetAllObjects(); // !!! გამოცდაზე: MyObject-ის სახელი
                _objectListBox.DataSource = objects;
            }
        }

        private void AddObject_Click(object sender, EventArgs e)
        {
            using (AddEditForm addEditForm = new AddEditForm()) // !!! გამოცდაზე: AddEditForm-ის სახელი
            {
                if (addEditForm.ShowDialog() == DialogResult.OK)
                {
                    _dbHelper.AddObject(addEditForm.NewObject); // !!! გამოცდაზე: NewObject-ის სახელი
                    LoadObjects(); // სიის განახლება
                }
            }
        }

        private void DeleteObject_Click(object sender, EventArgs e)
        {
            if (_objectListBox != null && _objectListBox.SelectedItem is MyObject selectedObject) // !!! გამოცდაზე: MyObject-ის სახელი
            {
                // !!! გამოცდაზე: შეცვალე ტექსტი ობიექტის ტიპის მიხედვით
                DialogResult result = MessageBox.Show($"დარწმუნებული ხართ, რომ გსურთ '{selectedObject.Property1}' ობიექტის წაშლა?", "წაშლის დადასტურება", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _dbHelper.DeleteObject(selectedObject.Id);
                    LoadObjects(); // სიის განახლება
                }
            }
            else
            {
                MessageBox.Show("გთხოვთ, აირჩიოთ ობიექტი წასაშლელად.", "ობიექტი არ არის არჩეული", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
