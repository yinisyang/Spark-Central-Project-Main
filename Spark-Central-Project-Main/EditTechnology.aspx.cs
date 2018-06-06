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
        //Make sure there is a query string, else redirect to Technology page.
        try
        {
            if (!Page.IsPostBack)
            {
                Technology t = getTech(Request.QueryString["item_id"]);
                techName.Text = t.name;
                techAssn.Text = t.assn.ToString();

            }
        }
        catch(Exception error)
        {
            Console.WriteLine(error.Message);
            Response.Redirect("Technology.aspx");
        }
    }

    /*
     * getTech()
     * 
     * Params: string id -> The Id number of the technology item to return.
     * 
     * This method simply creates a GET request for the technology item with the specified id.
     * 
     * Returns: A Technology Object retrieved from the API.
     * 
     */ 
    private Technology getTech(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("item_id", id);
        string url = "http://api.sparklib.org/api/technology";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<Technology>(response);
    }

    /*
     * Cancel_Click()
     * 
     * This method occurs when the user clicks the cancel button.
     * It simply returns the user to the Catalog page.
     * 
     * 
     */ 
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

    /*
     * Submit_Click()
     * 
     * This method fires when the Submit button is clicked.
     * It takes the data from the edit fields and constructs a Technology object.
     * Then it makes a PUT request to the API to update that record with the new data. 
     * 
     */ 
    protected void Submit_Click(object sender, EventArgs e)
    {
        int assn;

        Technology t = new Technology();
        t.name = techName.Text;
        Int32.TryParse(techAssn.Text, out assn);
        t.assn = assn;

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