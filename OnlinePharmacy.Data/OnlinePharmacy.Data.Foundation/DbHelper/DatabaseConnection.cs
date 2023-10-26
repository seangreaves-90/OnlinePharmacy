using System.Data;
using Microsoft.Data.SqlClient;

namespace ScriptEase.OnlinePharmacy.Data.DbHelper
{
    

public class DatabaseConnection
{
    private string ConnectionString { get; set; } = "Your_Connection_String_Here";

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}

}
