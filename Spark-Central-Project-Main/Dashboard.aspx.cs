﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void buttonNewMember_Click(object sender, EventArgs e)
    {
        Response.Redirect("/NewMember.aspx");
    }
    protected void buttonViewMembers_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Members.aspx");
    }
}