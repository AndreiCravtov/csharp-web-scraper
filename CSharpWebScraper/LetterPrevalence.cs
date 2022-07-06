namespace CSharpWebScraper;

public class LetterPrevalence
{
    private Dictionary<char, ulong> _letters;
    private ulong _totalLetters;

    public LetterPrevalence()
    {
        _letters = new Dictionary<char, ulong>()
        {
            { 'A', 0 },
            { 'B', 0 },
            { 'C', 0 },
            { 'D', 0 },
            { 'E', 0 },
            { 'F', 0 },
            { 'G', 0 },
            { 'H', 0 },
            { 'I', 0 },
            { 'J', 0 },
            { 'K', 0 },
            { 'L', 0 },
            { 'M', 0 },
            { 'N', 0 },
            { 'O', 0 },
            { 'P', 0 },
            { 'Q', 0 },
            { 'R', 0 },
            { 'S', 0 },
            { 'T', 0 },
            { 'U', 0 },
            { 'V', 0 },
            { 'W', 0 },
            { 'X', 0 },
            { 'Y', 0 },
            { 'Z', 0 },
        };
        _totalLetters = 0;
    }

    public float[] GetAllPercentages()
    {
        float[] result = new float[_letters.Count];

        // avoid dividing by zero
        if (_totalLetters == 0f)
            return result;

        // calculate each letter's percentage
        int iterator = 0;
        foreach (KeyValuePair<char, ulong> letter in _letters)
        {
            result[iterator] = (letter.Value / (float)_totalLetters) * 100;
            iterator++;
        }

        return result;
    }

    public float GetLetterPercentage(char key)
    {
        // check if key is appropriate
        if (!IsEnglishLetter(key))
            return 0f;

        // avoid dividing by zero
        if (_totalLetters == 0f)
            return 0f;

        // get number of times the `key` letter was analyzed
        float specificNumber = _letters[char.ToUpper(key)];

        // calculate percentage
        return (specificNumber / (float)_totalLetters) * 100;
    }

    public void AddLetterAnalyzed(char key, uint amount = 1)
    {
        // check if key is appropriate
        if (!IsEnglishLetter(key))
            return;

        // increment the number of the `key` letter by given amount
        _letters[char.ToUpper(key)] += amount;
        _totalLetters += amount;
    }

    public ulong GetTotalLettersAnalyzed()
    {
        return _totalLetters;
    }

    private bool IsEnglishLetter(char a)
    {
        if ("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(a))
            return true;
        return false;
    }
}