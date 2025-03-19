using System.Text.RegularExpressions;

namespace apbd_02;

public class DeviceManager
{

    private List<Device> devices;
    private string _filepath;

    public DeviceManager(string filepath)
    {
        _filepath = filepath;
        devices = new List<Device>();

        try
        {
            if (File.Exists(_filepath))
            {
                var lines = File.ReadAllLines(_filepath);

                foreach (var line in lines)
                {
                    if (devices.Count < 15)
                    {
                        try
                        {
                            var device = ParseDevice(line);
                            if (device != null)
                            {
                                devices.Add(device);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Error parsing device: {e}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading file: {e}");
        }
    }

    private Device ParseDevice(string line)
    {
        try
        {
            var parts = line.Split(',');

            if (parts.Length < 4)
                return null;

            string idPart = parts[0];
            string namepart = parts[1];
            bool isTurnedOn = bool.Parse(parts[2]);
            string additionalData = parts[3];

            // I didnt come up with anything better than looking for first founded digit >.<
            var match = Regex.Match(namepart, @"\d+");
            if (!match.Success)
            {
                Console.WriteLine($"Invalid device name: {idPart}");
                return null;
            }

            int deviceId = int.Parse(match.Value);

            if (idPart.StartsWith("SW"))
            {
                if (parts.Length != 4) return null;
                int batteryLevel = int.Parse(additionalData);
                return new Smartwatch(deviceId, namepart, isTurnedOn, batteryLevel);
            }
            else if (idPart.StartsWith("P"))
            {
                if (parts.Length != 4) return null;
                var operationSystem = additionalData;
                return new PersonalComputer(deviceId, namepart, isTurnedOn, operationSystem);
            }
            else if (idPart.StartsWith("ED"))
            {
                if (parts.Length != 5) return null;
                string ipAdress = additionalData;
                string networkName = parts[4];
                return new EmbeddedDevice(deviceId, namepart, isTurnedOn, ipAdress, networkName);
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating device from line '{line}': {e}");
            return null;
        }
    }
}