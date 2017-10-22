using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GSG.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("DNJhL14yjgXnAcJI53oO~EavIC-PtkCFq81MJqCPSOw~AvjldwBkvg_aWgNn6_pBQztKb8uazyYhb34Xbbf_tDKjNbZXFBV1tBBjR8bhUjki");
            LoadApplication(new GSG.App());
        }
    }
}
