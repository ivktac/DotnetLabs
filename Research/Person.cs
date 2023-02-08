namespace Research;

public class Person
{
    private string _name;
    private string _surname;
    private DateTime _birthday;

    public Person() : this("John", "Doe", new DateTime(1990, 1, 1)) { }

    public Person(string name, string surname, DateTime birthday)
    {
        _name = name;
        _surname = surname;
        _birthday = birthday;
    }

    public string Name => _name;

    public string Surname => _surname;

    public DateTime BirthDay => _birthday;

    public int Age
    {
        get => DateTime.Now.Year - _birthday.Year;
        init => _birthday = DateTime.Now.AddYears(-value);
    }

    public override string ToString() => $"{_name} {_surname} ({_birthday.ToShortDateString()})";

    public virtual string ToShortString() => $"{_name} {_surname}";
}