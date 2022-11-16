using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

/// <summary>
/// information about an in the aaPanel registered database
/// </summary>
public struct Database
{
    internal aaPanel p;
    internal Database(_Datum o, aaPanel b)
    {
        Id = (int)o.Id;
        Name = o.Name;
        Username = o.Username;
        Password = o.Password;
        p = b;
    }
    
    /// <summary>
    /// the aaPanel internal id of the database
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// the name of the database
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// the username of the database
    /// </summary>
    public string Username { get; }
    
    /// <summary>
    /// the password of the database
    /// </summary>
    public string Password { get; }

    /// <summary>
    /// delete this database (connot be undone)
    /// </summary>
    /// <returns>success of the action</returns>
    public bool Delete()
    {
        return aaPanelHelper.Post<_DbCreate>(p.BuildUrl("/database?action=DeleteDatabase"), new Dictionary<string, string>()
        {
            {"id", Id.ToString()},
            {"name", Name}
        }, p.ApiKey).Status;
    }
    
    /// <summary>
    /// change the password of this databse
    /// </summary>
    /// <returns>success of the action</returns>
    public bool ChangePassword(string newPassword)
    {
        return aaPanelHelper.Post<_DbCreate>(p.BuildUrl("/database?action=ResDatabasePassword"), new Dictionary<string, string>()
        {
            {"id", Id.ToString()},
            {"name", Name},
            {"password", newPassword}
        }, p.ApiKey).Status;
    }
}