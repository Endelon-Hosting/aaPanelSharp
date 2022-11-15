using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// SystemStatistics represents the system statistics sent by the panel
/// </summary>
public class SystemStatistics
{
    internal SystemStatistics(_SystemStatistics @base)
    {
        aaPanelVersion = @base.Version;
        FTPUsersCount = (int)@base.FtpTotal;
        DatabasesCount = (int)@base.DatabaseTotal;
        WebsitesCount = (int)@base.SiteTotal;
        SystemUptime = @base.Time;
    }
    
    /// <summary>
    /// The currently installed version of the aaPanel
    /// </summary>
    public string aaPanelVersion { get; }
    
    /// <summary>
    /// The count of the in the aaPanel listed FTP users
    /// </summary>
    public int FTPUsersCount { get; }
    
    /// <summary>
    /// The count of the in the aaPanel listed databases
    /// </summary>
    public int DatabasesCount { get; }
    
    /// <summary>
    /// The count of the in the aaPanel listed websites
    /// </summary>
    public int WebsitesCount { get; }
    
    /// <summary>
    /// The uptime of the device running this aaPanel
    /// </summary>
    public string SystemUptime { get; }
}