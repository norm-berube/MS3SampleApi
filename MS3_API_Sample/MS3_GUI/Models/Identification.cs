using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MS3_GUI.Models
{
    public class Identification
    {
        public int PkId { get; set; }
        [DisplayName("First Name")] 
        public string FirstName { get; set; }
        [DisplayName("Last Name")] 
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DOB { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public List<Address> Address { get; set; }
        public List<Contact> Communication { get; set; }

        public Identification()
        {
            this.Address = new List<Address>();
            this.Communication = new List<Contact>();
        }

        public static Identification ByDataRow(DataRow row)
        {
            Identification id = new Identification()
            {
                PkId = (int)row["PKID"],
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                DOB = Convert.ToDateTime(row["DOB"]),
                Gender = row["Gender"].ToString(),
                Title = row["Title"].ToString(),
                CreatedBy = row["CreatedBy"].ToString(),
                CreatedOn = Convert.ToDateTime(row["CreatedOn"]),
                UpdatedBy = row["UpdatedBy"].ToString(),
                UpdatedOn = Convert.ToDateTime(row["UpdatedOn"])
            };
            return id;
        }
    }
}