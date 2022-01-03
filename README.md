# TechMarket
```
/src
* ApplicationCore
* Infrastructure
* Web

/tests
* UnitTests
```

### Packages
```
/ApplicationCore
Install-Package Ardalis.Specification


/Infrastructure
Install-Package Microsoft.EntityFrameworkCore -v 5.0.13
Install-Package Ardalis.Specification.EntityFrameworkCore
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -v 5.0.10
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 5.0.13

/Web
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL -v 5.0.10
Install-Package Microsoft.EntityFrameworkCore.Tools -v 5.0.13
```

### Migrations
```
/Infrastructure
Add-Migration InitialCreate -c MarketContext -s Web -o Data/Migrations
Update-Database -Context MarketContext -s Web
Add-Migration InitialIdentity -c AppIdentityDbContext -s Web -o Identity/Migrations
Update-Database -Context AppIdentityDbContext -s Web
```

### Resources
* https://github.com/dotnet-architecture/eShopOnWeb
* https://github.com/yigith/WatchShop
* https://www.postgresql.org
* https://www.npgsql.org/doc/types/basic.html
* https://www.npgsql.org/efcore/
* https://getbootstrap.com/docs/4.6/components/card/#example
* https://stackoverflow.com/questions/5269713/css-ellipsis-on-second-line
* https://getbootstrap.com/docs/4.6/components/pagination/#alignment
* https://gist.github.com/yigith/c6f999788b833dc3d22ac6332a053dd1
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-5.0
* https://getbootstrap.com/docs/4.3/examples/checkout/
* https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-5.0#walkthrough-creating-a-simple-view-component
* https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
* https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-5.0#action-filters