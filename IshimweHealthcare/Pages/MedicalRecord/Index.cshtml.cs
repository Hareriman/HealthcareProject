using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IshimweHealthcare.Pages.MedicalRecord
{
    public class IndexModel : PageModel
    {
       public List<MedicalRecords> listRecords = new List<MedicalRecords>();
       //public List<MedicalHistory> listMedical = new List<MedicalHistory>();
        public void OnGet()
        {
            listRecords.Clear();
            try
            {

                String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=ASPCOREQUIZ_DB;Integrated Security=True";
               // String conString = "Data Source=DESKTOP-GJFOBQ8\\SQLEXPRESS1;Initial Catalog=EricHealthCaredb;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    String sqlQuery = "SELECT * FROM MedicalRecords";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicalRecords medicalRecord = new MedicalRecords();
                                medicalRecord.UUID = "" + reader.GetInt32(0);
                                medicalRecord.patientid = "" + reader.GetInt32(1);
                                medicalRecord.description = reader.GetString(2);
                                medicalRecord.doctorname = reader.GetString(3);
                              

                                listRecords.Add(medicalRecord);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }

        }
       
    }
    public class MedicalRecords
    {
        public string UUID;
        public string patientid;
        public string description;
        public string doctorname;
        // public string

    }
    public class MedicalHistory 
    {
        public string MedicalHistoryID;
        public string PatientID;
        public string VisitDate;
        public string Diagnosis;
        public string Treatment;
        public string Medications;
        public string Notes;
    }
}
   

