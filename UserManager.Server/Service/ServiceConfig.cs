namespace UserManager.Server.Service;

public class ServiceConfig
{

    private readonly Dictionary<string, string> _sqlDic = new();

    public static ServiceConfig Instance { get; } = new();

    public void AddConnectionString(string key, string value)
    {
        _sqlDic.Add(key, value);
    }

    public string GetConnectionString(string key)
    {
        return _sqlDic[key];
    }
    
}