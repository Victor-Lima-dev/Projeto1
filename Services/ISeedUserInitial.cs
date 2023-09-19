using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolverQuestao.Services
{
    public interface ISeedUserInitial
    {
        Task SeedRolesAsync();

        Task SeedUsersAsync();


    }
}