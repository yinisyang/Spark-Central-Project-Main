using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using SparkWebSite;
using System.Web.Mvc;
using System.Web.Script.Services;

public partial class Members : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }

        //table.Rows.Clear();
        //table.Rows.Add(addMemberTitleRow());
        //List<Member> memberList;
        //memberList = getMemberList();

        /*foreach (Member cur in memberList)
        {
            table.Rows.Add(addMemberRow(cur));
        }*/

        //Set Note if one exists.
        if (Page.Session["mNote"] != null)
        {
            NoteLabel.Text = Page.Session["mNote"].ToString().ToUpper();
            Page.Session["mNote"] = null;
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
        ret.TableSection = TableRowSection.TableHeader;
        ret.Cells.Add(Utilities.addHeaderCell("ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Last Name"));
        ret.Cells.Add(Utilities.addHeaderCell("First Name"));
        //ret.Cells.Add(Utilities.addHeaderCell("Guardian"));
        //ret.Cells.Add(Utilities.addHeaderCell("Date of Birth"));
        ret.Cells.Add(Utilities.addHeaderCell("Phone"));
        ret.Cells.Add(Utilities.addHeaderCell("Street"));
        ret.Cells.Add(Utilities.addHeaderCell("City"));
        //ret.Cells.Add(Utilities.addHeaderCell("State"));
        //ret.Cells.Add(Utilities.addHeaderCell("Zip"));
        //ret.Cells.Add(Utilities.addHeaderCell("Quota"));
        ret.Cells.Add(Utilities.addHeaderCell("Adult"));
        //ret.Cells.Add(Utilities.addHeaderCell("Ethnicity"));
        ret.Cells.Add(Utilities.addHeaderCell("Tech Restricted"));
        //ret.Cells.Add(Utilities.addHeaderCell("West Central"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit/Delete"));
        //ret.Cells.Add(Utilities.addHeaderCell("Check-Out"));

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
        ret.TableSection = TableRowSection.TableBody;
        ret.Cells.Add(Utilities.addCell(m.member_id.ToString()));
        ret.Cells.Add(Utilities.addCell(m.last_name));
        ret.Cells.Add(Utilities.addCell(m.first_name));
        //ret.Cells.Add(Utilities.addCell(m.guardian_name));
        //ret.Cells.Add(Utilities.addCell(m.dob.ToShortDateString()));
        ret.Cells.Add(Utilities.addCell(m.phone));
        ret.Cells.Add(Utilities.addCell(m.street_address));
        ret.Cells.Add(Utilities.addCell(m.city));
        //ret.Cells.Add(Utilities.addCell(m.state));
        //ret.Cells.Add(Utilities.addCell(m.zip.ToString()));
        //ret.Cells.Add(Utilities.addCell(m.quota.ToString()));
        ret.Cells.Add(Utilities.addCell(m.is_adult.ToString()));
        //ret.Cells.Add(Utilities.addCell(m.ethnicity));
        ret.Cells.Add(Utilities.addCell(m.restricted_to_tech.ToString()));
        //ret.Cells.Add(Utilities.addCell(m.west_central_resident.ToString()));
        ret.Cells.Add(addButtonCell(m.member_id));
        //ret.Cells.Add(addCheckOutCell(m.member_id));
        return ret;
    }


    private TableCell addCheckOutCell(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton checkOut = new HtmlButton();
        checkOut.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        checkOut.Attributes.Add("id", id.ToString());
        checkOut.Attributes["title"] = "Check-Out";
        checkOut.Attributes["type"] = "button";
        checkOut.InnerHtml = "<i class = \"material-icons\">shopping_cart</i>";
        ret.Controls.Add(checkOut);
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
        //HtmlButton del = new HtmlButton();
        HtmlButton edit = new HtmlButton();

        /*del.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
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
        

        del.Attributes["title"] = "Delete";*/

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        //edit.Attributes["title"] = "Edit";

        //edit.ServerClick += new EventHandler(editClick);

        ret.Controls.Add(edit);
        //ret.Controls.Add(del);

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
        int id = Int32.Parse(((HtmlButton)sender).Attributes["id"]);
        Page.Response.Redirect("EditMember.aspx?member_id=" + id);


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
    [WebMethod]
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
        if (!firstName.Text.Equals(""))
        {
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
        else
        {
            Response.Write(@"<script langauge='text/javascript'>alert('Member Name is blank');</script>");
        }
    }

    [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GetData(object parameters)
    {
        try
        {
            // Initialization.   
            var req = DataTableParameters.Get(parameters);
            var resultSet = new DataTableResultSet();

            List<Member> data = Utilities.getMemberList();

            resultSet.draw = req.Draw;
            resultSet.recordsTotal = data.Count();

            string search = req.SearchValue;
            int order = req.Order[0].Column;
            string orderDirection = req.Order[0].Direction;

            int startRec = req.Start;
            int pageSize = req.Length;

            //Search
            if (!string.IsNullOrEmpty(search) &&
               !string.IsNullOrWhiteSpace(search))
            {
                // Apply search   
                List<Member> searchResults = new List<Member>();

                foreach (Member m in data)
                {
                    if(
                        Utilities.containsStr(search, m.member_id.ToString()) ||
                        Utilities.containsStr(search, m.last_name) ||
                        Utilities.containsStr(search, m.first_name) ||
                        Utilities.containsStr(search, m.guardian_name) ||
                        Utilities.containsStr(search, m.phone) ||
                        Utilities.containsStr(search, m.city)
                        )
                    {
                        searchResults.Add(m);
                    }
                }
                data = searchResults;

            }
            resultSet.recordsFiltered = data.Count();

            //Sorting
            data = Utilities.SortByColumnWithOrder(order, orderDirection, data);
            
  
            // Apply pagination.   
            data = data.Skip(startRec).Take(pageSize).ToList();

            foreach (Member m in data)
            { 
                var columns = new List<string>();
                columns.Add(m.member_id.ToString());
                columns.Add(m.last_name);
                columns.Add(m.first_name);
                columns.Add(m.phone);
                columns.Add(m.email);
                columns.Add(m.city);
                columns.Add(m.state);
                columns.Add("<button id=" + m.member_id.ToString() + " class='mdl-button mdl-js-button mdl-button--icon' onClick='editMember(event); return false' title ='Edit'><i class= 'material-icons'>edit</i></button>"

                    );

                resultSet.data.Add(columns);
            }
            SendResponse(HttpContext.Current.Response, resultSet);



            
        }
        catch (Exception ex)
        {
            // Info   
            Console.Write(ex);
        }
        // Return info.   
        
    }
    private static void SendResponse(HttpResponse response, DataTableResultSet result)
    {
        response.Clear();
        response.Headers.Add("X-Content-Type-Options", "nosniff");
        response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        response.ContentType = "application/json; charset=utf-8";
        response.Write(result.ToJSON());
        response.Flush();
        response.End();
    }

}
