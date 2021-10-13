using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace platformer.Scripts.InputWrapperClass
{
    public class InputWapper
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Toggle { get; set; }
    }
}
