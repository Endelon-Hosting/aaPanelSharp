using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// the class represents a mailbox in the aaPanel
/// </summary>
public class MailServerMailbox
{
    private aaPanel _panel;
    
    /// <summary>
    /// the status of the mailbox
    /// </summary>
    public bool Active { get; }
    
    /// <summary>
    /// the name of the mailbox
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// the email of the mailbox
    /// </summary>
    public string Mail { get; }
    
    /// <summary>
    /// the memory limitations for the mailbox
    /// </summary>
    public int Quota { get; }
    
    /// <summary>
    /// the admin status of the mailbox
    /// </summary>
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

    /// <summary>
    /// set the status for this mailbox
    /// </summary>
    /// <param name="active">the new value</param>
    /// <returns>if it was successful</returns>
    public bool SetActive(bool active)
    {
        int i = active ? 1 : 0;
        return Mod(Quota.ToString(), Name, i, IsAdmin ? 1 : 0);
    }

    /// <summary>
    /// sets if this is an admin mailbox
    /// </summary>
    /// <param name="isAdmin">the new value</param>
    /// <returns>if it was successful</returns>
    public bool SetIsAdmin(bool isAdmin)
    {
        int i = isAdmin ? 1 : 0;
        return Mod(Quota.ToString(), Name, Active ? 1 : 0, i);
    }

    /// <summary>
    /// sets the new quota
    /// </summary>
    /// <param name="quota">the new value</param>
    /// <returns>if the action was successful</returns>
    public bool SetQuota(string quota)
    {
        return Mod(quota, Name, Active ? 1 : 0, IsAdmin ? 1 : 0);
    }

    /// <summary>
    /// sets the name of the mailbox
    /// </summary>
    /// <param name="name">the new value</param>
    /// <returns>if the action was successful</returns>
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