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
    public partial class Verification : Form
    {
        private string emailAddress;
        public Verification( string emailAddress)
        {
            InitializeComponent();
            this.emailAddress = emailAddress;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Assuming nameBox contains the full name in the format "FirstName MiddleName LastName"
            string fullName = nameBox.Text;

            // Split the full name into first, middle, and last names
            string[] names = fullName.Split(' ');

            // Ensure there are at least three parts (first, middle, last names)
            if (names.Length >= 3)
            {
                string firstName = names[0];
                string middleName = names[1];
                string lastName = names[2];

                

                // Call the function to check if the email and name match in the database
                bool isInDatabase = AreNameAndEmailMatching(emailAddress, firstName, middleName, lastName);

                // Use the result as needed
                if (isInDatabase)
                {
                    MessageBox.Show("Email and Name match in the database.");
                    Reset newPasswordForm = new Reset(emailAddress);
                    newPasswordForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email and Name do not match in the database.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid full name in the format 'FirstName MiddleName LastName'.");
            }
        }


        private bool AreNameAndEmailMatching(string email, string firstName, string middleName, string lastName)
        {
            using (MySqlConnection conn = MySQL_Connection.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE Email = @Email AND FirstName = @Firstname AND Middlename = @MiddleName AND Lastname = @LastName";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@FirstName", firstName);
                        command.Parameters.AddWithValue("@MiddleName", middleName);
                        command.Parameters.AddWithValue("@LastName", lastName);

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }
    }
}
