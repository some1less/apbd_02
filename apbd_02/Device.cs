namespace apbd_02;

public abstract class Device
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsTurnedOn { get; set; }

    protected Device(int id, string name, bool isTurnedOn)
    {
        Id = id;
        Name = name;
        IsTurnedOn = isTurnedOn;
    }

    public abstract void Activate();
}