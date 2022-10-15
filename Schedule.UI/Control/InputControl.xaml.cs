using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Schedule.UI.Control
{
    /// <summary>
    /// Interaction logic for InputControl.xaml
    /// </summary>
    public partial class InputControl : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(nameof(LabelText), typeof(string), typeof(InputControl), new PropertyMetadata(""));
        public static readonly DependencyProperty VerticalTextAlignmentProperty = DependencyProperty.Register(nameof(VerticalTextAlignment), typeof(VerticalAlignment), typeof(InputControl), new PropertyMetadata(VerticalAlignment.Center));
        public static readonly DependencyProperty HorizontalTextAlignmentProperty = DependencyProperty.Register(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(InputControl), new PropertyMetadata(TextAlignment.Center));
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type), typeof(InputType), typeof(InputControl), new PropertyMetadata(InputType.Text));
        public static readonly DependencyProperty InputProperty = DependencyProperty.Register(nameof(Input), typeof(string), typeof(InputControl), new PropertyMetadata(""));

        [Bindable(true)]
        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }
        [Bindable(true)]

        public VerticalAlignment VerticalTextAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        [Bindable(true)]
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        [Bindable(true)]
        public InputType Type
        {
            get => (InputType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        [Bindable(true)]
        public string Input
        {
            get => (string)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public InputControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            Input = passwordBox.Password;
        }
    }

    public enum InputType
    {
        Text = 0,
        Password = 1
    }
}