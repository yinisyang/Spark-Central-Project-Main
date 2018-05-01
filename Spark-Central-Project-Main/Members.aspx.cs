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



public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        table.Rows.Clear();
        table.Rows.Add(addMemberTitleRow());

        List<Member> memberList;

        /*
         * Check if there is a Search query string and if there is call the preform search function.
         * 
         * 
         */
        if (Request.QueryString["search"] != null)
        {
            performSearch(Request.QueryString["search"]);
        }

        //Check if there is a memberlist currently stored in session state, if there is load it into the memberlist; if not get member list from api.
        if (Page.Session["mList"] != null)
        {
            memberList = (List<Member>)Page.Session["mList"];
        }
        else
        {
            memberList = getMemberList();
        }


        //Populate member table from the member list.
        foreach (Member cur in memberList)
        {
            table.Rows.Add(addMemberRow(cur));
        }

        //Set Note if one exists.
        if (Page.Session["note"] != null)
        {
            NoteLabel.Text = Page.Session["note"].ToString().ToUpper();
        }

    }



    /*
     * getMemberList()
     * 
     * Obtains the member table from the api and deserializes it into a List of Members.
     * 
     * returns: filled list of members form the spark-central API.
     */
    private List<Member> getMemberList()
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/Member");

        return new JavaScriptSerializer().Deserialize<List<Member>>(response);

    }


    /*
     * addMemberTitleRow()
     * 
     * returns: TableHeaderRow populated with information relevant to the member table.
     * 
     */
    protected TableHeaderRow addMemberTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(Utilities.addHeaderCell("ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Last Name"));
        ret.Cells.Add(Utilities.addHeaderCell("First Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Guardian"));
        ret.Cells.Add(Utilities.addHeaderCell("Date of Birth"));
        ret.Cells.Add(Utilities.addHeaderCell("Phone"));
        ret.Cells.Add(Utilities.addHeaderCell("Street"));
        ret.Cells.Add(Utilities.addHeaderCell("City"));
        ret.Cells.Add(Utilities.addHeaderCell("State"));
        ret.Cells.Add(Utilities.addHeaderCell("Zip"));
        ret.Cells.Add(Utilities.addHeaderCell("Quota"));
        ret.Cells.Add(Utilities.addHeaderCell("Adult"));
        ret.Cells.Add(Utilities.addHeaderCell("Ethnicity"));
        ret.Cells.Add(Utilities.addHeaderCell("Tech Restricted"));
        ret.Cells.Add(Utilities.addHeaderCell("West Central"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit/Delete"));

        ret.BorderWidth = 3;

        return ret;
    }


    /*
     * addMemberRow()
     * 
     * params: Member m -> the member object to use for populating the information in this TableRow
     * 
     * returns: TableRow populated with information from the passed-in member.
     * 
     */
    private TableRow addMemberRow(Member m)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(Utilities.addCell(m.member_id.ToString()));
        ret.Cells.Add(Utilities.addCell(m.last_name));
        ret.Cells.Add(Utilities.addCell(m.first_name));
        ret.Cells.Add(Utilities.addCell(m.guardian_name));
        ret.Cells.Add(Utilities.addCell(m.dob.ToShortDateString()));
        ret.Cells.Add(Utilities.addCell(m.phone));
        ret.Cells.Add(Utilities.addCell(m.street_address));
        ret.Cells.Add(Utilities.addCell(m.city));
        ret.Cells.Add(Utilities.addCell(m.state));
        ret.Cells.Add(Utilities.addCell(m.zip.ToString()));
        ret.Cells.Add(Utilities.addCell(m.quota.ToString()));
        ret.Cells.Add(Utilities.addCell(m.is_adult.ToString()));
        ret.Cells.Add(Utilities.addCell(m.ethnicity));
        ret.Cells.Add(Utilities.addCell(m.restricted_to_tech.ToString()));
        ret.Cells.Add(Utilities.addCell(m.west_central_resident.ToString()));
        ret.Cells.Add(addButtonCell(m.member_id));
        return ret;
    }

    /*
     * addButtonCell function
     * params: int id -> the member_id of the current record
     * 
     * This method dynamically creates an edit and delete button for the current record inside the member table.
     * These buttons are placed inside the TableCell that is returned.
     * 
     * returns: A table cell that contains the edit and delete buttons.
     * 
     */
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
            "deleteMember(" + id.ToString() + ");" +
            "swal('Member deleted', { icon: 'success',});" +
            "} else {" +
            "return false;" +
            "}" +
            "})){ return false; };";


        del.Attributes["title"] = "Delete";

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";

        edit.ServerClick += new EventHandler(editClick);

        ret.Controls.Add(edit);
        ret.Controls.Add(del);

        return ret;
    }


    /*
     * editClick
     * 
     * handler for clicking any of the dynamically created edit buttons.
     * Retrieves the member_id from the sender object.
     * 
     */ 
    public void editClick(object sender, EventArgs e)
    {

        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditMember.aspx?memberid=" + id);


    }



    /*Delete Button Click WebMethod
     * 
     * Params: int id -> the id of the Member record to be removed
     * 
     * This method sends a delete request to the API to remove a specific member from the database.
     * It is a static WebMethod so that it can be called asyncronously from javascript.
     * 
     * returns: void
     * 
     */
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


    //Search Button Click
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var searchText = Server.UrlEncode(txtSearch.Text);
        if (searchText == "")
        {
            Page.Session["mList"] = null;
            Page.Session["note"] = null;
            Response.Redirect("Members.aspx");
        }

        Response.Redirect("Members.aspx?search=" + searchText);

    }



    /*
     * performSearch Method
     * params: string text -> the string to search for
     * 
     * This method obtains the member list from the api, searches for the text and stores all matches in a table inside the session state.
     * 
     *
     * 
     * returns: void
     * 
     * 
     */
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
            if (Utilities.containsStr(arrow, cur.last_name.ToLower()) ||
                Utilities.containsStr(arrow, cur.first_name.ToLower()) ||
                Utilities.containsStr(arrow, cur.member_id.ToString()) ||
                Utilities.containsStr(arrow, cur.state.ToString().ToLower()) ||
                Utilities.containsStr(arrow, cur.zip.ToString()) ||
                Utilities.containsStr(arrow, cur.phone.ToString().ToLower()) ||
                Utilities.containsStr(arrow, cur.city.ToString().ToLower()))
            {
                results.Add(cur);
            }
            else if (cur.guardian_name != null)
            {
                if (Utilities.containsStr(arrow, cur.guardian_name.ToString().ToLower()))
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