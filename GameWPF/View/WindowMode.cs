using System;
using System.Collections.Generic;
using System.Windows;

namespace GameWPF.View
{
    [Serializable]
    public class WindowMode
    {
        private static Dictionary<string, WindowMode> cashedWindowModes = new Dictionary<string, WindowMode>();
        public string Name { get; set; }
        public WindowState WindowState { get; set; }
        public WindowStyle WindowStyle { get; set; }

        static WindowMode()
        {
            cashedWindowModes.Add("FullScreen", new WindowMode("FullScreen", WindowState.Maximized, WindowStyle.None));
            cashedWindowModes.Add("Windowed", new WindowMode("Windowed", WindowState.Maximized, WindowStyle.ThreeDBorderWindow));
            cashedWindowModes.Add("WindowBordered", new WindowMode("WindowBordered", WindowState.Normal, WindowStyle.ThreeDBorderWindow));
        }

        public WindowMode() { }

        public WindowMode(string displayName, WindowState windowState, WindowStyle windowStyle)
        {
            Name = displayName;
            WindowState = windowState;
            WindowStyle = windowStyle;
        }

        public static WindowMode GetWindowMode(string name)
        {
            cashedWindowModes.TryGetValue(name, out WindowMode windowMode);
            return windowMode;
        }
    }
}
