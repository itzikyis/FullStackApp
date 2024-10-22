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
