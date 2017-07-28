using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SpellChecker.Data_Models;

namespace SpellChecker
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<WordHistory> wordHistoryTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://funwithbingspellcheck.azurewebsites.net");
            this.wordHistoryTable = this.client.GetTable<WordHistory>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }
        public async Task<List<WordHistory>> GetWordHistoryList()
        {
            return await this.wordHistoryTable.ToListAsync();
        }
        public async Task PostWordHistory(WordHistory wordHistory)
        {
            await this.wordHistoryTable.InsertAsync(wordHistory);
        }
    }
}
