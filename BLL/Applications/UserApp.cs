using System;
using System.Collections.Generic;
using System.Text;
using ToolBox.TOs;
using DALC.Repository;

namespace BLL.Applications
{
    public class UserApp : Application<UserTO>
    {
        private UserCommand _Repo = new UserCommand();
    }
}
