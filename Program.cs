using myfinance_web_netcore.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

CriaInstanciaDAL(app);

app.Run();


void CriaInstanciaDAL(WebApplication app)
{
    IConfiguration configuration = app.Configuration;
    DAL.Configuration = configuration;
    var objDAL = DAL.GetInstancia;
    objDAL.Conectar();
    //var teste = objDAL.RetornarDataTable("select * from plano_contas");
    //objDAL.ExecutarComandoSql("INSERT INTO PLANO_CONTAS(DESCRICAO, TIPO) VALUES('LUZ','D')");
}