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
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/checkout");

        foreach (Checkout cur in new JavaScriptSerializer().Deserialize<List<Checkout>>(response))
        {
            if(cur.resolved == false)
            {
                table.Rows.Add(addRow(cur));
            }
        }
    }

    private TableHeaderRow addTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.TableSection = TableRowSection.TableHeader;
        ret.Cells.Add(Utilities.addHeaderCell("Item ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Item Type"));
        ret.Cells.Add(Utilities.addHeaderCell("Item Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Member ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Member Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Issue Date"));
        ret.Cells.Add(Utilities.addHeaderCell("Due Date"));
        //ret.Cells.Add(Utilities.addHeaderCell("Resolved"));
        ret.Cells.Add(Utilities.addHeaderCell("Check-In"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableCell addCheckInCell(int id, string type)
    {
        TableCell ret = new TableCell();
        HtmlButton checkIn = new HtmlButton();
        checkIn.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        checkIn.Attributes.Add("id", id.ToString());
        checkIn.Attributes.Add("type", type);
        checkIn.Attributes["title"] = "Check-In";
        checkIn.Attributes["type"] = "button";
        checkIn.InnerHtml = "<i class = \"material-icons\">done</i>";
        ret.Controls.Add(checkIn);
        return ret;
    }


    private TableRow addRow(Checkout c)
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");




        TableRow ret = new TableRow();
        ret.TableSection = TableRowSection.TableBody;

        ret.Cells.Add(Utilities.addCell(c.item_id.ToString()));
        ret.Cells.Add(Utilities.addCell(c.item_type));

        if (c.item_type.Equals("book"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/book?item_id=" + c.item_id.ToString());
                Book b = new JavaScriptSerializer().Deserialize<Book>(response);
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

        ret.Cells.Add(Utilities.addCell(c.member_id.ToString()));


        try
        {
            var response = client.DownloadString("http://api.sparklib.org/api/member?member_id=" + c.member_id.ToString());
            Member m = new JavaScriptSerializer().Deserialize<Member>(response);
            ret.Cells.Add(Utilities.addCell(m.first_name + " " + m.last_name));
        }
        catch(Exception e)
        {
            ret.Cells.Add(Utilities.addCell("N/A"));
        }


        ret.Cells.Add(Utilities.addCell("N/A"));

        ret.Cells.Add(Utilities.addCell(c.due_date.ToShortDateString()));
        //ret.Cells.Add(Utilities.addCell(c.resolved.ToString()));
        ret.Cells.Add(addCheckInCell(c.item_id, c.item_type));
        return ret;
    }
}