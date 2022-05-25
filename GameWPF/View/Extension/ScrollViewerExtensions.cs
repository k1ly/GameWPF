using System.Windows;
using System.Windows.Controls;

namespace GameWPF.View.Extension
{
    public class ScrollViewerExtensions
    {
        public static readonly DependencyProperty ScrollToEndProperty
            = DependencyProperty.RegisterAttached("ScrollToEnd", typeof(bool), typeof(ScrollViewerExtensions), new PropertyMetadata(false, ScrollToEndChanged));

        private static bool AutoScroll { get; set; }

        private static void ScrollToEndChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            if (scroll != null)
            {
                if ((e.NewValue != null) && (bool)e.NewValue)
                {
                    scroll.ScrollToEnd();
                    scroll.ScrollChanged += ScrollChanged;
                }
                else { scroll.ScrollChanged -= ScrollChanged; }
            }
        }

        public static bool GetScrollToEnd(ScrollViewer scroll)
        {
            return (bool)scroll.GetValue(ScrollToEndProperty);
        }

        public static void SetScrollToEnd(ScrollViewer scroll, bool scrollToEnd)
        {
            scroll.SetValue(ScrollToEndProperty, scrollToEnd);
        }

        private static void ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scroll = sender as ScrollViewer;
            if (e.ExtentHeightChange == 0)
                AutoScroll = scroll.VerticalOffset == scroll.ScrollableHeight;
            if (AutoScroll && e.ExtentHeightChange != 0)
                scroll.ScrollToVerticalOffset(scroll.ExtentHeight);
        }
    }
}
