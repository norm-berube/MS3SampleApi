using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MS3_API_Sample.Controllers;
using MS3_API_Sample.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace API_Sample_tests
{
    [TestClass]
    public class ClientControllerTests
    {
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
        public void GetList_Test()
        {
            ClientController ic = new ClientController();

            List<Client> idList = ic.Get();

            Assert.IsTrue(idList.Count > 0);
        }

        [TestMethod]
        public void GetSingle_Test()
        {
            ClientController ic = new ClientController();

            Client id1 = ic.Get(1);   //This won't work once we start adding and deleting records, if we delete the first one.

            Assert.IsTrue(id1.FirstName.Length > 0);
        }

        [TestMethod]
        public void Save_Test()
        {
            ClientController ic = new ClientController();

            ic.PostNewClient(BarneyRubble());   

            Assert.IsTrue(true);
        }

        //[TestMethod]
        //public void Update_Test()
        ////{
        ////    ClientController ic = new ClientController();

        ////    Client step1 = ic.PostNewClient(BarneyRubble());

        ////    step1.FirstName = "Sally";
        ////    Client results = ic.PostNewClient(step1);

        ////    Assert.IsTrue(results.FirstName == "Sally");
        //}


        [TestMethod]
        public void Delete_Test()
        {
            ClientController ic = new ClientController();

            //Again, this only works if you know the record exists
            ic.Delete(3);

            Assert.IsTrue(true);
        }

    }


}
