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
using System.Text.RegularExpressions;
using SparkWebSite;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Member mem = getMember(Request.QueryString["memberid"]);
            firstName.Text = mem.first_name;
            lastName.Text = mem.last_name;
            isAdult.Checked = mem.is_adult;
            phone.Text = mem.phone;
            email.Text = mem.email;
            dateOfBirth.Text = mem.dob.ToShortDateString();
            checkoutQuota.Text = mem.quota.ToString();
            streetAddress.Text = mem.street_address;
            city.Text = mem.city;
            state.Text = mem.state;
            zipCode.Text = mem.zip.ToString();
            guardianName.Text = mem.guardian_name;
            ethnicity.Text = mem.ethnicity;
            isRestrictedToTech.Checked = mem.restricted_to_tech;
            isWestCentralResident.Checked = mem.west_central_resident;
        }
    }

    private Member getMember(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("member_id", id);
        string url = "http://api.sparklib.org/api/member";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<Member>(response);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Members.aspx");
    }

    [System.Web.Services.WebMethod]
    public static void deleteMem(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/member?member_id=" + id;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        var member = new
        {
            first_name = firstName.Text,
            last_name = lastName.Text,
            is_adult = isAdult.Checked,
            guardian_name = (guardianName.Text.Equals("") ? null : guardianName.Text),
            email = email.Text,
            dob = dateOfBirth.Text,
            phone = phone.Text,
            street_address = streetAddress.Text,
            city = city.Text,
            state = state.Text,
            zip = zipCode.Text,
            quota = checkoutQuota.Text,
            ethnicity = ethnicity.Text,
            restricted_to_tech = isRestrictedToTech.Checked,
            west_central_resident = isWestCentralResident.Checked
        };

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(member);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/member?member_id=" + Request.QueryString["memberid"];
                client.UploadString(apiString, "PUT", json);

                Response.Redirect("Members.aspx");

            }
            catch (Exception ex)
            {
            }
        }

    }
}