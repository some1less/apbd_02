namespace apbd_02;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryLevel {get; set;}

    
    public Smartwatch(int id, string name, bool isTurnedOn, int batteryLevel) : base(id, name, isTurnedOn)
    {
        if (batteryLevel < 0 || batteryLevel > 100)
        {
            _batteryLevel = -1;
        }
        else
        {
            _batteryLevel = batteryLevel;

        }
        
    }
    
    public void notify()
    {
        Console.WriteLine($"[Smartwatch] Low Battery Level!: {_batteryLevel}%");
    }
    
    public override void Activate()
    {

        if (_batteryLevel == -1)
        {
            Console.WriteLine("batteryLevel out of range. Cannot activate Smartwatch");
            return;
        }

        if (_batteryLevel == 0)
        {
            Console.WriteLine("batteryLevel is 0%. Cannot activate Smartwatch");
            return;

        }
        
        Console.WriteLine($"Smartwatch activated. Registered info about Smartwatch:" +
                          "ID: {id}; Name: {name}; Status: {isTurnedOn}; Battery Level: {batteryLevel}%");

        if (_batteryLevel <= 20)
        {
            notify();
        }
        
        
        
    }
    
}