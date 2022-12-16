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

    internal string BuildUrl(string url)
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
    /// The currently registered databases in the aaPanel (get => fetching data)
    /// </summary>
    public Database[] Databases
    {
        get
        {
            List<Database> result = new();

            _DbList fetchPage(int page, out _DbList res)
            {
                res = aaPanelHelper.Post<_DbList>(BuildUrl("/data?action=getData"), new Dictionary<string, string>()
                {
                    {"table","databases"},
                    {"search",""},
                    {"limit", "100"},
                    {"p", page.ToString()}
                }, ApiKey);
                return res;
            }

            _DbList temp;
            int i = 1;
            while (fetchPage(i++, out temp).Data.Length > 0)
            {
                foreach (var v in temp.Data)
                {
                    result.Add(new Database(v, this));
                }
            }
            
            return result.ToArray();
        }
    }

    public Website[] Websites
    {
        get
        {
            List<Website> result = new List<Website>();
            
            _Site fetchPage(int page, out _Site res)
            {
                res = aaPanelHelper.Post<_Site>(BuildUrl("/data?action=getData"), new Dictionary<string, string>()
                {
                    {"table","sites"},
                    {"search",""},
                    {"limit", "100"},
                    {"type","-1"},
                    {"p", page.ToString()}
                }, ApiKey);
                return res;
            }

            _Site temp = new _Site();
            int i = 1;
            while (fetchPage(i++, out temp).Data.Length > 0)
            {
                foreach (var v in temp.Data)
                {
                    result.Add(new Website(v, this));
                }
            }
            
            return result.ToArray();
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