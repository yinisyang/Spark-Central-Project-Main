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
        if (Request.QueryString["fine_id"] != null)
        {
            Fine f = getFine(Request.QueryString["fine_id"]);
            txtFineId.Text = f.fine_id.ToString();
            txtMemId.Text = f.member_id.ToString();
            txtAmount.Text = f.amount.ToString("F2");
            txtFineDescription.Text = f.description;

            txtFineId.ReadOnly = true;
        }
        else
        {
            Response.Redirect("Circulations.aspx");
        }
    }

    /*
     * getFine()
     * 
     * Params: string id -> The Id number of the Fine item to return.
     * 
     * This method simply creates a GET request for the Fine item with the specified id.
     * 
     * Returns: A Fine Object retrieved from the API.
     * 
     */
    private Fine getFine(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("fine_id", id);
        string url = "http://api.sparklib.org/api/fines";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<Fine>(response);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Circulations.aspx");
    }

    [System.Web.Services.WebMethod]
    public static void deleteFine(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/fines?fine_id=" + id;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {

        Fine f = new Fine();
        int fid, mid;
        Int32.TryParse(txtFineId.Text, out fid);
        Int32.TryParse(txtMemId.Text, out mid);
        double amount;
        Double.TryParse(txtAmount.Text, out amount);

        f.fine_id = fid;
        f.member_id = mid;
        f.amount = amount;
        f.description = txtFineDescription.Text;



        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(f);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/fines?fine_id=" + Request.QueryString["fine_id"];
                client.UploadString(apiString, "PUT", json);
                var response = client.ResponseHeaders;

                Response.Redirect("Circulations.aspx");

            }
            catch (Exception ex)
            {
            }
        }

    }
}