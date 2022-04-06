using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowApplication.ServiceReference1;

namespace WindowApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.PatientNo = Convert.ToInt32(txtPatientNo.Text);
            p.Name = txtName.Text;
            p.Email = txtEmail.Text;
            p.PhoneNo = txtPhoneNo.Text;
            p.DateOfBirth = dob.Text;
            p.Symptom = txtSymptom.Text;
            p.Duration = txtDuration.Text;
            p.Description = txtDescription.Text;

            Service1Client service = new Service1Client();

            if(service.InsertPatient(p)==1)
            {
                MessageBox.Show("Patient Details Inserted Successfully");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Patient p = new Patient()
            {
                PatientNo = Convert.ToInt32(txtPatientNo.Text),
                Name = txtName.Text,
                Email = txtEmail.Text,
                PhoneNo = txtPhoneNo.Text,
                DateOfBirth = dob.Text,
                Symptom = txtSymptom.Text,
                Duration = txtDuration.Text,
                Description = txtDescription.Text
            };
            

            Service1Client service = new Service1Client();

            if (service.UpdatePatient(p) == 1)
            {
                MessageBox.Show("Patient Details Updated Successfully");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.PatientNo = Convert.ToInt32(txtPatientNo.Text);
         

            Service1Client service = new Service1Client();

            if (service.DeletePatient(p) == 1)
            {
                MessageBox.Show("Patient Details Deleted Successfully");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            List<Patient> patientL = new List<Patient>();
            Patient p = new Patient()
            {
                PatientNo = Convert.ToInt32(txtPatientNo.Text)
            };
            Service1Client service = new Service1Client();
            patientL.Add(service.GetPatient(p));
            dataGridViewPatients.DataSource = patientL;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            List<Patient> patientL = new List<Patient>();
            Service1Client service = new Service1Client();
            dataGridViewPatients.DataSource = service.GetAllPatients();
        }
    }
}
