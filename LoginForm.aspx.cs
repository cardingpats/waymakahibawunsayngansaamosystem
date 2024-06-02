using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI;

namespace Team_poora
{
    public partial class LoginForm : System.Web.UI.Page
    {
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM loginform WHERE username = @Username AND password = @Password";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                    if (count > 0)
                    {
                        // Successful login, redirect to another page or perform any action.
                        Response.Redirect("Products.aspx");
                    }
                    else
                    {
                        // Failed login, show error message or perform any action.
                        Response.Write("Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred: " + ex.Message);
            }
        }
    }
}