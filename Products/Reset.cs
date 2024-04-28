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
    public partial class Reset : Form
    {
        private string emailAddress;
        public Reset(string emailAddress)
        {
            InitializeComponent();
            this.emailAddress = emailAddress;
            passwordBox.UseSystemPasswordChar = true;
            confirmBox.UseSystemPasswordChar = true;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                confirmBox.UseSystemPasswordChar = false;
            }
            else
            {
                confirmBox.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string newPassword = passwordBox.Text;
            string confirmPassword = confirmBox.Text;

            if (newPassword == confirmPassword)
            {
                if (UpdatePasswordInDatabase(newPassword))
                {
                    MessageBox.Show("Password updated successfully!");
                    // Redirect to login form (Form1)
                    new Login().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Failed to update password.");
                }
            }
            else
            {
                MessageBox.Show("Password not confirmed.");
            }
        }

        private bool UpdatePasswordInDatabase(string newPassword)
        {
            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE users SET password = @Password WHERE Email = @Email";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Password", newPassword);
                        command.Parameters.AddWithValue("@Email", emailAddress);

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
