using FrontEnd.ExtensionMethods;
using MeterApp.Controller;
using System.Windows.Controls;

namespace MeterApp.View
{
    public partial class ReadingList : Page
    {
        public ReadingList()
        {
            InitializeComponent();
            this.SetController(new ReadingListController());
        }
    }
}