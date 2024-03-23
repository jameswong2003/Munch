using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReservationAPI
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreateReservation_Click(object sender, EventArgs e)
        {
            string jsonString = "{\"id\": \"mP1EdIafQKMuOm9O4PzAfA\", \"alias\": \"barcelona-wine-bar-south-end-boston-6\", \"name\": \"Barcelona Wine Bar South End\", \"image_url\": \"https://s3-media3.fl.yelpcdn.com/bphoto/5b1kPcvyjRezfN7gB7bJOg/o.jpg\", \"is_claimed\": true, \"is_closed\": false, \"url\": \"https://www.yelp.com/biz/barcelona-wine-bar-south-end-boston-6?adjust_creative=g71Xf3LSsqwyvP75W36_9g&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_lookup&utm_source=g71Xf3LSsqwyvP75W36_9g\", \"phone\": \"+16172662600\", \"display_phone\": \"(617) 266-2600\", \"review_count\": 1467, \"categories\": [{\"alias\": \"spanish\", \"title\": \"Spanish\"}, {\"alias\": \"wine_bars\", \"title\": \"Wine Bars\"}, {\"alias\": \"tapasmallplates\", \"title\": \"Tapas/Small Plates\"}], \"rating\": 4.4, \"location\": {\"address1\": \"525 Tremont St\", \"address2\": null, \"address3\": \"\", \"city\": \"Boston\", \"zip_code\": \"02116\", \"country\": \"US\", \"state\": \"MA\", \"display_address\": [\"525 Tremont St\", \"Boston, MA 02116\"], \"cross_streets\": \"Milford St & Dwight St\"}, \"coordinates\": {\"latitude\": 42.3449355147724, \"longitude\": -71.0705436362457}, \"photos\": [], \"hours\": [{\"open\": [{\"is_overnight\": false, \"start\": \"1600\", \"end\": \"2300\", \"day\": 0}, {\"is_overnight\": false, \"start\": \"1600\", \"end\": \"2300\", \"day\": 1}, {\"is_overnight\": false, \"start\": \"1600\", \"end\": \"2300\", \"day\": 2}, {\"is_overnight\": true, \"start\": \"1600\", \"end\": \"0100\", \"day\": 3}, {\"is_overnight\": true, \"start\": \"1400\", \"end\": \"0100\", \"day\": 4}, {\"is_overnight\": true, \"start\": \"1030\", \"end\": \"0100\", \"day\": 5}, {\"is_overnight\": false, \"start\": \"1030\", \"end\": \"2300\", \"day\": 6}], \"hours_type\": \"REGULAR\", \"is_open_now\": true}], \"transactions\": [\"delivery\"]}\r\n";
            var restaurant = JsonConvert.DeserializeObject<Restaurant>(jsonString);
            tBoxName.Text = restaurant.Name;
            tboxLocation.Text = $"{restaurant.Location.Address1}, {restaurant.Location.City}, {restaurant.Location.State}, {restaurant.Location.Zip_Code}";
        }

        public class Restaurant
        {
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Image_Url { get; set; }
            public Location Location { get; set; }
            public List<Category> Categories { get; set; }
            public double Rating { get; set; }
        }

        public class Location
        {
            public string Address1 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip_Code { get; set; }
        }

        public class Category
        {
            public string Title { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnCreateRes_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
                DateTime endDate = startDate.AddHours(1); 
                
                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = "1010263615545-1qe5busakh2ei19r8n51l9ak1586qrug.apps.googleusercontent.com",
                        ClientSecret = "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",
                    },
                    new[] { CalendarService.Scope.Calendar },
                    "user", CancellationToken.None);

                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Munch",
                });

                Event newEvent = new Event()
                {
                    Summary = tBoxName.Text, 
                    Location = tboxLocation.Text,
                    Start = new EventDateTime()
                    {
                        DateTime = startDate,
                        TimeZone = "America/New_York" 
                    },
                    End = new EventDateTime()
                    {
                        DateTime = endDate, 
                        TimeZone = "America/New_York" 
                    },
                   
                };
                EventsResource.InsertRequest request = service.Events.Insert(newEvent, "primary"); // "primary" calendar
                Event createdEvent = await request.ExecuteAsync();

                MessageBox.Show("Event Created: " + createdEvent.HtmlLink);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                     new ClientSecrets
                     {
                         ClientId = "1010263615545-1qe5busakh2ei19r8n51l9ak1586qrug.apps.googleusercontent.com",
                         ClientSecret = "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",
                     },
                     new[] { CalendarService.Scope.Calendar },
                     "user", CancellationToken.None);

                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Munch",
                });

                DateTime selectedDate = dateTimePicker1.Value.Date;
                DateTime startOfDay = selectedDate;
                DateTime endOfDay = selectedDate.AddHours(22); // 10 PM

                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = startOfDay;
                request.TimeMax = endOfDay;
                request.SingleEvents = true;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                Events events = await request.ExecuteAsync();

                TimeSpan slotDuration = TimeSpan.FromHours(1);
                DateTime? freeSlotStart = FindFreeSlot(startOfDay, events.Items, slotDuration, endOfDay);

                if (freeSlotStart.HasValue)
                {
                    dateTimePicker2.Value = freeSlotStart.Value;
                }
                else
                {
                    MessageBox.Show("No free slot available today.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private DateTime? FindFreeSlot(DateTime startOfDay, IList<Event> events, TimeSpan slotDuration, DateTime endOfDay)
        {
            DateTime currentStart = startOfDay > DateTime.Now ? startOfDay : DateTime.Now;

            foreach (var eventItem in events)
            {
                DateTime eventStart = DateTime.Parse(eventItem.Start.DateTimeRaw);
                DateTime eventEnd = DateTime.Parse(eventItem.End.DateTimeRaw);

                if (currentStart.Add(slotDuration) <= eventStart)
                {
                    if (currentStart.Add(slotDuration) <= endOfDay)
                    {
                        return currentStart;
                    }
                    break;
                }

                if (eventEnd > currentStart)
                {
                    currentStart = eventEnd;
                }
            }

            if (currentStart.Add(slotDuration) <= endOfDay)
            {
                return currentStart;
            }

            return null;
        }

    }
}
