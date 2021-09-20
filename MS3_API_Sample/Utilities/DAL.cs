using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MS3_API_Sample.Models;

namespace MS3_API_Sample.Utilities
{
    public class DAL
    {
        private static string _connStr = "server=DESKTOP-1S2PPOT;database=MS3_Sample;uid=MS3User;password=Sample123;";

        public static List<Client> GetClients()
        {
            List<Client> ids = new List<Client>();

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand idCmd = new SqlCommand(GetClientSelect(), conn);

            using (SqlDataAdapter sda = new SqlDataAdapter(idCmd))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach(DataRow row in dt.Rows)
                    {
                        ids.Add(Client.ByDataRow(row));
                    }
                }
            }

            //For now let's just work with a list of records and we can deal with the details later.

            return ids;
        }

        public static Client GetClient(int pkId)
        {
            Client ident = new Client();

            SqlConnection conn = new SqlConnection(_connStr);
            SqlCommand idCmd = new SqlCommand(GetClientSelect(pkId), conn);

            //Get the identity Record first
            using (SqlDataAdapter sda = new SqlDataAdapter(idCmd))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    ident = Client.ByDataRow(dt.Rows[0]);
                }
            }

            //Then get all the associated Address Records
            idCmd = new SqlCommand(GetAddressSelect(pkId), conn);
            using (SqlDataAdapter sda = new SqlDataAdapter(idCmd))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        ident.Addresses.Add(Address.ByDataRow(row));
                    }
                }
            }

            //Then get all the associated Contact Records
            idCmd = new SqlCommand(GetContactSelect(pkId), conn);
            using (SqlDataAdapter sda = new SqlDataAdapter(idCmd))
            {
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        ident.Contacts.Add(Contact.ByDataRow(row));
                    }
                }
            }

            return ident;
        }

        public static Client SaveFullId(Client model)
        {
            //_connStr = ConfigurationManager.ConnectionStrings["MS3Connection"].ConnectionString;
            //Because I don't have time to be playing hide and seek with some mystery SQLExpress connection string
            Client results = new Client();

            int pkId = model.PkId;
            try
            {

                SqlConnection conn = new SqlConnection(_connStr);
                if (pkId == 0)          //Do an insert
                {
                    pkId =  SaveClient(model, conn);

                    foreach (Address addr in model.Addresses)
                    {
                        SaveAddress(addr, pkId, conn);
                    }

                    foreach (Contact com in model.Contacts)
                    {
                        SaveContact(com, pkId, conn);
                    }
                }
                else                    //Do an update
                {
                    UpdateClient(model, conn);

                    foreach (Address addr in model.Addresses)
                    {
                        if(addr.PkId != 0)      //Existing address
                        {
                            UpdateAddress(addr, conn);
                        }
                        else                    //New address
                        {
                           SaveAddress(addr, pkId, conn);
                        }
                    }

                    foreach (Contact com in model.Contacts)
                    {
                        if(com.PkId != 0)       //Existing record
                        {
                            UpdateContact(com, conn);
                        }
                        else                    //new record
                        {
                            SaveContact(com, pkId, conn);
                        }
                    }

                }

                results = GetClient(pkId);

            }
            catch(Exception)
            {
                // There should be logging and/or a specific indicator to the program there was an error.
            }
            return results;
        }

        public static void DeleteFullId(int id )
        {
            //_connStr = ConfigurationManager.ConnectionStrings["MS3Connection"].ConnectionString;

            try
            {
                SqlConnection conn = new SqlConnection(_connStr);

                Client model = GetClient(id);
                //It is important to do this backwards, or at least the children records first
                foreach (Address add in model.Addresses)
                {
                    DeleteAddress(add.PkId, conn);
                }
                foreach (Contact comm in model.Contacts)
                {
                    DeleteContact(comm.PkId, conn);
                }
                DeleteClient(model.PkId, conn);     //Remove the parent
            }
            catch (Exception) 
            {
                // There should be logging and/or a specific indicator to the program there was an error.
            }

        }

        #region "Client"
        private static int SaveClient(Client model, SqlConnection conn)
        {
           
            SqlCommand idCmd = new SqlCommand(GetClientInsert(), conn);

            List<SqlParameter> prms = new List<SqlParameter>()
                {
                    new SqlParameter("@FirstName", SqlDbType.VarChar) {Value = model.FirstName},
                    new SqlParameter("@LastName", SqlDbType.VarChar) {Value = model.LastName},
                    new SqlParameter("@DOB", SqlDbType.DateTime2) {Value = model.DOB},
                    new SqlParameter("@Gender", SqlDbType.VarChar) {Value = model.Gender},
                    new SqlParameter("@Title", SqlDbType.VarChar) {Value = model.Title},
                    new SqlParameter("@CreatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@CreatedOn", SqlDbType.DateTime2) {Value = DateTime.Now},
                    new SqlParameter("@UpdatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@UpdatedOn", SqlDbType.DateTime2) {Value = DateTime.Now}
                 };

            idCmd.Parameters.AddRange(prms.ToArray());

            conn.Open();
            int newPk = (int)idCmd.ExecuteScalar();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            return newPk;
        }

        private static void UpdateClient(Client model, SqlConnection conn)
        {

            SqlCommand idCmd = new SqlCommand(GetClientUpdate(), conn);

            List<SqlParameter> prms = new List<SqlParameter>()
                {
                    new SqlParameter("@FirstName", SqlDbType.VarChar) {Value = model.FirstName},
                    new SqlParameter("@LastName", SqlDbType.VarChar) {Value = model.LastName},
                    new SqlParameter("@DOB", SqlDbType.DateTime2) {Value = model.DOB},
                    new SqlParameter("@Gender", SqlDbType.VarChar) {Value = model.Gender},
                    new SqlParameter("@Title", SqlDbType.VarChar) {Value = model.Title},
                    new SqlParameter("@UpdatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@UpdatedOn", SqlDbType.DateTime2) {Value = DateTime.Now},
                    new SqlParameter("@PkId", SqlDbType.Int) {Value = model.PkId}
                 };

            idCmd.Parameters.AddRange(prms.ToArray());

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        private static void DeleteClient(int pkId, SqlConnection conn)
        {
            string delete = "DELETE FROM [dbo].[Client] WHERE PkId = @PkId";

            SqlCommand idCmd = new SqlCommand(delete, conn);
            idCmd.Parameters.Add(new SqlParameter("@PkId", SqlDbType.Int) { Value = pkId });

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        #endregion

        #region "Address"
        private static void SaveAddress(Address model, int idFk, SqlConnection conn)
        {

            SqlCommand idCmd = new SqlCommand(GetAddressInsert(), conn);

            List<SqlParameter> prms = new List<SqlParameter>()
                {
                    new SqlParameter("@IdFK", SqlDbType.Int) {Value = idFk},
                    new SqlParameter("@Type", SqlDbType.VarChar) {Value = model.Type},
                    new SqlParameter("@Number", SqlDbType.VarChar) {Value = model.Number},
                    new SqlParameter("@Street", SqlDbType.VarChar) {Value = model.Street},
                    new SqlParameter("@Unit", SqlDbType.VarChar) {Value = model.Unit},
                    new SqlParameter("@City", SqlDbType.VarChar) {Value = model.City},
                    new SqlParameter("@State", SqlDbType.VarChar) {Value = model.State},
                    new SqlParameter("@ZipCode", SqlDbType.VarChar) {Value = model.ZipCode},
                    new SqlParameter("@CreatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@CreatedOn", SqlDbType.DateTime2) {Value = DateTime.Now},
                    new SqlParameter("@UpdatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@UpdatedOn", SqlDbType.DateTime2) {Value = DateTime.Now}
                 };

            idCmd.Parameters.AddRange(prms.ToArray());

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

        }

        private static void UpdateAddress(Address model, SqlConnection conn)
        {

            SqlCommand idCmd = new SqlCommand(GetAddressUpdate(), conn);

            List<SqlParameter> prms = new List<SqlParameter>()
                {
                    new SqlParameter("@IdFK", SqlDbType.Int) {Value = model.IdFk},
                    new SqlParameter("@Type", SqlDbType.VarChar) {Value = model.Type},
                    new SqlParameter("@Number", SqlDbType.VarChar) {Value = model.Number},
                    new SqlParameter("@Street", SqlDbType.VarChar) {Value = model.Street},
                    new SqlParameter("@Unit", SqlDbType.VarChar) {Value = model.Unit},
                    new SqlParameter("@City", SqlDbType.VarChar) {Value = model.City},
                    new SqlParameter("@State", SqlDbType.VarChar) {Value = model.State},
                    new SqlParameter("@ZipCode", SqlDbType.VarChar) {Value = model.ZipCode},
                    new SqlParameter("@UpdatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@UpdatedOn", SqlDbType.DateTime2) {Value = DateTime.Now},
                    new SqlParameter("@PkId", SqlDbType.Int) {Value = model.PkId}
                 };

            idCmd.Parameters.AddRange(prms.ToArray());

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        private static void DeleteAddress(int pkId, SqlConnection conn)
        {
            string delete = "DELETE FROM [dbo].[Address] WHERE PkId = @PkId";

            SqlCommand idCmd = new SqlCommand(delete, conn);
            idCmd.Parameters.Add(new SqlParameter("@PkId", SqlDbType.Int) { Value = pkId });

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        #endregion

        #region "Contact"
        private static void SaveContact(Contact model, int idFk, SqlConnection conn)
        {

            SqlCommand idCmd = new SqlCommand(GetContactInsert(), conn);

            List<SqlParameter> prms = new List<SqlParameter>()
                {
                    new SqlParameter("@IdFK", SqlDbType.Int) {Value = idFk},
                    new SqlParameter("@Type", SqlDbType.VarChar) {Value = model.Type},
                    new SqlParameter("@Value", SqlDbType.VarChar) {Value = model.Value},
                    new SqlParameter("@Preferred", SqlDbType.Bit) {Value = model.Preferred ? 1 : 0},
                    new SqlParameter("@CreatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@CreatedOn", SqlDbType.DateTime2) {Value = DateTime.Now},
                    new SqlParameter("@UpdatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@UpdatedOn", SqlDbType.DateTime2) {Value = DateTime.Now}
                 };

            idCmd.Parameters.AddRange(prms.ToArray());

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

        }

        private static void UpdateContact(Contact model, SqlConnection conn)
        {

            SqlCommand idCmd = new SqlCommand(GetContactUpdate(), conn);

            List<SqlParameter> prms = new List<SqlParameter>()
                {
                    new SqlParameter("@IdFK", SqlDbType.Int) {Value = model.IdFk},
                    new SqlParameter("@Type", SqlDbType.VarChar) {Value = model.Type},
                    new SqlParameter("@Value", SqlDbType.VarChar) {Value = model.Value},
                    new SqlParameter("@Preferred", SqlDbType.Bit) {Value = model.Preferred ? 1 : 0},
                    new SqlParameter("@UpdatedBy", SqlDbType.VarChar) {Value = "System"},
                    new SqlParameter("@UpdatedOn", SqlDbType.DateTime2) {Value = DateTime.Now},
                    new SqlParameter("@PkId", SqlDbType.Int) {Value = model.PkId}
                 };

            idCmd.Parameters.AddRange(prms.ToArray());

            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        private static void DeleteContact(int pkId, SqlConnection conn)
        {
            string delete = "DELETE FROM [dbo].[Contact] WHERE PkId = @PkId";
            
            SqlCommand idCmd = new SqlCommand(delete, conn);
            idCmd.Parameters.Add(new SqlParameter("@PkId", SqlDbType.Int) { Value = pkId });
            
            conn.Open();
            idCmd.ExecuteNonQuery();
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        #endregion 

        #region "SQL Statments"
        private static string GetClientInsert()
        {
            string query = @"INSERT INTO [dbo].[Client]
                       ([FirstName]
                       ,[LastName]
                       ,[DOB]
                       ,[Gender]
                       ,[Title]
                       ,[CreatedBy]
                       ,[CreatedOn]
                       ,[UpdatedBy]
                       ,[UpdatedOn])
                 VALUES
                       (@FirstName
                       ,@LastName
                       ,@DOB
                       ,@Gender
                       ,@Title
                       ,@CreatedBy
                       ,@CreatedOn
                       ,@UpdatedBy
                       ,@UpdatedOn);
                  Select Cast (scope_identity() as int)";
            return query;
        }

        private static string GetClientUpdate()
        {
            string query = @"UPDATE [dbo].[Client]
                               SET [FirstName] = @FirstName
                                  ,[LastName] = @LastName
                                  ,[DOB] = @DOB
                                  ,[Gender] = @Gender
                                  ,[Title] = @Title
                                  ,[UpdatedBy] = @UpdatedBy
                                  ,[UpdatedOn] = @UpdatedOn
                              WHERE PkId = @PkId";
            return query;
        }

        private static string GetClientSelect(int id = 0)
        {
            string select1 = "Select PKID, FirstName, LastName, DOB, Gender, Title, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn from Client";
            string where1 = "";
            if(id > 0)
            {
                where1 = " where PKID = " + id.ToString();
            }
            return select1 + where1;
        }

        private static string GetAddressInsert()
        {
            string query = @"INSERT INTO [dbo].[Address]
                                   ([IdFK]
                                   ,[Type]
                                   ,[Number]
                                   ,[Street]
                                   ,[Unit]
                                   ,[City]
                                   ,[State]
                                   ,[ZipCode]
                                   ,[CreatedBy]
                                   ,[CreatedOn]
                                   ,[UpdatedBy]
                                   ,[UpdatedOn])
                             VALUES
                                   (@IdFK
                                   ,@Type
                                   ,@Number
                                   ,@Street
                                   ,@Unit
                                   ,@City
                                   ,@State
                                   ,@ZipCode
                                   ,@CreatedBy
                                   ,@CreatedOn
                                   ,@UpdatedBy
                                   ,@UpdatedOn)";
            return query;
        }

        private static string GetAddressUpdate()
        {
            string query = @"UPDATE [dbo].[Address]
                               SET [IdFK] = @IdFK
                                  ,[Type] = @Type
                                  ,[Number] = @Number
                                  ,[Street] = @Street
                                  ,[Unit] = @Unit
                                  ,[City] = @City
                                  ,[State] = @State
                                  ,[ZipCode] = @ZipCode
                                  ,[UpdatedBy] = @UpdatedBy
                                  ,[UpdatedOn] = @UpdatedOn
                              WHERE PkId = @PkId";
            return query;
        }

        private static string GetAddressSelect(int fkId)
        {
            return GetAddressSelect(0, fkId);
        }

        private static string GetAddressSelect(int id = 0, int fkId = 0)
        {
            string select1 = "Select PKID, IdFk, Type, Number, Street, Unit, City, State, ZipCode, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn from [Address]";
            string where1 = "";
            if (id > 0)
            {
                where1 = " where PKID = " + id.ToString();
            }
            if (fkId > 0)
            {
                if (where1.Length > 0)
                {
                    where1 += " and ";
                }
                else
                {
                    where1 = " where ";
                }
                where1 += "idFk = " + fkId.ToString();
            }

            return select1 + where1;
        }

        private static string GetContactInsert()
        {
            string query = @"INSERT INTO [dbo].[Contact]
                                   ([IdFK]
                                   ,[Type]
                                   ,[Value]
                                   ,[Preferred]
                                   ,[CreatedBy]
                                   ,[CreatedOn]
                                   ,[UpdatedBy]
                                   ,[UpdatedOn])
                             VALUES
                                   (@IdFK
                                   ,@Type
                                   ,@Value
                                   ,@Preferred
                                   ,@CreatedBy
                                   ,@CreatedOn
                                   ,@UpdatedBy
                                   ,@UpdatedOn)";
            return query;
        }

        private static string GetContactUpdate()
        {
            string query = @"UPDATE [dbo].[Contact]
                               SET [IdFK] = @IdFK
                                  ,[Type] = @Type
                                  ,[Value] = @Value
                                  ,[Preferred] = @Preferred
                                  ,[UpdatedBy] = @UpdatedBy
                                  ,[UpdatedOn] = @UpdatedOn
                              WHERE PkId = @PkId";
            return query;
        }

        private static string GetContactSelect(int fkId)
        {
            return GetContactSelect(0, fkId);
        }

        private static string GetContactSelect(int id = 0, int fkId = 0)
        {
            string select1 = "Select PKID, IdFk, Type, Value, Preferred, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn from Contact";
            string where1 = "";
            if (id > 0)
            {
                where1 = " where PKID = " + id.ToString();
            }
            if (fkId > 0)
            {
                if (where1.Length > 0)
                {
                    where1 += " and ";
                }
                else
                {
                    where1 = " where ";
                }
                where1 += "idFk = " + fkId.ToString();
            }

            return select1 + where1;
        }

        #endregion
    }
}