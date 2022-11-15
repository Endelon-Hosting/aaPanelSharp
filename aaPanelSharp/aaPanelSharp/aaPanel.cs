﻿using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// The API Helper for the aaPanel
/// </summary>
public class aaPanel
{
    private string _baseUrl = "";
    
    /// <summary>
    /// The url of the aaPanel
    /// </summary>
    public string BaseUrl {
        get
        {
            return _baseUrl;
        }
        set
        {
            _baseUrl = value.TrimEnd('/');
        }
    }

    /// <summary>
    /// The API key of the aaPanel
    /// </summary>
    public string ApiKey { get; set; } = "";

    /// <summary>
    /// initialize the aaPanel API client
    /// </summary>
    public aaPanel()
    {
        
    }

    /// <summary>
    /// initialize the aaPanel API client
    /// </summary>
    /// <param name="baseUrl">The url of the aaPanel</param>
    /// <param name="apiKey">The API key of the aaPanel</param>
    public aaPanel(string baseUrl, string apiKey)
    {
        BaseUrl = baseUrl;
        ApiKey = apiKey;
    }

    private string BuildUrl(string url)
    {
        return _baseUrl + "/" + url.TrimStart('/');
    }

    public SystemStatistics SystemStatistics
    {
        get
        {
            var result = aaPanelHelper.Post<_SystemStatistics>(BuildUrl("/system?action=GetNetWork"),
                new Dictionary<string, string>() { }, ApiKey);
            return new SystemStatistics(result);
        }
    }
}