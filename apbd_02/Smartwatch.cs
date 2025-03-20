using apbd_02.exception;

namespace apbd_02;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryLevel;

    public Smartwatch(string id, string name, bool isTurnedOn, int batteryLevel) : base(id, name, isTurnedOn)
    {
        
        BatteryLevel = batteryLevel;
        Console.WriteLine("[Object] Smartwatch Created");

        if (_batteryLevel <= 20)
        {
            Notify();
        }
        
    }

    private int BatteryLevel
    {
        get { return _batteryLevel; }
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException("batteryLevel out of range. Cannot activate Smartwatch");
            }
            else
            {
                _batteryLevel = value;
            }
        }
    }
    
    public void Notify()
    {
        Console.WriteLine($"[Smartwatch] Low Battery Level!: {_batteryLevel}%");
    }
    
    public override void TurnMode()
    {

        if (IsTurnedOn)
        {
            IsTurnedOn = false;
            Console.WriteLine("[Smartwatch] Turned Off");
        }
        else
        {
            if (_batteryLevel < 11)
            {
                throw new EmptyBatteryException();
            } 
            else
            {
                IsTurnedOn = true;
                BatteryLevel -= 10;
                Console.WriteLine("[Smartwatch] Turned On");

                if (_batteryLevel <= 20)
                {
                    Notify();
                }
            }
            
        }
    }

    public override void Info()
    {
        Console.WriteLine($"Registered info about Smartwatch: \n" +
                          $"ID: {Id}; Name: {Name}; TurnedOn: {IsTurnedOn}; Current Battery Level: {_batteryLevel}%");
    }
    
}