<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Student_Registration_Application.SignUp" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Teacher Sign Up</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 50px; background-color: #f4f4f9; }
        .box { width: 320px; padding: 20px; border: 1px solid #ccc; border-radius: 5px; background-color: #fff; margin: auto; }
        .form-group { margin-bottom: 15px; }
        label { display: block; margin-bottom: 5px; font-weight: bold; }
        input[type="text"], input[type="password"] { width: 95%; padding: 8px; border: 1px solid #ccc; }
        .btn { width: 100%; padding: 10px; background-color: #007bff; color: white; border: none; cursor: pointer; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <h2>Teacher Registration</h2>
            
            <div class="form-group">
                <label>Full Name</label>
                <asp:TextBox ID="txtFullName" runat="server" required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Choose Username</label>
                <asp:TextBox ID="txtRegUsername" runat="server" required="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Choose Password</label>
                <asp:TextBox ID="txtRegPassword" runat="server" TextMode="Password" required="true"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnRegister" runat="server" Text="Sign Up as Teacher" CssClass="btn" OnClick="btnRegister_Click" />
            <br /><br />
            <asp:Label ID="lblRegMessage" runat="server" Font-Bold="true"></asp:Label>
            <br /><br />
            <a href="LoginPage.aspx">Back to Login</a>
        </div>
    </form>
</body>
</html>