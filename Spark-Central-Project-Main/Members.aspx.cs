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
        if(!Page.IsPostBack)
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


        }

    }

    protected void buttonNewMember_Click(object sender, EventArgs e)
    {
        Response.Redirect("/NewMember.aspx");
    }

    protected TableHeaderRow addMemberTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addCell("ID"));
        ret.Cells.Add(addCell("Last Name"));
        ret.Cells.Add(addCell("First Name"));
        ret.Cells.Add(addCell("Guardian"));
        ret.Cells.Add(addCell("Date of Birth"));
        ret.Cells.Add(addCell("Phone"));
        ret.Cells.Add(addCell("Street"));
        ret.Cells.Add(addCell("City"));
        ret.Cells.Add(addCell("State"));
        ret.Cells.Add(addCell("Zip"));
        ret.Cells.Add(addCell("Quota"));
        ret.Cells.Add(addCell("Adult"));
        ret.Cells.Add(addCell("Ethnicity"));
        ret.Cells.Add(addCell("Tech Restricted"));
        ret.Cells.Add(addCell("West Central"));

        ret.BorderWidth = 5;
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
}