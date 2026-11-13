using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Student_Registration_Application
{
    public partial class SignUp : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                // Check if username already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username=@User";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@User", txtRegUsername.Text.Trim());

                con.Open();
                int userExists = (int)checkCmd.ExecuteScalar();

                if (userExists > 0)
                {
                    lblRegMessage.Text = "Username already taken!";
                    lblRegMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // Insert new teacher
                    string insertQuery = "INSERT INTO Users (FullName, Username, Password, Role) VALUES (@Name, @User, @Pass, 'Teacher')";
                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@User", txtRegUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Pass", txtRegPassword.Text.Trim());

                    cmd.ExecuteNonQuery();
                    lblRegMessage.Text = "Registration successful! You can now log in.";
                    lblRegMessage.ForeColor = System.Drawing.Color.Green;
                }
            }
        }
    }
}