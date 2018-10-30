# CodeCamp2015.SignalR.Demo
Source code for live demo during Code Camp NYC 2015 session "Modernizing Your .NET Skills." Demonstrates using SignalR to notify connected clients of data changes in real time. Uses GlobalHost.ConnectionManager.GetHubContext to get the SignalR hub reference in MVC or WebAPI controllers to broadcast data change events.

Note this project is for historical reference purposes. SignalR has undergone a significant rewrite since this demo was created. This project will not be updated to reflect new versions and remains for reference.


**To Use:**

Clone the repository, build and run.

Open 2 Browser Windows Side by Side

 >Window 1: Navigate to the Events List
 
 >Window 2: Create a New Event

Observe the Events List Get Updated when New Event Created
