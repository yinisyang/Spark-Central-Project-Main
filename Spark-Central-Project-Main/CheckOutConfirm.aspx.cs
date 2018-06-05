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
    protected void Page_Load(object sender, EventArgs e)
    {

        string memberid = Page.Session["CheckOutMemberID"].ToString();
        int itemAssn;
        Int32.TryParse(Page.Session["CheckOutItemAssn"].ToString(), out itemAssn);
        TxtMemberId.Text = memberid;
        TxtItemAssn.Text = itemAssn.ToString();
        TxtDate.Text = DateTime.Now.AddDays(7).ToShortDateString();

        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        var response = client.DownloadString("http://api.sparklib.org/api/Member?member_id=" + memberid);

        Member m = new JavaScriptSerializer().Deserialize<Member>(response);
        findAssn(itemAssn);

        if (Page.Session["itemType"].ToString().Equals("technology"))
        {
            Technology t = (Technology)Page.Session["item"];
            lblItem.Text = t.name;
            Page.Session["itemid"] = t.item_id;
        }
        else if (Page.Session["itemType"].ToString().Equals("book"))
        {
            Book b = (Book)Page.Session["item"];
            lblItem.Text = b.title;
            Page.Session["itemid"] = b.item_id;
        }
        else if (Page.Session["itemType"].ToString().Equals("dvd"))
        {
            DVD d = (DVD)Page.Session["item"];
            lblItem.Text = d.title;
            Page.Session["itemid"] = d.item_id;
        }



        lblName.Text = m.first_name + " " + m.last_name;


    }

    protected void Submit_ClickCancel(object sender, EventArgs e)
    {
        Response.Redirect("Circulations.aspx");
    }

    protected void Submit_ClickConfirm(object sender, EventArgs e)
    {
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


    private void findAssn(int assnNumber)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

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