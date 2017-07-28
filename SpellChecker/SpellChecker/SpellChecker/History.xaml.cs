using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using SpellChecker.Data_Models;

namespace SpellChecker
{
    public partial class History : ContentPage
    {
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;
        public History()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty IsProcessingProperty =
            BindableProperty.Create("IsProcessing", typeof(bool), typeof(SpellCheckerPage), false);

        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }
        async void RefreshHistory(object sender, System.EventArgs e)
        {
            IsProcessing = true;
            List<WordHistory> WordHistoryList = await AzureManager.AzureManagerInstance.GetWordHistoryList();
            WordList.ItemsSource = WordHistoryList;
            IsProcessing = false;
        }
    }
}
