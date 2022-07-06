using System.Collections;

namespace CSharpWebScraper;

class LinkSet : IEnumerable
{
    private List<string> _uniqueSet;
    private List<string> _unvisitedSet;
    private List<string> _visitedSet;
    private string _baseURL;

    public LinkSet(string baseURL)
    {
        _uniqueSet = new List<string>();
        _unvisitedSet = new List<string>();
        _visitedSet = new List<string>();
        _baseURL = baseURL;
    }

    /// <summary>
    /// Gets a random unvisited link.
    /// </summary>
    /// <returns>The random uvisited link if one exists, otherwise null</returns>
    public string? GetUnvisitedLink()
    {
        if (_unvisitedSet.Count == 0) return null;

        Random random = new Random();
        string item = _unvisitedSet[random.Next(0, _unvisitedSet.Count)];
        _unvisitedSet.Remove(item);
        _visitedSet.Add(item);

        return item;
    }

    /// <summary>
    /// Adds a link to the visited URL set.
    /// </summary>
    /// <param name="URL">The link to add.</param>
    /// <returns>Returns true for a valid unvisited link, otherwise false.</returns>
    public bool AddLink(string URL)
    {
        // check link validity
        if (!IsLinkValid(URL)) return false;

        // check if it already exists
        if (_uniqueSet.Contains(URL)) return false;

        // add url
        _uniqueSet.Add(URL);
        _unvisitedSet.Add(URL);
        return true;
    }

    /// <summary>
    /// Checks if the link is valid or not.
    /// </summary>
    /// <param name="URL">The link.</param>
    /// <returns>Returns true for a valid link, otherwise false.</returns>
    public bool IsLinkValid(string URL)
    {
        // check if base contained
        if (!URL.Contains(_baseURL)) return false;

        // get URL remainder
        string remainder = URL.Replace(_baseURL, "");

        if (
            remainder.Contains("File") ||       // if it's a file page
            remainder.Contains("#") ||          // if it's same page
            remainder.Contains("/") ||          // if it's a sub-page
            remainder.Contains("identifier") || // if it's a redirect
            remainder.Contains(":")             // if misc
        )
            return false;

        return true;
    }

    public IEnumerator GetEnumerator()
    {
        for (int i=0; i<_uniqueSet.Count; i++)
            yield return _uniqueSet[i];
    }
}
