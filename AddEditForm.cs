using System;
using System.Drawing; // საჭიროა Point, Size
using System.Windows.Forms; // საჭიროა Form, Label, TextBox, Button, TableLayoutPanel და ა.შ.

namespace MyProject // !!! დარწმუნდი, რომ ეს namespace ზუსტად ემთხვევა შენი პროექტის სახელს (მაგ. 'MyProject') !!!
{
    public partial class AddEditForm : Form
    {
        public MyObject NewObject { get; private set; } // NewObject და MyObject-ის სახელები

        // ესენი არის UI ელემენტები, რომლებსაც კოდით ვქმნით
        private TextBox _prop1TextBox;
        private TextBox _prop2TextBox;
        private TextBox _prop3TextBox;
        private TextBox _prop4TextBox;
        private Button _saveButton;

        public AddEditForm()
        {
            // InitializeComponent(); // !!! ეს ხაზი უნდა იყოს წაშლილი, რადგან UI-ს კოდით ვქმნით !!!
            NewObject = new MyObject(); // ახალი ობიექტის ინსტანცირება
            InitializeUI(); // UI ელემენტების ინიციალიზაცია კოდით
        }

        private void InitializeUI()
        {
            this.Text = "ახალი ობიექტის დამატება"; // !!! გამოცდაზე: შეცვალე, მაგ. "ახალი მანქანის დამატება"
            this.Size = new Size(400, 300);
            this.MinimumSize = new Size(350, 250); // მინიმალური ზომა, რომ არ გაქრეს ელემენტები
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // TableLayoutPanel-ის შექმნა ელემენტების ლამაზად განლაგებისთვის
            TableLayoutPanel mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Padding = new Padding(10);
            mainLayout.ColumnCount = 2;
            mainLayout.RowCount = 5; // 4 თვისება (row) + 1 ღილაკის row
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // პირველი სვეტი (ლეიბლებისთვის) ავტომატური ზომა
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // მეორე სვეტი (TextBox-ებისთვის) მთელი სიგანე
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // ავტომატური სიმაღლე
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // ბოლო row-ს დაიკავებს დარჩენილ ადგილს

            // Label-ების შექმნა და დამატება
            // !!! გამოცდაზე: შეცვალე Label-ების ტექსტები !!!
            mainLayout.Controls.Add(new Label() { Text = "თვისება 1:", Anchor = AnchorStyles.Right }, 0, 0);
            mainLayout.Controls.Add(new Label() { Text = "თვისება 2:", Anchor = AnchorStyles.Right }, 0, 1);
            mainLayout.Controls.Add(new Label() { Text = "თვისება 3:", Anchor = AnchorStyles.Right }, 0, 2);
            mainLayout.Controls.Add(new Label() { Text = "თვისება 4:", Anchor = AnchorStyles.Right }, 0, 3);

            // TextBox-ების შექმნა და დამატება
            _prop1TextBox = new TextBox() { Dock = DockStyle.Fill };
            _prop2TextBox = new TextBox() { Dock = DockStyle.Fill };
            _prop3TextBox = new TextBox() { Dock = DockStyle.Fill };
            _prop4TextBox = new TextBox() { Dock = DockStyle.Fill };

            mainLayout.Controls.Add(_prop1TextBox, 1, 0); // Property1-ის TextBox
            mainLayout.Controls.Add(_prop2TextBox, 1, 1); // Property2-ის TextBox
            mainLayout.Controls.Add(_prop3TextBox, 1, 2); // Property3-ის TextBox
            mainLayout.Controls.Add(_prop4TextBox, 1, 3); // Property4-ის TextBox

            this.Controls.Add(mainLayout); // TableLayoutPanel-ის დამატება Form-ზე

            // შენახვის ღილაკი
            _saveButton = new Button();
            _saveButton.Text = "შენახვა";
            _saveButton.Size = new Size(100, 30);
            _saveButton.Dock = DockStyle.Bottom;
            _saveButton.Margin = new Padding(10);
            _saveButton.Click += SaveButton_Click; // Event Handler-ის მიბმა
            this.Controls.Add(_saveButton);

            _saveButton.BringToFront(); // ღილაკი წინა პლანზე (რომ ჩანდეს)
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // ვალიდაცია: შემოწმება, რომ სავალდებულო ველები არ არის ცარიელი
            if (string.IsNullOrWhiteSpace(_prop1TextBox.Text))
            {
                MessageBox.Show("თვისება 1 არ შეიძლება იყოს ცარიელი.", "ვალიდაციის შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_prop2TextBox.Text)) // Property2 სავალდებულოა SQL-შიც
            {
                MessageBox.Show("თვისება 2 არ შეიძლება იყოს ცარიელი.", "ვალიდაციის შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // მნიშვნელობების მინიჭება
            NewObject.Property1 = _prop1TextBox.Text;
            NewObject.Property2 = _prop2TextBox.Text;
            NewObject.Property3 = _prop3TextBox.Text; // Property3 შეიძლება იყოს null (ცარიელი ველი)
            NewObject.Property4 = _prop4TextBox.Text; // Property4 შეიძლება იყოს null (ცარიელი ველი)

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
