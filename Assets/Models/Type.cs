public class Type : Model
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Type(int Id, string Name, string Description): base(Id)
    {
        this.Name = Name;
        this.Description = Description;
    }
}
