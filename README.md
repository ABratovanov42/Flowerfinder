# Flowerfinder

A flower catalog and identification app built with ASP.NET Core 8 MVC and SQLite.

- **Catalog** — browse, search and filter flowers (color, light, season, watering, perennial/annual), with pagination.
- **Detail pages** — story, care guide, growing timeline, care calendar, troubleshooting, FAQs and gear list per flower.
- **Identify** — drop in a photo and get species guesses via the [PlantNet API](https://my.plantnet.org), matched against the catalog.

## Running on a new PC

1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Clone and run:

   ```
   git clone <your-repo-url>
   cd Flowerfinder
   dotnet run --project Flowerfinder
   ```

That's it — the SQLite database (`flowerapp.db`) is created, migrated and seeded with the starter catalog automatically on first run.

## PlantNet API key (photo identification)

The committed config ships with `"ApiKey": "demo"`, which returns a canned result so the Identify page works out of the box. For real identification:

1. Get a free key at [my.plantnet.org](https://my.plantnet.org) (Settings → API).
2. Create `Flowerfinder/appsettings.Local.json`:

   ```json
   {
     "PlantNet": {
       "ApiKey": "your-real-key-here"
     }
   }
   ```

This file is in `.gitignore`, so your key never ends up on GitHub.
