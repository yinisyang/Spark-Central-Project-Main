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
        public ArrayList Get(int? item_id = null, int? member_id = null, string item_type = null, bool? resolved = null)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            checkp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("item_type", item_type, System.Data.SqlDbType.VarChar, 50);
            checkp.addCallField("resolved", resolved, System.Data.SqlDbType.Bit, 1);

            return checkp.GetAll();
        }
        public Checkout Get(int member_id, int item_id, string item_type)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            checkp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("item_type", item_type, System.Data.SqlDbType.VarChar, 50);

            return (Checkout)checkp.Get();
        }

        public HttpResponseMessage Post([FromBody]Checkout value)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            int item_id = checkp.Save(value, "member_id");

            if (item_id != -1)
            {
                value.item_id = item_id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("checkout?member_id={0}", item_id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        public HttpResponseMessage Put(int item_id, int member_id, String item_type, [FromBody]Checkout value)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            checkp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("item_type", item_type, System.Data.SqlDbType.VarChar, 50);

            bool recordExisted = checkp.Update(value);

            HttpResponseMessage response;
            if(recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
        public HttpResponseMessage Delete(int item_id, int member_id, String item_type)
        {
            CheckoutPersistence checkp = new CheckoutPersistence();
            checkp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);
            checkp.addCallField("item_type", item_type, System.Data.SqlDbType.VarChar, 50);

            bool recordExisted = checkp.Delete();

            HttpResponseMessage response;
            if(recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
