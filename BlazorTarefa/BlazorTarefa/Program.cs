using BlazorTarefa.Components;
using BlazorTarefas.Dados;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// HttpClient que será nescessario para o client comunicar com a API (configurado tambem no Client)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7110/")
});

// Serviços
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents()
    ;

builder.Services.AddControllers();

// Entity com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("Postgres"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 5))
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));


// MudBlazor, Framework de componentes graficos usados para essa aplicação
builder.Services.AddMudServices();


var app = builder.Build();

app.MapControllers();

// Aplica migrações automaticamente
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configuração HTTP
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorTarefa.Client._Imports).Assembly);

app.Run();
