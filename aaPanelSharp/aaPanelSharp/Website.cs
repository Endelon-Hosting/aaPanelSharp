using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public class Website
{
    internal aaPanel panel;
    internal Website(_SDatum d, aaPanel p)
    {
        Added = d.Addtime;
        Backups = (int)d.BackupCount;
        Domains = (int)d.Domain;
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
    public int Domains { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Status { get; set; }
    public string ProjectType { get; set; }
    public string PhpVersion { get; set; }
}