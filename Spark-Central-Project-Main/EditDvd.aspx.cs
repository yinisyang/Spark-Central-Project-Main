using System;
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

        if (Request.QueryString["item_id"] != null)
        {
            DVD d = getDVD(Request.QueryString["item_id"]);
            dvdTitle.Text = d.title;
            dvdRating.Text = d.rating;
            dvdYear.Text = d.release_year.ToString();
            dvdAssn.Text = d.assn.ToString();

        }
        else
        {
            Response.Redirect("DVD.aspx");
        }


    }

    /*
     * getDVD()
     * 
     * Params: string id -> The Id number of the DVD item to return.
     * 
     * This method simply creates a GET request for the DVD item with the specified id.
     * 
     * Returns: A DVD Object retrieved from the API.
     * 
     */
    private DVD getDVD(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("item_id", id);
        string url = "http://api.sparklib.org/api/dvd";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<DVD>(response);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Catalog.aspx");
    }

    [System.Web.Services.WebMethod]
    public static void deleteDv(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/dvd?item_id=" + id;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        int year, assn;

        DVD d = new DVD();
        d.title = dvdTitle.Text;
        Int32.TryParse(dvdYear.Text, out year);
        Int32.TryParse(dvdAssn.Text, out assn);
        d.release_year = year;
        d.assn = assn;
        d.rating = dvdRating.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(d);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/dvd?item_id=" + Request.QueryString["item_id"];
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