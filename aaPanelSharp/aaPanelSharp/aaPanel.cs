using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// The API Helper for the aaPanel
/// </summary>
public class aaPanel
{
    private string _baseUrl = "";
    
    /// <summary>
    /// The url of the aaPanel
    /// </summary>
    public string BaseUrl {
        get
        {
            return _baseUrl;
        }
        set
        {
            _baseUrl = value.TrimEnd('/');
        }
    }

    /// <summary>
    /// The API key of the aaPanel
    /// </summary>
    public string ApiKey { get; set; } = "";

    /// <summary>
    /// initialize the aaPanel API client
    /// </summary>
    public aaPanel()
    {
        
    }

    /// <summary>
    /// initialize the aaPanel API client
    /// </summary>
    /// <param name="baseUrl">The url of the aaPanel</param>
    /// <param name="apiKey">The API key of the aaPanel</param>
    public aaPanel(string baseUrl, string apiKey)
    {
        BaseUrl = baseUrl;
        ApiKey = apiKey;
    }

    private string BuildUrl(string url)
    {
        return _baseUrl + "/" + url.TrimStart('/');
    }

    /// <summary>
    /// The current system statistics of the device and the aaPanel (get => fetching new system statistics)
    /// </summary>
    public SystemStatistics SystemStatistics
    {
        get
        {
            var result = aaPanelHelper.Post<_SystemStatistics>(BuildUrl("/system?action=GetNetWork"),
                new Dictionary<string, string>() { }, ApiKey);
            return new SystemStatistics(result);
        }
    }

    /// <summary>
    /// Create a database in the aaPanel
    /// </summary>
    /// <param name="name">the name for the database</param>
    /// <param name="username">the username for the database</param>
    /// <param name="password">the password for the database</param>
    /// <param name="type">the database type (default: MySQL)</param>
    /// <returns>success of the action</returns>
    public bool CreateDatabase(string name, string username, string password, string type = "MySQL")
    {
        var result = aaPanelHelper.Post<_DbCreate>(BuildUrl("/database?action=AddDatabase"),
            new Dictionary<string, string>()
            {
                {"name", name},
                {"codeing", "utf8"},
                {"db_user", username},
                {"password", password},
                {"dtype", type},
                {"dataAccess", "%"},
                {"sid", "0"},
                {"active", "false"},
                {"address", "%"},
                {"ps", name},
                {"ssl", ""}
            }, ApiKey);
        return result.Status;
    }
}