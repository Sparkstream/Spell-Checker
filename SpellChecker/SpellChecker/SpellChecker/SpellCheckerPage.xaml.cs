using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using SpellChecker.Data_Models;

namespace SpellChecker
{
    public partial class SpellCheckerPage : ContentPage
    {
        IBingSpellCheckService bingSpellCheckService;

        public static readonly BindableProperty IsProcessingProperty =
            BindableProperty.Create("IsProcessing", typeof(bool), typeof(SpellCheckerPage), false);

        public bool IsProcessing
        {
            get { return (bool)GetValue(IsProcessingProperty); }
            set { SetValue(IsProcessingProperty, value); }
        }

        public SpellCheckerPage()
        {
            InitializeComponent();
            bingSpellCheckService = new BingSpellCheckService();
        }

        async void OnSpellCheckButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Phrase.Text))
                {
                    IsProcessing = true;

                    var spellCheckResult = await bingSpellCheckService.SpellCheckTextAsync(Phrase.Text);
                    await PostWords();
                    foreach (var flaggedToken in spellCheckResult.FlaggedTokens)
                    {
                        Phrase.Text = Phrase.Text.Replace(flaggedToken.Token, flaggedToken.Suggestions.FirstOrDefault().Suggestion);
                    }
                    OnPropertyChanged("SpellCheckText");

                    IsProcessing = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        async Task PostWords()
        {
            WordHistory model = new WordHistory()
            {
                Words = Phrase.Text
            };
            await AzureManager.AzureManagerInstance.PostWordHistory(model);
        }

    }
}
