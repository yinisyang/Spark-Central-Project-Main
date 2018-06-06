using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
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

    /*
     * loadDvd()
     * 
     * this method initializes the DVD table and fills it with data gained from the API.
     * 
     * 
     */ 
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

    /*
     *  The below 3 methods handle when the user clicks one of the Picture buttons
     *  They will navigate the user to the corresponding page.
     * 
     */
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

    /*
     * SubmitBook_Click()
     * 
     * This method fires when the user clicks the submit button inside the standard add book dialiog.
     * It retrieves data from the add book dialog and creates a book object
     * and then sends this object to the API via a POST request.
     * 
     */
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

        b.assn = Utilities.getNextAssn();
        b.image = "";

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

                    Page.Session["cNote"] = "Book Added With Assn: " + b.assn;

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

    /*
     * SubmitDVD_Click()
     * 
     * This method fires when the user clicks the submit button inside the add dvd dialiog.
     * It retrieves data from the add dvd dialog and creates a dvd object
     * and then sends this object to the API via a POST request.
     * 
     */
    protected void SubmitDvd_Click(object sender, EventArgs e)
    {
        int year;

        DVD d = new DVD();
        d.title = dvdTitle.Text;
        Int32.TryParse(dvdYear.Text, out year);
        d.release_year = year;
        d.rating = dvdRating.Text;
        d.assn = Utilities.getNextAssn();

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

                    Page.Session["cNote"] = "DVD Added With Assn: " + d.assn;

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

    /*
     * SubmitTech_Click()
     * 
     * This method fires when the user clicks the submit button inside the add technology dialiog.
     * It retrieves data from the add technology dialog and creates a technology object
     * and then sends this object to the API via a POST request.
     * 
     */
    protected void SubmitTech_Click(object sender, EventArgs e)
    {
        Technology t = new Technology();
        t.name = techName.Text;
        t.assn = Utilities.getNextAssn();

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

                    Page.Session["cNote"] = "Technology Added With Assn: " + t.assn;

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

    /*
     *  SubmitClick()
     *  
     *  Params: Book b -> a book object to be added to the database
     * 
     *  This method is fired when the user clicks the submit button on the smart add dialog.
     *  Takes a book object and serializes it into a Request and sends it to the API.
     * 
     * 
     */
    [System.Web.Services.WebMethod]
    public static string Submit_Click(Book b)
    {
        b.assn = Utilities.getNextAssn();
        b.image = "";
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