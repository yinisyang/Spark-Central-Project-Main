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

public partial class Circulations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        table.Rows.Add(addTitleRow());

        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        var response = client.DownloadString("http://api.sparklib.org/api/checkout");

        foreach (Checkout cur in new JavaScriptSerializer().Deserialize<List<Checkout>>(response))
        {
            if (cur.resolved == false)
            {
                table.Rows.Add(addRow(cur));
            }
        }


        finetable.Rows.Add(addFineTitleRow());
        response = client.DownloadString("http://api.sparklib.org/api/fines");

        foreach (Fine cur in new JavaScriptSerializer().Deserialize<List<Fine>>(response))
        {
            finetable.Rows.Add(addFineRow(cur));
        }
    }

    private TableHeaderRow addTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.TableSection = TableRowSection.TableHeader;
        ret.Cells.Add(Utilities.addHeaderCell("Item Assn"));
        ret.Cells.Add(Utilities.addHeaderCell("Item Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Item Type"));
        ret.Cells.Add(Utilities.addHeaderCell("Member ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Member Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Issue Date"));
        ret.Cells.Add(Utilities.addHeaderCell("Due Date"));
        //ret.Cells.Add(Utilities.addHeaderCell("Resolved"));
        ret.Cells.Add(Utilities.addHeaderCell("Check-In"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableHeaderRow addFineTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.TableSection = TableRowSection.TableHeader;
        ret.Cells.Add(Utilities.addHeaderCell("Fine ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Member ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Member Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Description"));
        ret.Cells.Add(Utilities.addHeaderCell("Amount"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableCell addCheckInCell(int itemid, int memberid, string type)
    {
        TableCell ret = new TableCell();
        HtmlButton checkIn = new HtmlButton();
        checkIn.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        checkIn.Attributes.Add("memberid", memberid.ToString());
        checkIn.Attributes.Add("itemid", itemid.ToString());
        checkIn.Attributes.Add("itemtype", type);
        checkIn.Attributes["title"] = "Check-In";
        checkIn.Attributes["type"] = "button";
        checkIn.InnerHtml = "<i class = \"material-icons\">done</i>";

        checkIn.ServerClick += new EventHandler(checkInClick);
        ret.Controls.Add(checkIn);
        return ret;
    }

    protected void checkInClick(object sender, EventArgs e)
    {
        string memberid = ((HtmlButton)sender).Attributes["memberid"];
        string itemid = ((HtmlButton)sender).Attributes["itemid"];
        string type = ((HtmlButton)sender).Attributes["typetype"];

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/checkout?member_id=" + memberid + "&item_id=" + itemid + "&item_type=" + type;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }

        }

    }

    private TableRow addFineRow(Fine f)
    {
        TableRow ret = new TableRow();
        ret.TableSection = TableRowSection.TableBody;

        ret.Cells.Add(Utilities.addCell(f.fine_id.ToString()));
        ret.Cells.Add(Utilities.addCell(f.member_id.ToString()));

        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        var response = client.DownloadString("http://api.sparklib.org/api/member?member_id=" + f.member_id.ToString());
        Member m = new JavaScriptSerializer().Deserialize<Member>(response);
        ret.Cells.Add(Utilities.addCell(m.first_name + " " + m.last_name));

        ret.Cells.Add(Utilities.addCell(f.description));
        ret.Cells.Add(Utilities.addCell("$"+f.amount.ToString("F2")));
        ret.Cells.Add(addButtonCell_Fine(f.fine_id));

        return ret;
    }

    private TableCell addButtonCell_Fine(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton edit = new HtmlButton();

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";

        edit.ServerClick += new EventHandler(editFineClick);

        ret.Controls.Add(edit);

        return ret;
    }

    protected void editFineClick(object sender, EventArgs e)
    {
        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditFine.aspx?fine_id=" + id);
    }


    private TableRow addRow(Checkout c)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        TableRow ret = new TableRow();
        ret.TableSection = TableRowSection.TableBody;

        if (c.item_type.Equals("book"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/book?item_id=" + c.item_id.ToString());
                Book b = new JavaScriptSerializer().Deserialize<Book>(response);
                ret.Cells.Add(Utilities.addCell(b.assn.ToString()));
                ret.Cells.Add(Utilities.addCell(b.title));
            }
            catch (Exception e)
            {
                ret.Cells.Add(Utilities.addCell("N/A"));
            }
        }

        else if (c.item_type.Equals("technology"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/technology?item_id=" + c.item_id.ToString());
                Technology t = new JavaScriptSerializer().Deserialize<Technology>(response);
                ret.Cells.Add(Utilities.addCell(t.assn.ToString()));
                ret.Cells.Add(Utilities.addCell(t.name));
            }
            catch (Exception e)
            {
                ret.Cells.Add(Utilities.addCell("N/A"));
            }
        }

        else if (c.item_type.Equals("dvd"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/dvd?item_id=" + c.item_id.ToString());
                DVD d = new JavaScriptSerializer().Deserialize<DVD>(response);
                ret.Cells.Add(Utilities.addCell(d.assn.ToString()));
                ret.Cells.Add(Utilities.addCell(d.title));
            }
            catch (Exception e)
            {
                ret.Cells.Add(Utilities.addCell("N/A"));
            }
        }

        else
        {
            ret.Cells.Add(Utilities.addCell("N/A"));
        }

        ret.Cells.Add(Utilities.addCell(c.item_type));
        ret.Cells.Add(Utilities.addCell(c.member_id.ToString()));


        try
        {
            var response = client.DownloadString("http://api.sparklib.org/api/member?member_id=" + c.member_id.ToString());
            Member m = new JavaScriptSerializer().Deserialize<Member>(response);
            ret.Cells.Add(Utilities.addCell(m.first_name + " " + m.last_name));
        }
        catch (Exception e)
        {
            ret.Cells.Add(Utilities.addCell("N/A"));
        }


        ret.Cells.Add(Utilities.addCell(c.checkout_date.ToShortDateString()));

        ret.Cells.Add(Utilities.addCell(c.due_date.ToShortDateString()));
        //ret.Cells.Add(Utilities.addCell(c.resolved.ToString()));
        ret.Cells.Add(addCheckInCell(c.item_id, c.member_id, c.item_type));
        return ret;
    }

    protected void Submit_ClickCheckOut(object sender, EventArgs e)
    {
        Page.Session["CheckOutMemberID"] = txtmemid.Text;
        Page.Session["CheckOutItemAssn"] = txtitemassn.Text;
        Response.Redirect("CheckOutConfirm.aspx");
    }


    protected void SubmitFine_Click(object sender, EventArgs e)
    {
        Fine f = new Fine();
        int mid;
        double amount;
        Int32.TryParse(txtMemberId.Text, out mid);
        Double.TryParse(txtAmount.Text, out amount);

        f.member_id = mid;
        f.amount = amount;
        f.description = txtFineDescription.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(f);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                client.UploadString(new Uri("http://api.sparklib.org/api/fines"), "POST", json);
                var response = client.ResponseHeaders;

                Response.Redirect("Circulations.aspx");

            }
            catch (Exception ex)
            {
            }
        }

    }
}