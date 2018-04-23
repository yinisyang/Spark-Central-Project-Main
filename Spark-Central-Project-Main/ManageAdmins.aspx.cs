using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManageAdmins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        populateTable();
    }

    protected void populateTable() {
        table.Rows.Clear();
        table.Rows.Add(addAdminTitleRow());

        SqlConnection con = new SqlConnection("Data Source=SQL7004.site4now.net;Initial Catalog=DB_A3414F_SparkCentralLib;User Id=DB_A3414F_SparkCentralLib_admin;Password=CreativeCr0ssing;");
        SqlCommand cmd = new SqlCommand("SELECT username FROM Admins", con);
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            table.Rows.Add(addAdminRow(reader.GetString(reader.GetOrdinal("username"))));
        }

        con.Close();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection("Data Source=SQL7004.site4now.net;Initial Catalog=DB_A3414F_SparkCentralLib;User Id=DB_A3414F_SparkCentralLib_admin;Password=CreativeCr0ssing;");

        SqlCommand cmd = new SqlCommand("SELECT username FROM Admins WHERE username = '" + userName.Text + "'", con);
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (!reader.Read()) {
            reader.Close();
            String sqlCommand = "INSERT INTO Admins (username, password_hash) VALUES (@username, @hashedpassword)";
            cmd = new SqlCommand();
            cmd.Connection = con;
            
            String username = userName.Text;
            String hashed_password = hashPassword(password.Text);

            SqlParameter userNameParam = new SqlParameter("@username", System.Data.SqlDbType.VarChar, 50);
            userNameParam.Value = username;
            cmd.Parameters.Add(userNameParam);
            SqlParameter passwordParam = new SqlParameter("@hashedpassword", System.Data.SqlDbType.VarChar, 64);
            passwordParam.Value = hashed_password;
            cmd.Parameters.Add(passwordParam);

            cmd.CommandText = sqlCommand;
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            populateTable();
        }
        else
        {
            // user already exists
        }
    }

    private TableRow addAdminRow(String username)
    {
        TableRow ret = new TableRow();
        ret.Cells.Add(addCell(username));
        return ret;
    }

    private TableCell addCell(string content)
    {
        TableCell ret = new TableCell();
        ret.Text = content;
        return ret;
    }

    protected TableHeaderRow addAdminTitleRow()
    {
        TableHeaderRow ret = new TableHeaderRow();
        ret.Cells.Add(addHeaderCell("Username"));

        ret.BorderWidth = 3;

        return ret;
    }

    private TableHeaderCell addHeaderCell(string content)
    {
        TableHeaderCell ret = new TableHeaderCell();
        ret.Text = content;
        return ret;
    }

    private String hashPassword(String password)
    {
        SHA256 mySHA256 = SHA256Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(password);

        byte[] result = mySHA256.ComputeHash(bytes);

        String hash = "";

        int i;
        for (i = 0; i < result.Length; i++)
        {
            hash += String.Format("{0:X2}", result[i]);
        }

        return hash;
    }
}