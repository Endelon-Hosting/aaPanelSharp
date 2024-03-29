﻿using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// SystemStatistics represents the system statistics sent by the panel
/// </summary>
public struct SystemStatistics
{
    internal SystemStatistics(_SystemStatistics @base)
    {
        aaPanelVersion = @base.Version;
        FTPUsersCount = (int)@base.FtpTotal;
        DatabasesCount = (int)@base.DatabaseTotal;
        WebsitesCount = (int)@base.SiteTotal;
        SystemUptime = @base.Time;
        var memUsage = (float) @base.Mem.MemRealUsed / (float) @base.Mem.MemTotal;
        RAMUsage = memUsage;
        TotalRAM = (int) @base.Mem.MemTotal;
        UsedRAM = (int) @base.Mem.MemRealUsed;
        System = @base.System;
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
    
    /// <summary>
    /// The ram usage of the device running this aaPanel (0-1)
    /// </summary>
    public float RAMUsage { get; }
    
    /// <summary>
    /// The total ram of the device running this aaPanel (in MB)
    /// </summary>
    public int TotalRAM { get; }
    
    /// <summary>
    /// The used ram of the device running this aaPanel (in MB)
    /// </summary>
    public int UsedRAM { get; }
    
    /// <summary>
    /// OS information and python version of the device running this aaPanel
    /// </summary>
    public string System { get; }
}