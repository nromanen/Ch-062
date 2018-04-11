using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Model.DB
{
    public class Role: IdentityRole
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
    }
}
