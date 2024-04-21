using HtmlAgilityPack;
using Microsoft.Playwright;

public class ScrapingService {

    public ScrapingService() {}

    public async Task<ScrapingResults> GetRestaurantUrl(string restaurantName) {
        using var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });

        string url = "https://www.google.com/search?q=";

        var words = restaurantName.Split(' ');

        foreach (var word in words) {
            url += word + "+";
        }

        var page = await browser.NewPageAsync();

        await page.GotoAsync(url);

        var html = await page.ContentAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        string reservationXPath = "//div[contains(@class, \"kDxHqc M3hg4d\")]/a[@class=\"vsXl0d\"]";

        var reservationElement = doc.DocumentNode.SelectSingleNode(reservationXPath);

        ScrapingResults result = new ScrapingResults();

        if (reservationElement != null) {
            result.ReservationURL = reservationElement.GetAttributeValue<string>("href", "");
        }

        var websiteXPath = "//a[@jsname=\"UWckNb\"]";

        var websiteElement = doc.DocumentNode.SelectSingleNode(websiteXPath);

        if (websiteElement != null) {
            result.RestaurantWebsiteURL = websiteElement.GetAttributeValue<string>("href", "");
        }

        return result;
    }

    public class ScrapingResults {

        public string? ReservationURL { get; set; }
        public string? RestaurantWebsiteURL { get; set; }

        public ScrapingResults() {
            ReservationURL = null;
            RestaurantWebsiteURL = null;
        }
    }
}