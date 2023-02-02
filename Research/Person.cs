namespace Research;

class Person : INameAndCopy
{
    private string _name;
    private string _surname;
    private DateTime _birthday;


    public Person(string name, string surname, DateTime birthday)
    {
        _name = name;
        _surname = surname;
        _birthday = birthday;
    }

    public Person() : this("John", "Doe", new DateTime(1990, 1, 1)) { }

    string INameAndCopy.Name { get => _name; set => _name = value; }

    public virtual object DeepCopy()
    {
        return new Person(Name, Surname, BirthDay);
    }

    public string Name => _name;

    public string Surname => _surname;

    public DateTime BirthDay => _birthday;

    public int Age
    {
        get => DateTime.Now.Year - _birthday.Year;
        init => _birthday = DateTime.Now.AddYears(-value);
    }

    public static bool operator ==(Person p1, Person p2) => p1.Equals(p2);

    public static bool operator !=(Person p1, Person p2) => !p1.Equals(p2); // or should I use !(p1 == p2)?

    public override bool Equals(object? obj)
    {
        var person = obj as Person;

        if (person is null)
        {
            return false;
        }

        return Name == person.Name && Surname == person.Surname && BirthDay == person.BirthDay;
    }

    public override int GetHashCode() => (Name, Surname, BirthDay).GetHashCode();

    public override string ToString() => $"{_name} {_surname} ({_birthday.ToShortDateString()})";

    public virtual string ToShortString() => $"{_name} {_surname}";
}