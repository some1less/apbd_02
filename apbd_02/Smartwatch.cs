namespace apbd_02;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryLevel {get; set;}

    
    public Smartwatch(int id, string name, bool isTurnedOn, int batteryLevel) : base(id, name, isTurnedOn)
    {
        if (batteryLevel < 0 || batteryLevel > 100)
        {
            Console.WriteLine("batteryLevel out of range");
        }
        else
        {
            _batteryLevel = batteryLevel;
            if (_batteryLevel <= 20)
            {
                notify();

            }
        }
        
    }
    
    public void notify()
    {
        Console.WriteLine($"[Smartwatch] Low Battery Level!: {_batteryLevel}%");
    }
    
    public override void Activate()
    {
        Console.WriteLine($"Smartwatch activated. Registered info about Smartwatch:" +
                           "ID: {id}; Name: {name}; Status: {isTurnedOn}; Battery Level: {batteryLevel}%");
    }
    
}