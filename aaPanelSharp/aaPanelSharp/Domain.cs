using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// the class represents a domain of a website
/// </summary>
public struct Domain
{
    private aaPanel panel;
    private Website site;
    internal Domain(_Domain d, aaPanel panel, Website s)
    {
        this.panel = panel;
        Id = (int)d.Id;
        Port = (int)d.Port;
        DomainName = d.Name;
        Added = d.Addtime;
        site = s;
    }
    
    /// <summary>
    /// the id the aaPanel uses for this domain
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// the port the website listens on for the domain
    /// </summary>
    public int Port { get; }
    
    /// <summary>
    /// the name of the domain
    /// </summary>
    public string DomainName { get; }
    
    /// <summary>
    /// the time the domain was added
    /// </summary>
    public DateTimeOffset Added { get; }

    /// <summary>
    /// removes the domain
    /// </summary>
    /// <returns>returns if deletion was successful</returns>
    public bool Remove()
    {
        return aaPanelHelper.Post<_DbCreate>(panel.BuildUrl("/site?action=DelDomain"), new Dictionary<string, string>()
        {
            {"id", site.Id.ToString()},
            {"webname", site.Name},
            {"domain", DomainName},
            {"port", Port.ToString()}
        }, panel.ApiKey).Status;
    }
}