namespace Research;

public class Team : INameAndCopy
{
    private string _organization;
    private int _registrationNumber;


    public Team() : this("Default team", 0) { }

    public Team(string name, int registrationNumber)
    {
        _organization = name;
        _registrationNumber = registrationNumber;
    }

    string INameAndCopy.Name { get => _organization; set => _organization = value; }

    public string Organization { get => _organization; set => _organization = value; }

    public int RegistrationNumber
    {
        get => _registrationNumber;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Registration number cannot be negative");
            }

            _registrationNumber = value;
        }
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

    public override int GetHashCode() => HashCode.Combine(_organization, _registrationNumber);

    public override string ToString() => $"{Organization} ({RegistrationNumber})";

    public virtual object DeepCopy()
    {
        var team = MemberwiseClone() as Team;

        if (team is null)
        {
            throw new NullReferenceException("Team cannot be null");
        }

        team.Organization = Organization;
        team.RegistrationNumber = RegistrationNumber;
        return team;
    }
}