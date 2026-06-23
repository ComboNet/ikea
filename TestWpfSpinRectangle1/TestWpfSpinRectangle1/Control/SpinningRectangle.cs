using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace TestWpfSpinRectangle1.Control
{
    public class SpinningRectangle : UserControl
    {
        private RotateTransform3D _rotateTransform;
        private AxisAngleRotation3D _rotation;
        private DoubleAnimation _animation;

        static SpinningRectangle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpinningRectangle),
                new FrameworkPropertyMetadata(typeof(SpinningRectangle)));
        }

        public SpinningRectangle()=> Loaded += OnLoaded;

        private void OnLoaded(object sender, RoutedEventArgs e) => SetupAnimation();

        #region Dependency Properties

        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Speed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(double), typeof(SpinningRectangle), new PropertyMetadata(1.0, OnAnimationPropertyChanged));


        public bool IsRunning
        {
            get => (bool)GetValue(IsRunningProperty);
            set => SetValue(IsRunningProperty, value);
        }
        public static readonly DependencyProperty IsRunningProperty =
            DependencyProperty.Register(nameof(IsRunning), typeof(bool), typeof(SpinningRectangle),
                new PropertyMetadata(false, OnAnimationPropertyChanged));

        public int Direction
        {
            get => (int)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(nameof(Direction), typeof(int), typeof(SpinningRectangle),
                new PropertyMetadata(1, OnAnimationPropertyChanged));

        #endregion

        private static void OnAnimationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpinningRectangle control)
                control.SetupAnimation();
        }

        private void SetupAnimation()
        {
            if (_rotation == null)
            {
                _rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
                _rotateTransform = new RotateTransform3D(_rotation);
            }
            if (IsRunning)
            {
                _animation = new DoubleAnimation
                {
                    From = 0,
                    To = Direction > 0 ? 360 : -360,
                    Duration = TimeSpan.FromSeconds(Speed),
                    RepeatBehavior = RepeatBehavior.Forever
                };
                _rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, _animation);
            }
            else
                _rotation.BeginAnimation(AxisAngleRotation3D.AngleProperty, null);
        }
    }
}
