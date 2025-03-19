using apbd_02.exception;

namespace apbd_02;

public class Smartwatch : Device, IPowerNotifier
{
    private int _batteryLevel;

    public Smartwatch(){}
    public Smartwatch(int id, string name, bool isTurnedOn, int batteryLevel) : base(id, name, isTurnedOn)
    {
        
        BatteryLevel = batteryLevel;
        
        if (_batteryLevel <= 20)
        {
            notify();
        }
        
        Console.WriteLine("[Object] Smartwatch Created");

    }

    public int BatteryLevel
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
    
    public void notify()
    {
        Console.WriteLine($"[Smartwatch] Low Battery Level!: {_batteryLevel}%");
    }
    
    public override void SwitchMode()
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
                _batteryLevel -= 10;
                Console.WriteLine("[Smartwatch] Turned On");
                Console.WriteLine($"Registered info about Smartwatch: \n" +
                                  $"ID: {Id}; Name: {Name}; TurnedOn: {IsTurnedOn}; Battery Level: {_batteryLevel}%");
            }
            
        }
    }
    
}