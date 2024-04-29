# Munch 

### Created by: Sean McCarty, Jeremy Bui, James Wong, Martin Brnak

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