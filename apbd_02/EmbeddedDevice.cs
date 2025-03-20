using System.Net;
using System.Text.RegularExpressions;
using apbd_02.exception;

namespace apbd_02;

public class EmbeddedDevice : Device
{
    private string _ipAddress;
    
    private string _networkName;

    public string NetworkName
    {
        get { return _networkName; }
        set
        {
            _networkName = value;
        }
    }
    
    public EmbeddedDevice(string id, string name, bool isTurnedOn, string ipAddress, string networkName) : base(id, name, isTurnedOn)
    {
        IpAddress = ipAddress;
        _networkName = networkName;
        if (!_networkName.Contains("MD Ltd.") && isTurnedOn)
        {
            throw new ArgumentException("The network name must contain MD Ltd. to be turned on", "networkName");
        }
        Console.WriteLine("[Object] Embedded device created");
    }

    public string IpAddress
    {
        get {return _ipAddress;}
        set
        {
            if (!IsValidIpAddress(value))
            {
                throw new ArgumentException("Invalid IP Address");
            }
            else
            {
                _ipAddress = value;
            }
        }
    }

    private static bool IsValidIpAddress(string ipAddress)
    {
        string pattern = @"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$";
        Regex regex = new Regex(pattern);

        return regex.IsMatch(ipAddress);
    }

    public override void TurnMode()
    {
        if (IsTurnedOn)
        {
            IsTurnedOn = false;
            Console.WriteLine("[EmbeddedDevice] Turned Off");
        }
        else
        {
            if (Connect(_networkName))
            {
                IsTurnedOn = true;
                Console.WriteLine("[EmbeddedDevice] Turned On");
                Console.WriteLine("[EmbeddedDevice] Successfully connected Network Name: " + _networkName + " contains MD Ltd.");
            }
            else
            {
                throw new ConnectionException();
            }
            
        }
    }

    private bool Connect(string networkName)
    {
        if (networkName.Contains("MD Ltd."))
        {
            return true;
        }

        return false;
    }
    
    public override void Info()
    {
        Console.WriteLine($"Registered info about Embedded Device: \n" +
                          $"ID: {Id}; Name: {Name}; TurnedOn: {IsTurnedOn}; IP address: {IpAddress}, " +
                          $"Networking name: {_networkName}%");
    }
}