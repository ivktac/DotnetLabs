namespace Research;

public class Team : INameAndCopy
{
    private string _organization;
    private int _registrationNumber;

    public Team(string name, int registrationNumber)
    {
        _organization = name;
        _registrationNumber = registrationNumber;
    }

    public Team() : this("Default team", 0) { }

    string INameAndCopy.Name { get => _organization; set => _organization = value; }

    public virtual object DeepCopy()
    {
        return new Team(Organization, RegistrationNumber);
    }

    public string Organization { get => _organization; init => _organization = value; }

    public int RegistrationNumber
    {
        get => _registrationNumber;
        init => _registrationNumber = value > 0 ? value : throw new ArgumentException("Registration number must be greater than 0");
    }

    public static bool operator ==(Team t1, Team t2) => t1.Equals(t2);

    public static bool operator !=(Team t1, Team t2) => !t1.Equals(t2);

    public override bool Equals(object? obj)
    {
        var team = obj as Team;

        if (team is null)
        {
            return false;
        }

        return Organization == team.Organization && RegistrationNumber == team.RegistrationNumber;
    }

    public override int GetHashCode() => (Organization, RegistrationNumber).GetHashCode();

    public override string ToString() => $"{Organization} ({RegistrationNumber})";
}