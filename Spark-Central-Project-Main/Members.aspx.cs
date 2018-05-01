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

public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        table.Rows.Clear();
        table.Rows.Add(addMemberTitleRow());

        List<Member> memberList;

        if (Request.QueryString["search"] != null)
        {
            performSearch(Request.QueryString["search"]);
        }


        if (Page.Session["mList"] != null)
        {
            memberList = (List<Member>)Page.Session["mList"];
        }
        else
        {
            memberList = getMemberList();
        }

        foreach (Member cur in memberList)
        {
            table.Rows.Add(addMemberRow(cur));
        }

        if (Page.Session["note"] != null)
        {
            NoteLabel.Text = Page.Session["note"].ToString().ToUpper();
        }

    }

    private List<Member> getMemberList()
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/Member");

        return new JavaScriptSerializer().Deserialize<List<Member>>(response);

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
        ret.Cells.Add(addHeaderCell("Edit/Delete"));

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
        ret.Cells.Add(addButtonCell(m.member_id));
        return ret;
    }


    private TableCell addButtonCell(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton del = new HtmlButton();
        HtmlButton edit = new HtmlButton();

        del.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        del.InnerHtml = "<i class = \"material-icons\">delete</i>";
        del.Attributes.Add("id", id.ToString());
        del.Attributes["onclick"] = "if(swal({" +
            "title: 'Delete Member'," +
            "text: 'Are you sure you want to delete member id: " + id.ToString() + "?'," +
            "icon: 'warning'," +
            "buttons: true," +
            "dangerMode: true," +
            "}).then((value) => {" +
            "if(value){" +
            "deleteMember(" + id.ToString() +");" +
            "swal('Member deleted', { icon: 'success', });" +
            "} else {" +
            "return false;" +
            "}" +
            "})){ return false; };";
            

        del.Attributes["title"] = "Delete";

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";
        edit.Attributes["onclick"] = "if(swal('Hello World')){return false;};";

        edit.ServerClick += new EventHandler(editClick);

        ret.Controls.Add(edit);
        ret.Controls.Add(del);

        return ret;
    }

    public void editClick(object sender, EventArgs e)
    {

        return;
    }

    //Delete Button Click WebMethod
    [System.Web.Services.WebMethod]
    public static void deleteClick(int id)
    {

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            try
            {
                String apiString = "http://api.sparklib.org/api/member?member_id=" + id.ToString();
                client.UploadString(new Uri(apiString), "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
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


    private bool containsStr(String arrow, String target)
    {
        try
        {
            Regex reg = new Regex(arrow);
            return reg.IsMatch(target);

        }
        catch(Exception e)
        {
            return false;
        }
    }


    //Search Button Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var searchText = Server.UrlEncode(txtSearch.Text);
        if( searchText == "")
        {
            Page.Session["mList"] = null;
            Page.Session["note"] = null;
            Response.Redirect("Members.aspx");
        }

        Response.Redirect("Members.aspx?search=" + searchText);

    }


    protected void performSearch(string text)
    {
        if (text == "")
        {
            Page.Session["mList"] = null;
            Page.Session["note"] = null;
            return;
        }
        String arrow = text.ToLower();
        List<Member> memberList = getMemberList();
        List<Member> results = new List<Member>();
        foreach (Member cur in memberList)
        {
            if (containsStr(arrow, cur.last_name.ToLower()) ||
                containsStr(arrow, cur.first_name.ToLower()) ||
                containsStr(arrow, cur.member_id.ToString()) ||
                containsStr(arrow, cur.state.ToString().ToLower()) ||
                containsStr(arrow, cur.zip.ToString()) ||
                containsStr(arrow, cur.phone.ToString().ToLower()) ||
                containsStr(arrow, cur.city.ToString().ToLower()))
            {
                results.Add(cur);
            }
            else if (cur.guardian_name != null)
            {
                if (containsStr(arrow, cur.guardian_name.ToString().ToLower()))
                {
                    results.Add(cur);
                }
            }
        }
        Page.Session["mList"] = results;
        Page.Session["note"] = "Search Results For: '" + arrow + "'";
    }

    //Add New Member Submit Click
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

                Page.Session["note"] = "Member Added With ID: " + id;

                Response.Redirect("Members.aspx");
                
            }
            catch (Exception ex)
            {
            }
        }
    }
}