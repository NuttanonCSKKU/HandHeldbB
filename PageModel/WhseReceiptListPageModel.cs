using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandHeldbB.Model;
using HandHeldbB.Service;

namespace HandHeldbB.PageModel
{
    public partial class WhseReceiptListPageModel : ObservableObject
    {
        private readonly DocumentService _documentService;

        [ObservableProperty]
        private ObservableCollection<DocumentModel> products;

        public IAsyncRelayCommand GetDocumentsCommand { get; }

        public WhseReceiptListPageModel()
        {
            _documentService = new DocumentService();
            Products = new ObservableCollection<DocumentModel>();
            GetDocumentsCommand = new AsyncRelayCommand(GetDocumentsAsync);
        }

        private async Task GetDocumentsAsync()
        {
            try
            {
                var documents = await _documentService.GetDocumentsAsync();

                Products.Clear();

                foreach (var doc in documents)
                {
                    Products.Add(doc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading documents: {ex.Message}");
            }
        }

    }
}
