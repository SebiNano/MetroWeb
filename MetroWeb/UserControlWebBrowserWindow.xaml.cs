using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace MetroWeb
{
    /// <summary>
    /// Interaction logic for UserControlWebBrowserWindow.xaml
    /// </summary>
    public partial class UserControlWebBrowserWindow : System.Windows.Controls.UserControl
    {
        TabItem myTabItem;
        public bool IsLoading;

        public UserControlWebBrowserWindow(TabItem TabItem)
        {
            myTabItem = TabItem;
            InitializeComponent();
        }

        void _browser_DownloadComplete(object sender, EventArgs e)
        {
            IsLoading = false;
            Utils.EndLoad(myTabItem);
        }

        private void WebBrowser_StartNewWindow(object sender, ExtendedWebBrowser2.BrowserExtendedNavigatingEventArgs e)
        {
            if ((e.NavigationContext & ExtendedWebBrowser2.UrlContext.UserFirstInited)
             == ExtendedWebBrowser2.UrlContext.UserFirstInited &&
             (e.NavigationContext & ExtendedWebBrowser2.UrlContext.UserInited)
             == ExtendedWebBrowser2.UrlContext.UserInited)
                Utils.myMainWindow.AddTab(e.Url);

            e.Cancel = true;
        }

        private void WebBrowser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            Utils.UpdateProgress(e.CurrentProgress, myTabItem); 
        }

        private void WebBrowser_CanGoBackChanged(object sender, EventArgs e)
        {
            Utils.CanGoBackChanged(WebBrowser.CanGoBack, myTabItem);
        }

        private void WebBrowser_CanGoForwardChanged(object sender, EventArgs e)
        {
            Utils.CanGoForwardChanged(WebBrowser.CanGoForward, myTabItem);
        }

        private void WebBrowser_Downloading(object sender, EventArgs e)
        {
            IsLoading = true;
            Utils.StartLoad(myTabItem);
        }

        private void WebBrowser_DocumentTitleChanged(object sender, EventArgs e)
        {
            Utils.UpdateTitle(this.WebBrowser.DocumentTitle, myTabItem);
        }

        private void WebBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Utils.UpdateAddressBox(this.WebBrowser.Document.Url, myTabItem);
        }
    }
}
