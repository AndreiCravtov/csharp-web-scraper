namespace CSharpWebScraper;

public class ConsoleControl
{
    private LetterPrevalence _letterPrevalence;
    private int[] _loaderCounter = new int[] {0, 8};

    public ConsoleControl(ref LetterPrevalence letterPrevalence)
    {
        // get letter prevalence reference
        _letterPrevalence = letterPrevalence;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
    }

    private void DisplayBarChart()
    {
        // get percentages
        float[] percentageArray = _letterPrevalence.GetAllPercentages();

        // pick bar chart size
        float maxPercent = percentageArray.Max();
        (float, float, float) size; // (max value, decrement, modulus)
        if (maxPercent <= 10)
        {
            size = (10f, 0.2f, 1f);
        }
        else if (maxPercent <= 20)
        {
            size = (20f, 0.4f, 2f);
        }
        else if (maxPercent <= 30)
        {
            size = (30f, 0.6f, 3f);
        }
        else if (maxPercent <= 40)
        {
            size = (40f, 0.8f, 4f);
        }
        else if (maxPercent <= 50)
        {
            size = (50f, 1f, 5);
        }
        else if (maxPercent <= 60)
        {
            size = (60f, 1.2f, 6);
        }
        else if (maxPercent <= 70)
        {
            size = (70f, 1.4f, 7);
        }
        else if (maxPercent <= 80)
        {
            size = (80f, 1.6f, 8);
        }
        else if (maxPercent <= 90)
        {
            size = (90f, 1.8f, 9);
        }
        else
        {
            size = (100f, 2f, 10);
        }

        // construct bar chart
        string printable =
            "                                                                                       \n" +
            "      |                                                                                \n";
        string leftPart;
        string centerPart;
        string rightPart;
        for (double i = size.Item1; i > 0; i = Math.Round(i - size.Item2, 1))
        {
            if (i % size.Item3 == 0)
            {
                leftPart = $" {i.ToString().PadLeft(3)}% |";
                centerPart = "";
                for (int j = 0; j < percentageArray.Length; j++)
                {
                    centerPart += "‾‾";
                    centerPart += (percentageArray[j] >= i) ? "■" : "‾";
                }
                centerPart += "‾‾";
            }
            else
            {
                leftPart = "      |";
                centerPart = "";
                for (int j = 0; j < percentageArray.Length; j++)
                {
                    centerPart += "  ";
                    centerPart += (percentageArray[j] >= i) ? "■" : " ";
                }
                centerPart += "  ";
            }
            rightPart = "\n";
            printable += leftPart + centerPart + rightPart;
        }
        printable += "   0% \\——A——B——C——D——E——F——G——H——I——J——K——L——M——N——O——P——Q——R——S——T——U——V——W——X——Y——Z——\n";

        Console.Write(printable);
    }

    private void DisplayWaitMessage()
    {
        string printable =
            "                                                                                       \n" +
            "   /‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾\\   \n" +
            "   |                                                                               |   \n" +
            "   |                             >> WAITING FOR DATA <<                            |   \n" +
            "   |                                    ";

        for (int i=0; i < _loaderCounter[1]; i++)
        {
            if (i == _loaderCounter[0])
                printable += "=";
            else
                printable += " ";
        }
        _loaderCounter[0] = (_loaderCounter[0] + 1) % _loaderCounter[1];

        printable +=
            "                                   |   \n" +
            "   |                                                                               |   \n" +
            "   \\_______________________________________________________________________________/   \n" +
            "                                                                                       \n";
        Console.Write(printable);
    }

    private void DisplayAnalytics()
    {
        string printable = 
            $"\n   >> Letters analyzed: {_letterPrevalence.GetTotalLettersAnalyzed()} <<\n"+
            "   >> Hold Q to quit <<\n";
        Console.Write(printable);
    }

    public void DisplayData()
    {
        if (_letterPrevalence.GetTotalLettersAnalyzed() == 0)
            DisplayWaitMessage();
        else
        {
            DisplayBarChart();
            DisplayAnalytics();
        }
    }

    public void ClearScreen(bool strict = false)
    {
        if (strict)
            Console.Clear();
        else
            Console.SetCursorPosition(0, 0);
    }
}
