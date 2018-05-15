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
    protected void Submit_ClickMember(object sender, EventArgs e)
    {
        var member = new
        {
            first_name = firstName.Text,
            last_name = lastName.Text,
            is_adult = (memberGroupValue.Value.Equals("Adult") ? true : false),
            guardian_name = (guardianName.Text.Equals("") ? null : guardianName.Text),
            email = email.Text,
            dob = dateOfBirth.Text,
            phone = phone.Text,
            street_address = streetAddress.Text,
            city = city.Text,
            state = stateValue.Value,
            zip = zipCode.Text,
            quota = checkoutQuota.Text,
            ethnicity = ethnicityValue.Value,
            restricted_to_tech = isRestrictedToTech.Checked,
            west_central_resident = isWestCentralResident.Checked
        };

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(member);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            try
            {
                client.UploadString(new Uri("http://api.sparklib.org/api/member"), "POST", json);
                var response = client.ResponseHeaders;
                string location = response.Get("Location");
                string id = location.Split('=')[1];

                Page.Session["mNote"] = "Member Added With ID: " + id;

                Response.Redirect("Members.aspx");

            }
            catch (Exception ex)
            {
            }
        }
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