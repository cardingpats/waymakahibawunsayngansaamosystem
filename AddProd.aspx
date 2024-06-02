<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProd.aspx.cs" Inherits="Team_poora.AddProd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Product</title>
    <script>
        function validateInput(id) {
            var input = document.getElementById(id).value.trim();
            if (input === '') {
                alert("Please enter a value for " + id.replace('txt', '').toLowerCase());
                return false;
            }
            return true;
        }

        function validateForm() {
            var productId = document.getElementById('txtproductid').value.trim();
            var productPrice = document.getElementById('txtproductprice').value.trim();
            var stock = document.getElementById('txtstock').value.trim();

            // Check if product ID is empty
            if (productId === '') {
                alert("Please enter a value for Product ID.");
                return false;
            }

            // Check if product ID contains "-"
            if (productId.indexOf('-') !== -1) {
                alert("Product ID should not contain '-' (negative sign).");
                return false;
            }

            // Check if product price is empty or negative
            if (productPrice === '' || parseFloat(productPrice) < 0) {
                alert("Please enter a valid product price.");
                return false;
            }

            // Check if stock is empty or negative
            if (stock === '' || parseInt(stock) < 0) {
                alert("Please enter a valid product stock.");
                return false;
            }

            // Validate other fields
            return validateInput('txtproductname') && validateInput('txtproductdesc');
        }

        function validateKeyPress(event) {
            var key = event.key;
            if (key === '-') {
                event.preventDefault();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Add Product</h1>
            <div>
                <label for="txtproductid">Product ID:</label>
                <asp:TextBox ID="txtproductid" runat="server" onkeypress="validateKeyPress(event)"></asp:TextBox>
            </div>
            <div>
                <label for="txtproductname">Product Name:</label>
                <asp:TextBox ID="txtproductname" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtproductdesc">Product Description:</label>
                <asp:TextBox ID="txtproductdesc" runat="server"></asp:TextBox>
            </div>
            <div>
                <label for="txtproductprice">Product Price:</label>
                <asp:TextBox ID="txtproductprice" runat="server" onkeypress="validateKeyPress(event)"></asp:TextBox>
            </div>
            <div>
                <label for="txtstock">Product Stock:</label>
                <asp:TextBox ID="txtstock" runat="server" onkeypress="validateKeyPress(event)"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="BtnAddProduct" runat="server" Text="Add Product" OnClientClick="return validateForm();" OnClick="BtnAddProduct_Click" />
                <asp:Button ID="BtnBack" runat="server" Text="Back to Products" OnClick="BtnBack_Click" />
            </div>
        </div>
    </form>
</body>
</html>