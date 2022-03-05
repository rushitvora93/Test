using System.Collections.Generic;
using Common.UseCases.Interfaces;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface ILoginUseCase
    {
        string GetBase64Token(string userName, string password, long groupId, string pcfqdn);
        List<Group> GetQstGroupByUserName(string userName);
    }

    public interface ILoginDataAccess
    {
        User GetUserByName(string userName);
        User GetUserByNumber(long userNumber);
        List<Group> GetQstGroupByUserName(string userName);
        List<Group> GetQstGroupByUserNumber(long userNumber);
    }

    public interface IPasswordUtilities
    {
        bool IsCorrectCspPassword(string passwordToCheck);
        bool IsCorrectUserPassword(User user, string password);
    }

    public class LoginUseCase : ILoginUseCase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILoginDataAccess _loginDataAccess;
        private readonly IPasswordUtilities _passwordUtilities;
        private readonly ILicenseUseCase _licenseUseCase;

        public LoginUseCase(ITokenGenerator tokenGenerator, ILoginDataAccess loginDataAccess, IPasswordUtilities passwordUtilities, ILicenseUseCase licenseUseCase)
        {
            _tokenGenerator = tokenGenerator;
            _loginDataAccess = loginDataAccess;
            _passwordUtilities = passwordUtilities;
            _licenseUseCase = licenseUseCase;
        }

        public string GetBase64Token(string userName, string password, long groupId, string pcfqdn)
        {
            User user = null;
            if (long.TryParse(userName, out var userNumber))
            {
                user = _loginDataAccess.GetUserByNumber(userNumber);
            }
            if (user == null)
            {
                user = _loginDataAccess.GetUserByName(userName);
            }
            if (userName == "csp")
            {
                if (!_passwordUtilities.IsCorrectCspPassword(password))
                {
                    return "";
                }
                user = new User
                {
                    Group = user?.Group, 
                    UserId = new UserId(-100),
                    UserName = userName
                };
            }
            else
            {
                if (user == null)
                {
                    return "";
                }
                if (!_passwordUtilities.IsCorrectUserPassword(user, password))
                {
                    return "";
                }
            }

            if (_licenseUseCase.IsLicenseOk(pcfqdn))
            {
                var authenticationUser = new AuthenticationUser()
                {
                    UserId = user.UserId.ToLong(),
                    UserName = user.UserName,
                    GroupId = user.Group?.Id.ToLong() ?? -1,
                    GroupName = user.Group?.GroupName
                };
                return _tokenGenerator.GenerateToken(authenticationUser, pcfqdn);
            }

            return "";
        }

        public List<Group> GetQstGroupByUserName(string userName)
        {
            if (long.TryParse(userName, out var userNumber))
            {
                return _loginDataAccess.GetQstGroupByUserNumber(userNumber);
            }

            return _loginDataAccess.GetQstGroupByUserName(userName);
        }
    }
}
