# Plugin.Maui.DataView

A .NET MAUI based plugin that makes it possible to interrogate a database.

## Usage

In order to use the DataView control within your application some initialisation is required. Note that this plugin currently provides support for the following databases:

- Sqlite-net
- LiteDB

> [!NOTE]
> While the above list is limited it is entirely possible to connect the view to other database types, you just need to do a bit more work, see [Adding support for another database](#adding-support-for-another-database)

### Setup

Inside your `MauiProgram.CreateMauiApp` you can provide the following based on the database that you are using:

#### Sqlite-net

You can use the `UseSqlite` extension method to register the package, you will need to provide a method of creating a connection to the database. Note this supports both synchronous and asynchronous models provided by Sqlite-net.

```csharp
public class Data
{
    internal static SQLiteConnection CreateConnection()
    {
        var path = FileSystem.AppDataDirectory;
        var connection = new SQLite.SQLiteConnection(Path.Combine(path, "data.db"));

        connection.CreateTable<Course>();
        connection.CreateTable<Student>();
        connection.CreateTable<Enrolment>();
        
        return connection;
    }
}

builder
    .UseMauiApp<App>()
    .UseSqlite(Data.CreateConnection)
```

#### LiteDB

You can use the `UseLiteDB` extension method to register the package, you will need to provide a method of creating a connection to the database.

```csharp

public class LiteData
{
    internal static LiteDatabase CreateDatabase()
    {
        var path = FileSystem.AppDataDirectory;
        var database = new LiteDatabase(Path.Combine(path, "litedata.db"));

        database.GetCollection<Course>();
        database.GetCollection<Student>();
        database.GetCollection<Enrolment>();
        
        return database;
    }
}

builder
    .UseMauiApp<App>()
    .UseLiteDB(LiteData.CreateDatabase)
```

Include the namespace

```xaml
xmlns:dataView="clr-namespace:Plugin.Maui.DataView;assembly=Plugin.Maui.DataView"
```

Include the control in your application

```xaml
<dataView:DataViewer />
```

## Adding support for another database

In order to use a currently unsupported database you can implement the `IDataProvider` interface and then simply assign it to `DataProvider.Instance` e.g.

```csharp
public class MySqlDataProvider : IDataProvider
{
    // Implementation
}

DataProvider.Instance = new MySqlDataProvider();

```