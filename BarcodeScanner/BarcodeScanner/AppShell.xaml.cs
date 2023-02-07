using BarcodeScanner.ViewModels;
using BarcodeScanner.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BarcodeScanner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ScanHistoryView), typeof(ScanHistoryView));
        }

    }
}
