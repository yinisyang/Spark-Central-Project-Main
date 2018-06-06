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
        if(Request.QueryString["memberid"] != null)
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
        else
        {
            Response.Redirect("Members.aspx");
        }
    }

    /*
     * getMember()
     * 
     * Params: string id -> The Id number of the Member item to return.
     * 
     * This method simply creates a GET request for the Member item with the specified id.
     * 
     * Returns: A Member Object retrieved from the API.
     * 
     */
    private Member getMember(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("member_id", id);
        string url = "http://api.sparklib.org/api/member";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<Member>(response);
    }

    /*
     * Cancel_Click()
     * 
     * This method occurs when the user clicks the cancel button.
     * It simply returns the user to the Members page.
     * 
     * 
     */
    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Members.aspx");
    }

    /*Delete Button Click WebMethod
     * 
     * Params: int id -> the id of the Member record to be removed
     * 
     * This method sends a delete request to the API to remove a specific Member from the database.
     * It is a static WebMethod so that it can be called asyncronously from javascript.
     * 
     * returns: void
     * 
     */
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

    /*
     * Submit_Click()
     * 
     * This method fires when the Submit button is clicked.
     * It takes the data from the edit fields and constructs a Member object.
     * Then it makes a PUT request to the API to update that record with the new data. 
     * 
     */
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