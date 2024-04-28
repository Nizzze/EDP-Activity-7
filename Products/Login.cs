using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using MySql.Data.MySqlClient;


namespace Products
{
    public partial class Login : Form
    {
        private string firstname = "";
        private string lastname = "";
        private string middlename = "";

        public Login()
        {
            InitializeComponent();
            passwordBox.UseSystemPasswordChar = true;
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            new PassRecovery().Show();
            this.Hide();
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameBox.Text;
            string password = passwordBox.Text;
            

            // Authenticate user and get user type and status
            Tuple<string, string> userAuthenticationResult = AuthenticateAndGetUserTypeAndStatus(username, password);

            if (userAuthenticationResult != null)
            {
                string userType = userAuthenticationResult.Item1;
                string userStatus = userAuthenticationResult.Item2;

                if (userStatus == "Active")
                {
                    // Set the logged-in user and update status to 'Active' in the database
                    UserSession.SetLoggedInUser(username, userType, firstname, lastname, middlename);
                    UpdateUserStatusToActive(username);
                    firstname = "";
                    lastname = "";
                    middlename = "";

                    // Redirect based on user type
                    if (userType == "Admin")
                    {
                        // Redirect to Admin Dashboard
                        new Dashboard().Show();
                        this.Hide();
                    }
                    else if (userType == "Employee")
                    {
                        // Redirect to Employee Dashboard
                        /*Form13 employeeDashboardForm = new Form13();
                        employeeDashboardForm.Show();
                        this.Hide();*/
                    }
                }
                else if (userStatus == "Inactive")
                {
                    MessageBox.Show("This account is inactive. Please contact the administrator.");
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.");
                }
            }
        }

        private Tuple<string, string> AuthenticateAndGetUserTypeAndStatus(string username, string password)
        {
            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "SELECT Firstname, Lastname, Middlename, Usertype, Status FROM users WHERE username = @Username AND password = @Password";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string userType = reader["Usertype"].ToString();
                                string userStatus = reader["Status"].ToString();
                                firstname = reader["Firstname"].ToString();
                                lastname = reader["Lastname"].ToString();
                                middlename = reader["Middlename"].ToString();

                                return new Tuple<string, string>(userType, userStatus);
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Password");
                                return null; // Authentication failed
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }

        


        private void UpdateUserStatusToActive(string username)
        {
            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE users SET Status = 'Active' WHERE username = @Username";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void usernameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                passwordBox.UseSystemPasswordChar = false;
            }
            else
            {
                passwordBox.UseSystemPasswordChar = true;
            }
        }
    }
}
