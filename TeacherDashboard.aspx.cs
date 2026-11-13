using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Student_Registration_Application
{
    public partial class TeacherDashboard : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Teacher")
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                lblTeacherName.Text = Session["FullName"].ToString();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindStudents();
                BindTasks();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LoginPage.aspx");
        }

        private void BindStudents()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT UserID, FullName FROM Users WHERE Role='Student'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvStudents.DataSource = dt;
                gvStudents.DataBind();
            }
        }

        private void BindTasks()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT t.TaskID, u.FullName, a.Title, a.DueDate, t.IsCompleted 
                                 FROM TaskStatus t
                                 INNER JOIN Users u ON t.StudentID = u.UserID
                                 INNER JOIN Assignments a ON t.AssignmentID = a.AssignmentID
                                 ORDER BY a.DueDate DESC, u.FullName";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvTasks.DataSource = dt;
                gvTasks.DataBind();
            }
        }

        protected void btnSaveAttendance_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                foreach (GridViewRow row in gvStudents.Rows)
                {
                    int studentId = Convert.ToInt32(gvStudents.DataKeys[row.RowIndex].Value);
                    CheckBox chk = (CheckBox)row.FindControl("chkPresent");
                    string status = chk.Checked ? "Present" : "Absent";

                    string query = "INSERT INTO Attendance (StudentID, AttendanceDate, Status) VALUES (@StudentID, @Date, @Status)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@Date", txtDate.Text);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.ExecuteNonQuery();
                }
            }
            lblMessage.Text = "Attendance saved successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        protected void btnAssignHomework_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDueDate.Text))
            {
                lblMessage.Text = "Title and Due Date are required!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string assignQuery = @"INSERT INTO Assignments (Title, Description, DueDate, AssignedBy) 
                                       OUTPUT INSERTED.AssignmentID 
                                       VALUES (@Title, @Desc, @Date, @TeacherID)";
                SqlCommand cmd = new SqlCommand(assignQuery, con);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                cmd.Parameters.AddWithValue("@Desc", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Date", txtDueDate.Text);
                cmd.Parameters.AddWithValue("@TeacherID", Session["UserID"]);

                int newAssignmentId = (int)cmd.ExecuteScalar();

                string taskQuery = @"INSERT INTO TaskStatus (AssignmentID, StudentID, IsCompleted)
                                     SELECT @AssignID, UserID, 0 FROM Users WHERE Role='Student'";
                SqlCommand taskCmd = new SqlCommand(taskQuery, con);
                taskCmd.Parameters.AddWithValue("@AssignID", newAssignmentId);
                taskCmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Homework assigned to all students!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            txtTitle.Text = "";
            txtDescription.Text = "";
            BindTasks();
        }

        protected void btnUpdateTasks_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                foreach (GridViewRow row in gvTasks.Rows)
                {
                    int taskId = Convert.ToInt32(gvTasks.DataKeys[row.RowIndex].Value);
                    CheckBox chk = (CheckBox)row.FindControl("chkCompleted");
                    int isCompleted = chk.Checked ? 1 : 0;

                    string query = "UPDATE TaskStatus SET IsCompleted = @Status WHERE TaskID = @TaskID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Status", isCompleted);
                    cmd.Parameters.AddWithValue("@TaskID", taskId);
                    cmd.ExecuteNonQuery();
                }
            }
            lblMessage.Text = "Task statuses updated successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;
            BindTasks();
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewStudentName.Text) || string.IsNullOrWhiteSpace(txtNewStudentUser.Text) || string.IsNullOrWhiteSpace(txtNewStudentPass.Text))
            {
                lblMessage.Text = "All fields are required to add a student!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                // Verify username isn't taken
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username=@User", con);
                checkCmd.Parameters.AddWithValue("@User", txtNewStudentUser.Text.Trim());
                con.Open();

                if ((int)checkCmd.ExecuteScalar() > 0)
                {
                    lblMessage.Text = "Student Username already exists!";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Insert the student
                string query = "INSERT INTO Users (FullName, Username, Password, Role) VALUES (@Name, @User, @Pass, 'Student')";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", txtNewStudentName.Text.Trim());
                cmd.Parameters.AddWithValue("@User", txtNewStudentUser.Text.Trim());
                cmd.Parameters.AddWithValue("@Pass", txtNewStudentPass.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            lblMessage.Text = "New student added successfully!";
            lblMessage.ForeColor = System.Drawing.Color.Green;

            // Clear textboxes
            txtNewStudentName.Text = "";
            txtNewStudentUser.Text = "";
            txtNewStudentPass.Text = "";

            // Refresh the attendance grid to show the new student
            BindStudents();
        }
    }
}