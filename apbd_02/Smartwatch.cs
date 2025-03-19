namespace apbd_02;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryLevel {get; set;}

    
    public Smartwatch(int id, string name, bool isTurnedOn, int batteryLevel) : base(id, name, isTurnedOn)
    {
        if (batteryLevel < 0 || batteryLevel > 100)
        {
            throw new ArgumentOutOfRangeException("batteryLevel out of range. Cannot activate Smartwatch");
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

        if (_batteryLevel == 0)
        {
            Console.WriteLine("batteryLevel is 0%. Cannot activate Smartwatch");
            return;

        }

        if (_batteryLevel > 0)
        {
            Console.WriteLine($"Smartwatch activated. Registered info about Smartwatch: \n" +
                $"ID: {Id}; Name: {Name}; TurnedOn: {IsTurnedOn}; Battery Level: {_batteryLevel}%");
            
            if (_batteryLevel <= 20)
            {
                notify();
            }
        }
    }
    
}