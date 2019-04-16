using System.Windows;

namespace OrbitalSimulator.AttatchedProperties
{
    public abstract class AbstractDependancyProperty<T, Parent>
    {
        /// <summary>
        /// Register a new Dependancy Property to be used bind to custom UserControl Properties
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(T), typeof(AbstractDependancyProperty<T, Parent>));

        /// <summary>
        /// Getter for property
        /// </summary>
        /// <param name="dependencyObject">The Object that is parent to property</param>
        /// <returns></returns>
        public static T GetValue(DependencyObject dependencyObject) => (T)dependencyObject.GetValue(ValueProperty);

        /// <summary>
        /// Setter for the property
        /// </summary>
        /// <param name="dependencyObject">The object parent too the property</param>
        /// <param name="value"></param>
        public static void SetValue(DependencyObject dependencyObject, T value) => dependencyObject.SetValue(ValueProperty, value);
    }
}



