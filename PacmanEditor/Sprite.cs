using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanEditor
{
    internal class Sprite
    {
        internal byte x, y;
        internal byte pal = 1;
        internal ushort chr = 0;
        public Sprite(byte x, byte y, ushort chr, byte pal = 1) 
        {
            this.x = x;
            this.y = y;
            this.pal = pal;
            this.chr = chr;
        }

    }
}
