using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWrireFile
{
    public class Staff
    {
        public string ID { get; set; }  
        public string Name { get; set; }    
        public DateTime Birth { get; set; }
        public string Adress { get; set; }  
        public string Phone { get; set; }
        public string Gender { get; set; }

        public Staff() { }
        public Staff(string iD, string name, DateTime birth, string adress, string phone, string gender)
        {
            ID = iD;
            Name = name;
            Birth = birth;
            Adress = adress;
            Phone = phone;
            Gender = gender;
        }

        public override string ToString()
        {
            return ID + ";" + Name + ";" + Birth + ";" + Adress + ";" + Phone + ";" + Gender;
        }
    }
}
