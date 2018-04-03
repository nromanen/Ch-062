using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Model.DB
{
    public class User : IdentityUser
    {
        //public virtual IdentityRole Role { get; set; }

        //public string RoleId { get; set; }
    }
}
