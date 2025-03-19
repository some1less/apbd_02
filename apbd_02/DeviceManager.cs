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
}