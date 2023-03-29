namespace Research.Models;

public class TeamsJournal
{
    private readonly List<TeamsJournalEntry> _entries;

    public TeamsJournal() => _entries = new();

    public void OnResearchTeamAddedOrInserted(object sender, TeamListHandlerEventArgs args)
    {
        var entry = new TeamsJournalEntry(args.CollectionName, args.ChangeType, args.IndexElement);
        _entries.Add(entry);
    }

    public override string ToString()
    {
        var stringBuilder = new System.Text.StringBuilder();
        foreach (var entry in _entries)
        {
            stringBuilder.AppendLine(entry.ToString());
        }
        return stringBuilder.ToString();
    }
}
