using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;

namespace MS3_GUI.Models
{
    public class Address
    {
        public int PkId { get; set; }
        public int IdFk { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public static Address ByDataRow(DataRow row)
        {
            Address add1 = new Address()
            {
                PkId = (int)row["PKID"],
                IdFk = (int)row["IdFK"],
                Type = row["Type"].ToString(),
                Number = row["Number"].ToString(),
                Street = row["Street"].ToString(),
                Unit = row["Unit"].ToString(),
                City = row["City"].ToString(),
                State = row["State"].ToString(),
                ZipCode = row["ZipCode"].ToString(),
                CreatedBy = row["CreatedBy"].ToString(),
                CreatedOn = Convert.ToDateTime(row["CreatedOn"]),
                UpdatedBy = row["UpdatedBy"].ToString(),
                UpdatedOn = Convert.ToDateTime(row["UpdatedOn"])
            };
            return add1;
        }

    }
}