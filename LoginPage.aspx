<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Student_Registration_Application.Login" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Login - Student Management System</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 50px; background-color: #f4f4f9; }
        .login-box { width: 320px; padding: 20px; border: 1px solid #ccc; border-radius: 5px; background-color: #fff; margin: auto; box-shadow: 0px 4px 8px rgba(0,0,0,0.1); }
        .form-group { margin-bottom: 15px; }
        label { display: block; margin-bottom: 5px; font-weight: bold; }
        input[type="text"], input[type="password"], select { width: 95%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; }
        .btn { width: 100%; padding: 10px; background-color: #28a745; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px; }
        .btn:hover { background-color: #218838; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-box">
            <h2>System Login</h2>
            
            <div class="form-group">
                <label>Login As:</label>
                <asp:DropDownList ID="ddlRole" runat="server">
                    <asp:ListItem Text="-- Select Role --" Value=""></asp:ListItem>
                    <asp:ListItem Text="Teacher" Value="Teacher"></asp:ListItem>
                    <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Username</label>
                <asp:TextBox ID="txtUsername" runat="server" required="true"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>Password</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required="true"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn" OnClick="btnLogin_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        <br /><br />
            <div style="text-align: center;">
                <p>New Teacher? <a href="SignUp.aspx">Sign up here</a></p>
            </div>
    </form>
</body>
</html>