using DublinBikesWeb.Components;
using DublinBikesWeb.Services;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

Console.WriteLine(Directory.GetCurrentDirectory());
Console.WriteLine(File.Exists(".env"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient<IDublinBikeService, DublinBikeService>(); // Register DublinBikeService with HttpClient

builder.Services.AddMemoryCache(); //register the memory cash service
builder.Services.AddHostedService<TimedBackgroundCache>(); //register the backgroundservice

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
