# CS-Applitools-Hackathon-CSharp


## Pre-requisites:
1. Install Visual Studios from [here](https://visualstudio.microsoft.com/downloads/), 
2. Install ChromeDriver from [here](https://chromedriver.chromium.org/downloads) if you are running on mac, you can also install it using the brew,
   simply run ```brew install chromedriver```
   -  Please check to make sure your google chrome browser is the same version as the chromedriver you install
   1)  Open chrome and navigate to chrome://settings/help
   2)  Check your version!
   ![YourChromeVersion](https://user-images.githubusercontent.com/21107409/96691179-3ecef880-138d-11eb-84a3-cd52106944c6.png)
   3)  Download the compatible chriomeDriver version:  [here](https://chromedriver.chromium.org/downloads)
    
3. Register to Applitools and [create an account](https://auth.applitools.com/users/register)  
4. Ensure you have your Applitools API Key 


   
## Hackathon Overview
Imagine you wrote tests for the [1st Version of the App (V1)](https://demo.applitools.com/hackathon.html)

Then Next [Version (V2)](https://demo.applitools.com/hackathonV2.html) came along later and has changes. (including bugs) 
Some of these bugs are functional bugs and some of are visual bugs. 

### The Challenge
Write an automated test for both versions which leverages Applitools.
Run both a traditional test (provided) and the new test you wrote.
Compare the results.

### Instructions

1) Review Traditional Script (provided) 
TraditionalTestSuite has been provided to you, as we assume you have already written these scripts before.
Analyze them, make sure they are ok, and feel free to add any additional coverage/lines of code you see fit.

2) Run the test suite against both Version 1 and Version 2.
You are going to find a lot of failures in Version 2. (changes have been made, including bugs)

3) Review the scripts again, and review how many assertions and locators required to cover all the elements in the page.

4) Open the VisualAISuite and set your ApiKey in string 'conf.SetApiKey("...")' (or comment the string and set APPLITOOLS_API_KEY environment variable).

5) Set your dedicated cloud URL if you have one, if not comment this line.

6) Modified the different tests to include visual assertion to achieve the same coverage as with the TraditionalSuite.

7) Run the same test again and see all the differences between the screenshots of the 1st version and the 2nd version of the app.

Note: When you run the tests against V2, you’ll see differences in screenshots because the app is actually different. 
You should see exactly what those differences are (highlighted in pink) in ~your~ Applitools dashboard. 

## Running the example:
 1. Download the example
    * Option 1: `git clone https://github.com/applitools/CS-Applitools-Hackathon-CSharp`
    * Option 2: Download it as a Zip file and extract it
    * in the [SetUp] region of the test files, please add the chromedriver path within the following command
          ``driver = new ChromeDriver('/path/to/chromedriver/')``
    
2. Run the project
### In order to run the project from theIDE perform next steps:

   2.1. Navigate to the recently cloned folder cs-applitools-java-hackathon.
   
   2.2. Open the folder through Visual Studios.
   
   2.3. Run or Debug class TraditionalSuite or method test(), in the TestSetup method, to run the AppV1 simply make sure that ``driver.Navigate().GoToUrl(OriginalAppURL);`` is uncommented. To run AppV2, please make sure that ``driver.Navigate().GoToUrl(NewAppURL);`` is uncommented.
   
   2.4. To Run the FullSolution, simply open VisualAITestSolution.cs, and to run the AppV1 simply make sure that ``driver.Navigate().GoToUrl(OriginalAppURL);`` is uncommented. To run AppV2, please make sure that ``driver.Navigate().GoToUrl(NewAppURL);`` is uncommented.
   
### 3.  The Mission
      3.1. Make a comparison between the traditional test and your Applitools test for both V1 and V2, regarding:
        3.1.1. Run time.
        3.1.2 Code length.
        3.1.3 Code complexity.
        3.1.4 Dedicated bugs.
        3.1.5 Undedicated bugs.
        3.1.6 Code scalability (predicted)