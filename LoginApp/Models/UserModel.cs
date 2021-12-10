using System;

namespace LoginApp.Models
{
    public class UserModel
    {
        public Guid UserId{ get; set; } = new Guid();
        public string  UserName{ get; set; }
        public string Email{ get; set; }
        public string Password { get; set; }

        internal static bool Any(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}