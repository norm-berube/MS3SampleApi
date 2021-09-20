using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web;

namespace API_Sample2.Models
{
    public class Identification
    {
        public int PkId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
