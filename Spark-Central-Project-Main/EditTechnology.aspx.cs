﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using SparkWebSite;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            Technology t = getTech(Request.QueryString["item_id"]);
            techName.Text = t.name;

        }
    }

    private Technology getTech(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("item_id", id);
        string url = "http://api.sparklib.org/api/technology";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<Technology>(response);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Catalog.aspx");
    }

    [System.Web.Services.WebMethod]
    public static void deleteTe(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/technology?item_id=" + id;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {

        Technology t = new Technology();
        t.name = techName.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(t);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/technology?item_id=" + Request.QueryString["item_id"];
                client.UploadString(apiString, "PUT", json);
                var response = client.ResponseHeaders;

                Response.Redirect("Catalog.aspx");

            }
            catch (Exception ex)
            {
            }
        }

    }
}