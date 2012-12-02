using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MetroWeb
{
    class Utils
    {
        public static MainWindow myMainWindow;

        public static void UpdateAddressBox(Uri Url, TabItem TabItem)
        {
            if (TabItem.IsSelected)
            {
                myMainWindow.textBoxAddress.Text = Url.ToString();
            }
        }

        public static void UpdateTitle(string Title, TabItem TabItem)
        {
            if (Title == string.Empty)
                Title = "Untitled";
            TabItem.Header = Title;

            if (TabItem.IsSelected)
            {
                myMainWindow.Title = Title + " - MetroWeb";
            }
        }

        public static void UpdateProgress(long Value, TabItem TabItem)
        {
            if (TabItem.IsSelected)
            {
                myMainWindow.progressBarLoadProgress.Value = Value;
            }
        }

        public static void StartLoad(TabItem TabItem)
        {
            if (TabItem.IsSelected)
            {
                myMainWindow.appBarButtonRefresh.Visibility = System.Windows.Visibility.Hidden;
                myMainWindow.appBarButtonStop.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public static void EndLoad(TabItem TabItem)
        {
            if (TabItem.IsSelected)
            {
                myMainWindow.appBarButtonStop.Visibility = System.Windows.Visibility.Hidden;
                myMainWindow.appBarButtonRefresh.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public static void CanGoBackChanged(bool Value, TabItem TabItem)
        {
            if (TabItem.IsSelected)
            {
                myMainWindow.appBarButtonBack.IsEnabled = Value;
            }
        }

        public static void CanGoForwardChanged(bool Value, TabItem TabItem)
        {
            if (TabItem.IsSelected)
            {
                myMainWindow.appBarButtonForward.IsEnabled = Value;
            }
        }
    }
}
