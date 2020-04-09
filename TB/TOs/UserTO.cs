using Microsoft.AspNetCore.Identity;
using System;

namespace ToolBox.TOs
{
    public class UserTO : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
