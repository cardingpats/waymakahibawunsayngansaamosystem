using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace Team_poora
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Define the SQL query to join products and stocks tables
                    string query = "SELECT p.productid, p.productname, p.productdesc, FORMAT(p.productprice, 2) AS productprice, s.stocks " +
                                   "FROM products p " +
                                   "INNER JOIN stock s ON p.productid = s.productid";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    connection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        GridViewProducts.DataSource = dt;
                        GridViewProducts.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        protected void BtnAddProduct_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProd.aspx");
        }

        protected void GridViewProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = e.RowIndex;
                string productid = GridViewProducts.DataKeys[rowIndex]["productid"].ToString();

                // Call a method to delete the product with the given productId from the database
                DeleteProduct(productid);

                // Rebind the GridView to refresh the product list
                BindProducts();
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred while deleting product: " + ex.Message);
            }
        }

        private void DeleteProduct(string productid)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // First, delete the stock records associated with the product
                    string deleteStockQuery = "DELETE FROM stock WHERE productid = @productid";
                    MySqlCommand deleteStockCommand = new MySqlCommand(deleteStockQuery, connection);
                    deleteStockCommand.Parameters.AddWithValue("@productid", productid);
                    deleteStockCommand.ExecuteNonQuery();

                    // Then, delete the product itself
                    string deleteProductQuery = "DELETE FROM products WHERE productid = @productid";
                    MySqlCommand deleteProductCommand = new MySqlCommand(deleteProductQuery, connection);
                    deleteProductCommand.Parameters.AddWithValue("@productid", productid);
                    deleteProductCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred while deleting product: " + ex.Message);
            }
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            // Clear session and redirect to login page
            Session.Clear();
            Response.Redirect("LoginForm.aspx");
        }

        protected void BtnViewStock_Click(object sender, EventArgs e)
        {
            Button btnViewStock = (Button)sender;
            string productId = btnViewStock.CommandArgument;
            // Assuming you want to pass the productId to the Stock.aspx page using query string
            Response.Redirect($"Stock.aspx?productId={productId}");
        }
    }
}