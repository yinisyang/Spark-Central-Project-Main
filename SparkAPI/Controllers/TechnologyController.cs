﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class TechnologyController : ApiController
    {
        // GET api/<controller>
        public ArrayList Get()
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            return tp.getTechnology(); 
        }

        // GET api/<controller>/5
        public Technology Get(int id)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            return tp.getTechnology(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Technology value)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            int id = tp.saveTechnology(value);
            value.ItemId = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("technology/{0}", id));
            return response;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            bool recordExisted = tp.deleteTechnology(id);

            HttpResponseMessage response;
            if (recordExisted)
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