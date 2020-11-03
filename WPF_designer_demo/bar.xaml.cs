using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WPF_designer_demo
{
    /// <summary>
    /// Interaction logic for bar.xaml
    /// </summary>
    public partial class bar : UserControl
    {
        DispatcherTimer dispatcherTimer;

        public bar()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        public double ProcessValue
        {
            get { return (double)GetValue(ProcessValueProperty); }
            set { SetValue(ProcessValueProperty, value); }
        }
        public static readonly DependencyProperty ProcessValueProperty =
            DependencyProperty.Register("ProcessValue", typeof(double), typeof(bar), new PropertyMetadata(0.0, new PropertyChangedCallback(OnProcessValueChanged)));

        private static void OnProcessValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            bar b = (bar)d;
            b.NeedleHeight = b.normalizeHeight((double)e.NewValue);
        }

        double min = 0.0;
        double max = 400.00;

        double normalizeHeight(double processvalue)
        {
            double span = max - min;
            double normalized = (processvalue / span) * dynamic_range.Height;
            return normalized;
        }

        public double NeedleHeight
        {
            get { return (double)GetValue(NeedleHeightProperty); }
            set { SetValue(NeedleHeightProperty, value); }
        }
        public static readonly DependencyProperty NeedleHeightProperty =
            DependencyProperty.Register("NeedleHeight", typeof(double), typeof(bar), new PropertyMetadata(0.0));


        void dispatcherTimer_Tick(object sender, object e)
        {
            if (ProcessValue < max)
                ProcessValue++;
        }
    }
}
