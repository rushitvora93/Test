using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Models
{
    public class UserModel : BindableBase
    {
        public User Entity { get; private set; }

        public long UserId
        {
            get => Entity.UserId.ToLong();
            set
            {
                Entity.UserId = new UserId(value);
                RaisePropertyChanged();
            }
        }

        public string UserName
        {
            get => Entity.UserName;
            set
            {
                Entity.UserName = value;
                RaisePropertyChanged();
            }
        }

        public UserModel(User entity)
        {
            Entity = entity ?? new User();
        }

        public static UserModel GetModelFor(User entity)
        {
            return entity != null ? new UserModel(entity) : null;
        }
    }
}
