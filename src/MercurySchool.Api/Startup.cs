namespace MercurySchool.Api;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var databaseOptions = new DatabaseOptions
        {
            DataSource = Configuration.GetValue<string>("DataSource"),
            InitialCatalog = Configuration.GetValue<string>("InitialCatalog"),
            UserID = Configuration.GetValue<string>("UserID"),
            Password = Configuration.GetValue<string>("Password"),
        };

        _ = services.Configure<DatabaseOptions>(options =>
          {
              options = databaseOptions;
          });

        _ = services.AddScoped<IPersonRepository, PersonRepository>();

        _ = services.AddControllers();
        _ = services.AddSwaggerGen(c =>
          {
              c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercurySchool.Api", Version = "v1" });
          });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MercurySchool.Api v1"));
        }

        _ = app.UseHttpsRedirection();
        _ = app.UseRouting();

        _ = app.UseAuthorization();

        _ = app.UseEndpoints(endpoints =>
          {
              endpoints.MapControllers();
          });
    }
}
