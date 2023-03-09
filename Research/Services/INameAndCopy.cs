namespace Research.Services;

public interface INameAndCopy
{
    string Name { get; set; }
    object DeepCopy();
}
