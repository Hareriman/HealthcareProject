using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IshimweHealthcare.Pages.patients
{
    public class EditModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        { 
            patientInfo.id = Request.Query["id"];
           
            try
            {
                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Patients WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", patientInfo.id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //PatientInfo info = new PatientInfo();
                                patientInfo.id = "" + reader.GetInt32(0);
                                patientInfo.name = reader.GetString(1);
                                patientInfo.email = reader.GetString(2);
                                patientInfo.phone = reader.GetString(3);
                                patientInfo.address = reader.GetString(4);
                                patientInfo.createdAt = reader.GetDateTime(5) + "";
                                patientInfo.FirstName = reader.GetString(6);
                                patientInfo.Gender = reader.GetString(7);
                                patientInfo.InsuranceProvider = reader.GetString(8);
                                //patientInfo.InsurancePolicyNumber = "" + reader.GetInt32(9); ;
                              

                            }
                        }
                    }

                }


            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost()
        {
            patientInfo.id = Request.Query["id"];
            patientInfo.name = Request.Form["name"];
            patientInfo.FirstName = Request.Form["FirstName"];
            patientInfo.email = Request.Form["email"];
            patientInfo.phone = Request.Form["phone"];
            patientInfo.address = Request.Form["address"];
            patientInfo.createdAt = Request.Form["createdAt"];
            patientInfo.Gender = Request.Form["gender"];
            patientInfo.InsuranceProvider = Request.Form["insura"];
            patientInfo.InsurancePolicyNumber = Request.Form["number"];


            if (patientInfo.name.Length == 0 || patientInfo.email.Length == 0 ||
                patientInfo.phone.Length == 0 || patientInfo.address.Length == 0)
            {
                errorMessage = "All field are required";
                return;
            }
            try
            {

                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "UPDATE Patients SET name=@name,email=@email,phone=@phone,address=@address,createdAt=@createdAt,Last_name=@Last_name,Gender=@gender,InsuranceProvider=@InsuranceProvider WHERE id=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", patientInfo.id);
                        cmd.Parameters.AddWithValue("@name", patientInfo.name);
                        cmd.Parameters.AddWithValue("@email", patientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", patientInfo.phone);
                        cmd.Parameters.AddWithValue("@createdAt", patientInfo.createdAt);
                        cmd.Parameters.AddWithValue("@address", patientInfo.address);
                        cmd.Parameters.AddWithValue("@Last_name", patientInfo.FirstName);
                        cmd.Parameters.AddWithValue("@gender", patientInfo.Gender);
                        cmd.Parameters.AddWithValue("@InsuranceProvider", patientInfo.InsuranceProvider);
                        //cmd.Parameters.AddWithValue("@@InsuranceProviderNumber", patientInfo.InsurancePolicyNumber);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo info = new PatientInfo();
                                info.id = "" + reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.phone = reader.GetString(3);
                                info.address = reader.GetString(4);
                                info.createdAt = reader.GetDateTime(5) + "";
                                info.FirstName = reader.GetString(6);
                                info.Gender = reader.GetString(7);
                                info.InsuranceProvider = reader.GetString(8);

                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
                
            }
            Response.Redirect("Index");

        }
    }

}
