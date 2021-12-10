using System.Collections.Generic;

namespace LoginApp.Models.Services
{
    public interface ISecuritySevice
    {
        public IList<UserModel> Users { get; set; }
        public bool IsValid(UserModel user);
    }
}
