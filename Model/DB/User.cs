using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Model.DB
{
    public class User : IdentityUser
    {
        

        public virtual Role Role { get; set; }

        public int RoleId { get; set; }
    }
}
