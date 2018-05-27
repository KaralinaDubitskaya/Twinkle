using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinkle.Models;
using System.IO;

namespace Twinkle.Parser
{
    public abstract class Parser
    {
        public abstract string Extension { get; }
        public abstract string Name { get; }
        public abstract void Save(Tweets tweets, StreamWriter stream);
    }
}
