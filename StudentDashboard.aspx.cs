using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Student_Registration_Application
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Student")
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                lblStudentName.Text = Session["FullName"].ToString();
                LoadAttendanceInfo();
                LoadHomeworkTasks();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LoginPage.aspx");
        }

        private void LoadAttendanceInfo()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT 
                                    COUNT(*) AS TotalDays, 
                                    SUM(CASE WHEN Status = 'Present' THEN 1 ELSE 0 END) AS PresentDays 
                                 FROM Attendance 
                                 WHERE StudentID = @StudentID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StudentID", Session["UserID"]);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read() && dr["TotalDays"] != DBNull.Value && Convert.ToInt32(dr["TotalDays"]) > 0)
                {
                    int totalDays = Convert.ToInt32(dr["TotalDays"]);
                    int presentDays = dr["PresentDays"] != DBNull.Value ? Convert.ToInt32(dr["PresentDays"]) : 0;

                    double percentage = ((double)presentDays / totalDays) * 100;
                    lblAttendancePercent.Text = Math.Round(percentage, 2) + "%";
                }
                else
                {
                    lblAttendancePercent.Text = "No attendance records yet.";
                }
            }
        }

        private void LoadHomeworkTasks()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT a.Title, a.Description, a.DueDate, t.IsCompleted 
                                 FROM TaskStatus t
                                 INNER JOIN Assignments a ON t.AssignmentID = a.AssignmentID
                                 WHERE t.StudentID = @StudentID
                                 ORDER BY a.DueDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@StudentID", Session["UserID"]);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvMyTasks.DataSource = dt;
                gvMyTasks.DataBind();
            }
        }
    }
}