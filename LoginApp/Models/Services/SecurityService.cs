using System.Collections.Generic;
using System.Linq;

namespace LoginApp.Models.Services
{
    public class SecurityService : ISecuritySevice
    {
        public IList<UserModel> Users { get; set; } = new List<UserModel>();

        public bool IsValid(UserModel user)
        {
            return Users.Any(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
