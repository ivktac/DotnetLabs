namespace Research;

class ResearchTeam
{
    private string topic;
    public string Topic
    {
        get { return topic; }
        init { topic = value; }
    }

    private string organization;
    public string Organization
    {
        get { return organization; }
        init { organization = value; }
    }

    private int registrationNumber;
    public int RegistrationNumber
    {
        get { return registrationNumber; }
        init { registrationNumber = value; }
    }

    private TimeFrame timeFrame;
    public TimeFrame TimeFrame
    {
        get { return timeFrame; }
        init { timeFrame = value; }
    }

    private Paper[] publications;
    public Paper[] Publications
    {
        get { return publications; }
        init { publications = value; }
    }

    // last published Paper
    public Paper? LastPublication
    {
        get
        {
            if (publications.Length == 0)
            {
                return null;
            }

            Paper lastPublication = publications[0];
            foreach (Paper publication in publications)
            {
                if (publication.PublishDate > lastPublication.PublishDate)
                {
                    lastPublication = publication;
                }
            }
            return lastPublication;
        }
    }

    public bool this[TimeFrame timeFrame]
    {
        get
        {
            return this.timeFrame == timeFrame;
        }
    }


    public ResearchTeam(string topic, string organization, int registrationNumber, TimeFrame timeFrame)
    {
        this.topic = topic;
        this.organization = organization;
        this.registrationNumber = registrationNumber;
        this.timeFrame = timeFrame;
        this.publications = new Paper[0];
    }

    public ResearchTeam()
    {
        topic = "No topic";
        organization = "No organization";
        registrationNumber = 0;
        timeFrame = TimeFrame.Year;
        publications = new Paper[0];
    }

    public void AddPapers(params Paper[] papers)
    {
        Paper[] newPublications = new Paper[publications.Length + papers.Length];
        publications.CopyTo(newPublications, 0);
        papers.CopyTo(newPublications, publications.Length);
        publications = newPublications;
    }

    public override string ToString()
    {
        string result = $"Topic: {topic}\nOrganization: {organization}\nRegistration number: {registrationNumber}\nTime frame: {timeFrame}\nPublications:\n";
        foreach (Paper publication in publications)
        {
            result += publication.ToString() + "\n";
        }
        return result;
    }

    public virtual string ToShortString()
    {
        return $"Topic: {topic}\nOrganization: {organization}\nRegistration number: {registrationNumber}\nTime frame: {timeFrame}\n";
    }
}