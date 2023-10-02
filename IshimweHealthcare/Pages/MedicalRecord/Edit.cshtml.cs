using IshimweHealthcare.Pages.patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IshimweHealthcare.Pages.MedicalRecord
{
    public class EditModel : PageModel
    {
        public MedicalRecords medicalRecordInfo = new MedicalRecords();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            medicalRecordInfo.UUID = Request.Query["id"];

            try
            {
                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM MedicalRecords WHERE UUID=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", medicalRecordInfo.UUID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //PatientInfo info = new PatientInfo();
                                medicalRecordInfo.UUID = "" + reader.GetInt32(0);
                                medicalRecordInfo.patientid = "" +reader.GetInt32(1);
                                medicalRecordInfo.description = reader.GetString(2);
                                medicalRecordInfo.doctorname = reader.GetString(3);
   

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
            medicalRecordInfo.UUID = Request.Query["id"];
            medicalRecordInfo.patientid = Request.Form["patient"];
            medicalRecordInfo.description = Request.Form["description"];
            medicalRecordInfo.doctorname = Request.Form["doctor"];


            if (medicalRecordInfo.UUID.Length == 0 || medicalRecordInfo.patientid.Length == 0 ||
                medicalRecordInfo.description.Length == 0 || medicalRecordInfo.doctorname.Length == 0)
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
                    String sqlQuery = "UPDATE MedicalRecords SET patientid=@patientid,description=@description,doctorname=@doctorname WHERE UUID=@id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@id", medicalRecordInfo.UUID);
                        cmd.Parameters.AddWithValue("@patientid", medicalRecordInfo.patientid);
                        cmd.Parameters.AddWithValue("@description", medicalRecordInfo.description);
                        cmd.Parameters.AddWithValue("@doctorname", medicalRecordInfo.doctorname);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicalRecords info = new MedicalRecords();
                                info.UUID = "" + reader.GetInt32(0);
                                info.patientid = "" + reader.GetInt32(1);
                                info.description = reader.GetString(2);
                                info.doctorname = reader.GetString(3);

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


