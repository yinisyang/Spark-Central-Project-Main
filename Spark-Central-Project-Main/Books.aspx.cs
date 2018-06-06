using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using SparkWebSite;
using System.Web.Mvc;
using System.Web.Script.Services;


public partial class Catalog : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

        if (Page.Session["cNote"] != null)
        {
            NoteLabel.Text = Page.Session["cNote"].ToString().ToUpper();
            Page.Session["cNote"] = null;
        }
    }


    /*
     * GetData()
     * 
     * This method is called when the server recieves a request from the DataTables library.
     * It parses the information received and creates a response corresponding to the DataTables requirements.
     * 
     */ 
    [WebMethod(Description = "Server Side DataTables support", EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GetData(object parameters)
    {
        try
        {
            // Initialization.   
            var req = DataTableParameters.Get(parameters);
            var resultSet = new DataTableResultSet();

            List<Book> data = getBookList();

            resultSet.draw = req.Draw;
            resultSet.recordsTotal = data.Count();

            string search = req.SearchValue;
            int order = req.Order.Values.ElementAt(0).Column;
            string orderDirection = req.Order.Values.ElementAt(0).Direction;

            int startRec = req.Start;
            int pageSize = req.Length;

            //Search
            if (!string.IsNullOrEmpty(search) &&
               !string.IsNullOrWhiteSpace(search))
            {
                // Apply search   
                List<Book> searchResults = new List<Book>();

                foreach (Book b in data)
                {
                    if (
                        Utilities.containsStr(search, b.item_id.ToString()) ||
                        Utilities.containsStr(search, b.title) ||
                        Utilities.containsStr(search, b.author) ||
                        Utilities.containsStr(search, b.publisher) ||
                        Utilities.containsStr(search, b.publication_year.ToString()) ||
                        Utilities.containsStr(search, b.assn.ToString()) ||
                        Utilities.containsStr(search, b.isbn_10) ||
                        Utilities.containsStr(search, b.isbn_13)
                        )
                    {
                        searchResults.Add(b);
                    }
                }
                data = searchResults;

            }
            resultSet.recordsFiltered = data.Count();

            //Sorting
            data = SortByColumnWithOrder(order, orderDirection, data);


            // Apply pagination.   
            data = data.Skip(startRec).Take(pageSize).ToList();

            foreach (Book m in data)
            {
                var columns = new List<string>();
                columns.Add(m.item_id.ToString());
                columns.Add(m.assn.ToString());
                columns.Add(m.title);
                columns.Add(m.author);
                columns.Add(m.category);
                columns.Add(m.publisher);
                columns.Add(m.publication_year.ToString());
                columns.Add("<button id=" + m.item_id.ToString() + " class='mdl-button mdl-js-button mdl-button--icon' onClick='editBook(event); return false' title ='Edit'><i class= 'material-icons'>edit</i></button>"

                    );

                resultSet.data.Add(columns);
            }
            SendResponse(HttpContext.Current.Response, resultSet);




        }
        catch (Exception ex)
        {
            // Info   
            Console.Write(ex);
        }
        // Return info.   

    }

    private static void SendResponse(HttpResponse response, DataTableResultSet result)
    {
        response.Clear();
        response.Headers.Add("X-Content-Type-Options", "nosniff");
        response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
        response.ContentType = "application/json; charset=utf-8";
        response.Write(result.ToJSON());
        response.Flush();
        response.End();
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
                client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

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

    private static List<Book> getBookList()
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());

        var response = client.DownloadString("http://api.sparklib.org/api/book");

        return new JavaScriptSerializer().Deserialize<List<Book>>(response);
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


    /*
     * This Method is used to sort the Book Table serverside
     *  
     * 
     */ 
    public static List<Book> SortByColumnWithOrder(int order, string orderDir, List<Book> data)
    {
        // Initialization.  
        List<Book> ret = data;
        // Sorting   
        switch (order)
        {
            case 0:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.item_id).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.item_id).ToList();
                }
                break;

            case 1:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.assn).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.assn).ToList();
                }
                break;

            case 2:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.title).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.title).ToList();
                }
                break;

            case 3:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.author).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.author).ToList();
                }
                break;

            case 4:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.category).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.category).ToList();
                }
                break;

            case 5:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.publisher).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.publisher).ToList();
                }
                break;

            case 6:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.publication_year).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.publication_year).ToList();
                }
                break;


            default:
                if (orderDir.ToLower().Equals("asc"))
                {
                    ret = ret.OrderBy(m => m.item_id).ToList();
                }
                else
                {
                    ret = ret.OrderByDescending(m => m.item_id).ToList();
                }
                break;
        }

        return ret;
    }
}