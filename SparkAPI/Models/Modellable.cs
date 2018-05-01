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
        // Receives: the name of a property of a model as a string
        // Returns: The associated SqlDB type and length as it appears in the database
        Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(String propertyName);

        // Receives:
        // Returns: a list of column names that make up the primary key of the associated model
        String[] FieldsNotSpecifiedInPOST();
    }
}
