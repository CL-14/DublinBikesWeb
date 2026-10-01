# Dublin Bikes Web

A Blazor Web App (.NET 8) that connects to the JCDecaux Dublin Bikes API and
shows every bike station in Dublin with live availability.

## Features

- Fetches live station data from the JCDecaux API (`/vls/v3/stations`)
- Station list page showing name, number, status, position, bikes available,
  free stands and last update time
- Live search: the list filters as you type

Not yet implemented:

- Station detail page (master/detail navigation)
- Interactive map

## Technologies

- C# / .NET 8 SDK
- Blazor Web App, interactive server render mode
- `HttpClient` with `System.Net.Http.Json` for API calls and JSON deserialization
- `DotNetEnv` for loading the API key from `.env`
- Bootstrap (bundled with the template) for layout

## Prerequisites

1. The [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. A free JCDecaux API key. Register at <https://developer.jcdecaux.com> and
   copy the key from your account.

## Setup

1. Clone the repository and enter the project folder:

   ```
   git clone paste_url
   cd DublinBikesWeb
   ```

2. Create your `.env` file from the example and add your key.

   Linux / macOS:

   ```
   cp .env.example .env
   ```

   Windows (PowerShell):

   ```
   copy .env.example .env
   ```

   Then open `.env` and replace the placeholder so it reads:

   ```
   API_KEY=this_is_where_your_real_key_goes_replace_me
   ```

   No spaces around `=`, no quotes.

## Build and run

Run from the project root (the folder containing `DublinBikesWeb.csproj`),
because `.env` is read from the current working directory:

```
dotnet run
```

Open the URL printed in the terminal (for example `http://localhost:5237`),
then go to `/StationInformation`. I'm too lazy to add a nav menu for now.

For live reload, use `dotnet watch` instead.

## Troubleshooting

- **"API_KEY environment variable is not set"**: `.env` is missing, misnamed,
  or you ran the app from a different folder. Check the file is called exactly
  `.env` and sits next to the `.csproj`.
- **"Error loading stations" on the page**: the real error is printed in the
  terminal where the app is running. A 401/403 means a wrong or inactive key.

## Data source and references

JCDecaux (n.d.) *JCDecaux Developer: Self-service bike API*. Available at:
<https://developer.jcdecaux.com>
