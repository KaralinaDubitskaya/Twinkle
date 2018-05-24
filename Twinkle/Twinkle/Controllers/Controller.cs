using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinkle.Controllers
{
    public abstract class Controller
    {
        public abstract void HandleNavigation(object args);
    }
}
