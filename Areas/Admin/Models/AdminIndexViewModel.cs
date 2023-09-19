using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ResolverQuestao.Areas.Admin.Models
{
    public class AdminIndexViewModel
    {
        public List<IdentityUser> Users { get; set; }

        public List<IdentityRole> Roles { get; set; }

    
    }
}