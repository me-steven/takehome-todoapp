To Do Application
=====

Utilizes .NET for the backend and a Vue.js frontend.

Within Visual Studio I utilized the ASP.NET and Vue.js base project and followed various resources such as:


>[The Microsoft docs on how to use ASP.NET and Vue.js](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-vue?view=vs-2022)
>
>[Or this useful series on YouTube](https://youtube.com/playlist?list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&si=5CTZOZp4_kvo4voy)
>
>

Functionality:
------
After running the instance you'll be given an interface that opens in a browser. When opened you'll have a table that contains a list of tasks, their descriptions, the ability to update it's completion status, and delete the task.

Above the table is a search bar that allows the user to filter by names and below are inputs to submit a new task with name and description.

If everything is configured correctly the table will reference a database and fill with tasks from that database table.

**!! If it's not configured correctly it will populate with generic tasks !!**

Requirements
-----
> .NET runtime and SDK
> 
> Visual Studio
> 
> Node.js
>
> Axios(node library)
>
> SQL Server Management Software[SSMS] (optional but is used as reference below)
> 

Configuration
-----
There are a few necessary steps to get everything working.

The first one is that your appsettings.json file in the ToDoApp.Server directory isn't configured properly. I've intentionally left the "DefaultConnection" key commented out so that it throws an error(Because JSON doesn't support comments).

For me, using SSMS, my used something like:
```
"DefaultConnection": "Server=[[SERVER-NAME]];Initial Catalog=ToDoTasks;Integrated Security = True;Connect Timeout = 30; Encrypt = False;TrustServerCertificate = False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
```

Except that '[[SERVER-NAME]]' was replaced with my actual desktop name and server type for the connection to be made. Very similar to:

```
"DefaultConnection": "Server=DESKTOP-123ABC\\SQLEXPRESS;Initial Catalog=ToDoTasks;Integrated Security = True;Connect Timeout = 30; Encrypt = False;TrustServerCertificate = False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
```

If you're running a similar setup you'll just need to create a new database within SSMS and get your 'Server Name'

Assuming that you already have SSMS running and all prerequisites are met all you have to do is open the directory ToDoApp.server in a Terminal instance(Powershell and CMD are fine) and run the following commands:

```
dotnet ef migrations add init

dotnet ef database update
```

If you are having issues with Entity Framework you can try the command below or try to follow this [link](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install) for more comprehensive troubleshooting
```
dotnet tool install --global dotnet-ef
```

Then hop over to Visual Studio and run your project and you should see something like this(except you wont have any tasks yet):

![image](https://github.com/user-attachments/assets/74a8ab97-dfe2-43c6-a3d9-de29fa99d9d3)

If you have the webpage run but see it populate with some generic tasks, like this:

![image](https://github.com/user-attachments/assets/796c9100-3b4a-4e70-9f82-8140f1cc5deb)

Then something went wrong!

It could just be that you had a strange occurence that caused a bad request. To be sure I would just refresh the page. 

If that doesn't resolve it the most likely cause of this is going to be that 

1. Your SQL Server isn't running
2. your appsettings.json file in ToDoApp.Server directory is misconfigured
3. Or for some reason Axios isn't working and you'll need to run 
```
npm install axios
```
in your todoapp.client directory


How Does It Work?
-----

In short? 

It performs a GET request on your server, returns all the tasks stored on it and serves it to you alongside some actions you can perform.


In depth?

It performs a GET request through Axios to your server and as long as the request returns OK it'll replace the default tasks with whatever is hosted on your server.

After serving your tasks you'll have the option to update the completion status of tasks between 'In Progress' and 'Completed'. It does this by performing a PUT request to the server and will update the array by replacing it with whatever is returned(i.e. the task that is now on the server). This is mostly done to prevent any changes from occuring on the frontend without any changes happening on the backend.

Another option you have is to 'Delete' any of the tasks. This is just a 'Delete' request sent to the server with the Tasks UUID. If the server returns 'No Content'(which is a success for Delete requests) it'll update the frontend.

You will also have the option to add new tasks as long as you fill out the 'Task Name' and 'Task Description' fields. Upon hitting 'Add Task' you'll perform a POST request and if the server successfully adds it to your database the frontend will update with your new task.

The only other option you have as a user is to search through the tasks on the table. This is a mostly straightforward filter applied to the tasks that are in the tasks array.

Anatomy of a Task
-----
A task is a simple object.

```
task = {
  uuid: string,
  epochTime: number,
  name: string,
  description: string,
  isActive: boolean,
}
```

Most things should be fairly self-explanatory but I will cover the UUID and EpochTime.

UUID stands for Universal Unique IDentifier. It is a standardized format that all but guarantees that something will have a totally unique id for whatever you are working on. We will only start seeing duplicate keys in about 2 decades and unless you are using 2^122 entries in your database, none of them should occur in your projects. So for unique tasks I decided that I would go for UUID. The standard is to use a 'SERIAL' id which is really just an integer that increments for every entry added to a database. SERIAL would be sufficient for this use case but I wasn't very satisfied with it and wanted to implement UUID.

EpochTime is simply the milliseconds since time began at Midnight, January 1st, 1970. At least that's when computers think that's when time started. I added it in case it would be helpful for any future debugging and in case I wanted to add a composite key to my database but in the project's final form it's mostly vestigial and serves as a way to look up the creation date of the task.
