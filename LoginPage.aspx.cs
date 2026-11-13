using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Student_Registration_Application
{
    public partial class Login : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlRole.SelectedValue))
            {
                lblMessage.Text = "Error: Please select whether you are a Teacher or a Student.";
                return;
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT UserID, FullName, Role FROM Users WHERE Username=@User AND Password=@Pass AND Role=@Role";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@User", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Pass", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedValue);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Session["UserID"] = dr["UserID"].ToString();
                    Session["FullName"] = dr["FullName"].ToString();
                    Session["Role"] = dr["Role"].ToString();

                    if (dr["Role"].ToString() == "Teacher")
                    {
                        Response.Redirect("TeacherDashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("StudentDashboard.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Error: Invalid Username, Password, or Role mismatch!";
                }
            }
        }
    }
}