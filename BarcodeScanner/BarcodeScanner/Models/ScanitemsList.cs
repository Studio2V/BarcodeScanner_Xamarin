using BarcodeScanner.ViewModels;
using SQLite;
using System;

namespace BarcodeScanner.Models
{
    public class ScanitemsList 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string linkType;

        private string linkText;

        private DateTime scantime;

        public string LinkType
        {
            get => linkType;
            set
            {
                linkType = value;
               // OnPropertyChanged();
            }
        }

        public string LinkText
        { 
            get => linkText;
            set
            {
                linkText = value;
               // OnPropertyChanged();
            }
        }

        public DateTime Scantime 
        {
            get => scantime;
            set
            {
                scantime = value;
                //OnPropertyChanged();
            }
        }
    }
}
