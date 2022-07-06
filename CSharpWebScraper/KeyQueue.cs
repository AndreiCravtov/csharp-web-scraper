namespace CSharpWebScraper;
class KeyQueue
{
    private Queue<char> _queue;
    public KeyQueue()
    {
        _queue = new Queue<char>();

        // start input thread
        Thread input = new Thread(GetInput);
        input.IsBackground = true;
        input.Start();
    }

    public char? GetNextInput()
    {
        if (_queue.Count == 0)
            return null;
        return _queue.Dequeue();
    }

    private void GetInput()
    {
        while (true)
        {
            char key = Console.ReadKey(true).KeyChar;
            _queue.Enqueue(key);
        }
    }
}