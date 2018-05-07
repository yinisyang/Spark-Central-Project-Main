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

        if (Page.Session["CatalogMode"] != null)
        {
            switch (Page.Session["CatalogMode"].ToString())
            {
                case "book":
                    loadBook();
                    break;
                case "dvd":
                    loadDvd();
                    break;
                case "tech":
                    loadTech();
                    break;
            }
        }
        else
        {
            Page.Session["CatalogMode"] = "book";

            loadBook();
        }

        if (Page.Session["cNote"] != null)
        {
            NoteLabel.Text = Page.Session["cNote"].ToString().ToUpper();
        }
    }

    protected void loadBook()
    {
        BookImage.ImageUrl = "images/bookS.png";
        DVDImage.ImageUrl = "images/dvd.png";
        TechImage.ImageUrl = "images/technology.png";

        table.Rows.Clear();
        table.Rows.Add(addBookTitleRow());

        List<Book> bookList;


        if (Request.QueryString["search"] != null)
        {
            performSearch(Request.QueryString["search"].ToString());
            bookList = (List<Book>)Page.Session["cList"];
        }
        else if (Page.Session["cList"] != null)
        {
            bookList = (List<Book>)Page.Session["cList"];
        }
        else
        {
            bookList = getBookList();
        }

        foreach (Book cur in bookList)
        {
            table.Rows.Add(addBookRow(cur));
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

        
        if (Request.QueryString["search"] != null)
        {
            performSearch(Request.QueryString["search"].ToString());
            dvdList = (List<DVD>)Page.Session["cList"];
        }
        else if (Page.Session["cList"] != null)
        {
            dvdList = (List<DVD>)Page.Session["cList"];
        }
        else
        {
            dvdList = getDVDList();
        }

        foreach (DVD cur in dvdList)
        {
            table.Rows.Add(addDVDRow(cur));
        }
    }

    protected void loadTech()
    {
        BookImage.ImageUrl = "images/book.png";
        DVDImage.ImageUrl = "images/dvd.png";
        TechImage.ImageUrl = "images/technologyS.png";

        table.Rows.Clear();
        table.Rows.Add(addTechTitleRow());

        List<Technology> techList;



        if (Request.QueryString["search"] != null)
        {
            performSearch(Request.QueryString["search"].ToString());
            techList = (List<Technology>)Page.Session["cList"];
        }
        else if (Page.Session["cList"] != null)
        {
            techList = (List<Technology>)Page.Session["cList"];
        }
        else
        {
            techList = getTechList();
        }

        foreach (Technology cur in techList)
        {
            table.Rows.Add(addTechRow(cur));
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        var searchText = Server.UrlEncode(txtSearch.Text);
        if (searchText == "")
        {
            Page.Session["cList"] = null;
            Page.Session["cNote"] = null;
            Response.Redirect("Catalog.aspx");
        }

        Response.Redirect("Catalog.aspx?search=" + searchText);
    }

    protected void performSearch(string text)
    {
        if (text == "")
        {
            Page.Session["cList"] = null;
            Page.Session["cNote"] = null;
            return;
        }

        String arrow = text.ToLower();

        switch (Page.Session["CatalogMode"].ToString())
        {
            case "book":
                List<Book> bList = getBookList();
                List<Book> bresults = new List<Book>();
                foreach (Book cur in bList)
                {
                    if (Utilities.containsStr(arrow, cur.item_id.ToString()) ||
                        Utilities.containsStr(arrow, cur.title.ToLower()) ||
                        Utilities.containsStr(arrow, cur.publisher.ToLower()) ||
                        Utilities.containsStr(arrow, cur.publication_year.ToString()) ||
                        Utilities.containsStr(arrow, cur.isbn_10.ToLower()) ||
                        Utilities.containsStr(arrow, cur.author.ToLower()) ||
                        Utilities.containsStr(arrow, cur.category.ToLower()) ||
                        Utilities.containsStr(arrow, cur.isbn_13.ToString().ToLower()))
                    {
                        bresults.Add(cur);
                    }
                }
                Page.Session["cList"] = bresults;
                Page.Session["cNote"] = "Book Search Results For: '" + arrow + "'";
                break;


            case "dvd":
                List<DVD> dList = getDVDList();
                List<DVD> dresults = new List<DVD>();
                foreach (DVD cur in dList)
                {
                    if (Utilities.containsStr(arrow, cur.title.ToLower()) ||
                        Utilities.containsStr(arrow, cur.release_year.ToString()) ||
                        Utilities.containsStr(arrow, cur.rating.ToString()) ||
                        Utilities.containsStr(arrow, cur.item_id.ToString()))
                    {
                        dresults.Add(cur);
                    }
                }
                Page.Session["cList"] = dresults;
                Page.Session["cNote"] = "DVD Search Results For: '" + arrow + "'";
                break;


            case "tech":
                List<Technology> tList = getTechList();
                List<Technology> tresults = new List<Technology>();
                foreach (Technology cur in tList)
                {
                    if (Utilities.containsStr(arrow, cur.name.ToLower()) ||
                        Utilities.containsStr(arrow, cur.item_id.ToString()))
                    {
                        tresults.Add(cur);
                    }
                }
                Page.Session["cList"] = tresults;
                Page.Session["cNote"] = "Technology Search Results For: '" + arrow + "'";
                break;
        }
    }


    protected void buttonBooks_Click(object sender, EventArgs e)
    {
        Page.Session["cList"] = null;
        Page.Session["cNote"] = null;
        Page.Session["CatalogMode"] = "book";

        Response.Redirect("Catalog.aspx");

    }

    protected void buttonDVD_Click(object sender, EventArgs e)
    {
        Page.Session["cList"] = null;
        Page.Session["cNote"] = null;
        Page.Session["CatalogMode"] = "dvd";

        Response.Redirect("Catalog.aspx");

    }

    protected void buttonTech_Click(object sender, EventArgs e)
    {
        Page.Session["cList"] = null;
        Page.Session["cNote"] = null;
        Page.Session["CatalogMode"] = "tech";

        Response.Redirect("Catalog.aspx");

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

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(b);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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

    protected void SubmitDvd_Click(object sender, EventArgs e)
    {
        int year;

        DVD d = new DVD();
        d.title = dvdTitle.Text;
        Int32.TryParse(dvdYear.Text, out year);
        d.release_year = year;
        d.rating = dvdRating.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(d);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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

    protected void SubmitTech_Click(object sender, EventArgs e)
    {
        Technology t = new Technology();
        t.name = techName.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(t);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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

    private List<Book> getBookList()
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/book");

        return new JavaScriptSerializer().Deserialize<List<Book>>(response);
    }

    private List<DVD> getDVDList()
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/dvd");

        return new JavaScriptSerializer().Deserialize<List<DVD>>(response);
    }

    private List<Technology> getTechList()
    {
        var client = new WebClient();
        client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

        var response = client.DownloadString("http://api.sparklib.org/api/technology");

        return new JavaScriptSerializer().Deserialize<List<Technology>>(response);
    }


    private TableHeaderRow addBookTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(Utilities.addHeaderCell("ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Title"));
        ret.Cells.Add(Utilities.addHeaderCell("Author"));
        ret.Cells.Add(Utilities.addHeaderCell("Category"));
        ret.Cells.Add(Utilities.addHeaderCell("Publisher"));
        ret.Cells.Add(Utilities.addHeaderCell("Year"));
        ret.Cells.Add(Utilities.addHeaderCell("Pages"));
        ret.Cells.Add(Utilities.addHeaderCell("ISBN"));
        ret.Cells.Add(Utilities.addHeaderCell("ISBN13"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit/Delete"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableHeaderRow addDVDTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(Utilities.addHeaderCell("ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Title"));
        ret.Cells.Add(Utilities.addHeaderCell("Year"));
        ret.Cells.Add(Utilities.addHeaderCell("Rating"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit/Delete"));

        ret.BorderWidth = 3;
        return ret;
    }

    private TableHeaderRow addTechTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(Utilities.addHeaderCell("ID"));
        ret.Cells.Add(Utilities.addHeaderCell("Name"));
        ret.Cells.Add(Utilities.addHeaderCell("Edit/Delete"));

        ret.BorderWidth = 3;
        return ret;
    }


    private TableRow addBookRow(Book b)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(Utilities.addCell(b.item_id.ToString()));
        ret.Cells.Add(Utilities.addCell(b.title));
        ret.Cells.Add(Utilities.addCell(b.author));
        ret.Cells.Add(Utilities.addCell(b.category));
        ret.Cells.Add(Utilities.addCell(b.publisher));
        ret.Cells.Add(Utilities.addCell(b.publication_year.ToString()));
        ret.Cells.Add(Utilities.addCell(b.pages.ToString()));
        ret.Cells.Add(Utilities.addCell(b.isbn_10));
        ret.Cells.Add(Utilities.addCell(b.isbn_13));
        ret.Cells.Add(addButtonCell_Book(b.item_id));
        return ret;
    }

    private TableRow addDVDRow(DVD d)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(Utilities.addCell(d.item_id.ToString()));
        ret.Cells.Add(Utilities.addCell(d.title));
        ret.Cells.Add(Utilities.addCell(d.release_year.ToString()));
        ret.Cells.Add(Utilities.addCell(d.rating));
        ret.Cells.Add(addButtonCell_DVD(d.item_id));

        return ret;
    }

    private TableRow addTechRow(Technology t)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(Utilities.addCell(t.item_id.ToString()));
        ret.Cells.Add(Utilities.addCell(t.name));
        ret.Cells.Add(addButtonCell_Tech(t.item_id));

        return ret;
    }

    protected void editBookClick(object sender, EventArgs e)
    {
        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditBook.aspx?item_id=" + id);
    }

    protected void editDVDClick(object sender, EventArgs e)
    {
        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditDvd.aspx?item_id=" + id);
    }

    protected void editTechClick(object sender, EventArgs e)
    {
        string id = ((HtmlButton)sender).Attributes["id"];
        Response.Redirect("EditTechnology.aspx?item_id=" + id);
    }

    [System.Web.Services.WebMethod]
    public static void deleteBookClick(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            try
            {
                String apiString = "http://api.sparklib.org/api/book?item_id=" + id.ToString();
                client.UploadString(new Uri(apiString), "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    [System.Web.Services.WebMethod]
    public static void deleteDVDClick(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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

    [System.Web.Services.WebMethod]
    public static void deleteTechClick(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            try
            {
                String apiString = "http://api.sparklib.org/api/technology?item_id=" + id.ToString();
                client.UploadString(new Uri(apiString), "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }


    private TableCell addButtonCell_Book(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton del = new HtmlButton();
        HtmlButton edit = new HtmlButton();

        del.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        del.InnerHtml = "<i class = \"material-icons\">delete</i>";
        del.Attributes.Add("id", id.ToString());
        del.Attributes["onclick"] = "if(swal({" +
            "title: 'Delete Book'," +
            "text: 'Are you sure you want to delete Book id: " + id.ToString() + "?'," +
            "icon: 'warning'," +
            "buttons: true," +
            "dangerMode: true," +
            "}).then((value) => {" +
            "if(value){" +
            "deleteBook(" + id.ToString() + ");" +
            "swal('Book deleted', { icon: 'success',});" +
            "} else {" +
            "return false;" +
            "}" +
            "})){ return false; };";


        del.Attributes["title"] = "Delete";

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";

        edit.ServerClick += new EventHandler(editBookClick);

        ret.Controls.Add(edit);
        ret.Controls.Add(del);

        return ret;
    }

    private TableCell addButtonCell_DVD(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton del = new HtmlButton();
        HtmlButton edit = new HtmlButton();

        del.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        del.InnerHtml = "<i class = \"material-icons\">delete</i>";
        del.Attributes.Add("id", id.ToString());
        del.Attributes["onclick"] = "if(swal({" +
            "title: 'Delete DVD'," +
            "text: 'Are you sure you want to delete DVD id: " + id.ToString() + "?'," +
            "icon: 'warning'," +
            "buttons: true," +
            "dangerMode: true," +
            "}).then((value) => {" +
            "if(value){" +
            "deleteDVD(" + id.ToString() + ");" +
            "swal('DVD deleted', { icon: 'success',});" +
            "} else {" +
            "return false;" +
            "}" +
            "})){ return false; };";


        del.Attributes["title"] = "Delete";

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";

        edit.ServerClick += new EventHandler(editDVDClick);

        ret.Controls.Add(edit);
        ret.Controls.Add(del);

        return ret;
    }

    private TableCell addButtonCell_Tech(int id)
    {
        TableCell ret = new TableCell();
        HtmlButton del = new HtmlButton();
        HtmlButton edit = new HtmlButton();

        del.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        del.InnerHtml = "<i class = \"material-icons\">delete</i>";
        del.Attributes.Add("id", id.ToString());
        del.Attributes["onclick"] = "if(swal({" +
            "title: 'Delete Technology'," +
            "text: 'Are you sure you want to delete Technology id: " + id.ToString() + "?'," +
            "icon: 'warning'," +
            "buttons: true," +
            "dangerMode: true," +
            "}).then((value) => {" +
            "if(value){" +
            "deleteTechnology(" + id.ToString() + ");" +
            "swal('Technology deleted', { icon: 'success',});" +
            "} else {" +
            "return false;" +
            "}" +
            "})){ return false; };";


        del.Attributes["title"] = "Delete";

        edit.Attributes["class"] = "mdl-button mdl-js-button mdl-button--icon";
        edit.InnerHtml = "<i class = \"material-icons\">edit</i>";
        edit.Attributes.Add("id", id.ToString());
        edit.Attributes["title"] = "Edit";

        edit.ServerClick += new EventHandler(editTechClick);

        ret.Controls.Add(edit);
        ret.Controls.Add(del);

        return ret;
    }
}