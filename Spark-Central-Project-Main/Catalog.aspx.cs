using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    /*
     * When this page loads it checks the Session state for "CatalogMode" and redirects the user to
     * the page it indicates.
     * 
     * If there is not a CatalogMode set it will default to books.
     * 
     */ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["CatalogMode"] == null)
        {
            Session["CatalogMode"] = "books";

        }

        String url = Session["CatalogMode"] + ".aspx";

        Response.Redirect(url);
    }
}