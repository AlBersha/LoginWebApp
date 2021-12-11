using System;
using LoginApp.Models;

namespace Domain.Models
{
    public class UpdatePasswordModel
    {
        public string UserName{ get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}