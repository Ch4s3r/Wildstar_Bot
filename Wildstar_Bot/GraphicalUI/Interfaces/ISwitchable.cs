using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wildstar_Bot.GraphicalUI
{
    interface ISwitchable
    {
        void UtilizeState(object data, StateMessage msg);
    }
}
