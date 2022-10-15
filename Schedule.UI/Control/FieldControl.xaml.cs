using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Schedule.UI.Control
{
    /// <summary>
    /// Interaction logic for FieldControl.xaml
    /// </summary>
    public partial class FieldControl : UserControl
    {
        public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register(nameof(FieldName), typeof(string), typeof(FieldControl), new PropertyMetadata(""));
        public static readonly DependencyProperty FieldValueProperty = DependencyProperty.Register(nameof(FieldValue), typeof(string), typeof(FieldControl), new PropertyMetadata(""));

        [Bindable(true)]
        public string FieldName
        {
            get => (string)GetValue(FieldNameProperty);
            set => SetValue(FieldNameProperty, value);
        }

        [Bindable(true)]
        public string FieldValue
        {
            get => (string) GetValue(FieldValueProperty);
            set => SetValue(FieldValueProperty, value);
        }

        public FieldControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
