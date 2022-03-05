using System.Text;
using System.Threading.Tasks;
using Common.UseCases.Interfaces;
using Core.UseCases;
using FrameworksAndDrivers.Authentication;
using FrameworksAndDrivers.DataAccess.DataAccess;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.PasswordUtilities;
using FrameworksAndDrivers.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.UseCases.UseCases;
using UnlockToolShared.DataAccess;
using UnlockToolShared.UseCases;

namespace gRPC.Service
{
    public class PermissionFromTokenRequirement : IAuthorizationRequirement
    {
        private readonly string _right;

        public PermissionFromTokenRequirement(string right)
        {
            _right = right;
        }
    }

    public class PermissionFromTokenRequirementHandler : AuthorizationHandler<PermissionFromTokenRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionFromTokenRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var key = "0a6d5b0e3cfcfa65e0395217c46bba8d5d75159d3d51ab9339421efe9b8c29e5";
            services.AddSingleton<IAuthorizationHandler, PermissionFromTokenRequirementHandler>();
            services.AddHttpContextAccessor();
            services.AddSingleton<SecurityKey>(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)));
            services.AddSingleton<ITokenGenerator, JWTBearerGenerator>();
            services.AddScoped<SqliteDbContext>((provider) => new SqliteDbContext(provider.GetService<IConfiguration>().GetSection("Database").GetSection("Sqlite").GetSection("FilePath").Value));
            services.AddScoped<ITransactionDbContext, EfTransactionDbContext>();
            services.AddScoped<ILoginDataAccess, LoginDataAccess>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddSingleton<IPasswordUtilities, PasswordUtilities>();

            services.AddSingleton<ITimeDataAccess, HumbleTimeDataAccess>();
            services.AddScoped<IGlobalHistoryDataAccess, GlobalHistoryDataAccess>();
            services.AddScoped<IManufacturerDataAccess, ManufacturerDataAccess>();
            services.AddScoped<IQstCommentDataAccess, QstCommentDataAccess>();
            services.AddSingleton<IUnlockResponseReadDataAccess, UnlockResponseReadDataAccess>();
            services.AddScoped<IWorkingCalendarData, WorkingCalendarDataAccess>();
            services.AddScoped<IShiftManagementData, ShiftManagementDataAccess>();
            services.AddScoped<IStatusDataAccess, StatusDataAccess>();
            services.AddScoped<IToleranceClassDataAccess, ToleranceClassDataAccess>();
            services.AddScoped<ITestLevelSetData, TestLevelSetDataAccess>();
            services.AddScoped<ITestLevelSetAssignmentData, TestLevelSetAssignmentDataAccess>();
            services.AddScoped<ILocationToolAssignmentData, LocationToolAssignmentDataAccess>();
            services.AddScoped<IClassicMfuTestData, ClassicMfuTestDataAccess>();
            services.AddScoped<IClassicChkTestData, ClassicChkTestDataAccess>();
            services.AddScoped<IClassicTestDataAccess, ClassicTestDataAccess>();
            services.AddScoped<IClassicProcessTestData, ClassicProcessTestDataAccess>();
            services.AddScoped<ISetupDataAccess, SetupDataAccess>();
            services.AddScoped<IHelperTableDataAccess, HelperTableDataAccess>();
            services.AddScoped<IToolUsageDataAccess, ToolUsageDataAccess>();
            services.AddScoped<IToolModelDataAccess, ToolModelDataAccess>();
            services.AddScoped<ILocationDataAccess, LocationDataAccess>();
            services.AddScoped<ILocationDirectoryDataAccess, LocationDirectoryDataAccess>();
            services.AddScoped<IPictureDataAccess, PictureDataAccess>();
            services.AddScoped<IToolDataAccess, ToolDataAccess>();
            services.AddScoped<ITransferToTestEquipmentDataAccess, TransferToTestEquipmentDataAccess>();
            services.AddScoped<ITestEquipmentDataAccess, TestEquipmentDataAccess>();
            services.AddScoped<ITestEquipmentModelDataAccess, TestEquipmentModelDataAccess>();
            services.AddScoped<IProcessControlDataAccess, ProcessControlDataAccess>();
            services.AddScoped<IProcessControlTechDataAccess, ProcessControlTechDataAccess>();
            services.AddScoped<IExtensionDataAccess, ExtensionDataAccess>();
            services.AddScoped<IHistoryData, HistoryDataAccess>();

            services.AddScoped<IManufacturerUseCase, ManufacturerUseCase>();
            services.AddScoped<IWorkingCalendarUseCase, WorkingCalendarUseCase>();
            services.AddScoped<IShiftManagementUseCase, ShiftManagementUseCase>();
            services.AddScoped<IStatusUseCase, StatusUseCase>();
            services.AddScoped<IToleranceClassUseCase, ToleranceClassUseCase>();
            services.AddSingleton<ILicenseUseCase, LicenseUseCase>();
            services.AddScoped<ITestLevelSetUseCase, TestLevelSetUseCase>();
            services.AddScoped<ITestLevelSetAssignmentUseCase, TestLevelSetAssignmentUseCase>();
            services.AddScoped<ITestDateCalculationUseCase, TestDateCalculationUseCase>();
            services.AddScoped<ILocationToolAssignmentUseCase, LocationToolAssignmentUseCase>();
            services.AddScoped<ISetupUseCase, SetupUseCase>();
            services.AddScoped<IHelperTableUseCase, HelperTableUseCase>();
            services.AddScoped<IToolUsageUseCase, ToolUsageUseCase>();
            services.AddScoped<IToolModelUseCase, ToolModelUseCase>();
            services.AddScoped<ILocationUseCase, LocationUseCase>();
            services.AddScoped<IToolUseCase, ToolUseCase>();
            services.AddScoped<IClassicTestUseCase, ClassicTestUseCase>();
            services.AddScoped<ITransferToTestEquipmentUseCase, TransferToTestEquipmentUseCase>();
            services.AddScoped<ITestEquipmentUseCase, TestEquipmentUseCase>();
            services.AddScoped<IProcessControlUseCase, ProcessControlUseCase>();
            services.AddScoped<IExtensionUseCase, ExtensionUseCase>();
            services.AddScoped<IHistoryUseCase, HistoryUseCase>();

            services.AddGrpc(options => {
                options.EnableDetailedErrors = true;
                options.MaxReceiveMessageSize = int.MaxValue;
                options.MaxSendMessageSize = null;
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
                };
            });

            

            services.AddAuthorization(opt =>
            {
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.StatusService.LoadStatus));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.StatusService.GetStatusToolLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.StatusService.InsertStatusWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.StatusService.UpdateStatusWithHistory));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ManufacturerService.LoadManufacturers));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ManufacturerService.GetManufacturerComment));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ManufacturerService.GetManufacturerModelLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ManufacturerService.InsertManufacturerWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ManufacturerService.UpdateManufacturerWithHistory));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToleranceClassService.LoadToleranceClasses));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToleranceClassService.GetToleranceClassLocationLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToleranceClassService.InsertToleranceClassesWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToleranceClassService.UpdateToleranceClassesWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToleranceClassService.GetToleranceClassLocationToolAssignmentLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToleranceClassService.GetToleranceClassesFromHistoryForIds));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ShiftManagementService.GetShiftManagement));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ShiftManagementService.SaveShiftManagement));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.WorkingCalendarService.GetWorkingCalendar));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.WorkingCalendarService.DeleteWorkingCalendarEntry));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.WorkingCalendarService.GetWorkingCalendarEntriesForWorkingCalendarId));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.WorkingCalendarService.InsertWorkingCalendarEntry));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.WorkingCalendarService.SaveWorkingCalendar));


                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetService.LoadTestLevelSets));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetService.InsertTestLevelSet));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetService.DeleteTestLevelSet));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetService.UpdateTestLevelSet));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetService.IsTestLevelSetNameUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetService.DoesTestLevelSetHaveReferences));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetAssignmentService.RemoveTestLevelSetAssignmentFor));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetAssignmentService.AssignTestLevelSetToLocationToolAssignments));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetAssignmentService.AssignTestLevelSetToProcessControlConditions));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestLevelSetAssignmentService.RemoveTestLevelSetAssignmentForProcessControl));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService.CalculateToolTestDateFor));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService.CalculateToolTestDateForAllLocationToolAssignments));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService.CalculateToolTestDateForTestLevelSet));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService.CalculateProcessControlDateFor));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService.CalculateProcessControlDateForAllProcessControlConditions));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService.CalculateProcessControlDateForTestLevelSet));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.LoadLocationToolAssignments));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.LoadLocationReferenceLinksForTool));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.LoadUnusedToolUsagesForLocation));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.GetLocationToolAssignmentsByIds));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.GetLocationToolAssignmentsByLocationId));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.AddTestConditions));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.UpdateLocationToolAssignmentsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService.InsertLocationToolAssignmentsWithHistory));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.SetupService.GetColumnWidthsForGrid));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.SetupService.GetQstSetupsByUserIdAndName));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.SetupService.InsertOrUpdateQstSetups));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HelperTableService.GetAllHelperTableEntities));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HelperTableService.GetHelperTableEntityModelLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HelperTableService.GetHelperTableEntityToolLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HelperTableService.InsertHelperTableEntityWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HelperTableService.UpdateHelperTableEntityWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HelperTableService.GetHelperTableByNodeId));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolUsageService.GetAllToolUsages));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolUsageService.UpdateToolUsagesWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolUsageService.GetToolUsageLocationToolAssignmentReferences));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolUsageService.InsertToolUsagesWithHistory));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolModelService.GetAllToolModels));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolModelService.GetAllDeletedToolModels));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolModelService.InsertToolModel));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolModelService.UpdateToolModel));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolModelService.GetReferencedToolLinks));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.LoadLocationDirectories));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.LoadDeletedLocationDirectories));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.InsertLocationDirectoriesWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.UpdateLocationDirectoriesWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.LoadLocations));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.LoadDeletedLocations));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.LoadLocationsByIds));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.InsertLocationsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.UpdateLocationsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.IsUserIdUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.GetReferencedLocPowIdsForLocationId));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.GetLocationComment));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.LocationService.LoadPictureForLocation));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.LoadTools));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.GetToolById));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.InsertToolsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.UpdateToolsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.GetLocationToolAssignmentLinkForTool));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.IsInventoryNumberUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.IsSerialNumberUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.GetToolComment));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.LoadPictureForTool));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.LoadToolsForModel));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.LoadModelsWithAtLeasOneTool));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ToolService.LoadDeletedModelsWithAtLeasOneTool));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetClassicChkHeaderFromTool));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetClassicMfuHeaderFromTool));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetToolsFromLocationTests));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetValuesFromClassicChkHeader));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetValuesFromClassicMfuHeader));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetClassicProcessHeaderFromLocation));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ClassicTestService.GetValuesFromClassicProcessHeader));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TransferToTestEquipmentService.LoadLocationToolAssignmentsForTransfer));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TransferToTestEquipmentService.InsertClassicChkTests));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TransferToTestEquipmentService.InsertClassicMfuTests));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TransferToTestEquipmentService.LoadProcessControlDataForTransfer));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.QstInformationService.LoadServerVersion));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.GetTestEquipmentsByIds));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.InsertTestEquipmentsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.UpdateTestEquipmentsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.UpdateTestEquipmentModelsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.LoadTestEquipmentModels));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.IsTestEquipmentInventoryNumberUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.IsTestEquipmentModelNameUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.IsTestEquipmentSerialNumberUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.TestEquipmentService.LoadAvailableTestEquipmentTypes));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ProcessControlService.LoadProcessControlConditionForLocation));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ProcessControlService.InsertProcessControlConditionsWithHistory));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ProcessControlService.UpdateProcessControlConditionsWithHistory));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ExtensionService.LoadExtensions));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ExtensionService.LoadDeletedExtensions));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ExtensionService.GetExtensionLocationLinks));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ExtensionService.IsExtensionInventoryNumberUnique));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ExtensionService.InsertExtensions));
                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.ExtensionService.UpdateExtensions));

                AddMethodPolicy(opt, nameof(FrameworksAndDrivers.NetworkView.Services.HistoryService.LoadLocationDiffsFor));
            });
        }

        private void AddMethodPolicy(AuthorizationOptions option, string methodName)
        {
            option.AddPolicy(methodName, policy => policy.Requirements.Add(new PermissionFromTokenRequirement(methodName)));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, ILicenseUseCase licenseUseCase)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.AuthenticationService>();
                if (licenseUseCase.IsLicenseFileLoaded())
                {
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ManufacturerService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.WorkingCalendarService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ShiftManagementService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.StatusService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ToleranceClassService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.TestLevelSetService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.TestLevelSetAssignmentService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.TestDateCalculationService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.LocationToolAssignmentService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.SetupService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.HelperTableService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ToolUsageService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ToolModelService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.LocationService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ToolService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ClassicTestService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.TransferToTestEquipmentService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.QstInformationService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.TestEquipmentService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ProcessControlService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.ExtensionService>();
                    endpoints.MapGrpcService<FrameworksAndDrivers.NetworkView.Services.HistoryService>();
                }
                else
                {
                    logger.LogWarning("license Problem, Server will start in disabled mode");
                }

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
