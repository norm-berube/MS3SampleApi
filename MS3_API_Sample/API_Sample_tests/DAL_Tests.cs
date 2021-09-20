using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MS3_API_Sample.Utilities;
using MS3_API_Sample.Models;
using System.Collections.Generic;

namespace API_Sample_tests
{
    [TestClass]
    public class DAL_Tests
    {
        private Client FredFlintstone ()
        {
            Client idMod = new Client()
            { 
                FirstName = "Fred",
                LastName = "Flintstone",
                DOB = Convert.ToDateTime("01/01/1927"),
                Gender = "M",
                Title = "Mr."
            };

            Address addr1 = new Address()
            {
                Type = "Home",
                Number = "345",
                Street = "Cave Stone Road",
                Unit = string.Empty,
                City = "Bedrock",
                State = "ME",
                ZipCode = "04736"
            };
            idMod.Addresses.Add(addr1);

            Contact comm1 = new Contact()
            {
                Type = "email",
                Value = "F.Flinstone1@blah.com",
                Preferred = true
            };
            idMod.Contacts.Add(comm1);

            Contact comm2 = new Contact()
            {
                Type = "home",
                Value = "207-555-1212"
            };
            idMod.Contacts.Add(comm2);

            return idMod;
        }

        private Client BarneyRubble()
        {
            Client idMod = new Client()
            {
                FirstName = "Barney",
                LastName = "Rubble",
                DOB = Convert.ToDateTime("01/30/1928"),
                Gender = "M",
                Title = "Mr."
            };

            Address addr1 = new Address()
            {
                Type = "Home",
                Number = "347",
                Street = "Cave Stone Road",
                Unit = string.Empty,
                City = "Bedrock",
                State = "ME",
                ZipCode = "04736"
            };
            idMod.Addresses.Add(addr1);

            Contact comm1 = new Contact()
            {
                Type = "email",
                Value = "Barney_Rubble@blah.com",
                Preferred = true
            };
            idMod.Contacts.Add(comm1);

            Contact comm2 = new Contact()
            {
                Type = "Mobile",
                Value = "207-555-2244"
            };
            idMod.Contacts.Add(comm2);

            return idMod;
        }



        [TestMethod]
        public void InsertIDRecord_Test()
        {
            Client idMod = FredFlintstone();

            Client newMod = DAL.SaveFullId(idMod);

            Assert.IsTrue(newMod.FirstName == "Fred");//Yeah, there should be more here and it shouldn't be hitting the live database, but a mock table

        }
        
        [TestMethod]
        public void GetAllIDRecords_Test()
        {
            List<Client> idList = DAL.GetClients();

            Assert.IsTrue(idList.Count > 0);
        }

        [TestMethod]
        public void GetAllFirstIDRecord_Test()
        {
            Client id = DAL.GetClient(1);

            Assert.IsTrue(id.FirstName == "Fred");
        }



        [TestMethod]
        public void DeleteIDRecord_Test()
        {

            DAL.DeleteFullId(2);

            Assert.IsTrue(true);


        }

    }
}
