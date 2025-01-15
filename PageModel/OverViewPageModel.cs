using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandHeldbB.Page;

namespace HandHeldbB.PageModel
{
    public partial class OverViewPageModel : ObservableObject
    {
        public ICommand BtnWhseReceiptClicked { get; }
        public ICommand BtnPutAwayClicked { get; }
        //public ICommand BtnStockCheck { get; }
        public ICommand BtnPickListClicked { get; }
        public ICommand BtnWhseShipmentClicked { get; }
        //public ICommand BtnReturnClicked { get; }
        //public ICommand CountClicked { get; }
        //public ICommand ReclassClicked { get; }

        public ImageSource IconSell { get; set; } = ImageSource.FromFile("icon_sell.png");

        //public ImageSource IconCountStock { get; set; } = ImageSource.FromResource("MobilebB.Resources.Images.icon_countstock.png");
        //public ImageSource IconKey { get; set; } = ImageSource.FromResource("MobilebB.Resources.Images.icon_key.png");
        //public ImageSource IconEdit { get; set; } = ImageSource.FromResource("MobilebB.Resources.Images.icon_edit.png");
        public ImageSource IconPut { get; set; } = ImageSource.FromFile("icon_put.png");
        public ImageSource IconPick { get; set; } = ImageSource.FromFile("icon_pick.png");
        public ImageSource IconReceive { get; set; } = ImageSource.FromFile("icon_receive.png");


        public OverViewPageModel()
        {

            BtnWhseReceiptClicked = new AsyncRelayCommand(WhseReceiptClicked);
            BtnPutAwayClicked = new AsyncRelayCommand(PutAwayClicked);
            //BtnStockCheck = new AsyncRelayCommand(StockCheck);
            BtnPickListClicked = new AsyncRelayCommand(PickListClicked);
            BtnWhseShipmentClicked = new AsyncRelayCommand(WhseShipmentClicked);
            //BtnReturnClicked = new AsyncRelayCommand(ReturnClicked);
            //CountClicked = new AsyncRelayCommand(CountStock);
            //ReclassClicked = new AsyncRelayCommand(Reclass);
        }
        private async Task WhseReceiptClicked() => await Shell.Current.GoToAsync("WhseReceiptListPage");//await Navigation.PushModalAsync(new WhseReceiptListPage());

        private async Task PutAwayClicked() =>
            await Shell.Current.GoToAsync("WhsePutAwayListPage");

        //private async Task StockCheck() =>
        //    await Shell.Current.GoToAsync("StockCheckPage");

        private async Task PickListClicked() =>
            await Shell.Current.GoToAsync("WhsePickListPage");

        private async Task WhseShipmentClicked() =>
            await Shell.Current.GoToAsync("WhseShipmentListPage");

        //private async Task ReturnClicked() =>
        //    await Shell.Current.GoToAsync("ReturnPage");

        //private async Task CountStock() =>
        //    await Shell.Current.GoToAsync("CountStockPage");

        //private async Task Reclass() =>
        //    await Shell.Current.GoToAsync("ReclassPage");
    }
}
