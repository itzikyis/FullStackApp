﻿Building full-stack applications is a powerful way to leverage the strengths of different technologies to create a seamless and efficient development process. In this tutorial, we will walk through the steps to set up a full-stack application using .NET on the backend and React on the front-end. By the end of this article, you’ll have a basic application running with both technologies integrated.

Prerequisites
Ensure you have the following installed on your machine:

.NET SDK
Node.js
Visual Studio Code (or any other code editor)
Git (optional, but recommended)

Step 1: Setting Up the Backend with .NET 8
Create a New .NET Project
Open a terminal and run the following command to create a new .NET web API project:
dotnet new webapi -n FullStackApp
cd FullStackApp
2. Create a Custom API Endpoint
Create a simple API endpoint that returns a list of items. Add a new controller file named ItemsController.cs in the Controllers folder:

using Microsoft.AspNetCore.Mvc;

namespace FullStackApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Item1", "Item2", "Item3" };
        }
    }
}
3. Run the Project
Navigate to the project directory and run the project to ensure everything is set up correctly:

dotnet run
The API should be available at https://localhost:5000/api/items.

Step 2: Setting Up the Frontend with React and Vite
Create a New React Project with Vite
Open a new terminal window and run the following commands to create a new React project using Vite:
npm create vite@latest client --template react
cd client
2. Run the React App
Navigate to the client directory and start the React development server:

npm install
npm run dev
Your default React app should now be running on http://localhost:5173.

Step 3: Integrating .NET and React
Configure the .NET API to Serve the React App
Open the Program.cs file and modify it as follows:
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "client/dist";
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseRouting();

app.UseAuthorization();

// Use CORS
app.UseCors();

// Map API Controllers
app.MapControllers();

// Use SPA only for non-API calls
app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"), spa =>
{
    spa.UseSpa(spaBuilder =>
    {
        spaBuilder.Options.SourcePath = "client";

        if (app.Environment.IsDevelopment())
        {
            spaBuilder.UseProxyToSpaDevelopmentServer("http://localhost:5173");
        }
    });
});

app.Run();
2. Build the React App
In the client directory, build the React app:

npm run build
This will create a dist folder containing the static files for your React app.

3. Test the Integration
Navigate back to the .NET project root directory and run the project:

dotnet run
Now, when you navigate to https://localhost:5173, you should see your React app being served. The API endpoints will still be available under the /api route.

Step 4: Fetching Data from the .NET API in React
Create an API Service in React
In the client/src directory, create a new folder named services and add a file named apiService.js:
const API_URL = "https://localhost:5000/api/items";

export async function fetchItems() {
    const response = await fetch(API_URL);
    if (!response.ok) {
        throw new Error("Failed to fetch items");
    }
    return await response.json();
}
2. Use the API Service in a React Component
Open the client/src/App.jsx file and modify it to fetch and display data from the API:

import React, { useEffect, useState } from 'react';
import { fetchItems } from './services/apiService';

function App() {
    const [items, setItems] = useState([]);

    useEffect(() => {
        async function getItems() {
            try {
                const data = await fetchItems();
                setItems(data);
            } catch (error) {
                console.error("Error fetching items:", error);
            }
        }

        getItems();
    }, []);

    return (
        <div className="App">
            <h1>Items List</h1>
            <ul>
                {items.map((item, index) => (
                    <li key={index}>{item}</li>
                ))}
            </ul>
        </div>
    );
}

export default App;
Conclusion
You’ve successfully created a basic full-stack application using .NET 8 on the backend and React (with Vite) on the front-end. This setup allows you to leverage the strengths of both technologies to build powerful and efficient applications. From here, you can expand on this foundation by adding more features, improving the UI, and implementing advanced backend logic. Happy coding!
