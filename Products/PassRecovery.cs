using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Products
{
    public partial class PassRecovery : Form
    {
        public PassRecovery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string emailAddress = emailBox.Text;

            if (IsValidEmail(emailAddress))
            {
                if (IsEmailInDatabase(emailAddress))
                {
                   
                    // Redirect to Form4 (New Password)
                    Verification newPasswordForm = new Verification(emailAddress);
                    newPasswordForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect email address.");
                }
            }
            else
            {
                MessageBox.Show("Invalid email format.");
            }
            
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsEmailInDatabase(string email)
        {
            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                    

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }


            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
