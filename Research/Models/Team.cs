using System;

using Research.Services;

namespace Research.Models;

public class Team : INameAndCopy, IComparable
{
    private string _organization = default!;
    private int _registrationNumber = default!;

    public Team()
        : this("Default team", 0) { }

    public Team(string name, int registrationNumber)
    {
        Organization = name;
        RegistrationNumber = registrationNumber;
    }

    public string Organization
    {
        get => _organization;
        set => _organization = value;
    }

    string INameAndCopy.Name
    {
        get => Organization;
        set => Organization = value;
    }

    public int RegistrationNumber
    {
        get => _registrationNumber;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Registration number cannot be negative");
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

    public int CompareTo(object? obj)
    {
        var team = obj as Team;

        if (team is null)
        {
            throw new ArgumentException("Object is not a Team");
        }

        return RegistrationNumber.CompareTo(team.RegistrationNumber);
    }

    public virtual object DeepCopy() => MemberwiseClone();
}
