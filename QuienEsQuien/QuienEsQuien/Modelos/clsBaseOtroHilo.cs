using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace QuienEsQuien.Modelos
{
    public class clsBaseOtroHilo : INotifyPropertyChanged {
        public  event PropertyChangedEventHandler PropertyChanged;

        protected async virtual void NotifyPropertyChangedHilo(string propertyName = null) {

            await Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });

            

        }

       
    }
}
