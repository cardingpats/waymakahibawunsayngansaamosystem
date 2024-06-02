using System;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Team_poora
{
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStock();
            }
        }

        private void BindStock()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT s.stockid, s.productid, p.productname, p.productdesc, p.productprice, s.stocks " +
                                   "FROM stock s " +
                                   "INNER JOIN products p ON s.productid = p.productid";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    connection.Open();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        GridViewStock.DataSource = dt;
                        GridViewStock.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred: " + ex.Message);
            }
        }

        protected void GridViewStock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewStock.EditIndex = e.NewEditIndex;
            BindStock();
        }

        protected void GridViewStock_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewStock.EditIndex = -1;
            BindStock();
        }

        protected void GridViewStock_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridViewStock.Rows[e.RowIndex];
                string stockid = GridViewStock.DataKeys[e.RowIndex].Value.ToString();
                TextBox txtProductName = (TextBox)row.FindControl("txtProductName");
                TextBox txtProductDesc = (TextBox)row.FindControl("txtProductDesc");
                TextBox txtProductPrice = (TextBox)row.FindControl("txtProductPrice");
                TextBox txtStocks = (TextBox)row.FindControl("txtStocks");

                // Check if both productname and productdesc have values
                if (!string.IsNullOrEmpty(txtProductName.Text) && !string.IsNullOrEmpty(txtProductDesc.Text))
                {
                    string newProductName = txtProductName.Text;
                    string newProductDesc = txtProductDesc.Text;
                    decimal newProductPrice = Convert.ToDecimal(txtProductPrice.Text);
                    int newStocks = Convert.ToInt32(txtStocks.Text);

                    UpdateProduct(stockid, newProductName, newProductDesc, newProductPrice, newStocks);
                }
                else
                {
                    // Show a message indicating that both fields are required
                    Response.Write("Product name and description are required.");
                }

                GridViewStock.EditIndex = -1;
                BindStock();
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred while updating product: " + ex.Message);
            }
        }

        private void UpdateProduct(string stockid, string newProductName, string newProductDesc, decimal newProductPrice, int newStocks)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE products SET productname = @productname, productdesc = @productdesc, productprice = @productprice WHERE productid = (SELECT productid FROM stock WHERE stockid = @stockid);" +
                                   "UPDATE stock SET stocks = @stocks WHERE stockid = @stockid";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@productname", newProductName);
                    command.Parameters.AddWithValue("@productdesc", newProductDesc);
                    command.Parameters.AddWithValue("@productprice", newProductPrice);
                    command.Parameters.AddWithValue("@stocks", newStocks);
                    command.Parameters.AddWithValue("@stockid", stockid);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Log or display the exception message
                Response.Write("An error occurred while updating product: " + ex.Message);
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            // Redirect to Products.aspx
            Response.Redirect("Products.aspx");
        }
    }
}