using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

public partial class NewMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        //Label1.Text = firstName.Text + " " + lastName.Text + " " + guardianName.Text + " " + memberGroupValue.Value + " " + streetAddress.Text + " " + city.Text + " " + stateValue.Value + " " + zipCode.Text + " " + phone.Text + " " + email.Text + " " + dateOfBirth.Text + " " + ethnicityValue.Value + " " + checkoutQuota.Text + " " + isRestrictedToTech.Checked + " === " + isWestCentralResident.Checked;
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://api.sparklib.org/api/member");
        client.DefaultRequestHeaders.Add("APIKey", "254a2c54-5e21-4e07-b2aa-590bc545a520");

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

        //var task = client.GetAsync("http://api.sparklib.org/api/member");
        //Label1.Text = task.Result.Content.ReadAsStringAsync().Result;

        //using (client)
        //{
            var response = client.PostAsJsonAsync("", json).Result;
        //}
        //Label1.Text = task.Result.Content.ReadAsStringAsync().Result;
    }
}