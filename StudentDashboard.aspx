<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="Student_Registration_Application.StudentDashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Student Dashboard</title>
    <style>
        body { font-family: Arial, sans-serif; padding: 20px; }
        .card { border: 1px solid #ccc; padding: 15px; margin-bottom: 20px; border-radius: 5px; background: #fafafa; }
        table { border-collapse: collapse; width: 100%; margin-top: 10px; background: #fff; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #28a745; color: white; }
        .status-completed { color: green; font-weight: bold; }
        .status-pending { color: red; font-weight: bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Student Dashboard</h2>
        <p>Welcome, <asp:Label ID="lblStudentName" runat="server" Font-Bold="true"></asp:Label>! 
           <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" style="float:right;" /></p>
        
        <div class="card">
            <h3>My Attendance Record</h3>
            <p>Overall Attendance: 
                <asp:Label ID="lblAttendancePercent" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="#0066cc"></asp:Label>
            </p>
        </div>

        <div class="card">
            <h3>My Homework Tasks</h3>
            <asp:GridView ID="gvMyTasks" runat="server" AutoGenerateColumns="False" EmptyDataText="No assignments yet.">
                <Columns>
                    <asp:BoundField DataField="Title" HeaderText="Assignment" />
                    <asp:BoundField DataField="Description" HeaderText="Details" />
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" 
                                Text='<%# Convert.ToBoolean(Eval("IsCompleted")) ? "Completed" : "Pending" %>'
                                CssClass='<%# Convert.ToBoolean(Eval("IsCompleted")) ? "status-completed" : "status-pending" %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>