using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// the class reprents a ftp user in the aaPanel
/// </summary>
public class FTPUser
{
    /// <summary>
    /// the id for this profile the aaPanel uses
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// the ftp username
    /// </summary>
    public string Username { get; }
    
    /// <summary>
    /// the ftp password
    /// </summary>
    public string Password { get; }
    
    /// <summary>
    /// the path the can be accessed with this ftp user
    /// </summary>
    public string Path { get; }
    internal FTPUser(_FTPDatum ftp, aaPanel panel)
    {
        Id = (int)ftp.Id;
        Username = ftp.Name;
        Password = ftp.Password;
        Path = ftp.Path;
    }
}