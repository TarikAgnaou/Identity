using Microsoft.AspNetCore.Identity;
using System;

namespace ToolBox.TOs
{
    public class RoleTO : IdentityRole<Guid>
    {
        public static string[] Roles = new string[]
        {
            "Admin",
            "Visiteur",
            "Salarié",
            "Directeur"
        };
    }
}
