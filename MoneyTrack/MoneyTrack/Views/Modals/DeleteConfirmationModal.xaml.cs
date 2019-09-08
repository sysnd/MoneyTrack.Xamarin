using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoneyTrack.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeleteConfirmationModal : ContentPage
    {
        public DeleteConfirmationModal()
        {
            InitializeComponent();
        }
    }
}