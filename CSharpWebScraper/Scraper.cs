using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CSharpWebScraper;

public class Scraper
{
    private const string StartLink = @"https://en.wikipedia.org/wiki/Wikipedia";
    private const string BaseURL = @"https://en.wikipedia.org/wiki/";
    private const int ThreadSleepTime = 150;

    private bool _stop = false;
    private LetterPrevalence _prevalence;
    private LinkSet _linkSet;
    private Thread _scraperThread;
    private IWebDriver _browser;

    public Scraper(ref LetterPrevalence prevalence)
    {
        // get letter prevalence reference
        _prevalence = prevalence;

        // create link set and load initial link
        _linkSet = new LinkSet(BaseURL);
        _linkSet.AddLink(StartLink);

        // set off a scraper thread
        _scraperThread = new Thread(StartScraperThread);
        _scraperThread.IsBackground = true;
        _scraperThread.Start();
    }

    public void Quit()
    {
        _stop = true;
        _browser?.Quit();
    }

    private void StartScraperThread()
    {
        while (!_stop)
        {
            // get next unvisited link, making sure it isn't null
            String? unvisitedLink = _linkSet.GetUnvisitedLink();
            if (unvisitedLink is null)
            {
                _stop = true;
                continue;
            }

            // initiate web browser and navigate to URL
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--log-level=3");
            _browser = new ChromeDriver(options);
            _browser.Navigate().GoToUrl(unvisitedLink);

            // add URL list
            var linkElements = _browser.FindElements(By.XPath("//a[@href]"));
            foreach (var linkElement in linkElements)
            {
                _linkSet.AddLink(linkElement.GetAttribute("href"));
            }

            // grab page content
            Queue<IWebElement> traversalQueue = new Queue<IWebElement>();
            var contentRoot = _browser.FindElement(By.Id("content"));
            traversalQueue.Enqueue(contentRoot);
            while (traversalQueue.Count > 0)
            {
                IWebElement element = traversalQueue.Dequeue();
                foreach (var child in element.FindElements(By.XPath("./child::*")))
                {
                    traversalQueue.Enqueue(child);
                }
                string elementContent = element.GetAttribute("innerText");
                if (elementContent != null && elementContent != "")
                {
                    // iterate over content, adding each letter to letter prevalence
                    foreach (char charecter in elementContent)
                    {
                        // sleep on this so that letters can be displayed slower
                        Thread.Sleep(ThreadSleepTime);

                        _prevalence.AddLetterAnalyzed(charecter);
                    }
                }
            }

            // quit web driver
            _browser.Quit();
        }
    }
}
