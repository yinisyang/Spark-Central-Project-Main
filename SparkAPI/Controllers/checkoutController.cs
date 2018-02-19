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
    public class CheckoutController : ApiController
    {
        public ArrayList Get()
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            return checkp.getCheckouts();
        }
        public ArrayList Get(int member_id)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            return checkp.getCheckouts(member_id);
        }

        public HttpResponseMessage Post([FromBody]Checkout value)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            int item_id = checkp.saveCheckout(value);
            value.ItemId = item_id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
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
