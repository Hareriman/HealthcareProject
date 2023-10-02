using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IshimweHealthcare.Pages.patients
{
    public class CreateModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void Onpost()
        {
            patientInfo.name = Request.Form["name"];
            patientInfo.FirstName = Request.Form["FirstName"];
            patientInfo.phone= Request.Form["phone"];
            patientInfo.email = Request.Form["email"];
            patientInfo.createdAt = Request.Form["createdAt"];
            patientInfo.address = Request.Form["address"];
            patientInfo.Gender = Request.Form["gender"];
            patientInfo.InsuranceProvider = Request.Form["insura"];
            patientInfo.InsurancePolicyNumber = Request.Form["number"];

            if (patientInfo.name.Length == 0 || patientInfo.email.Length == 0 ||
                patientInfo.phone.Length == 0 || patientInfo.address.Length == 0 || patientInfo.createdAt.Length==0)
            {
                errorMessage = "All field are required";
                return;
            }
            //Save the date
            try
            {
                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String SqlQuery = "INSERT INTO Patients(name,email,phone,address,createdAt,Last_name,Gender,InsuranceProvider,InsuranceProviderNumber) VALUES(@name,@email,@phone,@address,@createdAt,@Last_name,@Gender,@InsuranceProvider,@InsuranceProviderNumber)";
                    using (SqlCommand cmd = new SqlCommand(SqlQuery, con)) 
                    {
                        cmd.Parameters.AddWithValue("@name", patientInfo.name);
                        cmd.Parameters.AddWithValue("@email", patientInfo.email);
                        cmd.Parameters.AddWithValue("@phone", patientInfo.phone);
                        cmd.Parameters.AddWithValue("@address", patientInfo.address);
                        cmd.Parameters.AddWithValue("@createdAt", patientInfo.createdAt);
                        cmd.Parameters.AddWithValue("@Last_name", patientInfo.FirstName);
                        cmd.Parameters.AddWithValue("@Gender", patientInfo.Gender);
                        cmd.Parameters.AddWithValue("@InsuranceProvider", patientInfo.InsuranceProvider);
                        cmd.Parameters.AddWithValue("@InsuranceProviderNumber", patientInfo.InsurancePolicyNumber);
                        cmd.ExecuteNonQuery();

                    }

                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return;

            }
            patientInfo.name = ""; patientInfo.email = "";
            patientInfo.phone = ""; patientInfo.address = "";
            patientInfo.createdAt = "";

           successMessage = "New Patients Added with Success";

            Response.Redirect("Index");

        }
    }
}
        