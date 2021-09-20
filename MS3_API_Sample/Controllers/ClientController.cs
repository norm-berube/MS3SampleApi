using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using MS3_API_Sample.Models;
using MS3_API_Sample.Utilities;

namespace MS3_API_Sample.Controllers
{
    public class ClientController : ApiController
    {
        // GET api/values
        [HttpGet]
        public List<Client> Get()
        {
            return DAL.GetClients();
        }

        //There should also be other methods to get by Name or other Identifying ID/values (i.e. Find by phone, find by City, etc)
        // GET api/values/5
        [HttpGet]
        public Client Get(int Id)
        {
            return DAL.GetClient(Id);
        }

        
        // POST api/values
        // Performs both initial Save and Update
        public IHttpActionResult PostNewClient( Client model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            DAL.SaveFullId(model);

            return Ok();

            //return DAL.SaveFullId(model);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            DAL.DeleteFullId(id);
        }
    }
}
