using System.Collections.Generic;
using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class LoginDataAccess : DataAccessBase, ILoginDataAccess
    {
        public LoginDataAccess(SqliteDbContext dbContext, ITransactionDbContext transactionContext)
            : base(transactionContext, dbContext)
        { }

        public User GetUserByName(string userName)
        {
            var user = _dbContext.QstUsers.SingleOrDefault(x => x.UNAME.ToUpper() == userName.ToUpper());
            if (user == null)
            {
                return null;
            }

            return UserEntityFromUserDto(user);
        }

        private static User UserEntityFromUserDto(QstUser user)
        {
            return new User()
            {
                UserId = new UserId(user.USERID),
                UserName = user.UNAME,
                QstEncryptedPassword = user.PWD,
                Group = new Group()
                {
                    Id = new GroupId(user.UNUM),
                    GroupName = ""
                }
            };
        }

        public User GetUserByNumber(long userNumber)
        {
            var user = _dbContext.QstUsers.SingleOrDefault(u => u.UNUM == userNumber);
            if (user == null)
            {
                return null;
            }

            return UserEntityFromUserDto(user);
        }

        public List<Group> GetQstGroupByUserName(string userName)
        {
            var groups = (
                from g in _dbContext.QstGroups
                    join gu in _dbContext.QstUserGroups
                        on g.GROUPID equals gu.GROUPID
                    join u in _dbContext.QstUsers
                        on gu.USERID equals u.USERID
                where u.UNAME.ToUpper() == userName.ToUpper()
                select GetGroupFromDbGroup(g)
            ).ToList();

            return groups;
        }

        public List<Group> GetQstGroupByUserNumber(long userNumber)
        {
            var groups = (
                from g in _dbContext.QstGroups
                join gu in _dbContext.QstUserGroups
                    on g.GROUPID equals gu.GROUPID
                join u in _dbContext.QstUsers
                    on gu.USERID equals u.USERID
                where u.UNUM == userNumber
                select GetGroupFromDbGroup(g)
            ).ToList();

            return groups;
        }

        private static Group GetGroupFromDbGroup(DbEntities.QstGroup qstGroup)
        {
            return new Group()
            {
                Id = new GroupId(qstGroup.GROUPID),
                GroupName = qstGroup.NAME
            };
        }
    }
}
