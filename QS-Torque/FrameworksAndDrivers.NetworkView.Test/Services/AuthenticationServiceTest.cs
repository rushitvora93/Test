using System;
using System.Collections.Generic;
using AuthenticationService;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;
using TestHelper.Checker;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    public class LoginUseCaseMock : ILoginUseCase
    {
        public string GetBase64Token(string userName, string password, long groupId, string pcfqdn)
        {
            GetBase64TokenUserParam = userName;
            GetBase64TokenPasswordParam = password;
            GetBase64TokenGroupParam = groupId;
            GetBase64TokenPcfqdnParam = pcfqdn;
            return GetBase64TokenReturnValue;
        }

        public List<Group> GetQstGroupByUserName(string userName)
        {
            GetQstGroupByUserNameParameter = userName;
            return GetQstGroupByUserNameReturnValue;
        }

        public List<Group> GetQstGroupByUserNameReturnValue { get; set; } = new List<Group>();
        public string GetQstGroupByUserNameParameter { get; set; }
        public long GetBase64TokenGroupParam { get; set; }
        public string GetBase64TokenPasswordParam { get; set; }
        public string GetBase64TokenUserParam { get; set; }
        public string GetBase64TokenPcfqdnParam { get; set; }

        public string GetBase64TokenReturnValue { get; set; } = "";
    }
    public class AuthenticationServiceTest
    {
        [TestCase("blub","12345",99, "ned.ganz.so.cool")]
        [TestCase("Admin", "admin123", 1, "richtig.cool.pc")]
        public void LoginCallsUseCaseLogin(string userName, string password, long groupId, string pcfqdn)
        {
            var useCase = new LoginUseCaseMock();
            var authenticationService = new NetworkView.Services.AuthenticationService(null, useCase);
            var authenticationRequest = new AuthenticationRequest()
            {
                Username = userName,
                Password = password,
                GroupId = groupId,
                Pcfqdn = pcfqdn
            };

            var result = authenticationService.Login(authenticationRequest, null);
            result.Wait();
            Assert.AreEqual(userName, useCase.GetBase64TokenUserParam);
            Assert.AreEqual(password, useCase.GetBase64TokenPasswordParam);
            Assert.AreEqual(groupId, useCase.GetBase64TokenGroupParam);
            Assert.AreEqual(pcfqdn, useCase.GetBase64TokenPcfqdnParam);
        }

        [TestCase("sffjisgjd")]
        [TestCase("Test123")]
        public void LoginReturnsCorrectValue(string base64Token)
        {
            var useCase = new LoginUseCaseMock();
            useCase.GetBase64TokenReturnValue = base64Token;
            var authenticationService = new NetworkView.Services.AuthenticationService(null, useCase);
            var result = authenticationService.Login(new AuthenticationRequest(), null);
            result.Wait();
            Assert.AreEqual(base64Token, result.Result.Token.Base64);
        }

        [TestCase("hans")]
        [TestCase("mueller")]
        public void GetQstGroupByUserNameCallsUseCaseGetQstGroupByUserName(string userName)
        {
            var useCase = new LoginUseCaseMock();
            var authenticationService = new NetworkView.Services.AuthenticationService(null, useCase);
            var result = authenticationService.GetQstGroupByUserName(new QstGroupByUserNameRequest(){Username = userName}, null);
            result.Wait();
            Assert.AreEqual(userName, useCase.GetQstGroupByUserNameParameter);
        }


        static IEnumerable<List<Group>> GetQstGroupByUserNameData = new List<List<Group>>()
        {
            new List<Group>
            {
                CreateGroup.Parametrized(10, "GlobalAdmin"),
                CreateGroup.Parametrized(99, "Verwalter")
            },
            new List<Group>
            {
                CreateGroup.Parametrized(5, "Bereichsmanager")
            }
        };

        [TestCaseSource(nameof(GetQstGroupByUserNameData))]
        public void GetQstGroupByUserNameReturnsCorrectValue(List<Group> groups)
        {
            var useCase = new LoginUseCaseMock();
            useCase.GetQstGroupByUserNameReturnValue = groups;
            var authenticationService = new NetworkView.Services.AuthenticationService(null, useCase);
            var result = authenticationService.GetQstGroupByUserName(new QstGroupByUserNameRequest(), null);
            result.Wait();

            var comparer = new Func<Group, DtoTypes.Group, bool>((group, dtoGroup) =>
                group.Id.ToLong() == dtoGroup.GroupId && 
                group.GroupName == dtoGroup.GroupName
             );

            CheckerFunctions.CollectionAssertAreEquivalent(groups, result.Result.Groups, comparer);
        }
    }
}
