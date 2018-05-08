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

        SqlCommand cmd = new SqlCommand("westCentralResidents", con);
        cmd.CommandType = CommandType.StoredProcedure;

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

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

        int count = 0;
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
            count++;
        }

        SetNumberTable(count);
    }

    private void SetNumberTable(int num) {
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
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        table.Rows.Clear();
        numTable.Rows.Clear();

        SqlConnection con = new SqlConnection("Data Source=SQL7004.site4now.net;Initial Catalog=DB_A3414F_SparkCentralLib;User Id=DB_A3414F_SparkCentralLib_admin;Password=CreativeCr0ssing;");
        if (itemTypeValue.Value.Equals("technology"))
        {
            retrieveTechnologyReport(con);
        }
        else if (itemTypeValue.Value.Equals("book"))
        {
            retrieveBookReport(con);
        }
        else
        {
            retrieveDVDReport(con);
        }
    }

    private void retrieveTechnologyReport(SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("TechnologyCheckoutsInDateRange", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@start_date", SqlDbType.Date).Value = startDate.Text;
        cmd.Parameters.Add("@end_date", SqlDbType.Date).Value = endDate.Text;
        cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = restrictToBox.Text;

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        // header row
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell1 = new TableHeaderCell();
        headerCell1.Text = "Item ID";
        TableHeaderCell headerCell2 = new TableHeaderCell();
        headerCell2.Text = "ASSN";
        TableHeaderCell headerCell3 = new TableHeaderCell();
        headerCell3.Text = "Name";
        TableHeaderCell headerCell4 = new TableHeaderCell();
        headerCell4.Text = "Date Checked Out";

        headerRow.Cells.AddRange(new TableHeaderCell[] { headerCell1, headerCell2, headerCell3, headerCell4 });
        table.Rows.Add(headerRow);

        int count = 0;
        while (reader.Read())
        {
            TableRow tableRow = new TableRow();
            TableCell Cell1 = new TableCell();
            Cell1.Text = reader.GetInt32(reader.GetOrdinal("item_id")).ToString();
            TableCell Cell2 = new TableCell();
            Cell2.Text = reader.GetInt32(reader.GetOrdinal("assn")).ToString();
            TableCell Cell3 = new TableCell();
            Cell3.Text = reader.GetString(reader.GetOrdinal("name"));
            TableCell Cell4 = new TableCell();
            Cell4.Text = reader.GetDateTime(reader.GetOrdinal("checkout_date")).ToShortDateString();

            tableRow.Cells.AddRange(new TableCell[] { Cell1, Cell2, Cell3, Cell4 });
            table.Rows.Add(tableRow);

            count++;
        }

        SetNumberTable(count);
    }

    private void retrieveBookReport(SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("BookCheckoutsInDateRange", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@start_date", SqlDbType.Date).Value = startDate.Text;
        cmd.Parameters.Add("@end_date", SqlDbType.Date).Value = endDate.Text;
        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = restrictToBox.Text;

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        // header row
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell1 = new TableHeaderCell();
        headerCell1.Text = "Item ID";
        TableHeaderCell headerCell2 = new TableHeaderCell();
        headerCell2.Text = "Title";
        TableHeaderCell headerCell3 = new TableHeaderCell();
        headerCell3.Text = "ISBN-10";
        TableHeaderCell headerCell4 = new TableHeaderCell();
        headerCell4.Text = "Date Checked Out";

        headerRow.Cells.AddRange(new TableHeaderCell[] { headerCell1, headerCell2, headerCell3, headerCell4 });
        table.Rows.Add(headerRow);

        int count = 0;
        while (reader.Read())
        {
            TableRow tableRow = new TableRow();
            TableCell Cell1 = new TableCell();
            Cell1.Text = reader.GetInt32(reader.GetOrdinal("item_id")).ToString();
            TableCell Cell2 = new TableCell();
            Cell2.Text = reader.GetString(reader.GetOrdinal("title"));
            TableCell Cell3 = new TableCell();
            Cell3.Text = reader.GetString(reader.GetOrdinal("isbn_10"));
            TableCell Cell4 = new TableCell();
            Cell4.Text = reader.GetDateTime(reader.GetOrdinal("checkout_date")).ToShortDateString();

            tableRow.Cells.AddRange(new TableCell[] { Cell1, Cell2, Cell3, Cell4 });
            table.Rows.Add(tableRow);

            count++;
        }

        SetNumberTable(count);
    }

    private void retrieveDVDReport(SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand("DVDCheckoutsInDateRange", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@start_date", SqlDbType.Date).Value = startDate.Text;
        cmd.Parameters.Add("@end_date", SqlDbType.Date).Value = endDate.Text;
        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = restrictToBox.Text;

        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        // header row
        TableHeaderRow headerRow = new TableHeaderRow();
        TableHeaderCell headerCell1 = new TableHeaderCell();
        headerCell1.Text = "Item ID";
        TableHeaderCell headerCell2 = new TableHeaderCell();
        headerCell2.Text = "Title";
        TableHeaderCell headerCell3 = new TableHeaderCell();
        headerCell3.Text = "Date Checked Out";

        headerRow.Cells.AddRange(new TableHeaderCell[] { headerCell1, headerCell2, headerCell3 });
        table.Rows.Add(headerRow);

        int count = 0;
        while (reader.Read())
        {
            TableRow tableRow = new TableRow();
            TableCell Cell1 = new TableCell();
            Cell1.Text = reader.GetInt32(reader.GetOrdinal("item_id")).ToString();
            TableCell Cell2 = new TableCell();
            Cell2.Text = reader.GetString(reader.GetOrdinal("title"));
            TableCell Cell3 = new TableCell();
            Cell3.Text = reader.GetDateTime(reader.GetOrdinal("checkout_date")).ToShortDateString();

            tableRow.Cells.AddRange(new TableCell[] { Cell1, Cell2, Cell3 });
            table.Rows.Add(tableRow);

            count++;
        }

        SetNumberTable(count);
    }
}