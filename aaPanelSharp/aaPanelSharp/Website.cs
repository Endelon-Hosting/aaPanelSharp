using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public class Website
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

    public DateTimeOffset Added { get; set; }
    public int Backups { get; set; }

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

    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Status { get; set; }
    public string ProjectType { get; set; }
    public string PhpVersion { get; set; }

    public bool AddDomain(string name, int port)
    {
        return aaPanelHelper.Post<_DbCreate>(panel.BuildUrl("/site?action=AddDomain"), new Dictionary<string, string>()
        {
            {"id", Id.ToString()},
            {"webname", Name},
            {"domain", name + ":" + port}
        }, panel.ApiKey).Status;
    }
}