using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace QuienEsQuien.Converters {
    public class converterEsTuTurno : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, string language) {

            bool isTuTurno = (bool) value;

            if (isTuTurno) {
                return "Green";
            } else {
                return "Red";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }

    }
}
