<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Team_poora.Products" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Products</title>
    <style>
        /* Style the table */
        table {
            border-collapse: collapse;
            width: 100%;
        }
        th, td {
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }
        th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome to Stock Management System!<asp:Button ID="BtnLogout" runat="server" Text="Logout" OnClick="BtnLogout_Click" style="margin-left: 384px; margin-top: 17px" Width="107px" />
            </h1>
            <asp:GridView ID="GridViewProducts" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridViewProducts_RowDeleting" DataKeyNames="productid">
                <Columns>
                    <asp:BoundField DataField="productid" HeaderText="Product ID" />
                    <asp:BoundField DataField="productname" HeaderText="Product Name" />
                    <asp:BoundField DataField="productdesc" HeaderText="Product Description" />
                    <asp:BoundField DataField="productprice" HeaderText="Product Price" DataFormatString="&#8369;{0:N2}" />
                    <asp:BoundField DataField="stocks" HeaderText="Product Stock" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Button ID="BtnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("productid") %>' />
                            <asp:Button ID="BtnViewStock" runat="server" Text="View Stock" OnClick="BtnViewStock_Click" CommandArgument='<%# Eval("productid") %>' />                     
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
             <div>
                <asp:Button ID="BtnAddProduct" runat="server" Text="Add Product" OnClick="BtnAddProduct_Click" style="margin-top: 18px" />
            </div>
        </div>
    </form>
</body>
</html>