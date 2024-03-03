using System.Data;
using MySqlConnector;

public class DapperContext  :IDisposable
{  
    private readonly string _connectionString;  
    private IDbConnection _dbConnection;  
  
    public DapperContext(string connectionString)  
    {  
        _connectionString = connectionString;  
    }  
  
    public IDbConnection DbConnection  
    {  
        get  
        {  
            if (_dbConnection == null || _dbConnection.State != ConnectionState.Open)  
            {  
                _dbConnection = new MySqlConnection(_connectionString); // 如果您使用的是 MySQL  MySQL
                _dbConnection.Open();  
            }  
            return _dbConnection;  
        }  
    }  
  
    public void Dispose()  
    {  
        if (_dbConnection != null && _dbConnection.State == ConnectionState.Open)  
        {  
            _dbConnection.Close();  
        }  
        _dbConnection = null;  
    }  
}