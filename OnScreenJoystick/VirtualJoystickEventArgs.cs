using System;

namespace NewUI.MainView
{
    public class VirtualJoystickEventArgs:EventArgs
    {
        public double Angle { get; set; }
        public double Distance { get; set; }
    }

    public class VirtualJoystickClickEventArgs : EventArgs
    {
        public int ClickProxy { get; set; }
    }
}
