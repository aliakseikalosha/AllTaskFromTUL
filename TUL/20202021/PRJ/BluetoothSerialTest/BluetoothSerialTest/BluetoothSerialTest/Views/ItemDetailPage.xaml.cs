using BluetoothSerialTest.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BluetoothSerialTest.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}