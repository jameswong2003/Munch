using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using client2;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetEnv;
using Google.Cloud.Firestore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var firebaseJson = JsonSerializer.Serialize(new FirebaseSettings());

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddScoped<UserService>();
builder.Services.AddLogging(configure => configure.AddConsole());
builder.Services.AddSingleton(_ => new FirestoreService(
    new FirestoreDbBuilder
    {
        ProjectId = "munch-77a3b",
        CredentialsPath = "firebase-auth.json"
    }.Build()
));
builder.Services.AddScoped<ScrapingService>();
DotNetEnv.Env.Load();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
