using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// the class represents a mailserver Domain the aaPanel
/// </summary>
public class MailServerDomain
{
    private aaPanel _panel;
    
    /// <summary>
    /// the name of the domain
    /// </summary>
    public string Domain { get; }
    
    /// <summary>
    /// the value the dkim record should have
    /// </summary>
    public string DKIMValue { get; }
    
    /// <summary>
    /// the value the dkim record should be
    /// </summary>
    public string DMARCValue { get; }
    
    /// <summary>
    /// the status of the domain
    /// </summary>
    public bool Active { get; }
    internal MailServerDomain(_ms__DomainsDatum d, aaPanel panel)
    {
        Domain = d.Domain;
        DKIMValue = d.DkimValue;
        DMARCValue = d.DmarcValue;
        Active = d.Active == 1;
        _panel = panel;
    }

    /// <summary>
    /// getting the property fetches the list of mailboxes
    /// </summary>
    public List<MailServerMailbox> Mailboxes
    {
        get
        {
            List<MailServerMailbox> result = new();
            _ms__Mailbox domainsMsg;
            int page = 1;

            _ms__Mailbox fetch(int page, out _ms__Mailbox o)
            {
                var url = _panel.BuildUrl("/plugin?action=a&name=mail_sys&s=get_mailboxs");
                o = aaPanelHelper.Post<_ms__Mailbox>(
                    url, new Dictionary<string, string>()
                    {
                        {"p", page.ToString()},
                        {"size","10"},
                        {"domain", Domain}
                    }, _panel.ApiKey);
                return o;
            }

            while (fetch(page++, out  domainsMsg)?.Data.Length > 0)
            {
                foreach (var domain in domainsMsg.Data)
                {
                    result.Add(new MailServerMailbox(domain, _panel));
                }
            }

            return result;
        }
    }
}