using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public struct PHPVersion
{
    internal PHPVersion(_PhpVersion v)
    {
        Version = int.Parse(v.Version);
        Name = v.Name;
    }
    
    public int Version { get; set; }
    public string Name { get; set; }
}