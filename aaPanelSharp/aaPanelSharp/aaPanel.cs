using aaPanelSharp.RequestModels;
using aaPanelSharp.ResponseModels;
using Newtonsoft.Json;

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

    /// <summary>
    /// getting the property fetches the in the aaPanel installed php versions
    /// </summary>
    public PHPVersion[] PHPVersions
    {
        get
        {
            var vs = aaPanelHelper.Post<_PhpVersion[]>(BuildUrl("/site?action=GetPHPVersion"),
                new Dictionary<string, string>(), ApiKey);

            List<PHPVersion> result = new();
            
            foreach (var v in vs)
            {
                result.Add(new PHPVersion(v));
            }

            return result.ToArray();
        }
    }

    /// <summary>
    /// the mailserver property is used to control the mailserver aaPanel plugin
    /// </summary>
    public MailServer MailServer => new MailServer(this);
    
    /// <summary>
    /// getting the property fetches the in the aaPanel existing websites
    /// </summary>
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
    /// getting the property fetches the in the aaPanel existing ftp profiles
    /// </summary>
    public FTPUser[] FTPUsers
    {
        get
        {
            List<FTPUser> result = new List<FTPUser>();
            
            _FTP fetchPage(int page, out _FTP res)
            {
                res = aaPanelHelper.Post<_FTP>(BuildUrl("/data?action=getData"), new Dictionary<string, string>()
                {
                    {"table","ftps"},
                    {"search",""},
                    {"limit", "100"},
                    {"p", page.ToString()}
                }, ApiKey);
                return res;
            }

            _FTP temp = new _FTP();
            int i = 1;
            while (fetchPage(i++, out temp).Data.Length > 0)
            {
                foreach (var v in temp.Data)
                {
                    result.Add(new FTPUser(v, this));
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

    /// <summary>
    /// creates a new website in the aaPanel
    /// </summary>
    /// <param name="domains">the list of the domains this website uses</param>
    /// <param name="phpVersion">the php version which the website uses</param>
    public void CreateWebsite(string[] domains, PHPVersion phpVersion)
    {
        CreateWebsite(domains, phpVersion, "_auto");
    }
    
    /// <summary>
    /// creates a new website in the aaPanel
    /// </summary>
    /// <param name="domains">the list of the domains this website uses</param>
    /// <param name="phpVersion">the php version which the website uses</param>
    /// <param name="path">the path to the wwwroot folder</param>
    public void CreateWebsite(string[] domains, PHPVersion phpVersion, string path)
    {
        string mDomain = domains[0];
        int mPort = int.Parse(mDomain.Split(":").Last());
        string mDomainNoPort = mDomain.Split(":").First();
        string pth = path;
        if (pth == "_auto")
            pth = "/www/wwwroot/" + mDomainNoPort;
        var dList = domains.ToList();
        
        dList.RemoveAt(0);
        
        var wn = new _Webname
        {
            Domain = mDomain,
            Domainlist = dList.ToArray(),
            Count = dList.Count
        };

        var webname = JsonConvert.SerializeObject(wn);

        var vi = phpVersion.Version.ToString();
        if (vi == "0")
            vi = "00";
        
        var res = aaPanelHelper.Post<dynamic>(BuildUrl("/site?action=AddSite"), new Dictionary<string, string>()
        {
            {"webname",webname},
            {"type","PHP"},
            {"port",mPort.ToString()},
            {"ps",mDomainNoPort.ToLower().Replace(".","_")},
            {"path",pth},
            {"type_id","0"},
            {"version", vi},
            {"ftp","false"},
            {"sql","false"},
            {"codeing","utf8"},
            {"set_ssl","0"},
            {"force_ssl","0"}
        }, ApiKey);
    }
    
    /// <summary>
    /// creates a new website in the aaPanel
    /// </summary>
    /// <param name="domains">the list of the domains this website uses</param>
    /// <param name="phpVersion">the php version which the website uses</param>
    /// <param name="path">the path to the wwwroot folder</param>
    /// <param name="ftpUser">the username for the ftp profile that will be created</param>
    /// <param name="ftpPassword">the password for the ftp profile that will be created</param>
    public void CreateWebsite(string[] domains, PHPVersion phpVersion, string path, string ftpUser, string ftpPassword)
    {
        string mDomain = domains[0];
        int mPort = int.Parse(mDomain.Split(":").Last());
        string mDomainNoPort = mDomain.Split(":").First();
        string pth = path;
        if (pth == "_auto")
            pth = "/www/wwwroot/" + mDomainNoPort;
        var dList = domains.ToList();
        
        dList.RemoveAt(0);
        
        var wn = new _Webname
        {
            Domain = mDomain,
            Domainlist = dList.ToArray(),
            Count = dList.Count
        };

        var webname = JsonConvert.SerializeObject(wn);

        var vi = phpVersion.Version.ToString();
        if (vi == "0")
            vi = "00";
        
        var res = aaPanelHelper.Post<dynamic>(BuildUrl("/site?action=AddSite"), new Dictionary<string, string>()
        {
            {"webname",webname},
            {"type","PHP"},
            {"port",mPort.ToString()},
            {"ps",mDomainNoPort.ToLower().Replace(".","_")},
            {"path",pth},
            {"type_id","0"},
            {"version", vi},
            {"ftp","true"},
            {"sql","false"},
            {"codeing","utf8"},
            {"set_ssl","0"},
            {"force_ssl","0"},
            {"ftp_username",ftpUser},
            {"ftp_password",ftpPassword},
        }, ApiKey);
    }
}