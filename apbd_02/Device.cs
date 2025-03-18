namespace apbd_02;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool isTurnedOn { get; set; }

    
    public void Launch()
    {
        Console.WriteLine(Id + " " + Name + " " + isTurnedOn);
    }
    
    
}