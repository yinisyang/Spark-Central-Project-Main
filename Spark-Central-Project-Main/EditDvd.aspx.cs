using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DVD d = getDVD(Request.QueryString["item_id"]);
            dvdTitle.Text = d.title;
            dvdRating.Text = d.rating;
            dvdYear.Text = d.release_year.ToString();

        }
    }

    private DVD getDVD(string id)
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");
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
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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
        int year;

        DVD d = new DVD();
        d.title = dvdTitle.Text;
        Int32.TryParse(dvdYear.Text, out year);
        d.release_year = year;
        d.rating = dvdRating.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(d);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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