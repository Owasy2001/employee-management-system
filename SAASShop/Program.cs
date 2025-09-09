using Hemaiya.Communications;
using Hemaiya.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using SAASShop.Communications;
using SAASShopDataAccess;
using SAASShopDataAccess.DbModel;
using SAASShopDomain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development"); // Development - Test - Uat - Production
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();


#region Pages And Controllers

builder.Services.AddRazorPages()
	.AddRazorPagesOptions(op =>
	{
		op.Conventions.AuthorizeFolder("/");
		op.Conventions.AllowAnonymousToPage("/Security/Login");
		op.Conventions.AllowAnonymousToPage("/Security/ChangePassword");
		op.Conventions.AllowAnonymousToPage("/Security/MFAOption");
		op.Conventions.AllowAnonymousToPage("/Security/MFAVerification");
		op.Conventions.AllowAnonymousToPage("/Security/LoginDetail");
		op.Conventions.AllowAnonymousToPage("/Security/ResetNewPassword");
	}
	)
	.AddMvcOptions(options =>
	{
		var policy = new AuthorizationPolicyBuilder()
			.RequireAuthenticatedUser()
			.Build();
		options.Filters.Add(new AuthorizeFilter(policy));
	});

builder.Services.AddControllers();

#endregion

#region Services

#region Auth
builder.Services.AddScoped<IComService, ComServiceManager>();
builder.Services.AddScoped<IAuthService, AuthServiceManager>();
#endregion

builder.Services.AddScoped<IAuthorizationHandler, PrivilegedResourceHandler>();
builder.Services.AddScoped<ISecurity, SecurityManager>();
builder.Services.AddScoped<IDbLogger, DbLogManager>();
builder.Services.AddScoped<ISAASShop, SAASShopManager>();
builder.Services.AddScoped<ISysConfig, SysConfigManager>();
builder.Services.AddScoped<IEmployee, EmployeeManager>();
builder.Services.AddScoped<IPayroll, PayrollManager>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddSingleton<IReportServiceConfiguration>(sp =>
//   new ReportServiceConfiguration
//   {
//       HostAppId = "BeratenTulalip",
//       Storage = new FileStorage(),
//       ReportSourceResolver = new TypeReportSourceResolver()
//   });

#endregion

#region Authentication


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.AccessDeniedPath = new PathString("/UnAuthorized");
	options.LoginPath = new PathString("/Security/Login");
	options.LogoutPath = new PathString("/Security/Login?Handler=LogOff");
	options.SlidingExpiration = true;
	options.ExpireTimeSpan = System.TimeSpan.FromHours(7);
});

#endregion

#region Authorization

builder.Services.AddAuthorization(op =>
{
	op.FallbackPolicy = new AuthorizationPolicyBuilder()
		.RequireAuthenticatedUser()
		.Build();

	foreach (int eVal in Enum.GetValues(typeof(PrivilegedResource)))
	{
		string? name = Enum.GetName(typeof(PrivilegedResource), eVal);
		op.AddPolicy(name, x => x.Requirements.Add(new PrivilegedResourceRequirement(eVal)));
	}
	op.AddPolicy("AllClear", op.DefaultPolicy);
});

#endregion

#region IIS-Session-Database Config

builder.Services.AddSession(s => { s.IdleTimeout = TimeSpan.FromHours(7); });

builder.Services.Configure<IISServerOptions>(op =>
{
	op.AllowSynchronousIO = true;
	op.MaxRequestBodySize = 62914560;
});

string? dbCon = builder.Configuration.GetValue<string>("DbConnections:Local");


builder.Services.AddDbContext<SAASShopModel>(
	op => op.UseSqlServer(dbCon, x => x.MigrationsAssembly("SAASShopDataAccess")
	.CommandTimeout(90).MaxBatchSize(20).UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
#endregion

#region Config Stuff

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
	OnPrepareResponse = ctx =>
	{
		const int durationInSeconds = 60 * 60 * 0;
		ctx.Context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.CacheControl] = "public,max-age=" + durationInSeconds;
	}
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapBlazorHub();

app.MapRazorPages();
app.MapDefaultControllerRoute();

#endregion

app.Run();
