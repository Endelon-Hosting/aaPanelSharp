using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public class Domain
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
    
    public int Id { get; set; }
    public int Port { get; set; }
    public string DomainName { get; set; }
    public DateTimeOffset Added { get; set; }

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