using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class checkoutController : ApiController
    {
        public ArrayList Get()
        {
            checkoutPersistence checkp = new checkoutPersistance();
            return checkp.getCheckouts();
        }
        public Checkouts Get(int item_id)
        {
            checkoutPersistence checkp = new checkoutPersistance();
            return checkp.getCheckouts(item_id);
        }
        public Checkouts Get(int member_id)
        {
            checkoutPersistence checkp = new checkoutPersistance();
            return checkp.getCheckouts(member_id);
        }
        public Checkouts Get(int item_type)
        {
            checkoutPersistence checkp = new checkoutPersistance();
            return checkp.getCheckouts(item_type);
        }
        public HttpResponseMessage Post([FromBody]Checkouts value)
        {
            checkoutPersistence checkp = new checkoutPersistance();
            int item_id = checkp.saveCheckout(value);
            value.ItemId = item_id;
            HttpResponseMessage response = WebRequest.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("checkout/{0}", item_id));
            return response;
        }
        public void Put(int id, [FromBody]string value)
        {
            
        }
        public void Delete(int id)
        {
            
        }
    }
}
