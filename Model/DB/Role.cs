using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Model.DB
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
