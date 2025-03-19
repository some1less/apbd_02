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
                int batteryLevel = int.Parse(additionalData.TrimEnd('%'));
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
                string ipAddress = additionalData;
                string networkName = parts[4];
                return new EmbeddedDevice(deviceId, namepart, isTurnedOn, ipAddress, networkName);
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

    public void AddDevice(Device device)
    {
        try
        {
            if (devices.Count < 15)
            {
                devices.Add(device);
                Console.WriteLine($"Added device: {device}");
            }
            else
            {
                Console.WriteLine($"Cannot add more devices. Storage is full");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error adding device: {e}");
        }
        
    }

    public void RemoveDevice(int deviceId)
    {
        try
        {
            var deviceToRemove = devices.Find(d => d.Id == deviceId);
            if (deviceToRemove != null)
            {
                devices.Remove(deviceToRemove);
                Console.WriteLine($"Removed device: {deviceToRemove}");
            }
            else
            {
                Console.WriteLine($"Device not found: {deviceId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting device: {e}");
        }
    }

    // I did not come up with idea how to add possibility to edit ipaddress and etc
    public void EditDeviceData(int deviceId, string newName, bool newState)
    {
        try
        {
            var deviceToEdit = devices.Find(d => d.Id == deviceId);
            if (deviceToEdit != null)
            {
                deviceToEdit.Name = newName;
                deviceToEdit.IsTurnedOn = newState;
                Console.WriteLine($"Edited device: {deviceToEdit}");
            }
            else
            {
                Console.WriteLine($"Device not found: {deviceId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error editing device: {e}");
        }
    }

    public void TurnOnDevice(int deviceId)
    {
        try
        {
            var device = devices.Find(d => d.Id == deviceId);
            if (device != null)
            {
                device.IsTurnedOn = true;
                Console.WriteLine($"Turned on device: {device}");
            }
            else
            {
                Console.WriteLine($"Device not found: {deviceId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error turning on device: {e}");
        }
    }
    
    public void TurnOffDevice(int deviceId)
    {
        try
        {
            var device = devices.Find(d => d.Id == deviceId);
            if (device != null)
            {
                device.IsTurnedOn = false;
                Console.WriteLine($"Turned off device: {device}");
            }
            else
            {
                Console.WriteLine($"Device not found: {deviceId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error turning off device: {e}");
        }
    }

    public void ShowAllDevices()
    {
        try
        {
            if (devices.Count == 0)
            {
                Console.WriteLine("No devices found");
            }
            else
            {
                foreach (var device in devices)
                {
                    device.Info();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error showing devices: {e}");
        }
    }
}