namespace Research;

class Person
{
    private string name;
    public string Name
    {
        get { return name; }
    }

    private string surname;
    public string Surname
    {
        get { return surname; }
    }

    private DateTime birthday;

    public DateTime Birthday
    {
        get { return birthday; }
    }

    public int Age
    {
        get { return DateTime.Now.Year - birthday.Year; }
        init { birthday = DateTime.Now.AddYears(-value); }
    }


    public Person()
    {
        this.name = "John";
        this.surname = "Doe";
        this.birthday = new DateTime(1990, 1, 1);
    }

    public Person(string name, string surname, DateTime birthday)
    {
        this.name = name;
        this.surname = surname;
        this.birthday = birthday;
    }

    public override string ToString()
    {
        return $"{name} {surname} ({birthday.ToShortDateString()})";
    }

    public virtual string ToShortString()
    {
        return $"{name} {surname}";
    }
}