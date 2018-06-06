﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using SparkWebSite;
using System.Net;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Book b = getBook(Request.QueryString["item_id"]);
                bookTitle.Text = b.title;
                bookAuthor.Text = b.author;
                bookCategory.Text = b.category;
                bookDescription.Text = b.description;
                bookIsbn13.Text = b.isbn_13;
                bookIsnb10.Text = b.isbn_10;
                bookPages.Text = b.pages.ToString();
                bookPublisher.Text = b.publisher;
                bookYear.Text = b.publication_year.ToString();
                bookAssn.Text = b.assn.ToString();
            }
        }
        catch(Exception error)
        {
            Console.WriteLine(error.Message);
            Response.Redirect("Books.aspx");
        }
    }

    private Book getBook(string id)
    {
        var client = new WebClient();
        client.Headers.Add(Utilities.getApiKey());
        client.QueryString.Set("item_id", id);
        string url = "http://api.sparklib.org/api/book";

        var response = client.DownloadString(url);

        return new JavaScriptSerializer().Deserialize<Book>(response);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Catalog.aspx");
    }

    [System.Web.Services.WebMethod]
    public static void deleteBk(int id)
    {
        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/book?item_id=" + id;
                client.UploadString(apiString, "DELETE", "");

            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        int year;
        int pages;
        int assn;

        Book b = new Book();
        b.title = bookTitle.Text;
        b.author = bookAuthor.Text;
        b.publisher = bookPublisher.Text;

        Int32.TryParse(bookYear.Text, out year);
        Int32.TryParse(bookPages.Text, out pages);
        Int32.TryParse(bookAssn.Text, out assn);
        b.publication_year = year;
        b.pages = pages;
        b.assn = assn;

        b.category = bookCategory.Text;
        b.description = bookDescription.Text;
        b.isbn_10 = bookIsnb10.Text;
        b.isbn_13 = bookIsbn13.Text;

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string json = serializer.Serialize(b);

        using (var client = new WebClient())
        {
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add(Utilities.getApiKey());

            try
            {
                String apiString = "http://api.sparklib.org/api/book?item_id=" + Request.QueryString["item_id"];
                client.UploadString(apiString, "PUT", json);
                var response = client.ResponseHeaders;

                Response.Redirect("Catalog.aspx");

            }
            catch (Exception ex)
            {
            }
        }

    }
}