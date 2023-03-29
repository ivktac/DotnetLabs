using Research.Interfaces;

namespace Research.Models;

public class Team : INameAndCopy
{
    private string _organization = default!;
    private int _registrationNumber = default!;


    public Team() : this("Default team", 1) { }

    public Team(string name, int registrationNumber)
    {
        Organization = name;
        RegistrationNumber = registrationNumber;
    }

    string INameAndCopy.Name { get => Organization; set => Organization = value; }

    public string Organization { get => _organization; set => _organization = value; }

    public int RegistrationNumber
    {
        get => _registrationNumber;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException("Registration number cannot be negative");
            }

            _registrationNumber = value;
        }
    }

    public static bool operator ==(Team t1, Team t2) => t1.Equals(t2);

    public static bool operator !=(Team t1, Team t2) => !(t1 == t2);

    public override bool Equals(object? obj)
    {
        var team = obj as Team;

        if (team is null)
        {
            return false;
        }

        return Organization == team.Organization && RegistrationNumber == team.RegistrationNumber;
    }

    public override int GetHashCode() => HashCode.Combine(Organization, RegistrationNumber);

    public override string ToString() => $"{Organization} ({RegistrationNumber})";

    public virtual object DeepCopy() => MemberwiseClone();
}