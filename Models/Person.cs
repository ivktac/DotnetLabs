using Research.Interfaces;

namespace Research.Models;

public class Person : INameAndCopy
{
    private string _name = default!;
    private string _surname = default!;
    private DateTime _birthday = default!;

    public Person() : this("John", "Doe", new DateTime(1990, 1, 1)) { }

    public Person(string name, string surname, DateTime birthday)
    {
        Name = name;
        Surname = surname;
        BirthDay = birthday;
    }

    public string Name { get => _name; set => _name = value; }

    public string Surname { get => _surname; set => _surname = value; }

    public DateTime BirthDay { get => _birthday; private set => _birthday = value; }

    public int Age
    {
        get => DateTime.Now.Year - _birthday.Year;
        init => _birthday = DateTime.Now.AddYears(-value);
    }

    string INameAndCopy.Name { get => Name; set => Name = value; }

    public static bool operator ==(Person p1, Person p2) => p1.Equals(p2);

    public static bool operator !=(Person p1, Person p2) => !p1.Equals(p2);

    public override bool Equals(object? obj)
    {
        var person = obj as Person;

        if (person is null)
        {
            return false;
        }

        return Name == person.Name && Surname == person.Surname && BirthDay == person.BirthDay;
    }

    public override int GetHashCode() => HashCode.Combine(Name, Surname, BirthDay);

    public override string ToString() => $"{Name} {Surname} ({BirthDay.ToShortDateString()})";

    public virtual string ToShortString() => $"{Name} {Surname}";

    public virtual object DeepCopy() => MemberwiseClone();
}