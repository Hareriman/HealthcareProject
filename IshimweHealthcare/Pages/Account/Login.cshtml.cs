using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;

namespace IshimweHealthcare.Pages.Account
{
    public class LoginModel : PageModel
    {
        public Users user = new Users();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync() 
        { 
            user.Username = Request.Form["UserName"];
            user.Password = Request.Form["Password"];

            try
            {
                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string username, password;
                    username = user.Username;
                    password = user.Password;

                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(password);
                        byte[] hash = md5.ComputeHash(bytes);
                        StringBuilder builder = new StringBuilder();
                        for (int i = 0; i < hash.Length; i++)
                        {
                            builder.Append(hash[i].ToString("X2"));
                        }

                        string hardPassword = builder.ToString();

                        string querry = "select count (*) from Users where Username='" + user.Username + "' and Password = '" + hardPassword + "'";
                        SqlDataAdapter sda = new SqlDataAdapter(querry, con);
                        DataTable dtable = new DataTable();
                        sda.Fill(dtable);

                        if (!ModelState.IsValid) return Page();

                        if (dtable.Rows[0][0].ToString() == "1")
                        {
                            if (user.Username=="admin" && user.Password=="admin")
                            {

                                //Creating security context
                                //var claims = new List<Claim>
                                    //{
                                     //  new Claim(ClaimTypes.NameIdentifier, "Admin"),
                                       //new Claim(ClaimTypes.Name, "Admin"),
                                       //new Claim(ClaimTypes.Email, "admin@healthcare.com"),
                                       //new Claim("Department","CUSTOMER"),
                                      // new Claim("admin","true"),
                                     
                                   // };

                                //var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                               // ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                               // var authProperties = new AuthenticationProperties
                               // {
                                 //   IsPersistent = user.RememberMe
                                //};

                                //await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                                return RedirectToPage("/Admin/Index");
                            }

                            if (user.Username.Contains("Dr"))
                            {

                                //Creating security context
                                //var claims = new List<Claim>
                                  //  {
                                  //      new Claim(ClaimTypes.NameIdentifier, "Doctor"),
                                   //     new Claim(ClaimTypes.Name, "Doctor"),
                                    //    new Claim(ClaimTypes.Email, "doctor@healthcare.com"),
										//new Claim("Department","CUSTOMER")
									//};

                                //var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                                //ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                                //var authProperties = new AuthenticationProperties
                                //{
                                 //   IsPersistent = user.RememberMe
                                //};

                               // await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                                return RedirectToPage("/Doctor/doctor");
                            }
                            else
                            {
                                return RedirectToPage("/Admin/Index");
                            }
                        }

                    }
                }

            }
            catch (Exception ex) { }

            return Page();
        }
    }
   
    public class Users
    {
        [Required]
        [Display(Name = "Username")]
        //public string UserName { get; set; }
        public string Username;
        [Required]
        [DataType(DataType.Password)]
        //public string Password { get; set; }
        public string Password;
        public string Role;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
