using Asp.Versioning;
using FlashCard.Abstract.Extensions;
using FlashCard.Abstract.UserContext;
using FlashCard.API.Models.Options;
using FlashCard.API.Models.Validations;
using FlashCard.API.Samples;
using FlashCard.API.Services.Abstractions;
using FlashCard.API.Services;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net.Mime;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FlashCard.API.Middlewares;

namespace FlashCard.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddFLCOptions<IdentityClientOptions>();
            services.AddFLCOptions<ApiSwaggerOptions>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext>(sp =>
            {
                HttpContext? context = sp.GetService<IHttpContextAccessor>()?.HttpContext;
                ClaimsPrincipal? User = context?.User;

                bool validRegistration = int.TryParse(User?.FindFirstValue(ApiConstants.RegistrationIdKey), out int registrationId);
                string identityValue = validRegistration ? registrationId.ToString() :
                    User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? ApiConstants.AdminAlias;

                bool validDOB = DateTime.TryParse
                    (
                        User?.FindFirstValue(ClaimTypes.DateOfBirth),
                        out DateTime dateOrBirth
                    );

                StringValues token = new(Enumerable.Empty<string>().ToArray());
                bool hasToken = context?.Request.Headers.TryGetValue("Authorization", out token) ?? false;

                return new UserContext
                (
                    identityValue,
                    validDOB ? dateOrBirth : null,
                    validRegistration ? registrationId : null,
                    token.FirstOrDefault()
                );
            });
            services
             .AddControllers()
             .ConfigureApiBehaviorOptions(options =>
             {
                 options.InvalidModelStateResponseFactory = context =>
                 {
                     ValidationFailedResult result = new(context.ModelState);

                     result.ContentTypes.Add(MediaTypeNames.Application.Json);
                     result.StatusCode = StatusCodes.Status400BadRequest;

                     return result;
                 };
             });

            ApiSwaggerOptions apiSwaggerOptions = configuration.GetOptions<ApiSwaggerOptions>()
                ?? throw new NullReferenceException(nameof(apiSwaggerOptions));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(apiSwaggerOptions.Version, new OpenApiInfo
                {
                    Version = apiSwaggerOptions.Version,
                    Title = apiSwaggerOptions.Title,
                    Description = apiSwaggerOptions.Description,
                    TermsOfService = apiSwaggerOptions.TermsOfService,
                    Contact = apiSwaggerOptions.Contact,
                    License = apiSwaggerOptions.License
                });


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br/> 
                      Enter 'Bearer' [space] and then your token in the text input below.<br/>
                      <br/><b>Example:</b> 'Bearer 12345abcdef' <br/><br/>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                options.ExampleFilters();
            });
            services.AddHttpClient<IIdentityClient, IdentityClient>();
            services.AddApiVersioning(opt =>
            {
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.ReportApiVersions = true;
            });
            services.AddSwaggerExamplesFromAssemblyOf<RegisterSampleResponse>();
            return services
                 .AddMemoryCache()
                .AddTransient<IRegisterService, RegisterService>()
                .AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>(); ;
        }
        public static void Configure(this WebApplication app)
        {

            app.ConfigureExceptionHandler(app.Logger);


            ApiSwaggerOptions? apiSwaggerOptions = app.Configuration.GetOptions<ApiSwaggerOptions>();
            if (apiSwaggerOptions is null)
            {
                throw new NullReferenceException(nameof(apiSwaggerOptions));
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(apiSwaggerOptions.Endpoint, apiSwaggerOptions.Version);
                options.RoutePrefix = string.Empty;
                options.DisplayRequestDuration();
            });

            app.UseAuthentication();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
        public static T? GetOptions<T>(this IConfiguration configuration) where T : class
        {
            return configuration.GetSection(typeof(T).Name).Get<T>();
        }
    }
}
