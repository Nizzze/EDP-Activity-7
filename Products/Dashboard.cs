using FontAwesome.Sharp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Products
{
    public partial class Dashboard : Form
    {
        
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        //Constructor
        public Dashboard()
        {
            InitializeComponent();
            LoadOrderData();
            LoadProductData();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 42);
            panelMenu.Controls.Add(leftBorderBtn);
            label1.Text = UserSession.Fname + " " + UserSession.Mname + ". " + UserSession.Lname;

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        //Structs
        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.MidnightBlue;
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Current Child Form Icon
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;

            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(0, 120, 215);
                currentBtn.ForeColor = Color.Snow;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Snow;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        
        private void Reset()
        {
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.FromArgb(51, 153, 255);
            lbTitle.Text = "Dashboard";
        }


        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDashboard.Controls.Add(childForm);
            panelDashboard.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbTitle.Text = childForm.Text;

        }


        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        
        

        
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconDashboard_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color1);
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            Reset();
        }

        private void iconProducts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color2);
            
        }


        private void iconReports_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color4);
            OpenChildForm(new Reports());
        }

        private void iconAbout_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color5);
            OpenChildForm(new About());

        }

        private void iconSettings_Click(object sender, EventArgs e)
        {
            /*ActivateButton(sender, RGBColors.color6);
            //OpenChildForm(new FormDashboard());*/
            UserSession.ClearLoggedInUser();
            new Login().Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void parrotLineGraph1_Click(object sender, EventArgs e)
        {

        }

        private void iconUsers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.color3);
            OpenChildForm(new Users());
        }

        private void LoadOrderData()
        {
            try
            {
                using (MySqlConnection conn = MySQL_Connection.GetConnection())
                {
                    conn.Open();

                    // Include 'Status' column in the SQL query
                    string query = "SELECT OrderID, OrderDate, CustomerName, TotalAmount FROM orders";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            // Create a DataTable to hold the data
                            DataTable dataTable = new DataTable();

                            // Fill the DataTable with the results from the query
                            adapter.Fill(dataTable);

                            // Clear existing columns in the DataGridView
                            dataGridView1.Columns.Clear();

                            // Set AutoGenerateColumns to false
                            dataGridView1.AutoGenerateColumns = false;

                            // Add columns to the DataGridView with specified widths

                            dataGridView1.Columns.Add("OrderID", "ID");
                            dataGridView1.Columns["OrderID"].DataPropertyName = "OrderID";
                            dataGridView1.Columns["OrderID"].Width = 30;

                            dataGridView1.Columns.Add("OrderDate", "OrderDate");
                            dataGridView1.Columns["OrderDate"].DataPropertyName = "OrderDate";
                            dataGridView1.Columns["OrderDate"].Width = 70; // Set the width to 100 pixels

                            dataGridView1.Columns.Add("CustomerName", "Customer Name");
                            dataGridView1.Columns["CustomerName"].DataPropertyName = "CustomerName";
                            dataGridView1.Columns["CustomerName"].Width =98;

                            dataGridView1.Columns.Add("TotalAmount", "Total Amount");
                            dataGridView1.Columns["TotalAmount"].DataPropertyName = "TotalAmount";
                            dataGridView1.Columns["TotalAmount"].Width = 65;

                            

                            // Bind the DataTable to the DataGridView
                            dataGridView1.DataSource = dataTable;
                        }
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //products

        private void LoadProductData()
        {
            try
            {
                using (MySqlConnection conn = MySQL_Connection.GetConnection())
                {
                    conn.Open();

                    // Include 'Status' column in the SQL query
                    string query = "SELECT ProductID, ProductName, CategoryID, Price FROM products";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            // Create a DataTable to hold the data
                            DataTable dataTable = new DataTable();

                            // Fill the DataTable with the results from the query
                            adapter.Fill(dataTable);

                            // Clear existing columns in the DataGridView
                            dataGridView2.Columns.Clear();

                            // Set AutoGenerateColumns to false
                            dataGridView2.AutoGenerateColumns = false;

                            // Add columns to the DataGridView with specified widths

                            dataGridView2.Columns.Add("ProductID", "ID");
                            dataGridView2.Columns["ProductID"].DataPropertyName = "ProductID";
                            dataGridView2.Columns["ProductID"].Width = 30;

                            dataGridView2.Columns.Add("ProductName", "Product Name");
                            dataGridView2.Columns["ProductName"].DataPropertyName = "ProductName";
                            dataGridView2.Columns["ProductName"].Width = 112; // Set the width to 100 pixels

                            dataGridView2.Columns.Add("CategoryID", "Category ID");
                            dataGridView2.Columns["CategoryID"].DataPropertyName = "CategoryID";
                            dataGridView2.Columns["CategoryID"].Width = 51;

                            dataGridView2.Columns.Add("Price", "Price");
                            dataGridView2.Columns["Price"].DataPropertyName = "Price";
                            dataGridView2.Columns["Price"].Width = 70;

                            // Bind the DataTable to the DataGridView
                            dataGridView2.DataSource = dataTable;
                        }
                    }

                    conn.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
