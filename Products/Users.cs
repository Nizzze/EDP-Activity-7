using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Products
{
    public partial class Users : Form
    {

        private static bool addCondition = true;
        private static string userId = "";
        public Users()
        {
            InitializeComponent();
            LoadUserData();
            //typeComboBox1.DataSource = null;
            typeComboBox1.Items.Add("Admin");
            typeComboBox1.Items.Add("Employee");

            // Attach the KeyPress event handler
            statusComboBox1.KeyPress += statusComboBox1_KeyPress;

            // Add items to the ComboBox
            statusComboBox1.Items.Add("Active");
            statusComboBox1.Items.Add("Inactive");



        }




        private void LoadUserData()
        {
            try
            {
                using (MySqlConnection conn = MySQL_Connection.GetConnection())
                {
                    conn.Open();

                    // Include 'Status' column in the SQL query
                    string query = "SELECT id, FirstName, MiddleName, LastName, Email, username, password, Status, Usertype FROM users";

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

                            dataGridView1.Columns.Add("id", "ID");
                            dataGridView1.Columns["id"].DataPropertyName = "id";
                            dataGridView1.Columns["id"].Width = 30; 

                            dataGridView1.Columns.Add("FirstName", "FirstName");
                            dataGridView1.Columns["FirstName"].DataPropertyName = "FirstName";
                            dataGridView1.Columns["FirstName"].Width = 90; // Set the width to 100 pixels

                            dataGridView1.Columns.Add("MiddleName", "MiddleName");
                            dataGridView1.Columns["MiddleName"].DataPropertyName = "MiddleName";
                            dataGridView1.Columns["MiddleName"].Width = 90;

                            dataGridView1.Columns.Add("LastName", "LastName");
                            dataGridView1.Columns["LastName"].DataPropertyName = "LastName";
                            dataGridView1.Columns["LastName"].Width = 90;

                            dataGridView1.Columns.Add("Email", "Email");
                            dataGridView1.Columns["Email"].DataPropertyName = "Email";
                            dataGridView1.Columns["Email"].Width = 140;

                            dataGridView1.Columns.Add("Username", "Username");
                            dataGridView1.Columns["Username"].DataPropertyName = "username";
                            dataGridView1.Columns["Username"].Width = 70;

                            dataGridView1.Columns.Add("Password", "Password");
                            dataGridView1.Columns["Password"].DataPropertyName = "password";
                            dataGridView1.Columns["Password"].Width = 70;

                            dataGridView1.Columns.Add("Status", "Status");
                            dataGridView1.Columns["Status"].DataPropertyName = "Status";
                            dataGridView1.Columns["Status"].Width = 70;

                            dataGridView1.Columns.Add("Usertype", "User Type");
                            dataGridView1.Columns["Usertype"].DataPropertyName = "Usertype";
                            dataGridView1.Columns["Usertype"].Width = 70;

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

        private void statusComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cancel any key presses to prevent typing into the ComboBox
            e.Handled = true;
        }
        private void statusComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void clearInput()
        {
            fNameBox.Clear();
            mNameBox.Clear();
            lNameBox.Clear();
            usernameBox.Clear();
            passwordBox.Clear();
            emailBox.Clear();
            typeComboBox1.SelectedIndex = -1;
            statusComboBox1.SelectedIndex = -1;
            addCondition = true;
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = this.dataGridView1.Rows[e.RowIndex];


                userId = selectedRow.Cells["id"].Value?.ToString();
                fNameBox.Text = selectedRow.Cells["FirstName"].Value?.ToString();
                mNameBox.Text = selectedRow.Cells["MiddleName"].Value?.ToString();
                lNameBox.Text = selectedRow.Cells["LastName"].Value?.ToString();
                usernameBox.Text = selectedRow.Cells["Username"].Value?.ToString();
                passwordBox.Text = selectedRow.Cells["Password"].Value?.ToString();
                emailBox.Text = selectedRow.Cells["Email"].Value?.ToString();
               
                string typeValue = selectedRow.Cells["Usertype"].Value?.ToString();
                typeComboBox1.SelectedItem = typeValue;

                string statusValue = selectedRow.Cells["Status"].Value?.ToString();
                statusComboBox1.SelectedItem = statusValue;

                addCondition = false;

            }

        }

        //ADD

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            if (addCondition == false)
            {
                clearInput();
                addCondition = true;

            }
            else if (!string.IsNullOrEmpty(fNameBox.Text) &&
                    !string.IsNullOrEmpty(mNameBox.Text) &&
                    !string.IsNullOrEmpty(lNameBox.Text) &&
                    !string.IsNullOrEmpty(usernameBox.Text) &&
                    !string.IsNullOrEmpty(passwordBox.Text) &&
                    !string.IsNullOrEmpty(emailBox.Text) &&
                    typeComboBox1.SelectedIndex != -1 &&
                    statusComboBox1.SelectedIndex != -1 &&
                    addCondition == true)
            {
                // Get values from textboxes and combobox
                string firstName = fNameBox.Text;
                string middleName = mNameBox.Text;
                string lastName = lNameBox.Text;
                string email = emailBox.Text;
                string username = usernameBox.Text;
                string password = passwordBox.Text;
                string userType = typeComboBox1.SelectedItem.ToString(); // Assumes user types are listed in the combobox
                string status = statusComboBox1.SelectedItem.ToString();

                // Validate input (you can add more validation as needed)

                // Add employee to the database
                if (AddEmployeeToDatabase(firstName, middleName, lastName, email, username, password, userType, status))
                {
                    MessageBox.Show("User account added successfully!");
                    // You can add additional logic or clear the input fields as needed

                    // Clear the textboxes
                    clearInput();
                    LoadUserData(); 
                }
                else
                {
                    MessageBox.Show("Failed to add employee account.");
                }

               
            }
            else /*if (!string.IsNullOrEmpty(statusBox.Text))*/
            {
                MessageBox.Show("Incomplete Input to Add Account");
            }

        }

            




        

        private bool AddEmployeeToDatabase(string firstName, string middleName, string lastName, string email,  string username, string password, string userType, string status)
        {

            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                    try 
                    {
                        conn.Open();

                        string query = "INSERT INTO users (Firstname, Middlename, Lastname, Email,  username, password, Status, Usertype) " +
                                       "VALUES (@FirstName, @MiddleName, @LastName, @Email, @Username, @Password, @Status, @UserType)";

                        using (MySqlCommand command = new MySqlCommand(query, conn))
                        {
                            command.Parameters.AddWithValue("@FirstName", firstName);
                            command.Parameters.AddWithValue("@MiddleName", middleName);
                            command.Parameters.AddWithValue("@LastName", lastName);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@Password", password);
                            command.Parameters.AddWithValue("@UserType", userType);
                            command.Parameters.AddWithValue("@Status", status);

                        int rowsAffected = command.ExecuteNonQuery();

                            return rowsAffected > 0;
                        }

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }

            }
            

        }

        //Search

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void PerformSearch()
        {
            // Get the search text from the TextBox
            string searchText = searchBox.Text.Trim();

            // Execute the query with the filter condition
            string query = $"SELECT id, FirstName, MiddleName, LastName, Email, username, password, Status, Usertype FROM users WHERE " +
                           $"FirstName LIKE '%{searchText}%' OR " +
                           $"MiddleName LIKE '%{searchText}%' OR " +
                           $"LastName LIKE '%{searchText}%' OR " +
                           $"Email LIKE '%{searchText}%' OR " +
                           $"username LIKE '%{searchText}%'";

            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the filtered DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show("Error connecting to the database: " + ex.Message);
                }
            }
        }

        //EDIT
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fNameBox.Text) &&
                !string.IsNullOrEmpty(mNameBox.Text) &&
                !string.IsNullOrEmpty(lNameBox.Text) &&
                !string.IsNullOrEmpty(usernameBox.Text) &&
                !string.IsNullOrEmpty(passwordBox.Text) &&
                !string.IsNullOrEmpty(emailBox.Text) &&
                typeComboBox1.SelectedIndex != -1 &&
                statusComboBox1.SelectedIndex != -1 &&
                addCondition == false)
            {
                int userToEdit = 0;
                if (int.TryParse(userId, out userToEdit))
                {
                    // Get values from textboxes and combobox

                    string firstName = fNameBox.Text;
                    string middleName = mNameBox.Text;
                    string lastName = lNameBox.Text;
                    string email = emailBox.Text;
                    string username = usernameBox.Text;
                    string password = passwordBox.Text;
                    string userType = typeComboBox1.SelectedItem.ToString(); // Assumes user types are listed in the combobox
                    string status = statusComboBox1.SelectedItem.ToString();


                    // Validate input (you can add more validation as needed)

                    // Add employee to the database
                    if (UpdateUserInDatabase(userToEdit, firstName, middleName, lastName, email, username, password, userType, status))
                    {
                        MessageBox.Show("Employee account added successfully!");
                        // You can add additional logic or clear the input fields as needed

                        // Clear the textboxes
                        clearInput();
                        LoadUserData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add employee account.");
                    }

                }
                else
                {
                    MessageBox.Show("Invalid Employee ID.");
                }



            }
            else /*if (!string.IsNullOrEmpty(statusBox.Text))*/
            {
                MessageBox.Show("Incomplete Input to Update Account");
            }

        }


        private bool UpdateUserInDatabase(int userIDEdit, string firstName, string middleName, string lastName, string email,  string username, string password, string userType, string status)
        {
            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {

                try
                {

                    conn.Open();

                    string query = "UPDATE users SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, " +
                                   "Email = @Email, Username = @Username, Password = @Password, UserType = @UserType, Status = @Status " +
                                   "WHERE id = @UserId";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@UserId", userIDEdit);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@MiddleName", middleName);
                        command.Parameters.AddWithValue("@LastName", lastName);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@UserType", userType);
                        command.Parameters.AddWithValue("@Status", status);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }

        
    }
    }

