using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.Script.Serialization;
using SparkAPI.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

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
                Regex reg = new Regex(arrow.ToLower());
                return reg.IsMatch(target.ToLower());

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Member> getMemberList()
        {
            var client = new WebClient();
            client.Headers.Add("APIKey:254a2c54-5e21-4e07-b2aa-590bc545a520");

            var response = client.DownloadString("http://api.sparklib.org/api/Member");

            return new JavaScriptSerializer().Deserialize<List<Member>>(response);

        }
        public static List<Member> SortByColumnWithOrder(int order, string orderDir, List<Member> data)
        {
            // Initialization.   
            // Sorting   
            switch (order)
            {
                case 0:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.member_id).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.member_id).ToList();
                    }
                    break;

                case 1:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.last_name).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.last_name).ToList();
                    }
                    break;

                case 2:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.first_name).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.first_name).ToList();
                    }
                    break;
                case 3:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.phone).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.phone).ToList();
                    }
                    break;
                case 4:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.email).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.email).ToList();
                    }
                    break;
                case 5:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.city).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.city).ToList();
                    }
                    break;
                case 6:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.state).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.state).ToList();
                    }
                    break;
                default:
                    if (orderDir.ToLower().Equals("asc"))
                    {
                        data = data.OrderBy(m => m.member_id).ToList();
                    }
                    else
                    {
                        data = data.OrderByDescending(m => m.member_id).ToList();
                    }
                    break;
            }
  
            return data;
        }
    }

    public class DataTableParameters
    {
        public Dictionary<int, DataTableColumn> Columns;
        public int Draw;
        public int Length;
        public Dictionary<int, DataTableOrder> Order;
        public bool SearchRegex;
        public string SearchValue;
        public int Start;

        private DataTableParameters()
        {
        }

        /// <summary>
        /// Retrieve DataTable parameters from WebMethod parameter, sanitized against parameter spoofing
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DataTableParameters Get(object input)
        {
            return Get(JObject.FromObject(input));
        }

        /// <summary>
        /// Retrieve DataTable parameters from JSON, sanitized against parameter spoofing
        /// </summary>
        /// <param name="input">JToken object</param>
        /// <returns>parameters</returns>
        public static DataTableParameters Get(JToken input)
        {
            return new DataTableParameters
            {
                Columns = DataTableColumn.Get(input),
                Order = DataTableOrder.Get(input),
                Draw = (int)input["draw"],
                Start = (int)input["start"],
                Length = (int)input["length"],
                SearchValue =
                    new string(
                        ((string)input["search"]["value"]).Where(
                            c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray()),
                SearchRegex = (bool)input["search"]["regex"]
            };
        }
    }

    public class DataTableColumn
    {
        public int Data;
        public string Name;
        public bool Orderable;
        public bool Searchable;
        public bool SearchRegex;
        public string SearchValue;

        private DataTableColumn()
        {
        }

        /// <summary>
        /// Retrieve the DataTables Columns dictionary from a JSON parameter list
        /// </summary>
        /// <param name="input">JToken object</param>
        /// <returns>Dictionary of Column elements</returns>
        public static Dictionary<int, DataTableColumn> Get(JToken input)
        {
            return (
                (JArray)input["columns"])
                .Select(col => new DataTableColumn
                {
                    Data = (int)col["data"],
                    Name =
                        new string(
                            ((string)col["name"]).Where(
                                c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray()),
                    Searchable = (bool)col["searchable"],
                    Orderable = (bool)col["orderable"],
                    SearchValue =
                        new string(
                            ((string)col["search"]["value"]).Where(
                                c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || c == '-').ToArray()),
                    SearchRegex = (bool)col["search"]["regex"]
                })
                .ToDictionary(c => c.Data);
        }
    }

    public class DataTableOrder
    {
        public int Column;
        public string Direction;

        private DataTableOrder()
        {
        }

        /// <summary>
        /// Retrieve the DataTables order dictionary from a JSON parameter list
        /// </summary>
        /// <param name="input">JToken object</param>
        /// <returns>Dictionary of Order elements</returns>
        public static Dictionary<int, DataTableOrder> Get(JToken input)
        {
            return (
                (JArray)input["order"])
                .Select(col => new DataTableOrder
                {
                    Column = (int)col["column"],
                    Direction =
                        ((string)col["dir"]).StartsWith("desc", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC"
                })
                .ToDictionary(c => c.Column);
        }
    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DataTableResultSet
    {
        /// <summary>Array of records. Each element of the array is itself an array of columns</summary>
        public List<List<string>> data = new List<List<string>>();

        /// <summary>value of draw parameter sent by client</summary>
        public int draw;

        /// <summary>filtered record count</summary>
        public int recordsFiltered;

        /// <summary>total record count in resultset</summary>
        public int recordsTotal;

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DataTableResultError : DataTableResultSet
    {
        public string error;
    }


}