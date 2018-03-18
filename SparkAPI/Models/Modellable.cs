using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SparkAPI.Models
{
    public interface Modellable
    {
        // This interface is a marker interface. Any class that implements this interface can be passed
        // to a persistence object to perform API operations with
        Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(String propertyName);
        String[] GetKeyNames();
    }
}
