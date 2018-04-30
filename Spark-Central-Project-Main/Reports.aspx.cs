using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void westCentralButton_Click(object sender, EventArgs e)
    {
        table.Rows.Clear();
        numTable.Rows.Clear();

        SqlConnection con = new SqlConnection("Data Source=SQL7004.site4now.net;Initial Catalog=DB_A3414F_SparkCentralLib;User Id=DB_A3414F_SparkCentralLib_admin;Password=CreativeCr0ssing;");

        SqlCommand cmd = new SqlCommand("EXEC numberOfWestCentralResidents", con);
        
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        int num = reader.GetInt32(0);
        reader.Close();

        TableHeaderCell header = new TableHeaderCell();
        header.Text = "Number of Found Items";
        TableHeaderRow row = new TableHeaderRow();
        row.Cells.Add(header);
        numTable.Rows.Add(row);

        TableCell body = new TableCell();
        body.Text = num.ToString();
        TableRow bodyRow = new TableRow();
        bodyRow.Cells.Add(body);
        numTable.Rows.Add(bodyRow);

        cmd = new SqlCommand("EXEC westCentralResidents", con);

        reader = cmd.ExecuteReader();

        // header row
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell1 = new TableHeaderCell();
        headerCell1.Text = "Member ID";
        TableHeaderCell headerCell2 = new TableHeaderCell();
        headerCell2.Text = "First Name";
        TableHeaderCell headerCell3 = new TableHeaderCell();
        headerCell3.Text = "Last Name";

        headerRow.Cells.AddRange(new TableHeaderCell[]{ headerCell1, headerCell2, headerCell3});
        table.Rows.Add(headerRow);

        while (reader.Read())
        {
            TableRow tableRow = new TableRow();
            TableCell Cell1 = new TableCell();
            Cell1.Text = reader.GetInt32(reader.GetOrdinal("member_id")).ToString();
            TableCell Cell2 = new TableCell();
            Cell2.Text = reader.GetString(reader.GetOrdinal("first_name"));
            TableCell Cell3 = new TableCell();
            Cell3.Text = reader.GetString(reader.GetOrdinal("last_name"));

            tableRow.Cells.AddRange(new TableCell[] { Cell1, Cell2, Cell3 });
            table.Rows.Add(tableRow);
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        table.Rows.Clear();
        numTable.Rows.Clear();

        String start_date = startDate.Text;
        String end_date = endDate.Text;
        String item_type = itemTypeValue.Value;

        SqlConnection con = new SqlConnection("Data Source=SQL7004.site4now.net;Initial Catalog=DB_A3414F_SparkCentralLib;User Id=DB_A3414F_SparkCentralLib_admin;Password=CreativeCr0ssing;");

        SqlCommand cmd = new SqlCommand("numberOfCheckoutsInDateRange", con);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@start_date", SqlDbType.Date).Value = start_date;
        cmd.Parameters.Add("@end_date", SqlDbType.Date).Value = end_date;
        cmd.Parameters.Add("@item_type", SqlDbType.VarChar).Value = item_type;

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        reader.Read();
        int num = reader.GetInt32(0);
        reader.Close();

        TableHeaderCell header = new TableHeaderCell();
        header.Text = "Number of Found Items";
        TableHeaderRow row = new TableHeaderRow();
        row.Cells.Add(header);
        numTable.Rows.Add(row);

        TableCell body = new TableCell();
        body.Text = num.ToString();
        TableRow bodyRow = new TableRow();
        bodyRow.Cells.Add(body);
        numTable.Rows.Add(bodyRow);

        cmd = new SqlCommand("checkoutsInDateRange", con);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@start_date", SqlDbType.Date).Value = start_date;
        cmd.Parameters.Add("@end_date", SqlDbType.Date).Value = end_date;
        cmd.Parameters.Add("@item_type", SqlDbType.VarChar).Value = item_type;

        reader = cmd.ExecuteReader();

        // header row
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell1 = new TableHeaderCell();
        headerCell1.Text = "Item ID";
        TableHeaderCell headerCell2 = new TableHeaderCell();
        headerCell2.Text = "Item Type";

        headerRow.Cells.AddRange(new TableHeaderCell[] { headerCell1, headerCell2 });
        table.Rows.Add(headerRow);

        while (reader.Read())
        {
            TableRow tableRow = new TableRow();
            TableCell Cell1 = new TableCell();
            Cell1.Text = reader.GetInt32(reader.GetOrdinal("item_id")).ToString();
            TableCell Cell2 = new TableCell();
            Cell2.Text = reader.GetString(reader.GetOrdinal("item_type"));

            tableRow.Cells.AddRange(new TableCell[] { Cell1, Cell2 });
            table.Rows.Add(tableRow);
        }
    }
}