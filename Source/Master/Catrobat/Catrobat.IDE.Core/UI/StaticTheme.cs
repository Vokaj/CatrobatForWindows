﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.UI
{
    public class StaticTheme
    {
        public PortableSolidColorBrush ApplicationBackgroundBrush { get { return new PortableSolidColorBrush("#FFFFFFFF"); } } // Use this instead of the default background (light and dark)

        public PortableSolidColorBrush AppBarBorderBrush { get { return new PortableSolidColorBrush("#FF000000"); } }

        public PortableSolidColorBrush ActionsBrush { get { return new PortableSolidColorBrush("#FFEBB613"); } }

        public PortableSolidColorBrush SoundsBrush { get { return new PortableSolidColorBrush("#FF87778D"); } }
        public PortableSolidColorBrush CostumesBrush { get { return new PortableSolidColorBrush("#FF6F9263"); } }
        public PortableSolidColorBrush ObjectsBrush{ get { return new PortableSolidColorBrush("#FF891D1D"); } }

        public PortableSolidColorBrush LooksBrickBrush{ get { return new PortableSolidColorBrush("#FF6F9263"); } }
        public PortableSolidColorBrush MotionBrickBrush{ get { return new PortableSolidColorBrush("#FF0091C3"); } }
        public PortableSolidColorBrush SoundBrickBrush{ get { return new PortableSolidColorBrush("#FF87778D"); } }
        public PortableSolidColorBrush ControlBrickBrush{ get { return new PortableSolidColorBrush("#FFF29263"); } }
        public PortableSolidColorBrush VariableBrickBrush{ get { return new PortableSolidColorBrush("#FFE84F50"); } }
        public PortableSolidColorBrush BrickBorderBrush{ get { return new PortableSolidColorBrush("#FFFFFFFF"); } }
    }
}
