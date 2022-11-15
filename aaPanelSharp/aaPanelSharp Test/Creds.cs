namespace aaPanelSharp_Test;

public class Creds
{
    public static string API_URL
    {
        get { return File.ReadAllLines("../../../creds.key")[0]; }
    }

    public static string API_KEY
    {
        get { return File.ReadAllLines("../../../creds.key")[1]; }
    }
}