using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SparkAPI.Models;


public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            table.Rows.Clear();
            table.Rows.Add(addMemberTitleRow());

            var client = new WebClient();
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            var response = client.DownloadString("http://api.sparklib.org/api/Member");

            foreach (Member cur in new JavaScriptSerializer().Deserialize<List<Member>>(response))
            {
                table.Rows.Add(addMemberRow(cur));
            }

            String memNumber = Request.QueryString["id"];
            if(memNumber != null)
            {
                newMemberLabel.Text = "New Member Added With ID: " + memNumber;
            }
        }

    }

    protected void buttonNewMember_Click(object sender, EventArgs e)
    {
        Response.Redirect("/NewMember.aspx");
    }

    protected TableHeaderRow addMemberTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addHeaderCell("ID"));
        ret.Cells.Add(addHeaderCell("Last Name"));
        ret.Cells.Add(addHeaderCell("First Name"));
        ret.Cells.Add(addHeaderCell("Guardian"));
        ret.Cells.Add(addHeaderCell("Date of Birth"));
        ret.Cells.Add(addHeaderCell("Phone"));
        ret.Cells.Add(addHeaderCell("Street"));
        ret.Cells.Add(addHeaderCell("City"));
        ret.Cells.Add(addHeaderCell("State"));
        ret.Cells.Add(addHeaderCell("Zip"));
        ret.Cells.Add(addHeaderCell("Quota"));
        ret.Cells.Add(addHeaderCell("Adult"));
        ret.Cells.Add(addHeaderCell("Ethnicity"));
        ret.Cells.Add(addHeaderCell("Tech Restricted"));
        ret.Cells.Add(addHeaderCell("West Central"));

        ret.BorderWidth = 3;

        return ret;
    }

    private TableRow addMemberRow(Member m)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(m.member_id.ToString()));
        ret.Cells.Add(addCell(m.last_name));
        ret.Cells.Add(addCell(m.first_name));
        ret.Cells.Add(addCell(m.guardian_name));
        ret.Cells.Add(addCell(m.dob.ToShortDateString()));
        ret.Cells.Add(addCell(m.phone));
        ret.Cells.Add(addCell(m.street_address));
        ret.Cells.Add(addCell(m.city));
        ret.Cells.Add(addCell(m.state));
        ret.Cells.Add(addCell(m.zip.ToString()));
        ret.Cells.Add(addCell(m.quota.ToString()));
        ret.Cells.Add(addCell(m.is_adult.ToString()));
        ret.Cells.Add(addCell(m.ethnicity));
        ret.Cells.Add(addCell(m.restricted_to_tech.ToString()));
        ret.Cells.Add(addCell(m.west_central_resident.ToString()));
        return ret;
    }

    private TableCell addCell(string content)
    {
        TableCell ret = new TableCell();
        ret.Text = content;
        return ret;
    }

    private TableHeaderCell addHeaderCell(string content)
    {
        TableHeaderCell ret = new TableHeaderCell();
        ret.Text = content;
        return ret;
    }


    protected void Submit_Click(object sender, EventArgs e)
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

                Response.Redirect("Members.aspx?id=" + id);
            }
            catch (Exception ex)
            {
            }
        }
    }
}