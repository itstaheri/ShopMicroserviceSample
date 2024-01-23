using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddTestUsers(new List<Duende.IdentityServer.Test.TestUser> // تعریف کاربر تستی 
    {
        new Duende.IdentityServer.Test.TestUser
        {Username = "ali",
            Password="123",
            IsActive = true,
            SubjectId =Guid.NewGuid().ToString(),
        }
    })
    .AddInMemoryClients(new List<Client>()
    {
        new Client
        {
            ClientName="endpoint.Web",
            ClientId = "endpointweb",
            ClientSecrets = {new Secret("123".Sha256()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials, //شیوه ی انجام
            AllowedScopes = { "orderservice.fullaccess" } // کلاینت تعریف شده فقط میتواند به این اسکوپ  ها دسترسی داشته باشد
        },
        new Client
        {
          ClientName = "endpoint.login",
          ClientId="endpointlogin",
          ClientSecrets = {new Secret("123".Sha256()) },
          AllowedGrantTypes = GrantTypes.Code,
          RedirectUris={ "https://localhost:7127/signin-oidc" },
          PostLogoutRedirectUris ={ "https://localhost:7127/signout-callback-oidc" },
          AllowedScopes = { "openid","profile", "orderservice.fullaccess", "productservice.fullaccess"},
          AllowOfflineAccess = true,
          AccessTokenLifetime = Convert.ToInt32(TimeSpan.FromHours(2).TotalSeconds),
          RefreshTokenUsage = TokenUsage.ReUse, //هربار عوض شود
          RefreshTokenExpiration = TokenExpiration.Sliding //اگر قبل از تایم انقضا  توکن ساخته شد باز زمان خود را مجدد از سربگیرد و زمان انقضای توکن تمام نشود 

        }
    })  //تعریف کلاینت ها
    .AddInMemoryIdentityResources(new List<IdentityResource>()
    {
       new IdentityResources.OpenId(),
       new IdentityResources.Email(),
       new IdentityResources.Profile(),
    }) //ریسورس کاربران
    .AddInMemoryApiScopes(new List<ApiScope>() {
        new ApiScope("orderservice.fullaccess"), //تعریف محدوده دسترسی
        new ApiScope("productservice.fullaccess")
        }
    )
.AddInMemoryApiResources(new List<ApiResource>() { //ریسورس api ها
    new ApiResource("orderservice","order service api")
    {
        Scopes = { "orderservice.fullaccess" }
    },
    new ApiResource("productservice","product service api")
    {
        Scopes = {"productservice.fullaccess"}
    }
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
