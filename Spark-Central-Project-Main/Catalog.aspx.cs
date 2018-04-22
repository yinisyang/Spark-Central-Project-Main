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

public partial class Catalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            resetButtonColors();
        }
    }



    protected void buttonBooks_Click(object sender, EventArgs e)
    {
        resetButtonColors();
        buttonBooks.BackColor = System.Drawing.Color.BlanchedAlmond;

        table.Rows.Clear();
        table.Rows.Add(addBookTitleRow());

        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/book");

        foreach (Book cur in new JavaScriptSerializer().Deserialize<List<Book>>(response))
        {
            table.Rows.Add(addBookRow(cur));
        }

    }

    protected void buttonDVD_Click(object sender, EventArgs e)
    {
        resetButtonColors();
        buttonDVD.BackColor = System.Drawing.Color.BlanchedAlmond;

        table.Rows.Clear();
        table.Rows.Add(addDVDTitleRow());

        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/dvd");

        foreach (DVD cur in new JavaScriptSerializer().Deserialize<List<DVD>>(response))
        {
            table.Rows.Add(addDVDRow(cur));
        }

    }

    protected void buttonTech_Click(object sender, EventArgs e)
    {
        resetButtonColors();
        buttonTech.BackColor = System.Drawing.Color.BlanchedAlmond;

        table.Rows.Clear();
        table.Rows.Add(addTechTitleRow());

        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/technology");

        foreach (Technology cur in new JavaScriptSerializer().Deserialize<List<Technology>>(response))
        {
            table.Rows.Add(addTechRow(cur));
        }
    }

    private TableHeaderRow addBookTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addHeaderCell("ID"));
        ret.Cells.Add(addHeaderCell("Title"));
        ret.Cells.Add(addHeaderCell("Author"));
        ret.Cells.Add(addHeaderCell("Category"));
        ret.Cells.Add(addHeaderCell("Publisher"));
        ret.Cells.Add(addHeaderCell("Year"));
        ret.Cells.Add(addHeaderCell("Pages"));
        ret.Cells.Add(addHeaderCell("ISBN"));
        ret.Cells.Add(addHeaderCell("ISBN13"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableHeaderRow addDVDTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addHeaderCell("ID"));
        ret.Cells.Add(addHeaderCell("Title"));
        ret.Cells.Add(addHeaderCell("Year"));
        ret.Cells.Add(addHeaderCell("Rating"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableHeaderRow addTechTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addHeaderCell("ID"));
        ret.Cells.Add(addHeaderCell("Name"));

        ret.BorderWidth = 3;
        return ret;
    }


    private TableRow addBookRow(Book b)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(b.item_id.ToString()));
        ret.Cells.Add(addCell(b.title));
        ret.Cells.Add(addCell(b.author));
        ret.Cells.Add(addCell(b.category));
        ret.Cells.Add(addCell(b.publisher));
        ret.Cells.Add(addCell(b.publication_year.ToString()));
        ret.Cells.Add(addCell(b.pages.ToString()));
        ret.Cells.Add(addCell(b.isbn_10));
        ret.Cells.Add(addCell(b.isbn_13));
        return ret;
    }

    private TableRow addDVDRow(DVD d)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(d.item_id.ToString()));
        ret.Cells.Add(addCell(d.title));
        ret.Cells.Add(addCell(d.release_year.ToString()));
        ret.Cells.Add(addCell(d.rating));

        return ret;
    }

    private TableRow addTechRow(Technology t)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(t.item_id.ToString()));
        ret.Cells.Add(addCell(t.name));

        return ret;
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

    private void resetButtonColors()
    {
        buttonBooks.BackColor = System.Drawing.Color.LightGray;
        buttonDVD.BackColor = System.Drawing.Color.LightGray;
        buttonTech.BackColor = System.Drawing.Color.LightGray;
    }
}