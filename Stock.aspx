<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="Team_poora.Stock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock</title>
    <style>
        /* Add your CSS styles here */
    </style>
    <script>
        function validateInput(event) {
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
            <h1>Stock Information</h1>
            <asp:GridView ID="GridViewStock" runat="server" AutoGenerateColumns="False" OnRowEditing="GridViewStock_RowEditing" OnRowCancelingEdit="GridViewStock_RowCancelingEdit" OnRowUpdating="GridViewStock_RowUpdating" DataKeyNames="stockid">
                <Columns>
                    <asp:BoundField DataField="stockid" HeaderText="Stock ID" ReadOnly="true" />
                    <asp:BoundField DataField="productid" HeaderText="Product ID" ReadOnly="true" />
                    <asp:TemplateField HeaderText="Product Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProductName" runat="server" Text='<%# Bind("productname") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("productname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Description">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProductDesc" runat="server" Text='<%# Bind("productdesc") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProductDesc" runat="server" Text='<%# Eval("productdesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Price">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProductPrice" runat="server" Text='<%# Bind("productprice") %>' onkeypress="validateInput(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Literal ID="litProductPrice" runat="server" Text='<%# "₱" + Eval("productprice", "{0:N2}") %>'></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stocks">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtStocks" runat="server" Text='<%# Bind("stocks") %>' onkeypress="validateInput(event)"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblStocks" runat="server" Text='<%# Eval("stocks") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="true" ButtonType="Button" EditText="Edit" CancelText="Cancel" UpdateText="Update" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="BtnBack" runat="server" Text="Back" OnClick="BtnBack_Click" />
        </div>
    </form>
</body>
</html>