using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// the class represents an installed php version
/// </summary>
public struct PHPVersion
{
    internal PHPVersion(_PhpVersion v)
    {
        Version = int.Parse(v.Version);
        Name = v.Name;
    }
    
    /// <summary>
    /// the version as a representing integer
    /// </summary>
    public int Version { get; set; }
    
    /// <summary>
    /// the name of the version
    /// </summary>
    public string Name { get; set; }
}