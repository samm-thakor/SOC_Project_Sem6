using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public Service1()
        {
            ConnectToDb();
        }
        SqlConnection conn;
        SqlCommand comm;

        SqlConnectionStringBuilder connStringBuilder;

        void ConnectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = "LAPTOP-OSDRC2GD\\SQLEXPRESS";
            connStringBuilder.InitialCatalog = "HospitalDB";
            connStringBuilder.Encrypt = true;
            connStringBuilder.TrustServerCertificate = true;
            connStringBuilder.ConnectTimeout = 60;
            connStringBuilder.AsynchronousProcessing = true;
            connStringBuilder.MultipleActiveResultSets = true;
            connStringBuilder.IntegratedSecurity = true;

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();

        }


        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int InsertPatient(Patient p)
        {
            try
            {
                comm.CommandText = "INSERT INTO tbl_Patients VALUES(@PatientNo , @Name,@Email,@PhoneNo,@DateOfBirth,@Symptom,@Duration,@Description)";
                comm.Parameters.AddWithValue("PatientNo", p.PatientNo);
                comm.Parameters.AddWithValue("Name", p.Name);
                comm.Parameters.AddWithValue("Email", p.Email);
                comm.Parameters.AddWithValue("PhoneNo", p.PhoneNo);
                comm.Parameters.AddWithValue("DateOfBirth", p.DateOfBirth);
                comm.Parameters.AddWithValue("Symptom", p.Symptom);
                comm.Parameters.AddWithValue("Duration", p.Duration);
                comm.Parameters.AddWithValue("Description", p.Description);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int UpdatePatient(Patient p)
        {
            try
            {
                comm.CommandText = "UPDATE tbl_Patients SET  Name=@Name,Email=@Email,PhoneNo=@PhoneNo,DateOfBirth=@DateOfBirth,Symptom=@Symptom,Duration=@Duration,Description=@Description  WHERE PatientNo=@PatientNo";
                comm.Parameters.AddWithValue("PatientNo", p.PatientNo);
                comm.Parameters.AddWithValue("Name", p.Name);
                comm.Parameters.AddWithValue("Email", p.Email);
                comm.Parameters.AddWithValue("PhoneNo", p.PhoneNo);
                comm.Parameters.AddWithValue("DateOfBirth", p.DateOfBirth);
                comm.Parameters.AddWithValue("Symptom", p.Symptom);
                comm.Parameters.AddWithValue("Duration", p.Duration);
                comm.Parameters.AddWithValue("Description", p.Description);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if(conn!=null)
                {
                    conn.Close();
                }
            }
        }

        public int DeletePatient(Patient p)
        {
            try
            {
                comm.CommandText = "DELETE tbl_Patients WHERE PatientNo=@PatientNo";
                comm.Parameters.AddWithValue("PatientNo", p.PatientNo);
                
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Patient GetPatient(Patient p)
        {
            Patient patient = new Patient();
            try
            {
                comm.CommandText = "SELECT * FROM tbl_Patients WHERE PatientNo=@PatientNo";
                comm.Parameters.AddWithValue("PatientNo", p.PatientNo);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    patient.PatientNo = Convert.ToInt32(reader[0]);
                    patient.Name = reader[1].ToString();
                    patient.Email = reader[2].ToString();
                    patient.PhoneNo = reader[3].ToString();
                    patient.DateOfBirth = reader[4].ToString();
                    patient.Symptom = reader[5].ToString();
                    patient.Duration = reader[6].ToString();
                    patient.Description = reader[7].ToString();
                }
                return patient;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if(conn!=null)
                {
                    conn.Close();
                }
            }
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patientL = new List<Patient>();
            try
            {
                comm.CommandText = "SELECT * FROM tbl_Patients";
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    Patient patient = new Patient()
                    {
                        PatientNo = Convert.ToInt32(reader[0]),
                        Name = reader[1].ToString(),
                        Email = reader[2].ToString(),
                        PhoneNo = reader[3].ToString(),
                        DateOfBirth = reader[4].ToString(),
                        Symptom = reader[5].ToString(),
                        Duration = reader[6].ToString(),
                        Description = reader[7].ToString()  
                    };
                    patientL.Add(patient);
                }
                return patientL;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
