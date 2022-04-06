using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfServiceApp
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public int PatientNo { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNo { get; set; }
        [DataMember]
        public string DateOfBirth { get; set; }
        [DataMember]
        public string Symptom { get; set; }
        [DataMember]
        public string Duration { get; set; }
        [DataMember]
        public string Description { get; set; }

    }
}
