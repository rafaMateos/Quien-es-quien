﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace QuienEsQuien.Converters {

    public class InverseBooleanConverter : IValueConverter {
        #region IValueConverter Members
        
        public object Convert(object value, Type targetType, object parameter, string language) {
            //if (targetType != typeof(bool))
            //    throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
