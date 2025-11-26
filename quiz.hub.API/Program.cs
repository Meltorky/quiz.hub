
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using quiz.hub.Application.Interfaces.IServices.ICacheServices;
using quiz.hub.Application.Services.CacheServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// database connection service
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o=> 
{
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequireDigit = true;
    o.Password.RequiredLength = 6;
    o.Password.RequireLowercase = true;
    o.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<AppDbContext>();

// Add infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

// add JWT validation extention
builder.Services.AddJwtAuthentication();


builder.Services.AddControllers(options => 
{
    options.Filters.Add<ExecutionTimeFilter>(); // filter to log every request execution time  
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // configure swagger to use XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.EnableAnnotations();


    c.SwaggerDoc("v1", new() { Title = "after.senatone API", Version = "v1" });

    // Add JWT bearer auth to Swagger
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT token in the format: Bearer {your token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Bind JwtOptions once and validate it
builder.Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection("JWT"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// register DI of ActionFilter
builder.Services.AddScoped<ExecutionTimeFilter>();

// register caching services
builder.Services.AddScoped<ICandidateSessionService, CandidateSessionService>();

// register DI of services
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IIsAuthorized, IsAuthorized>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICandidateAnswerService, CandidateAnswerService>();

// Register my repos
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAnswerRepo, AnswerRepo>();
builder.Services.AddScoped<IQuizRepo, QuizRepo>();
builder.Services.AddScoped<IQuestionRepo, QuestionRepo>();
builder.Services.AddScoped<IQuizCandidatesRepo, QuizCandidatesRepo>();
builder.Services.AddScoped<ICommanRepo, CommanRepo>();
builder.Services.AddScoped<ICandidateAnswerRepo, CandidateAnswerRepo>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// use global exception handler middleware
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// seed Identity user and roles
await app.Services.SeedIdentityAsync();

app.Run();
