using System.Threading.Tasks;
using AuthenticationService;
using BasicTypes;
using DtoTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.NetworkView.Services
{
    [AllowAnonymous]
    public class AuthenticationService : global::AuthenticationService.Authentication.AuthenticationBase
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ILoginUseCase _loginUseCase;

        public AuthenticationService(ILogger<AuthenticationService> logger, ILoginUseCase loginUseCase)
        {
            _logger = logger;
            _loginUseCase = loginUseCase;
        }

        public override Task<AuthenticationResponse> Login(AuthenticationRequest request, ServerCallContext context)
        {
            var authResponse = new AuthenticationResponse
            {
                Token = new Token
                {
                    Base64 = _loginUseCase.GetBase64Token(
                        request.Username, 
                        request.Password, 
                        request.GroupId,
                        request.Pcfqdn)
                }
            };

            if (string.IsNullOrWhiteSpace(authResponse.Token.Base64))
            {
                _logger?.LogInformation($"Login for user '{request.Username}' failed: Invalid credentials");
            }
            else
            {
                _logger?.LogInformation($"Login for user '{request.Username}' succeeded.");
            }

            return Task.FromResult(authResponse);
        }

        public override Task<PingResponse> Ping(NoParams request, ServerCallContext context)
        {
            _logger?.LogTrace("Ping received from client " + context.Peer);

            return Task.FromResult(new PingResponse
            {
                Value = true
            });
        }

        public override Task<ListOfGroups> GetQstGroupByUserName(QstGroupByUserNameRequest request, ServerCallContext context)
        {
            var groups = _loginUseCase.GetQstGroupByUserName(request.Username);

            var listOfGroups = new ListOfGroups();
            groups.ForEach(g => listOfGroups.Groups.Add(GetGroupDtoFromGroup(g)));

            return Task.FromResult(listOfGroups);
        }

        private Group GetGroupDtoFromGroup(Server.Core.Entities.Group group)
        {
            return new Group()
            {
                GroupId = group.Id.ToLong(), 
                GroupName = group.GroupName
            };
        }
    }
}
