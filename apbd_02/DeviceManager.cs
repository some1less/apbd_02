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

            if (parts.Length != 4)
                return null;
            
            string idPart = parts[0];
            string namepart = parts[1];
            bool isTurnedOn = bool.Parse(parts[2]);
            string additionalData = parts[3];

            if (idPart.StartsWith("SW") || idPart.StartsWith("P-"))
            {
                bool isParsedSuccessfully = bool.TryParse(parts[2], out isTurnedOn);
                if (!isParsedSuccessfully)
                {
                    Console.WriteLine($"Invalid 'isTurnedOn' value for device '{idPart}'");
                    return null; // Invalid boolean value, return null
                }
            }
            
            if (idPart.StartsWith("SW"))
            {
                if (parts.Length != 4) return null;
                int batteryLevel = int.Parse(additionalData.TrimEnd('%'));
                return new Smartwatch(idPart, namepart, isTurnedOn, batteryLevel);
            }
            else if (idPart.StartsWith("P"))
            {
                if (parts.Length != 4) return null;
                var operationSystem = additionalData;
                return new PersonalComputer(idPart, namepart, isTurnedOn, operationSystem);
            }
            else if (idPart.StartsWith("ED"))
            {
                if (parts.Length != 5) return null;
                string ipAddress = additionalData;
                string networkName = parts[4];
                return new EmbeddedDevice(idPart, namepart, isTurnedOn, ipAddress, networkName);
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

    public void RemoveDevice(string deviceId)
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
    public void EditDeviceData(string deviceId, Device data)
    {
        try
        {
            Device device = devices.Find(d => d.Id == deviceId);

            if (device != null)
            {
                if (device.GetType() == data.GetType())
                {

                    Console.WriteLine($"Edited device: {device}");

                    if (device is Smartwatch smartwatch)
                    {
                        if (data is Smartwatch newSmartwatchData)
                        {
                            smartwatch.Name = newSmartwatchData.Name;
                            smartwatch.IsTurnedOn = newSmartwatchData.IsTurnedOn;
                            smartwatch.BatteryLevel = newSmartwatchData.BatteryLevel;
                            Console.WriteLine($"Smartwatch updated: Name: {smartwatch.Name}, " +
                                              $"Current turn status: {smartwatch.IsTurnedOn}, " +
                                              $"Battery Level: {smartwatch.BatteryLevel}%");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Smartwatch data");
                        }

                    }

                    if (device is PersonalComputer pc)
                    {
                        if (data is PersonalComputer newPersonalComputerData)
                        {
                            pc.Name = newPersonalComputerData.Name;
                            pc.IsTurnedOn = newPersonalComputerData.IsTurnedOn;
                            pc.OperationSystem = newPersonalComputerData.OperationSystem;
                            Console.WriteLine($"Personal Computer updated: Name: {pc.Name}, " +
                                              $"Current turn status: {pc.IsTurnedOn}, " +
                                              $"Operation system: {pc.OperationSystem}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid PersonalComputer data");
                        }
                    }

                    if (device is EmbeddedDevice ed)
                    {
                        if (data is EmbeddedDevice newEdData)
                        {
                            ed.Name = newEdData.Name;
                            ed.IsTurnedOn = newEdData.IsTurnedOn;
                            ed.IpAddress = newEdData.IpAddress;
                            ed.NetworkName = newEdData.NetworkName;
                            Console.WriteLine($"Embedded device updated: Name: {ed.Name}, " +
                                              $"Current turn status: {ed.IsTurnedOn}, " +
                                              $"IP Address: {ed.IpAddress}, " +
                                              $"Network name: {ed.NetworkName}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Embedded device data");
                        }
                    }

                }
                else
                {
                    Console.WriteLine($"Device type mismatch. Cannot edit {data.GetType().Name}" +
                                      $"on a {device.GetType().Name}");
                }
            }
            else
            {
                Console.WriteLine($"Device not found: {deviceId}");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Error editing device data: {e}");
        }
    }
    
    public void TurnOnDevice(string deviceId)
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
    
    public void TurnOffDevice(string deviceId)
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

    public void SaveDataToFile()
    {
        try
        {
            var lines = new List<string>();
            foreach (var device in devices)
            {
                string mainLine = $"{device.Id},{device.Name},{device.IsTurnedOn}";

                if (device is Smartwatch sw)
                {
                    mainLine += $",{sw.BatteryLevel}";
                }

                if (device is PersonalComputer pc)
                {
                    mainLine += $",{pc.OperationSystem}";
                }

                if (device is EmbeddedDevice ed)
                {
                    mainLine += $",{ed.IpAddress},{ed.NetworkName}";
                }
                else
                {
                    mainLine += ";";
                }

                lines.Add(mainLine);
            }

            File.WriteAllLines("output.txt", lines);
            Console.WriteLine("File saved");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving data of devices: {e}");
        }
    }
}