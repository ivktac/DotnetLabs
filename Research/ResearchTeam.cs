namespace Research;

class ResearchTeam
{
    private string _topic;
    private string _organization;
    private int _registrationNumber;
    private TimeFrame _timeFrame;
    private Paper[] _publications;

    public ResearchTeam(string topic, string organization, int registrationNumber, TimeFrame timeFrame)
    {
        _topic = topic;
        _organization = organization;
        _registrationNumber = registrationNumber;
        _timeFrame = timeFrame;
        _publications = new Paper[0];
    }

    public ResearchTeam() : this("No topic", "No organization", 0, TimeFrame.Year) { }


    public string Organization
    {
        get { return _organization; }
        init { _organization = value; }
    }

    public string Topic
    {
        get { return _topic; }
        init { _topic = value; }
    }

    public int RegistrationNumber
    {
        get { return _registrationNumber; }
        init { _registrationNumber = value; }
    }

    public TimeFrame TimeFrame
    {
        get { return _timeFrame; }
        init { _timeFrame = value; }
    }

    public Paper[] Publications
    {
        get { return _publications; }
        init { _publications = value; }
    }

    public Paper? LastPublication
    {
        get
        {
            if (Publications.Length == 0)
            {
                return null;
            }

            Paper lastPublication = Publications[0];
            foreach (Paper publication in Publications)
            {
                if (publication.PublishDate > lastPublication.PublishDate)
                {
                    lastPublication = publication;
                }
            }

            return lastPublication;
        }
    }

    public bool this[TimeFrame timeFrame] => TimeFrame == timeFrame;

    public void AddPapers(params Paper[]? papers)
    {
        if (papers == null)
        {
            return;
        }

        if (_publications == null)
        {
            _publications = papers;
            return;
        }

        Paper[] newPublications = new Paper[_publications.Length + papers.Length];
        _publications.CopyTo(newPublications, 0);
        papers.CopyTo(newPublications, _publications.Length);
        _publications = newPublications;
    }

    public sealed override string ToString()
    {
        string result = $"Topic: {_topic}\nOrganization: {_organization}\nRegistration number: {_registrationNumber}\nTime frame: {_timeFrame}\nPublications:\n";
        System.Text.StringBuilder sb = new System.Text.StringBuilder(result);
        foreach (Paper publication in _publications)
        {
            sb.AppendLine(publication.ToString());
        }
        return sb.ToString();
    }

    public string ToShortString() => $"Topic: {_topic}\nOrganization: {_organization}\nRegistration number: {_registrationNumber}\nTime frame: {_timeFrame}\n";
}