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

    /*
     * Submit_ClickCheckOut()
     * 
     * This method fires when the user clickes the checkout button.
     * It stores the information from the dialog into the Session State and redirects the user
     * to the Checkout Confirm page.
     * 
     */
    protected void Submit_ClickCheckOut(object sender, EventArgs e)
    {
        Page.Session["CheckOutMemberID"] = txtmemberid.Text;
        Page.Session["CheckOutItemAssn"] = txtitemassn.Text;
        Response.Redirect("CheckOutConfirm.aspx");
    }

    /*
     * Submit_ClickMember()
     * 
     * This method fires when the user clicks the Submit Member button in the add Member dialog.
     * 
     * It retrieves the data from the dialog fields and uses it to construct a member object.
     * Then it sends a POST request to the API in order to add the member to the database.
     * 
     * 
     */ 
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
            signup_date = DateTime.Now.ToString("yyyy-MM-dd"),
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
        if (!firstName.Text.Equals(""))
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(member);

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers.Add(Utilities.getApiKey());

                try
                {
                    client.UploadString(new Uri("http://api.sparklib.org/api/member"), "POST", json);
                    var response = client.ResponseHeaders;
                    string location = response.Get("Location");
                    string id = location.Split('=')[1];

                    Page.Session["mNote"] = "Member Added With ID: " + id;

                    Response.Redirect("Dashboard.aspx");

                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            Response.Write(@"<script langauge='text/javascript'>alert('Member Name is blank');</script>");
        }
    }


    /*
     *  SubmitClick()
     *  
     *  Params: Book b -> a book object to be added to the database
     * 
     *  This method is fired when the user clicks the submit button on the smart add dialog.
     *  Takes a book object and serializes it into a Request and sends it to the API.
     * 
     * 
     */
    [System.Web.Services.WebMethod]
    public static string Submit_Click(Book b)
    {
        b.assn = Utilities.getNextAssn();
        b.image = "";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(b);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

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