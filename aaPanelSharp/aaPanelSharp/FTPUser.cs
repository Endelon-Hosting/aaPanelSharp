using aaPanelSharp.ResponseModels;

namespace aaPanelSharp;

public class FTPUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public FTPUser(_FTPDatum ftp, aaPanel panel)
    {
        Id = (int)ftp.Id;
        Username = ftp.Name;
        Password = ftp.Password;
    }
}