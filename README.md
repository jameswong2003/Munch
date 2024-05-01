# Munch 

### Created by: Sean McCarty, Jeremy Bui, James Wong, Martin Brnak

## Technologies:

* [Playwright](https://playwright.dev/dotnet/) and [HTMLAgilityPack](https://html-agility-pack.net/) for webscraping
* [Firebase](https://firebase.google.com/) OAuth and FirestoreDB
* [GoogleCalendarAPI](https://developers.google.com/calendar/api/guides/overview)
    * NOTE: When you first use the app and try to use your google to log in with google calendar, it will give you a warning. You can ignore this and press "advanced" and sign in anyways
* [YelpAPI](https://docs.developer.yelp.com/docs/fusion-intro) for restaurant data
* [MailKit](https://github.com/jstedfast/MailKit) for email server connection
* [Geocoding.net](https://github.com/chadly/Geocoding.net) and [BingMaps](https://www.bingmapsportal.com/) for geocoding and reverse geocoding



## Installation:

The submission should have everything needed, with external dependencies already installed in the zip file. However, if the web scraping isn't working, the grader should try the following:
* Download powershell (pwsh)
    * Windows computers should already have this, if the grader is on mac, try installing with brew
    * `brew install pwsh`
* Build the project
    * I used the dotnet cli on the command line, navigate to the project directory and `dotnet build`
* Run the playwright powershell file
    * Once the project is built, there should be a script file tucked away in the bin directory
    * With powershell installed, run `pwsh bin/Debug/net7.0/playwright.ps1 install`
    * If the following isn't working, this [link](https://playwright.dev/dotnet/docs/intro) should help
    
Once everything is installed, run `dotnet run` on the command line and open up localhost:5045 on your browser


### Home Page
![Home Page](https://github.com/jameswong2003/Munch/blob/main/screenshots/home_ss.png?raw=true)

### Swipe Interface
![Swipe Interface](https://github.com/jameswong2003/Munch/blob/main/screenshots/swipe_ss.png?raw=true)

### Add to Calendar
![Add to Calendar](https://github.com/jameswong2003/Munch/blob/main/screenshots/add_calendar_ss.png?raw=true)

### Calendar View
![Calendar View](https://github.com/jameswong2003/Munch/blob/main/screenshots/calendar_ss.png?raw=true)

### Configuration
![Configuration](https://github.com/jameswong2003/Munch/blob/main/screenshots/configure_ss.png?raw=true)

### Login Page
![Login Page](https://github.com/jameswong2003/Munch/blob/main/screenshots/login_ss.png?raw=true)

