namespace Research.Models;

internal class TeamsJournalEntry
{
    public TeamsJournalEntry(string collectionName, string eventName, int indexOfNewElement)
    {
        CollectionName = collectionName;
        EventName = eventName;
        IndexOfNewElement = indexOfNewElement;
    }

    public string CollectionName { get; set; }
    public string EventName { get; set; }
    public int IndexOfNewElement { get; set; }

    public override string ToString() =>
        $"Collection name: {CollectionName}, event name: {EventName}, index of new element: {IndexOfNewElement}";
}
