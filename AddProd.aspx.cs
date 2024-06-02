using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Web.UI;

namespace Team_poora
{
    public partial class AddProd : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // You can add any necessary logic for page load here
        }

        protected void BtnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch connection string from the web.config file
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert into products table
                    string productInsertQuery = "INSERT INTO products (productid, productname, productdesc, productprice) VALUES (@productid, @productname, @productdesc, @productprice)";
                    using (MySqlCommand productCommand = new MySqlCommand(productInsertQuery, connection))
                    {
                        productCommand.Parameters.AddWithValue("@productid", txtproductid.Text);
                        productCommand.Parameters.AddWithValue("@productname", txtproductname.Text);
                        productCommand.Parameters.AddWithValue("@productdesc", txtproductdesc.Text);
                        productCommand.Parameters.AddWithValue("@productprice", decimal.Parse(txtproductprice.Text));
                        productCommand.ExecuteNonQuery();
                    }

                    // Insert into stocks table
                    string stockInsertQuery = "INSERT INTO stock (productid, stocks) VALUES (@productid, @stocks)";
                    using (MySqlCommand stockCommand = new MySqlCommand(stockInsertQuery, connection))
                    {
                        stockCommand.Parameters.AddWithValue("@productid", txtproductid.Text);
                        stockCommand.Parameters.AddWithValue("@stocks", Convert.ToInt32(txtstock.Text));
                        stockCommand.ExecuteNonQuery();
                    }
                }

                // Redirect back to Products.aspx after successful insertion
                Response.Redirect("Products.aspx");
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            // Redirect back to Products.aspx when the "Back" button is clicked
            Response.Redirect("Products.aspx");
        }
    }
}