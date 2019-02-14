using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace QuienEsQuien.Converters {
    public class converterEstadoCarta :IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {

            double opacidad = 1;

            bool isBajada = (bool)value;
            if (isBajada) {
                opacidad = 0.5;
            } else {
                opacidad = 1;
            }

            return opacidad;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
