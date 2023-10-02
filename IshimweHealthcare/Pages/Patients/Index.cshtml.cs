using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace IshimweHealthcare.Pages.patients
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> listPatients = new List<PatientInfo>();

        public void OnGet()
        {
            listPatients.Clear();
            try
            {

                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM Patients";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo patientInfo = new PatientInfo();
                                patientInfo.id = "" + reader.GetInt32(0);
                                patientInfo.name = reader.GetString(1);
                                patientInfo.email = reader.GetString(2);
                                patientInfo.phone = reader.GetString(3);
                                patientInfo.address = reader.GetString(4);
                                patientInfo.createdAt = reader.GetDateTime(5)+"";
                                patientInfo.FirstName = reader.GetString(6);
                                patientInfo.Gender = reader.GetString(7);
                                patientInfo.InsuranceProvider = reader.GetString(8);
                                patientInfo.InsurancePolicyNumber = "" + reader.GetInt32(9);

                                listPatients.Add(patientInfo);
                            }
                        }
                    }

                }
            }catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
          
        } 
    }
    public class PatientInfo
    {
        public  string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string createdAt;
        public string FirstName;
        public string Gender;
        public string InsuranceProvider;
        public string InsurancePolicyNumber;
    }
    //public class PatientInfo {
    //  public string PatientID;
    // public string FirstName;
    // public string LastName;
    // public string DateofBirth;
    // public string Gender;
    // public string email;
    // public string phone;
    // public string address;
    // public string InsuranceProvider;
    // public string InsurancePolicyNumber;
    // public string createdAt;

    //}
}
