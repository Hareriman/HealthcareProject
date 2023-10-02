using IshimweHealthcare.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace IshimweHealthcare.Pages.MedicalRecord
{
    public class CreateModel : PageModel
    {
        public MedicalRecordInfo medicalRecordInfo = new MedicalRecordInfo();
        public List<PatientInfo> patients = new List<PatientInfo>();
        public String errorMessage = "";
        public String successMessage = "";
        public IActionResult OnGet()
        {
            String connectionString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                String sqlQuery = "SELECT * FROM Patients";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PatientInfo patient = new PatientInfo();
                            patient.id = Convert.ToInt32(reader["id"]);
                            patient.name = Convert.ToString(reader["name"]);
                            patients.Add(patient);
                        }
                    }
                }
            }

            return Page();

        }
        public void OnPost()
        {
            medicalRecordInfo.patientId= Convert.ToInt32(Request.Form["patientId"]);
            medicalRecordInfo.doctorname = Request.Form["doctorname"];
            medicalRecordInfo.description = Request.Form["description"];
           // medicalRecordInfo.date = Convert.ToDateTime(Request.Form["date"]);

            if (medicalRecordInfo.patientId == 0 || medicalRecordInfo.doctorname.Length == 0 || medicalRecordInfo.description.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }

            // Save new medical record

            try
            {
                String connectionString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "INSERT INTO MedicalRecords(patientId,doctorname,description) VALUES(@patientId,@doctorName,@diagnosis)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@patientId", medicalRecordInfo.patientId);
                        cmd.Parameters.AddWithValue("@doctorName", medicalRecordInfo.doctorname);
                       // cmd.Parameters.AddWithValue("@date", medicalRecordInfo.date);
                        cmd.Parameters.AddWithValue("@diagnosis", medicalRecordInfo.description);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            medicalRecordInfo.patientId = 0; medicalRecordInfo.doctorname = "";
            medicalRecordInfo.description = "";
            successMessage = "New Medical Record Added with Success";

            Response.Redirect("/MedicalRecord/Index");
        }

    }
    public class MedicalRecordInfo
    {
        public int id { get; set; }
        public int patientId { get; set; }
        public string doctorname { get; set; }
        public string description { get; set; }
       // public DateTime date { get; set; }
    }

    public class PatientInfo
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
