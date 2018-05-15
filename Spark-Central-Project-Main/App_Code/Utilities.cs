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
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Utilities
/// </summary>

namespace SparkWebSite
{
    public class Utilities
    {
        //Generic method that returns a TableCell containing the string that is passed to it.
        public static TableCell addCell(string content)
        {
            TableCell ret = new TableCell();
            ret.Text = content;
            return ret;
        }

        //Generic method that returns a TableHeaderCell containing the string that is passed to it.
        public static TableHeaderCell addHeaderCell(string content)
        {
            TableHeaderCell ret = new TableHeaderCell();
            ret.Text = content;
            return ret;
        }

        //Method which compares the arrow string to the target and returns true if a match is found.
        public static bool containsStr(String arrow, String target)
        {
            try
            {
                Regex reg = new Regex(arrow);
                return reg.IsMatch(target);

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}