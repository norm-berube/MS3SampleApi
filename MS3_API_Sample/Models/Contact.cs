using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MS3_API_Sample.Models
{
    public class Contact
    {
        public int PkId { get; set; }
        public int IdFk { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool Preferred { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public static Contact ByDataRow(DataRow row)
        {
            Contact comm1 = new Contact()
            {
                PkId = (int)row["PKID"],
                IdFk = (int)row["IdFK"],
                Type = row["Type"].ToString(),
                Value = row["Value"].ToString(),
                Preferred = Convert.ToBoolean(row["Preferred"]),
                CreatedBy = row["CreatedBy"].ToString(),
                CreatedOn = Convert.ToDateTime(row["CreatedOn"]),
                UpdatedBy = row["UpdatedBy"].ToString(),
                UpdatedOn = Convert.ToDateTime(row["UpdatedOn"])
            };
            return comm1;
        }
    }
}