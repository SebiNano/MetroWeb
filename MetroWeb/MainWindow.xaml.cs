using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MetroWeb
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        public MainWindow()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            Utils.myMainWindow = this;
	    }

        #region Def
        private ExtendedWebBrowser2.ExtendedWebBrowser thisWebBrowser
        {
            get
            {
                try
                {
                    return ((UserControlWebBrowserWindow)((TabItem)tabControlMain.Items.GetItemAt(tabControlMain.SelectedIndex)).Content).WebBrowser;
                }
                catch
                {
                    return null;
                }
            }
        }

        private TabItem thisTabItem
        {
            get
            {
                try
                {
                    return (TabItem)tabControlMain.Items.GetItemAt(tabControlMain.SelectedIndex);
                }
                catch
                {
                    return null;
                }
            }
        }

        private TabItem ActualSelectedTab;
        #endregion

        #region Handlers
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tabControlMain.Items.Remove(TabItemRemove);
        }

        private void WindowCommandSettings_Click(object sender, RoutedEventArgs e)
        {
            WindowSettings myWindowSettings = new WindowSettings();
            myWindowSettings.ShowDialog();
        }
        #endregion

        #region TabManaging
        public void AddTab(Uri URL)
        {
            TabItem myTab = new TabItem();
            UserControlWebBrowserWindow myWebBrowserWindow = new UserControlWebBrowserWindow(myTab);

            myWebBrowserWindow.WebBrowser.Navigate(URL);

            myTab.Content = myWebBrowserWindow;
            myTab.Header = "Untitled";
            tabControlMain.Items.Add(myTab);
            myTab.Focus();
        }

        public void CloseTab(TabItem Tab)
        {
            if (Tab != tabItemHome)
            {
                tabControlMain.Items.Remove(TabItemRemove);

                (Tab.Content as UserControlWebBrowserWindow).WebBrowser.Dispose();
                tabControlMain.Items.Remove(Tab);
            }
        }

        private void tabControlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabItemHome.IsSelected)
            {
                //Hide the AppBar
                GridAppBar.Visibility = System.Windows.Visibility.Collapsed;

                this.Title = "Home - MetroWeb";

                tabControlMain.Items.Remove(TabItemRemove);

                textBoxHomeSearch.Focus();
            }
            else if (TabItemRemove.IsSelected)
            {
                tabControlMain.Items.Remove(TabItemRemove);
            }
            else if (thisWebBrowser != null)
            {
                //Show the AppBar
                GridAppBar.Visibility = System.Windows.Visibility.Visible;

                Utils.CanGoBackChanged(thisWebBrowser.CanGoBack, thisTabItem);
                Utils.CanGoForwardChanged(thisWebBrowser.CanGoForward, thisTabItem);
                Utils.EndLoad(thisTabItem);

                Utils.UpdateTitle(thisWebBrowser.DocumentTitle, thisTabItem);

                //Update the position of close button
                tabControlMain.Items.Remove(TabItemRemove);
                tabControlMain.Items.Insert(tabControlMain.SelectedIndex + 1, TabItemRemove);
            }
        }

        private void appBarButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (tabItemHome.IsSelected && ActualSelectedTab != null)
            {
                ActualSelectedTab.Focus();
            }
            else
            {
                ActualSelectedTab = thisTabItem;
                tabItemHome.Focus();
            }
        }

        private void appBarButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            CloseTab(thisTabItem);
        }

        private void tabControlMain_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TabItem && ((e.Source as TabItem) == tabItemHome || (e.Source as TabItem) == TabItemRemove))
                e.Handled = true;
        }
        #endregion

        #region AppBar
        private void appBarButtonBack_Click(object sender, RoutedEventArgs e)
        {
            thisWebBrowser.GoBack();
        }

        private void appBarButtonForward_Click(object sender, RoutedEventArgs e)
        {
            thisWebBrowser.GoForward();
        }

        private void appBarButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            thisWebBrowser.Refresh();
        }

        private void appBarButtonStop_Click(object sender, RoutedEventArgs e)
        {
            thisWebBrowser.Stop();
        }
        #endregion

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            AddTab(new Uri("https://www.google.com/"));
        }

        private void textBoxAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                thisWebBrowser.Navigate(textBoxAddress.Text);
            }
        }

        private void textBoxHomeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
