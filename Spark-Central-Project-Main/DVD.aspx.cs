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

public partial class Catalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        loadDvd();

        if (Page.Session["cNote"] != null)
        {
            NoteLabel.Text = Page.Session["cNote"].ToString().ToUpper();
            Page.Session["cNote"] = null;
        }
    }

    protected void loadDvd()
    {
        BookImage.ImageUrl = "images/book.png";
        DVDImage.ImageUrl = "images/dvdS.png";
        TechImage.ImageUrl = "images/technology.png";

        table.Rows.Clear();
        table.Rows.Add(addDVDTitleRow());

        List<DVD> dvdList;

        dvdList = getDVDList();


        foreach (DVD cur in dvdList)
        {
            table.Rows.Add(addDVDRow(cur));
        }
    }


    protected void buttonBooks_Click(object sender, EventArgs e)
    {
        Session["CatalogMode"] = "Books";
        Response.Redirect("Books.aspx");

    }

    protected void buttonDVD_Click(object sender, EventArgs e)
    {
        Session["CatalogMode"] = "DVD";
        Response.Redirect("DVD.aspx");

    }

    protected void buttonTech_Click(object sender, EventArgs e)
    {
        Session["CatalogMode"] = "Technology";
        Response.Redirect("Technology.aspx");

    }

    protected void SubmitBook_Click(object sender, EventArgs e)
    {
        int year;
        int pages;

        Book b = new Book();
        b.title = bookTitle.Text;
        b.author = bookAuthor.Text;
        b.publisher = bookPublisher.Text;

        Int32.TryParse(bookYear.Text, out year);
        Int32.TryParse(bookPages.Text, out pages);
        b.publication_year = year;
        b.pages = pages;

        b.category = bookCategory.Text;
        b.description = bookDescription.Text;
        b.isbn_10 = bookIsnb10.Text;
        b.isbn_13 = bookIsbn13.Text;

        if (!b.title.Equals(""))
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(b);

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers.Add(Utilities.getApiKey());

                try
                {
                    client.UploadString(new Uri("http://api.sparklib.org/api/book"), "POST", json);
                    var response = client.ResponseHeaders;
                    string location = response.Get("Location");
                    string id = location.Split('=')[1];

                    Page.Session["cNote"] = "Book Added With ID: " + id;

                    Response.Redirect("Catalog.aspx");

                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            Response.Write(@"<script langauge='text/javascript'>alert('Book Title was left blank');</script>");
        }
    }

    protected void SubmitDvd_Click(object sender, EventArgs e)
    {
        int year;

        DVD d = new DVD();
        d.title = dvdTitle.Text;
        Int32.TryParse(dvdYear.Text, out year);
        d.release_year = year;
        d.rating = dvdRating.Text;

        if (!d.title.Equals(""))
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(d);

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers.Add(Utilities.getApiKey());

                try
                {
                    client.UploadString(new Uri("http://api.sparklib.org/api/dvd"), "POST", json);
                    var response = client.ResponseHeaders;
                    string location = response.Get("Location");
                    string id = location.Split('=')[1];

                    Page.Session["cNote"] = "DVD Added With ID: " + id;

                    Response.Redirect("Catalog.aspx");

                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            Response.Write(@"<script langauge='text/javascript'>alert('DVD Title was left blank');</script>");
        }
    }

    protected void SubmitTech_Click(object sender, EventArgs e)
    {
        Technology t = new Technology();
        t.name = techName.Text;

        if (!t.name.Equals(""))
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(t);

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                client.Headers.Add(Utilities.getApiKey());

                try
                {
                    client.UploadString(new Uri("http://api.sparklib.org/api/technology"), "POST", json);
                    var response = client.ResponseHeaders;
                    string location = response.Get("Location");
                    string id = location.Split('=')[1];

                    Page.Session["cNote"] = "Technology Added With ID: " + id;

                    Response.Redirect("Catalog.aspx");

                }
                catch (Exception ex)
                {
                }
            }
        }
        else
        {
            Response.Write(@"<script langauge='text/javascript'>alert('Tech name was left blank');</script>");
        }
    }

    private List<DVD> getDVDList()
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        var response = client.DownloadString("http://api.sparklib.org/api/dvd");

        return new JavaScriptSerializer().Deserialize<List<DVD>>(response);
    }

    private TableHeaderRow addDVDTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.TableSection = TableRowSection.TableHeader;
        ret.Cells.Add(Utilities.addHeaderCell("ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Assn"));
        ret.Cells.Add(Utilities.addHeaderCell("Title"));
        ret.Cells.Add(Utilities.addHeaderCell("Year"));
        ret.Cells.Add(Utilities.addHeaderCell("Rating"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableRow addDVDRow(DVD d)
    {
        TableRow ret = new TableRow();
        ret.TableSection = TableRowSection.TableBody;
        ret.Cells.Add(Utilities.addCell(d.item_id.ToString()));
        ret.Cells.Add(Utilities.addCell(d.assn.ToString()));
        ret.Cells.Add(Utilities.addCell(d.title));
        ret.Cells.Add(Utilities.addCell(d.release_year.ToString()));
        ret.Cells.Add(Utilities.addCell(d.rating));
        ret.Cells.Add(addButtonCell_DVD(d.item_id));

        return ret;
    }

    protected void editDVDClick(object sender, EventArgs e)
    {
        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditDvd.aspx?item_id=" + id);
    }

    [System.Web.Services.WebMethod]
    public static void deleteDVDClick(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/dvd?item_id=" + id.ToString();
                client.UploadString(new Uri(apiString), "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    private TableCell addButtonCell_DVD(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton edit = new HtmlButton();

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";

        edit.ServerClick += new EventHandler(editDVDClick);

        ret.Controls.Add(edit);

        return ret;
    }

    [System.Web.Services.WebMethod]
    public static string Submit_Click(Book b)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(b);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                client.UploadString(new Uri("http://api.sparklib.org/api/book"), "POST", json);

                var response = client.ResponseHeaders;
                string location = response.Get("Location");
                string id = location.Split('=')[1];
                return id;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}