namespace Reseach.Services;

public class TeamListHandlerEventArgs : EventArgs
{
    public TeamListHandlerEventArgs(string collectionName, string changeType, int indexElement)
    {
        CollectionName = collectionName;
        ChangeType = changeType;
        IndexElement = indexElement;
    }

    public string CollectionName { get; set; }
    public string ChangeType { get; set; }
    public int IndexElement { get; set; }

    public override string ToString() =>
        $"Collection name: {CollectionName}, change type: {ChangeType}, index element: {IndexElement}";
}
