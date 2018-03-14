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
        ret.Cells.Add(addCell("ID"));
        ret.Cells.Add(addCell("Title"));
        ret.Cells.Add(addCell("Author"));
        ret.Cells.Add(addCell("Category"));
        ret.Cells.Add(addCell("Publisher"));
        ret.Cells.Add(addCell("Year"));
        ret.Cells.Add(addCell("Pages"));
        ret.Cells.Add(addCell("ISBN"));
        ret.Cells.Add(addCell("ISBN13"));

        ret.BorderWidth = 5;
        return ret;
    }

    private TableHeaderRow addDVDTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addCell("ID"));
        ret.Cells.Add(addCell("Title"));
        ret.Cells.Add(addCell("Year"));
        ret.Cells.Add(addCell("Rating"));

        ret.BorderWidth = 5;
        return ret;
    }

    private TableHeaderRow addTechTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addCell("ID"));
        ret.Cells.Add(addCell("Name"));

        ret.BorderWidth = 5;
        return ret;
    }


    private TableRow addBookRow(Book b)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(b.Id.ToString()));
        ret.Cells.Add(addCell(b.Title));
        ret.Cells.Add(addCell(b.Author));
        ret.Cells.Add(addCell(b.Category));
        ret.Cells.Add(addCell(b.Publisher));
        ret.Cells.Add(addCell(b.Year.ToString()));
        ret.Cells.Add(addCell(b.Pages.ToString()));
        ret.Cells.Add(addCell(b.Isbn10));
        ret.Cells.Add(addCell(b.Isbn13));
        return ret;
    }

    private TableRow addDVDRow(DVD d)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(d.ItemId.ToString()));
        ret.Cells.Add(addCell(d.Title));
        ret.Cells.Add(addCell(d.ReleaseYear.ToString()));
        ret.Cells.Add(addCell(d.Rating));

        return ret;
    }

    private TableRow addTechRow(Technology t)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(t.ItemId.ToString()));
        ret.Cells.Add(addCell(t.Name));

        return ret;
    }

    private TableCell addCell(string content)
    {
        TableCell ret = new TableCell();
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