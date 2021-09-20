using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Sample2.Models;

namespace API_Sample2.Controllers
{
    public class ClientsController : ApiController
    {
        public List<Identification> Get()
        {
            List<Identification> clients = DAL.GetClients();
            return clients;
        }

        public Identification Get(int Id)
        {
            Identification client = DAL.GetClient(Id);
            return client;
        }

        public IHttpActionResult PostNewClient(Identification client)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            DAL.SaveFullId(client);

            return Ok();
        }

        //Yeah, this is the exact same routine as the one above, but that is because I built the save routine to save and update.
        public IHttpActionResult Put(Identification client)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            
            DAL.SaveFullId(client); 

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            DAL.DeleteFullId(id);

            return Ok();
        }

    }
}
