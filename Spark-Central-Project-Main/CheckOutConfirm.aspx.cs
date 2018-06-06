using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using SparkWebSite;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;


public partial class _Default : System.Web.UI.Page
{

    /*
     * Page_Load()
     * 
     * When the CheckOutConfirm Page is loaded it needs to identify the member and item involved in the Checkout transaction.
     * It requires that the Member ID and the Item ASSN number are stored in the Session State as follows:
     * 
     * Member ID -> Page.Session["CheckOutMemberID"]
     * Item ASSN -> Page.Session["CheckOutItemAssn"]
     * 
     * The method makes calls to the api to gain information on the member and item respectively and prepares to
     * create a CheckOut object should the user press the confirm button.
     * 
     */ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Page.Session["CheckoutMemberID"] == null || Page.Session["CheckOutItemAssn"] == null)
        {
            Response.Redirect("Circulations.aspx");
        }

        string memberid = Page.Session["CheckOutMemberID"].ToString();
        int itemAssn;
        Int32.TryParse(Page.Session["CheckOutItemAssn"].ToString(), out itemAssn);
        lblMemberNumber.Text = "<b>Member ID:</b> " + memberid;
        lblItemNumber.Text = "<b>Item ASSN:</b> " + itemAssn.ToString();
        TxtDate.Text = DateTime.Now.AddDays(7).ToShortDateString();

        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        try
        {
            var response = client.DownloadString("http://api.sparklib.org/api/Member?member_id=" + memberid);
            Member m = new JavaScriptSerializer().Deserialize<Member>(response);
            lblName.Text = "<b>Member Name:</b> " + m.first_name + " " + m.last_name;
        }
        catch(Exception error)
        {
            Console.WriteLine(error.Message);
            lblName.Text = "Something Went Wrong. Please check your information and try again.";
            lblItemNumber.Text = "";
            lblMemberNumber.Text = "";
        }

        findAssn(itemAssn);

        if(Page.Session["itemtype"] == null)
        {
            return;
        }

        if (Page.Session["itemType"].ToString().Equals("technology"))
        {
            Technology t = (Technology)Page.Session["item"];
            lblItem.Text = "<b>Technology Name:</b> " + t.name;
            Page.Session["itemid"] = t.item_id;
        }
        else if (Page.Session["itemType"].ToString().Equals("book"))
        {
            Book b = (Book)Page.Session["item"];
            lblItem.Text = "<b>Book Title:</b> " + b.title;
            Page.Session["itemid"] = b.item_id;
        }
        else if (Page.Session["itemType"].ToString().Equals("dvd"))
        {
            DVD d = (DVD)Page.Session["item"];
            lblItem.Text = "<b>DVD Title:</b> " + d.title;
            Page.Session["itemid"] = d.item_id;
        }

    }


    /*
     *  Submit_ClickCancel()
     * 
     *  This method fires when the user clicks the cancel button.
     *  It simply navigates the user back to the Circulations Page.
     * 
     * 
     */ 
    protected void Submit_ClickCancel(object sender, EventArgs e)
    {
        Response.Redirect("Circulations.aspx");
    }


    /*
     * Submit_ClickConfirm()
     * 
     * This method fires after the user clicks the confirm button for the Check-Out.
     * It assembles the checkout object, creates a POST request for the API, and sends it.
     * The desired effect of this will be to add the new check out to the database.
     * 
     * returns: void
     * 
     */ 
    protected void Submit_ClickConfirm(object sender, EventArgs e)
    {
        if(Page.Session["itemType"] == null || Page.Session["itemid"] == null && Page.Session["CheckOutMemberID"] == null)
        {
            Response.Redirect("Circulations.aspx");
        }
        var co = new
        {
            item_id = (int)Page.Session["itemid"],
            member_id = Int32.Parse((String)Page.Session["CheckOutMemberID"]),
            item_type = (string)Page.Session["itemType"],
            due_date = TxtDate.Text,
            checkout_date = DateTime.Now.ToString("yyyy-MM-dd"),
            resolved = false
        };

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(co);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                client.UploadString(new Uri("http://api.sparklib.org/api/checkout"), "POST", json);
                var response = client.ResponseHeaders;
                string location = response.Get("Location");
                string id = location.Split('=')[1];

                Response.Redirect("Circulations.aspx");

            }
            catch (Exception ex)
            {
            }
        }

        Response.Redirect("Circulations.aspx");
    }



    /*
     *  findAssn(int assnNumber)
     *  
     *  Params: int assnNumber -> the assn number of the item to be searched for.
     *  
     *  This method traverses the Books, DVD and Technology tables in search of the item with the specified ASSN.
     *  After it has found such item it sets the item type and id into the Session State for use elsewhere.
     * 
     *  returns: void
     * 
     */ 
    private void findAssn(int assnNumber)
    {
        //Initialize WebClient
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        //Search Technology
        var response = client.DownloadString("http://api.sparklib.org/api/Technology");
        List<Technology> tList = new JavaScriptSerializer().Deserialize<List<Technology>>(response);
        foreach (Technology t in tList)
        {
            if (t.assn == assnNumber)
            {
                Page.Session["itemType"] = "technology";
                Page.Session["item"] = t;
                return;
            }
        }

        //Search Books
        response = client.DownloadString("http://api.sparklib.org/api/Book");
        List<Book> bList = new JavaScriptSerializer().Deserialize<List<Book>>(response);
        foreach (Book b in bList)
        {
            if (b.assn == assnNumber)
            {
                Page.Session["itemType"] = "book";
                Page.Session["item"] = b;
                return;
            }
        }

        //Search DVDs
        response = client.DownloadString("http://api.sparklib.org/api/DVD");
        List<DVD> dvdList = new JavaScriptSerializer().Deserialize<List<DVD>>(response);
        foreach (DVD d in dvdList)
        {
            if (d.assn == assnNumber)
            {
                Page.Session["itemType"] = "dvd";
                Page.Session["item"] = d;
                return;
            }
        }
    }
}