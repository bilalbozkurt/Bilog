# Bilog
Bilo's personal Bilog!


- .Net Core as backend (main directory)
- Angular as frontend (ClientApp)

# Usage
- Add your connection string and token to ``appsettings.json``
  - For current settings Bilog works with PostgreqSQL. If you want to use another database,  fork the project and then make required changes in ``program.cs``
    - ``builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("RemoteConnection"), options => options.EnableRetryOnFailure()));``
  - ``token`` is for user authentication but currently it is not handled on frontend.
  - token might be anything. just hide it properly.
- ``dotnet run``

# To Do
- I'll commit in English from now on. (Feb 20, 2022)

