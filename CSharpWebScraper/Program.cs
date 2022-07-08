namespace CSharpWebScraper;

class Program
{
    static void Main(string[] args)
    {
        // start input service
        KeyQueue keyQueue = new KeyQueue(); 

        // create letter prevalence
        LetterPrevalence letterPrevalence = new LetterPrevalence();

        // create console controls
        ConsoleControl consoleControl = new ConsoleControl(ref letterPrevalence);

        // create scraper
        Scraper scraper = new Scraper(ref letterPrevalence);

        // run mainloop
        int clearCounter = 0;
        char? inputCharecter = null;
        while (inputCharecter != 'q')
        {
            // clear screen
            if (clearCounter % 10 == 0)
                consoleControl.ClearScreen(true);
            else
                consoleControl.ClearScreen();

            // display data
            consoleControl.DisplayData();

            // sleep
            Thread.Sleep(50);

            // update loop variables
            clearCounter++;
            inputCharecter = keyQueue.GetNextInput();
            if (inputCharecter != null)
                inputCharecter = char.ToLower((char)inputCharecter);
        }

        // stop
        scraper.Quit();
    }
}
