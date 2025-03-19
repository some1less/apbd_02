using System.Text.RegularExpressions;

namespace apbd_02;

public class EmbeddedDevice : Device
{
    private string _ipAddress;
    private string _networkName;

    public EmbeddedDevice(int id, string name, bool isTurnedOn, string ipAddress, string networkName) : base(id, name, isTurnedOn)
    {
        IpAddress = ipAddress;
        _networkName = networkName;
    }

    private string IpAddress
    {
        get {return _ipAddress;}
        set
        {
            if (!isValidIpAddress(value))
            {
                throw new ArgumentException("Invalid IP Address");
            }
            else
            {
                _ipAddress = value;
            }
        }
    }

    private bool isValidIpAddress(string ipAddress)
    {
        string pattern = "/^((25[0-5]|(2[0-4]|1\\d|[1-9]|)\\d)\\.?\b){4}$/gm";
        Regex regex = new Regex(pattern);
        
        return regex.IsMatch(ipAddress);
    }
}