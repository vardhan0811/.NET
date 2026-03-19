<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="firstWebApplication.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>My First ASP.NET Page</h2>

            Enter Name:
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>

            <br /><br />

            <asp:Button ID="btnSubmit" runat="server" Text="Click Me"
                OnClick="btnSubmit_Click" />

            <br /><br />

            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
    </div>
    </form>
</body>
</html>