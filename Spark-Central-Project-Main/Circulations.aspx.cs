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
            table.Rows.Add(addRow(cur));
        }
    }

    private TableHeaderRow addTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addHeaderCell("Member ID"));
        ret.Cells.Add(addHeaderCell("Item ID"));
        ret.Cells.Add(addHeaderCell("Item Type"));
        ret.Cells.Add(addHeaderCell("Item Name"));
        ret.Cells.Add(addHeaderCell("Due Date"));
        ret.Cells.Add(addHeaderCell("Resolved"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableRow addRow(Checkout c)
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");




        TableRow ret = new TableRow();
        
        ret.Cells.Add(addCell(c.member_id.ToString()));
        ret.Cells.Add(addCell(c.item_id.ToString()));
        ret.Cells.Add(addCell(c.item_type));

        if (c.item_type.Equals("book"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/book?item_id=" + c.item_id.ToString());
                Book b = new JavaScriptSerializer().Deserialize<Book>(response);
                ret.Cells.Add(addCell(b.title));
            }
            catch(Exception e)
            {
                ret.Cells.Add(addCell("N/A"));
            }
        }

        else if (c.item_type.Equals("technology"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/technology?item_id=" + c.item_id.ToString());
                Technology t = new JavaScriptSerializer().Deserialize<Technology>(response);
                ret.Cells.Add(addCell(t.name));
            }
            catch (Exception e)
            {
                ret.Cells.Add(addCell("N/A"));
            }
        }

        else if (c.item_type.Equals("dvd"))
        {
            try
            {
                var response = client.DownloadString("http://api.sparklib.org/api/dvd?item_id=" + c.item_id.ToString());
                DVD d = new JavaScriptSerializer().Deserialize<DVD>(response);
                ret.Cells.Add(addCell(d.title));           }
            catch (Exception e)
            {
                ret.Cells.Add(addCell("N/A"));
            }
        }

        else
        {
            ret.Cells.Add(addCell("N/A"));
        }

        ret.Cells.Add(addCell(c.due_date.ToShortDateString()));
        ret.Cells.Add(addCell(c.resolved.ToString()));
        return ret;
    }


    private TableHeaderCell addHeaderCell(string content)
    {
        TableHeaderCell ret = new TableHeaderCell();
        ret.Text = content;
        return ret;
    }

    private TableCell addCell(string content)
    {
        TableCell ret = new TableCell();
        ret.Text = content;
        return ret;
    }
}