using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace MS3_API_Sample.Models
{
    public class Client
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
        public List<Address> Addresses { get; set; }
        public List<Contact> Contacts { get; set; }

        public Client()
        {
            this.Addresses = new List<Address>();
            this.Contacts = new List<Contact>();
        }

        public static Client ByDataRow(DataRow row)
        {
            Client id = new Client()
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