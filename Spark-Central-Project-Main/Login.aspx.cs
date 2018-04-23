using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        String username = userName.Text;
        String hash = hashPassword(password.Text);

        SqlConnection con = new SqlConnection("Data Source=SQL7004.site4now.net;Initial Catalog=DB_A3414F_SparkCentralLib;User Id=DB_A3414F_SparkCentralLib_admin;Password=CreativeCr0ssing;");

        SqlCommand cmd = new SqlCommand("SELECT password_hash FROM Admins WHERE username = '" + username + "'", con);
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            String database_hash = reader.GetString(reader.GetOrdinal("password_hash"));
            Console.WriteLine("Found username");
            if (hash.Equals(database_hash))
            {
                Console.WriteLine("Matched Password");
                Session["admin_login"] = true;
                Response.Redirect("Dashboard.aspx");
            }
        }
        else
        {
            Console.WriteLine("Didn't find username");
        }
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