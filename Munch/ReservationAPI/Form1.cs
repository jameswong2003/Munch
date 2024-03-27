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

        // load the data in from Yelp 
        private void btnCreateReservation_Click(object sender, EventArgs e)
        {
            // hardcode a test value - will change to input in future
            string jsonString = "{\"id\": \"mP1EdIafQKMuOm9O4PzAfA\", \"alias\": \"barcelona-wine-bar-south-end-boston-6\", \"name\": \"Barcelona Wine Bar South End\", \"image_url\": \"https://s3-media3.fl.yelpcdn.com/bphoto/5b1kPcvyjRezfN7gB7bJOg/o.jpg\", \"is_claimed\": true, \"is_closed\": false, \"url\": \"https://www.yelp.com/biz/barcelona-wine-bar-south-end-boston-6?adjust_creative=g71Xf3LSsqwyvP75W36_9g&utm_campaign=yelp_api_v3&utm_medium=api_v3_business_lookup&utm_source=g71Xf3LSsqwyvP75W36_9g\", \"phone\": \"+16172662600\", \"display_phone\": \"(617) 266-2600\", \"review_count\": 1467, \"categories\": [{\"alias\": \"spanish\", \"title\": \"Spanish\"}, {\"alias\": \"wine_bars\", \"title\": \"Wine Bars\"}, {\"alias\": \"tapasmallplates\", \"title\": \"Tapas/Small Plates\"}], \"rating\": 4.4, \"location\": {\"address1\": \"525 Tremont St\", \"address2\": null, \"address3\": \"\", \"city\": \"Boston\", \"zip_code\": \"02116\", \"country\": \"US\", \"state\": \"MA\", \"display_address\": [\"525 Tremont St\", \"Boston, MA 02116\"], \"cross_streets\": \"Milford St & Dwight St\"}, \"coordinates\": {\"latitude\": 42.3449355147724, \"longitude\": -71.0705436362457}, \"photos\": [], \"hours\": [{\"open\": [{\"is_overnight\": false, \"start\": \"1600\", \"end\": \"2300\", \"day\": 0}, {\"is_overnight\": false, \"start\": \"1600\", \"end\": \"2300\", \"day\": 1}, {\"is_overnight\": false, \"start\": \"1600\", \"end\": \"2300\", \"day\": 2}, {\"is_overnight\": true, \"start\": \"1600\", \"end\": \"0100\", \"day\": 3}, {\"is_overnight\": true, \"start\": \"1400\", \"end\": \"0100\", \"day\": 4}, {\"is_overnight\": true, \"start\": \"1030\", \"end\": \"0100\", \"day\": 5}, {\"is_overnight\": false, \"start\": \"1030\", \"end\": \"2300\", \"day\": 6}], \"hours_type\": \"REGULAR\", \"is_open_now\": true}], \"transactions\": [\"delivery\"]}\r\n";
            // use deseralize to get objects to use
            var restaurant = JsonConvert.DeserializeObject<Restaurant>(jsonString);
            // populate the name and location of the restaurant
            tBoxName.Text = restaurant.Name;
            tboxLocation.Text = $"{restaurant.Location.Address1}, {restaurant.Location.City}, {restaurant.Location.State}, {restaurant.Location.Zip_Code}";
        }

        // json mapping from yelp api return
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

        // create the reservation event
        private async void btnCreateRes_Click(object sender, EventArgs e)
        {
            try
            {
                // use the user's date and time inputed
                DateTime startDate = dateTimePicker1.Value.Date + dateTimePicker2.Value.TimeOfDay;
                DateTime endDate = startDate.AddHours(1); 
                
                // OAuth 2.0 id and secret
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

                // create the event with its name and time
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

        // find an available time
        private async void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                // credentials
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

                // use the time from now until 10 pm
                DateTime selectedDate = dateTimePicker1.Value.Date;
                DateTime startOfDay = selectedDate;
                DateTime endOfDay = selectedDate.AddHours(22); // 10 PM
                // Preparing a request to list events from the primary Google Calendar within a specified time range
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = startOfDay; 
                request.TimeMax = endOfDay; // Setting the maximum time for events to the end of the day (10 PM)
                request.SingleEvents = true; 
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime; 

                // executing the request asynchronously to retrieve events within the specified time range
                Events events = await request.ExecuteAsync();

                // defining the duration for a potential free slot as 1 hour
                TimeSpan slotDuration = TimeSpan.FromHours(1);
                // Calling the FindFreeSlot method to identify a time slot without any events
                DateTime? freeSlotStart = FindFreeSlot(startOfDay, events.Items, slotDuration, endOfDay);

                // If a free slot is found, updating the date picker to reflect the start time of the free slot
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

        // find a free slot for the meeting based on existing events
        private DateTime? FindFreeSlot(DateTime startOfDay, IList<Event> events, TimeSpan slotDuration, DateTime endOfDay)
        {
            // Determining the current start time, ensuring it's not in the past
            DateTime currentStart = startOfDay > DateTime.Now ? startOfDay : DateTime.Now;

            // Looping through all events to find a gap of the specified duration
            foreach (var eventItem in events)
            {
                DateTime eventStart = DateTime.Parse(eventItem.Start.DateTimeRaw);
                DateTime eventEnd = DateTime.Parse(eventItem.End.DateTimeRaw);

                if (currentStart.Add(slotDuration) <= eventStart)
                {
                    // if the slot ends before the day does, it's a valid free slot
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

            // returning the start time of the next free slot if it ends before the day does
            if (currentStart.Add(slotDuration) <= endOfDay)
            {
                return currentStart;
            }

            // returning null if no free slot is found
            return null;
        }


    }
}
