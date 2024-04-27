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

        // Old xpath: //div[contains(@class, \"kDxHqc M3hg4d\")]/a[@class=\"vsXl0d\"]
        string reservationXPath = "//div[@class=\"LXiaI\"]/a[@class=\"K9ZSQ\" and ./button/span=\"Reserve a table\"]";

        var reservationElement = doc.DocumentNode.SelectSingleNode(reservationXPath);

        ScrapingResults result = new ScrapingResults();

        if (reservationElement != null) {
            result.ReservationURL = reservationElement.GetAttributeValue<string>("href", "");
        }

        string orderXPath = "//div[@class=\"LXiaI\"]/a[@class=\"K9ZSQ\" and ./button/span=\"Order online\"]";

        var orderElement = doc.DocumentNode.SelectSingleNode(orderXPath);

        if (orderElement != null) {
            result.OrderURL = orderElement.GetAttributeValue<string>("href", "");
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
        public string? OrderURL { get; set; }
        public string? RestaurantWebsiteURL { get; set; }
        

        public ScrapingResults() {
            ReservationURL = null;
            RestaurantWebsiteURL = null;
        }
    }
}