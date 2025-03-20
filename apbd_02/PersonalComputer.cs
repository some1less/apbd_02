using apbd_02.exception;

namespace apbd_02;

public class PersonalComputer : Device
{
    private string _operationSystem;

    public PersonalComputer(
        string id,
        string name,
        bool isTurnedOn,
        string operationSystem) : base(id, name, isTurnedOn)
    {
        _operationSystem = operationSystem;

        if (operationSystem == null && isTurnedOn == true)
        {
            throw new EmptySystemException();
        }
        
        Console.WriteLine("[Object] PersonalComputer Created");
    }

    public override void TurnMode()
    {
        if (IsTurnedOn)
        {
            IsTurnedOn = false;
            Console.WriteLine("[PersonalComputer] Turned Off");

        }
        else
        {
            if (_operationSystem != null)
            {
                IsTurnedOn = true;
                Console.WriteLine("[PersonalComputer] Turned On");
            }
            else
            {
                throw new EmptySystemException();
            }
            

        }
    }

    public override void Info()
    {
        Console.WriteLine($"Registered info about Personal Computer: \n" +
                          $"ID: {Id}; Name: {Name}; TurnedOn: {IsTurnedOn}; Operation system: {_operationSystem}%");
    }
}