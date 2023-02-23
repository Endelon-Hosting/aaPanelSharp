using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public class MailServerMailbox
{
    private aaPanel _panel;
    public bool Active { get; }
    public string Name { get; }
    public string Mail { get; }
    public int Quota { get; }
    public bool IsAdmin { get; }
    internal MailServerMailbox(_ms__MailboxDatum d, aaPanel panel)
    {
        Active = d.Active == 1;
        Name = d.FullName;
        Mail = d.Username;
        Quota = (int)d.Quota;
        IsAdmin = d.IsAdmin == 1;

        _panel = panel;
    }

    public bool SetActive(bool active)
    {
        int i = active ? 1 : 0;
        return Mod(Quota.ToString(), Name, i, IsAdmin ? 1 : 0);
    }

    public bool SetIsAdmin(bool isAdmin)
    {
        int i = isAdmin ? 1 : 0;
        return Mod(Quota.ToString(), Name, Active ? 1 : 0, i);
    }

    public bool SetQuota(string quota)
    {
        return Mod(quota, Name, Active ? 1 : 0, IsAdmin ? 1 : 0);
    }

    public bool SetName(string name)
    {
        return Mod(Quota.ToString(), name, Active ? 1 : 0, IsAdmin ? 1 : 0);
    }

    private bool Mod(string quota, string name, int active, int isAdmin)
    {
        return aaPanelHelper.Post<_DbCreate>(_panel.BuildUrl("/plugin?action=a&name=mail_sys&s=update_mailbox"),
            new Dictionary<string, string>()
            {
                {"quota", quota},
                {"username", Mail},
                {"full_name", name},
                {"active", active.ToString()},
                {"is_admin", isAdmin.ToString()}
            }, _panel.ApiKey).Status;
    }
}