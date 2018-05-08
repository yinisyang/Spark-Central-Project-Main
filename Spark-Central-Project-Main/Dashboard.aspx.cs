using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using SparkWebSite;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void buttonNewMember_Click(object sender, EventArgs e)
    {
        Response.Redirect("/NewMember.aspx");
    }
    protected void buttonViewMembers_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Members.aspx");
    }

    [System.Web.Services.WebMethod]
    public static string Submit_Click(Book b)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(b);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            try
            {
                client.UploadString(new Uri("http://api.sparklib.org/api/book"), "POST", json);

                var response = client.ResponseHeaders;
                string location = response.Get("Location");
                string id = location.Split('=')[1];
                return id;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }

}