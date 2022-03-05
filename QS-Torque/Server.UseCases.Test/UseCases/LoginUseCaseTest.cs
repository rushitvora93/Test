using System.Collections.Generic;
using Common.UseCases.Interfaces;
using NUnit.Framework;
using Server.Core.Entities;
using Server.TestHelper.Factories;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    public class TokenGeneratorMock : ITokenGenerator
    {
        public string GenerateTokenReturnValue { get; set; }

        public string GenerateToken(AuthenticationUser user, string pcfqdn)
        {
            GenerateTokenUserParameter = user;
            return GenerateTokenReturnValue;
        }

        public AuthenticationUser GenerateTokenUserParameter { get; set; }
    }

    public class LoginDataAccessMock : ILoginDataAccess
    {
        public User GetUserByName(string userName)
        {
            GetUserByNameParameter = userName;
            return GetUserByNameReturnValue;
        }

        public User GetUserByNumber(long userNumber)
        {
            GetUserByNumberParameter = userNumber;
            return GetUserByNumberReturnValue;
        }

        public List<Group> GetQstGroupByUserName(string userName)
        {
            GetQstGroupByUserNameParameter = userName;
            return GetQstGroupByUserNameReturnValue;
        }

        public List<Group> GetQstGroupByUserNumber(long userNumber)
        {
            GetQstGroupByUserNumberParameter = userNumber;
            return GetQstGroupByUserNumberReturnValue;
        }

        public List<Group> GetQstGroupByUserNumberReturnValue { get; set; }
        public long GetQstGroupByUserNumberParameter { get; set; }
        public List<Group> GetQstGroupByUserNameReturnValue { get; set; }
        public string GetQstGroupByUserNameParameter { get; set; }
        public User GetUserByNameReturnValue { get; set; }
        public string GetUserByNameParameter { get; set; } = "";
        public long GetUserByNumberParameter { get; set; }
        public User GetUserByNumberReturnValue { get; set; }
    }

    public class PasswordUtilitiesMock : IPasswordUtilities
    {
        public bool IsCorrectCspPassword(string passwordToCheck)
        {
            IsCorrectCspPasswordParameter = passwordToCheck;
            IsCorrectCspPasswordWasCalled = true;
            return IsCorrectCspPasswordReturnValue;
        }

        public bool IsCorrectUserPassword(User user, string password)
        {
            IsCorrectUserPasswordParameterUser = user;
            IsCorrectUserPasswordParameterPassword = password;
            IsCorrectUserPasswordWasCalled = true;
            return IsCorrectUserPasswordReturnValue;
        }

        public string IsCorrectCspPasswordParameter;
        public bool IsCorrectCspPasswordReturnValue;
        public bool IsCorrectCspPasswordWasCalled;
        public User IsCorrectUserPasswordParameterUser;
        public string IsCorrectUserPasswordParameterPassword;
        public bool IsCorrectUserPasswordReturnValue;
        public bool IsCorrectUserPasswordWasCalled;
    }

    public class LicenseUseCaseMock : ILicenseUseCase
    {
        public bool licenseOk_ = true;
        public bool licenseFileOk_ = true;

        public bool IsLicenseOkCalled = false;
        public bool IsLicenseFileLoadedCalled = false;
        public LicenseUseCaseMock()
        {

        }
        public bool IsLicenseOk(string pcfqdn)
        {
            IsLicenseOkCalled = true;
            return licenseOk_;
        }

        public bool IsLicenseFileLoaded()
        {
            IsLicenseFileLoadedCalled = true;
            return licenseFileOk_;
        }
    }

    public class LoginUseCaseTest
    {
        [TestCase("234435436678")]
        [TestCase("TEST123")]
        public void GenerateBase64ReturnsCorrectValue(string token)
        {
            var environment = new Environment();
            environment.Mocks.PasswordUtilities.IsCorrectUserPasswordReturnValue = true;
            environment.Mocks.TokenGenerator.GenerateTokenReturnValue = token;
            environment.Mocks.DataAccess.GetUserByNameReturnValue = CreateUser.IdOnly(1);
            Assert.AreEqual(token, environment.UseCase.GetBase64Token("", "", 0, ""));
        }

        [TestCase("234435436678")]
        [TestCase("TEST123")]
        public void GenerateBase64WithInvalidLicense(string token)
        {
            var environment = new Environment();
            environment.Mocks.PasswordUtilities.IsCorrectUserPasswordReturnValue = true;
            environment.Mocks.TokenGenerator.GenerateTokenReturnValue = token;
            environment.Mocks.LicenseUseCase.licenseOk_ = false;
            environment.Mocks.DataAccess.GetUserByNameReturnValue = CreateUser.IdOnly(1);
            Assert.AreEqual("", environment.UseCase.GetBase64Token("", "", 0, ""));
            Assert.IsTrue(environment.Mocks.LicenseUseCase.IsLicenseOkCalled);
        }

        [TestCase("blub")]
        [TestCase("Test43954956")]
        public void LoggingInRequestUserByName(string username)
        {
            var environment = new Environment();
            environment.UseCase.GetBase64Token(username, "", 0,"");
            Assert.AreEqual(username, environment.Mocks.DataAccess.GetUserByNameParameter);
        }

        [TestCase(22)]
        [TestCase(2387492)]
        public void LoggingInWithNumberAsUserNameRequestsUserByNumber(long userNumber)
        {
            var environment = new Environment();
            environment.UseCase.GetBase64Token(userNumber.ToString(), "", 0,"");
            Assert.AreEqual(userNumber, environment.Mocks.DataAccess.GetUserByNumberParameter);
        }

        [TestCase(4,"userasstring", 99, "G1", false)]
        [TestCase(5,"7", 12, "Admin", true)]
        [TestCase(8, "hugo", 44, "Bereichsverwalter", false)]
        public void LoggingInCallsTokenGeneratorWithCorrectValue(long userId, string userName, long groupId, string groupName, bool userAsNumber)
        {
            var environment = new Environment();
            environment.Mocks.PasswordUtilities.IsCorrectUserPasswordReturnValue = true;
            var user = CreateUser.Parametrized(userId, userName, CreateGroup.Parametrized(groupId, groupName));
            var userByName = user;
            environment.Mocks.DataAccess.GetUserByNameReturnValue = userByName;
            var userByNumber = user;
            environment.Mocks.DataAccess.GetUserByNumberReturnValue = userByNumber;

            environment.UseCase.GetBase64Token(userName, "", 0, "");
            if (!userAsNumber)
            {
                Assert.IsTrue(CompareUserWithAuthenticationUser(userByName, environment.Mocks.TokenGenerator.GenerateTokenUserParameter));
            }
            else
            {
                Assert.IsTrue(CompareUserWithAuthenticationUser(userByNumber, environment.Mocks.TokenGenerator.GenerateTokenUserParameter));
            }
        }

        private bool CompareUserWithAuthenticationUser(User user, AuthenticationUser authUser)
        {
            return user.UserId.ToLong() == authUser.UserId && 
                   user.UserName == authUser.UserName &&
                   user.Group.Id.ToLong() == authUser.GroupId &&
                   user.Group.GroupName == authUser.GroupName;
        }

        [Test]
        public void LoggingInAsCspCallsTokenGeneratorWithCspUser()
        {
            var environment = new Environment();
            environment.Mocks.PasswordUtilities.IsCorrectCspPasswordReturnValue = true;
            environment.UseCase.GetBase64Token("csp", "", 0, "");
            Assert.AreEqual(
                ("csp", -100),
                (environment.Mocks.TokenGenerator.GenerateTokenUserParameter.UserName,
                    environment.Mocks.TokenGenerator.GenerateTokenUserParameter.UserId));
        }

        [TestCase("ajklsdfjl")]
        [TestCase("FranzIstToll123!")]
        public void LoggingInAsCspForwardsUserPasswordToCspPasswordCheck(string userPassword)
        {
            var environment = new Environment();
            environment.UseCase.GetBase64Token("csp", userPassword, 0, "");
            Assert.AreEqual(userPassword, environment.Mocks.PasswordUtilities.IsCorrectCspPasswordParameter);
        }

        [TestCase("adshfal", "jaskdflafl", true)]
        [TestCase("29", "jaskdflafl", false)]
        public void LoggingInAsUserForwardsPasswordToUserPasswordCheck(
            string userName, 
            string userPassword,
            bool getUserByName)
        {
            var environment = new Environment();
            var userByName = new User();
            environment.Mocks.DataAccess.GetUserByNameReturnValue = userByName;
            var userByNumber = new User();
            environment.Mocks.DataAccess.GetUserByNumberReturnValue = userByNumber;
            environment.UseCase.GetBase64Token(userName, userPassword, 0, "");
            Assert.AreEqual(
                getUserByName
                    ? userByName
                    : userByNumber,
                environment.Mocks.PasswordUtilities.IsCorrectUserPasswordParameterUser);
            Assert.AreEqual(
                userPassword,
                environment.Mocks.PasswordUtilities.IsCorrectUserPasswordParameterPassword);
        }

        [Test]
        public void LoggingInAsCspDoesNotCheckNormalUserPassword()
        {
            var environment = new Environment();
            environment.UseCase.GetBase64Token("csp", "", 0, "");
            Assert.IsFalse(environment.Mocks.PasswordUtilities.IsCorrectUserPasswordWasCalled);
        }

        [Test]
        public void LoggingInAsUserDoesNotCheckCspPassword()
        {
            var environment = new Environment();
            environment.UseCase.GetBase64Token("adsjlkfjal", "", 0, "");
            Assert.IsFalse(environment.Mocks.PasswordUtilities.IsCorrectCspPasswordWasCalled);
        }

        [TestCase("fajsdlja", false)]
        [TestCase("8491", true)]
        public void LoggingInAsUserWithWrongPasswordReturnsEmptyToken(string userName, bool loginByNumber)
        {
            var environment = new Environment();
            environment.Mocks.DataAccess.GetUserByNameReturnValue = new User();
            environment.Mocks.DataAccess.GetUserByNumberReturnValue = new User();
            environment.Mocks.LicenseUseCase.licenseOk_ = true;
            environment.Mocks.PasswordUtilities.IsCorrectUserPasswordReturnValue = false;
            var token = environment.UseCase.GetBase64Token(userName, "", 0, "");
            Assert.AreEqual("", token);
        }

        [Test]
        public void LoggingInAsCspWithWrongPasswordReturnsEmptyToken()
        {
            var environment = new Environment();
            environment.Mocks.LicenseUseCase.licenseOk_ = true;
            environment.Mocks.PasswordUtilities.IsCorrectUserPasswordReturnValue = false;
            var token = environment.UseCase.GetBase64Token("csp", "", 0, "");
            Assert.AreEqual("", token);
        }

        [Test]
        public void LogginInAsUserThatIsNotInDbReturnsEmptyTokenBeforeCheckingPassword()
        {
            var environment = new Environment();
            var token = environment.UseCase.GetBase64Token("unknown", "", 0, "");
            Assert.IsFalse(environment.Mocks.PasswordUtilities.IsCorrectUserPasswordWasCalled);
            Assert.AreEqual("", token);
        }

        [TestCase("CSP", false)]
        [TestCase("111", true)]
        [TestCase("Administartor 123456sdefgdg", false)]
        [TestCase("666", true)]
        public void GetQstGroupByUserNameCallsGetQstGroupByUserNameWithCorrectValue(string username, bool userAsNumber)
        {
            var environment = new Environment();
            environment.UseCase.GetQstGroupByUserName(username);
            Assert.AreEqual(username,
                !userAsNumber
                    ? environment.Mocks.DataAccess.GetQstGroupByUserNameParameter
                    : environment.Mocks.DataAccess.GetQstGroupByUserNumberParameter.ToString());
        }

        [Test]
        public void GetQstGroupWithUserNameReturnsQstGroups()
        {
            var environment = new Environment();
            var groups = new List<Group>();
            environment.Mocks.DataAccess.GetQstGroupByUserNameReturnValue = groups;
            var result = environment.UseCase.GetQstGroupByUserName("Hans");
            Assert.AreSame(groups, result);
        }

        [Test]
        public void GetQstGroupWithUserNumberReturnsQstGroups()
        {
            var environment = new Environment();
            var groups = new List<Group>();
            environment.Mocks.DataAccess.GetQstGroupByUserNumberReturnValue = groups;
            var result = environment.UseCase.GetQstGroupByUserName("1123");
            Assert.AreSame(groups, result);
        }

        private class Environment
        {
            public class Mock
            {
                public Mock()
                {
                    TokenGenerator = new TokenGeneratorMock();
                    DataAccess = new LoginDataAccessMock();
                    PasswordUtilities = new PasswordUtilitiesMock();
                    LicenseUseCase = new LicenseUseCaseMock();
                }

                public TokenGeneratorMock TokenGenerator;
                public LoginDataAccessMock DataAccess;
                public PasswordUtilitiesMock PasswordUtilities;
                public LicenseUseCaseMock LicenseUseCase;
            }

            public Environment(bool licenseOk = true, bool licenseFileOk = true)
            {
                Mocks = new Mock();
                UseCase = new LoginUseCase(Mocks.TokenGenerator, Mocks.DataAccess, Mocks.PasswordUtilities, Mocks.LicenseUseCase);
            }

            public Mock Mocks;
            public LoginUseCase UseCase;
        }
    }
}
