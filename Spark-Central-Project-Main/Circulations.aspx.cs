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
    /*
     * Page_Load()
     * 
     * This page initializes the Check-Out and Fines Tables when it loads.
     * 
     * 
     * 
     */
    protected void Page_Load(object sender, EventArgs e)
    {
        //Initialize WebClient
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        //Generate CheckOut Table
        var response = client.DownloadString("http://api.sparklib.org/api/checkout");
        table.Rows.Add(addTitleRow());
        foreach (Checkout cur in new JavaScriptSerializer().Deserialize<List<Checkout>>(response))
        {
            if (cur.resolved == false)
            {
                table.Rows.Add(addRow(cur));
            }
        }

        //Generate Fines Table
        response = client.DownloadString("http://api.sparklib.org/api/fines");
        finetable.Rows.Add(addFineTitleRow());
        foreach (Fine cur in new JavaScriptSerializer().Deserialize<List<Fine>>(response))
        {
            finetable.Rows.Add(addFineRow(cur));
        }
    }

    /*
     * addTitleRow()
     * 
     * This method returns a TableHeaderRow for the CheckOut Table.
     * 
     */
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
        ret.Cells.Add(Utilities.addHeaderCell("Check-In"));

        ret.BorderWidth = 3;
        return ret;
    }

    /*
     * addFineTitleRow()
     * 
     * This method returns a TableHeaderRow for the Fines Table.
     * 
     */
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

    /*
     * addCheckInCell()
     * 
     * Params:  int itemid -> the id of the checked out item for the current row.
     *          int memberid -> the id of the member of the current row.
     *          string type -> the item type, be it book, dvd, or technology.
     * 
     * This method adds the Check-In Button for each row in the Check-Out table.
     * It stores important item and member data inside of the button for use when the button is clicked.
     * 
     * 
     */
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

    /*
     * checkInClick()
     * 
     * This method fires when a check-in button is clicked. It retrieves data from the button object,
     * and uses it to create a DELETE request to the API.
     * 
     * This removes the Check-out from the database.
     * 
     * TODO: instead of deleting the checkout, make the method do a PUT request that switches the Resolved attribute to true.
     * This will allow past check-out data to reside on the database for reports.
     * 
     */
    protected void checkInClick(object sender, EventArgs e)
    {
        //Retrieve Data from the Button
        string memberid = ((HtmlButton)sender).Attributes["memberid"];
        string itemid = ((HtmlButton)sender).Attributes["itemid"];
        string itemtype = ((HtmlButton)sender).Attributes["itemtype"];

        //Initialize WebClient
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        client.QueryString.Set("item_id", itemid);
        client.QueryString.Set("member_id", memberid);
        client.QueryString.Set("item_type", itemtype);

        var response = client.DownloadString("http://api.sparklib.org/api/checkout");
        Checkout cur = new JavaScriptSerializer().Deserialize<Checkout>(response);

        cur.resolved = true;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(cur);

        using (client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/checkout?item_id="+itemid+"&member_id="+memberid+"&item_type="+itemtype;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }

        }

    }

    /*
     * addFineRow()
     * 
     * Params: Fine f -> The fine object with which to derive data for the row to be returned.
     * 
     * This method takes a fine object and uses it to create a Row for the fine table.
     * 
     * Returns: A TableRow constructed from the Fine object passed in.
     * 
     */ 
    private TableRow addFineRow(Fine f)
    {
        TableRow ret = new TableRow();
        ret.TableSection = TableRowSection.TableBody;

        ret.Cells.Add(Utilities.addCell(f.fine_id.ToString()));
        ret.Cells.Add(Utilities.addCell(f.member_id.ToString()));

        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        try
        {
            var response = client.DownloadString("http://api.sparklib.org/api/member?member_id=" + f.member_id.ToString());
            Member m = new JavaScriptSerializer().Deserialize<Member>(response);
            ret.Cells.Add(Utilities.addCell(m.first_name + " " + m.last_name));
        }
        catch(Exception error)
        {
            Console.WriteLine(error.Message);
            ret.Cells.Add(Utilities.addCell("N/A"));
        }

        ret.Cells.Add(Utilities.addCell(f.description));
        ret.Cells.Add(Utilities.addCell("$" + f.amount.ToString("F2")));
        ret.Cells.Add(addButtonCell_Fine(f.fine_id));

        return ret;
    }


    /*
     * addButtonCell_Fine()
     * 
     * Params: int id -> the Fine ID of the current row.
     * 
     * This method creates an edit fine button for the current row in the Fines Table.
     * 
     */ 
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


    /*
     * editFineClick()
     * 
     * This method fires when an edit fine button is clicked. 
     * It retrieves the Fine Id from the button and redirects the user to the EditFine page.
     * 
     */ 
    protected void editFineClick(object sender, EventArgs e)
    {
        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditFine.aspx?fine_id=" + id);
    }


    /*
     * addRow()
     * 
     * Params: Checkout c -> the Checkout object from which to construct the TableRow
     * 
     * This method creates a TableRow for the Check-Out table using data from the passed-in Checkout object.
     * 
     * Returns: the TableRow created.
     * 
     */ 
    private TableRow addRow(Checkout c)
    {
        //Initialize WebClient
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        //Initialize Row
        TableRow ret = new TableRow();
        ret.TableSection = TableRowSection.TableBody;

        //Depending on item type, it will need to proceed differently for retrieving the information.
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
                ret.Cells.Add(Utilities.addCell("N/A"));
            }
        }

        else
        {
            ret.Cells.Add(Utilities.addCell("N/A"));
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
        ret.Cells.Add(addCheckInCell(c.item_id, c.member_id, c.item_type));
        return ret;
    }

    /*
     * Submit_ClickCheckOut()
     * 
     * This method fires when the user clickes the checkout button.
     * It stores the information from the dialog into the Session State and redirects the user
     * to the Checkout Confirm page.
     * 
     */ 
    protected void Submit_ClickCheckOut(object sender, EventArgs e)
    {
        Page.Session["CheckOutMemberID"] = txtmemid.Text;
        Page.Session["CheckOutItemAssn"] = txtitemassn.Text;
        Response.Redirect("CheckOutConfirm.aspx");
    }

    /*
     * SubmitFine_Click()
     * 
     * This method fires when the user clicks the submit fine button in the Add Fine dialog.
     * It takes the information from the dialog boxes and creates a Fine object,
     * then it performs a PUT request on the API to add it to the database.
     * 
     */ 
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