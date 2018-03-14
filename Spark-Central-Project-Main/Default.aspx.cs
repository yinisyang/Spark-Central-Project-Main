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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            var client = new WebClient();
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            var response = client.DownloadString("http://api.sparklib.org/api/book");

            foreach( Book cur in new JavaScriptSerializer().Deserialize<List<Book>>(response))
            {
                table.Rows.Add(addBookRow(cur));
            }
        }
    }

    private TableRow addBookRow(Book b)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(b.Id.ToString()));
        ret.Cells.Add(addCell(b.Title));
        ret.Cells.Add(addCell(b.Author));
        ret.Cells.Add(addCell(b.Pages.ToString()));
        return ret;
    }

    private TableCell addCell(string content)
    {
        TableCell ret = new TableCell();
        ret.Text = content;
        return ret;
    }
}