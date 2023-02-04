# AuctionSite
Auction site created with ASP.NET Core and Entity Framework on backend, and Angular on frontend

# Database migrations
1. Configure your connection string in `appsettings.json`
2. Open terminal in AuctionSite.Database project
3. Run `dotnet ef migrations --startup-project ../AuctionSite.Api add [migration name]`
4. Then run `dotnet ef database --startup-project ../AuctionSite.Api update`

# Payment
Application uses stripe for payments: https://stripe.com/ <br>
To be able to use payments you must add stripe api secret key to `appsettings.json` as `"StripeKey"`
