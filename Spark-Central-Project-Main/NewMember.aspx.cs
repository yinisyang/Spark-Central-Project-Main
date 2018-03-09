using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NewMember : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        Label1.Text = firstName.Text + " " + lastName.Text + " " + guardianName.Text + " " + memberGroupValue.Value + " " + streetAddress.Text + " " + city.Text + " " + stateValue.Value + " " + zipCode.Text + " " + phone.Text + " " + email.Text + " " + dateOfBirth.Text + " " + ethnicityValue.Value + " " + checkoutQuota.Text + " " + isRestrictedToTech.Checked + " === " + isWestCentralResident.Checked;
    }
}