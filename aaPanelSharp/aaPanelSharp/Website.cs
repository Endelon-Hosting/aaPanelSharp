using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// the struct for an in the aaPanel registered website 
/// </summary>
public struct Website
{
    internal aaPanel panel;
    internal Website(_SDatum d, aaPanel p)
    {
        Added = d.Addtime;
        Backups = (int)d.BackupCount;
        Id = (int) d.Id;
        Name = d.Name;
        Path = d.Path;
        Status = d.Status;
        ProjectType = d.ProjectType;
        PhpVersion = d.PhpVersion;
        panel = p;
    }

    /// <summary>
    /// the time the website was created
    /// </summary>
    public DateTimeOffset Added { get; set; }
    
    /// <summary>
    /// the count of backups
    /// </summary>
    public int Backups { get; set; }

    /// <summary>
    /// getting the property fetches the domains of the website
    /// </summary>
    public Domain[] Domains
    {
        get
        {
            var domains = aaPanelHelper.Post<_Domain[]>(panel.BuildUrl("/data?action=getData"),
                new Dictionary<string, string>()
                {
                    {"table","domain"},
                    {"list","True"},
                    {"search", Id.ToString()}
                }, panel.ApiKey);

            List<Domain> result = new List<Domain>();
            foreach (var d in domains)
            {
                result.Add(new Domain(d, panel, this));
            }

            return result.ToArray();
        }
    }

    /// <summary>
    /// the id the aaPanel uses for this website
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// the name of the website
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// the path of the wwwroot folder
    /// </summary>
    public string Path { get; set; }
    
    /// <summary>
    /// the status of the website
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// the type of the website
    /// </summary>
    public string ProjectType { get; set; }
    
    /// <summary>
    /// the php version which is used for the website
    /// </summary>
    public string PhpVersion { get; set; }

    /// <summary>
    /// add a domain
    /// </summary>
    /// <param name="name">the name</param>
    /// <param name="port">the port the server listens on</param>
    /// <returns>if the action was successful</returns>
    public bool AddDomain(string name, int port)
    {
        return aaPanelHelper.Post<_DbCreate>(panel.BuildUrl("/site?action=AddDomain"), new Dictionary<string, string>()
        {
            {"id", Id.ToString()},
            {"webname", Name},
            {"domain", name + ":" + port}
        }, panel.ApiKey).Status;
    }

    /// <summary>
    /// delete only the website
    /// </summary>
    /// <returns>if the action was successful</returns>
    public bool Delete()
    {
        return Delete(false, false, false);
    }
    /// <summary>
    /// delete the website
    /// </summary>
    /// <param name="deleteFTP">delete the ftp profile</param>
    /// <param name="deleteDatabase">delete the database</param>
    /// <param name="deleteFiles">delete the content of the wwwroot folder</param>
    /// <returns></returns>
    public bool Delete(bool deleteFTP, bool deleteDatabase, bool deleteFiles)
    {
        var prs = new Dictionary<string, string>()
        {
            {"id", Id.ToString()},
            {"webname", Name},
        };
        if (deleteFTP)
            prs["ftp"] = "1";
        if (deleteDatabase)
            prs["database"] = "1";
        if (deleteFiles)
            prs["path"] = "1";
        return aaPanelHelper.Post<_DbCreate>(panel.BuildUrl("/site?action=DeleteSite"), prs, panel.ApiKey).Status;
    }

    /// <summary>
    /// disables the website
    /// </summary>
    /// <returns>if the action was successful</returns>
    public bool Disable()
    {
        var prs = new Dictionary<string, string>()
        {
            {"sites_id", Id.ToString()},
            {"status", "0"},
        };
        return aaPanelHelper.Post<_DbCreate>(panel.BuildUrl("/site?action=set_site_status_multiple"), prs, panel.ApiKey).Status;
    }

    /// <summary>
    /// enables the website
    /// </summary>
    /// <returns>if the action was successful</returns>
    public bool Enable()
    {
        var prs = new Dictionary<string, string>()
        {
            {"sites_id", Id.ToString()},
            {"status", "1"},
        };
        return aaPanelHelper.Post<_DbCreate>(panel.BuildUrl("/site?action=set_site_status_multiple"), prs, panel.ApiKey).Status;
    }
}