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
        client.Headers.Add(Utilities.getApiKey());

        var response = client.DownloadString("http://api.sparklib.org/api/Member");

        return new JavaScriptSerializer().Deserialize<List<Member>>(response);

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
            client.Headers.Add(Utilities.getApiKey());

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
                client.Headers.Add(Utilities.getApiKey());

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
            int order = req.Order.Values.ElementAt(0).Column;
            string orderDirection = req.Order.Values.ElementAt(0).Direction;

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
                        Utilities.containsStr(search, m.email)
                        )
                    {
                        searchResults.Add(m);
                    }
                }
                data = searchResults;

            }
            resultSet.recordsFiltered = data.Count();

            //Sorting
            data = SortByColumnWithOrder(order, orderDirection, data);
            
  
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
                columns.Add(m.guardian_name);
                columns.Add(m.restricted_to_tech.ToString());
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

    public static List<Member> SortByColumnWithOrder(int order, string orderDir, List<Member> data)
    {
        // Initialization.   
        // Sorting   
        switch (order)
        {
            case 0:
                if (orderDir.Equals("ASC"))
                {
                    data = data.OrderBy(m => m.member_id).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.member_id).ToList();
                }
                break;
            case 1:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.last_name).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.last_name).ToList();
                }
                break;
            case 2:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.first_name).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.first_name).ToList();
                }
                break;
            case 3:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.phone).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.phone).ToList();
                }
                break;
            case 4:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.email).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.email).ToList();
                }
                break;
            case 5:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.guardian_name).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.guardian_name).ToList();
                }
                break;
            case 6:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.restricted_to_tech).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.restricted_to_tech).ToList();
                }
                break;
            default:
                if (orderDir.ToLower().Equals("asc"))
                {
                    data = data.OrderBy(m => m.member_id).ToList();
                }
                else
                {
                    data = data.OrderByDescending(m => m.member_id).ToList();
                }
                break;
        }
        return data;
    }

}
