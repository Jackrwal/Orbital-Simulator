using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWOrbitalSimulatorPortable.ViewModels
{
    public abstract class UserControlViewModel : NotifyingViewModel
    {
        double _DimensionsToWindowSizeWighting;

        public UserControlViewModel(double dimensionsToWindowSizeWighting)
        {
            _DimensionsToWindowSizeWighting = dimensionsToWindowSizeWighting;
        }

        int Width  => (int)(_DimensionsToWindowSizeWighting * MasterViewModel.Instance.WindowWidth);
        int Height => (int)(_DimensionsToWindowSizeWighting * MasterViewModel.Instance.WindowWidth);

        public class ValidatedField<T>
        {
            T Value;

            public T GetValue() => Value;

            public void SetValue(object value)
            {
                Value = validateQuantity(value, typeof(T), out object Val) ? (T)Val : Value;
            }

            static private bool validateQuantity(object quantity, Type desiredType, out object value)
            {
                try
                {
                    value = Convert.ChangeType(quantity, desiredType);
                }
                catch (FormatException)
                {
                    value = null;
                    return false;
                }
                return true;
            }
        }
    }
}
