using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public class MailServer
{
    private aaPanel _panel;
    internal MailServer(aaPanel panel)
    {
        _panel = panel;
    }

    public List<MailServerDomain> Domains
    {
        get
        {
            List<MailServerDomain> result = new();
            _ms__Domains domainsMsg;
            int page = 1;

            _ms__Domains fetch(int page, out _ms__Domains o)
            {
                var url = _panel.BuildUrl("/plugin?action=a&name=mail_sys&s=get_domains");
                o = aaPanelHelper.Post<_ms__Domains>(
                    url, new Dictionary<string, string>()
                    {
                        {"p", page.ToString()},
                        {"size","10"}
                    }, _panel.ApiKey);
                return o;
            }

            while (fetch(page++, out  domainsMsg).Msg.Data.Length > 0)
            {
                foreach (var domain in domainsMsg.Msg.Data)
                {
                    result.Add(new MailServerDomain(domain, _panel));
                }
            }

            return result;
        }
    }
}