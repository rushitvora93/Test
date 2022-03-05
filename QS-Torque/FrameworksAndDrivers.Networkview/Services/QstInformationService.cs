using System.Reflection;
using System.Threading.Tasks;
using BasicTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using String = BasicTypes.String;

namespace FrameworksAndDrivers.NetworkView.Services
{
    public class QstInformationService : global::QstInformationService.QstInformationService.QstInformationServiceBase
    {
        private readonly ILogger<QstInformationService> _logger;

        public QstInformationService(ILogger<QstInformationService> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = nameof(LoadServerVersion))]
        public override Task<String> LoadServerVersion(NoParams request, ServerCallContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return Task.FromResult(new String(){Value = version});
        }
    }
}
