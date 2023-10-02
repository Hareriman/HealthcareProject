using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace IshimweHealthcare.Pages.Account
{
    public class SignupModel : PageModel
    {
        public UsersInfo usersInfo = new UsersInfo();
        public void OnGet()
        {
        }

        public void OnPost()
        {
            usersInfo.Username = Request.Form["username"];
            usersInfo.Password = Request.Form["Password"];
            usersInfo.confirmPassword = Request.Form["confirmPassword"];
            usersInfo.Role = "patient";
            try
            {
                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    if (usersInfo.Password == usersInfo.confirmPassword)
                    {
                        string pass = usersInfo.Password;
                        using (MD5 md5 = MD5.Create())
                        {
                            byte[] bytes = Encoding.UTF8.GetBytes(pass);
                            byte[] hash = md5.ComputeHash(bytes);
                            StringBuilder builder = new StringBuilder();
                            for (int i = 0; i < hash.Length; i++)
                            {
                                builder.Append(hash[i].ToString("X2"));
                            }

                            string hardPassword = builder.ToString(); 

                            string query = "INSERT INTO Users VALUES('" + usersInfo.Username + "','" + hardPassword + "','" + usersInfo.Role + "')";
                            SqlDataAdapter sda = new SqlDataAdapter(query, con);
                            sda.SelectCommand.ExecuteNonQuery();
                            Response.Redirect("/Account/Login");

                        }

                    }
                    else
                    {
                        usersInfo.Username= "";
                        usersInfo.Role = "";
                        usersInfo.Password = "";
                        usersInfo.confirmPassword = "";

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class UsersInfo
    {
        public string UserID;
        public string Username;
        public string Password;
        public string Role;
        public string confirmPassword;

    }
}
