<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherDashboard.aspx.cs" Inherits="Student_Registration_Application.TeacherDashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Teacher Dashboard</title>
    <style>
        body { font-family: Arial, sans-serif; padding: 20px; }
        .section { border: 1px solid #ccc; padding: 15px; margin-bottom: 20px; border-radius: 5px; background: #fafafa; }
        .form-group { margin-bottom: 10px; }
        table { border-collapse: collapse; width: 100%; margin-top: 10px; background: #fff; }
        th, td { border: 1px solid #ddd; padding: 8px; text-align: left; }
        th { background-color: #007bff; color: white; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Teacher Dashboard</h2>
        <p>Welcome, <asp:Label ID="lblTeacherName" runat="server" Font-Bold="true"></asp:Label>! 
           <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" style="float:right;" /></p>
        
        <asp:Label ID="lblMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>

        <div class="section">
            <h3>1. Mark Daily Attendance</h3>
            <div class="form-group">
                <label>Date: </label>
                <asp:TextBox ID="txtDate" runat="server" TextMode="Date" required="true"></asp:TextBox>
            </div>
            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="UserID">
                <Columns>
                    <asp:BoundField DataField="UserID" HeaderText="ID" />
                    <asp:BoundField DataField="FullName" HeaderText="Student Name" />
                    <asp:TemplateField HeaderText="Present?">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkPresent" runat="server" Checked="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnSaveAttendance" runat="server" Text="Save Attendance" OnClick="btnSaveAttendance_Click" />
        </div>

        <div class="section">
            <h3>2. Assign New Homework</h3>
            <div class="form-group">
                <label>Title: </label><br />
                <asp:TextBox ID="txtTitle" runat="server" Width="300px"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Description: </label><br />
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" Width="300px"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Due Date: </label><br />
                <asp:TextBox ID="txtDueDate" runat="server" TextMode="Date"></asp:TextBox>
            </div>
            <asp:Button ID="btnAssignHomework" runat="server" Text="Assign to All Students" OnClick="btnAssignHomework_Click" />
        </div>

        <div class="section">
            <h3>3. Update Homework Status</h3>
            <asp:GridView ID="gvTasks" runat="server" AutoGenerateColumns="False" DataKeyNames="TaskID">
                <Columns>
                    <asp:BoundField DataField="FullName" HeaderText="Student" />
                    <asp:BoundField DataField="Title" HeaderText="Assignment" />
                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:TemplateField HeaderText="Completed?">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkCompleted" runat="server" 
                                Checked='<%# Convert.ToBoolean(Eval("IsCompleted")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnUpdateTasks" runat="server" Text="Save Status Updates" OnClick="btnUpdateTasks_Click" />
        </div>
        <!-- Section 4: Add New Student -->
        <div class="section">
            <h3>4. Register a New Student</h3>
            <div class="form-group">
                <label>Student Full Name: </label>
                <asp:TextBox ID="txtNewStudentName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Student Username: </label>
                <asp:TextBox ID="txtNewStudentUser" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Student Password: </label>
                <asp:TextBox ID="txtNewStudentPass" runat="server"></asp:TextBox>
            </div>
            <asp:Button ID="btnAddStudent" runat="server" Text="Add Student" OnClick="btnAddStudent_Click" />
        </div>
    </form>
</body>
</html>