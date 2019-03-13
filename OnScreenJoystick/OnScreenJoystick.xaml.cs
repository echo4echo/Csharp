using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace NewUI.MainView
{
    /// <summary>Interaction logic for Joystick.xaml</summary>
    public partial class OnScreenJoystick : UserControl
    {
        //Additional control for special use
        private void SetFocus(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).Focus();
        }

        /// <summary>Current angle in degrees from 0 to 360</summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(OnScreenJoystick), null);

        /// <summary>Current distance (or "power"), from 0 to 100</summary>
        public static readonly DependencyProperty DistanceProperty =
            DependencyProperty.Register("Distance", typeof(double), typeof(OnScreenJoystick), null);

        /// <summary>How often should be raised StickMove event in degrees</summary>
        public static readonly DependencyProperty AngleStepProperty =
            DependencyProperty.Register("AngleStep", typeof(double), typeof(OnScreenJoystick), new PropertyMetadata(5.0));

        /// <summary>How often should be raised StickMove event in distance units</summary>
        public static readonly DependencyProperty DistanceStepProperty =
            DependencyProperty.Register("DistanceStep", typeof(double), typeof(OnScreenJoystick), new PropertyMetadata(5.0));


        /// <summary>Current click proxy 1 for up, 2 for down, 4 for right, 8 for left
        public static readonly DependencyProperty ClickProxyProperty =
            DependencyProperty.Register("ClickProxy", typeof(int), typeof(OnScreenJoystick), null);
        /* Unstable - needs work */
        ///// <summary>Indicates whether the joystick knob resets its place after being released</summary>
        //public static readonly DependencyProperty ResetKnobAfterReleaseProperty =
        //    DependencyProperty.Register(nameof(ResetKnobAfterRelease), typeof(bool), typeof(VirtualJoystick), new PropertyMetadata(true));

        /// <summary>Current angle in degrees from 0 to 360</summary>
        public double Angle
        {
            get { return Convert.ToDouble(GetValue(AngleProperty)); }
            private set { SetValue(AngleProperty, value); }
        }

        /// <summary>current distance (or "power"), from 0 to 100</summary>
        public double Distance
        {
            get { return Convert.ToDouble(GetValue(DistanceProperty)); }
            private set { SetValue(DistanceProperty, value); }
        }

        /// <summary>How often should be raised StickMove event in degrees</summary>
        public double AngleStep
        {
            get { return Convert.ToDouble(GetValue(AngleStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 90) value = 90;
                SetValue(AngleStepProperty, Math.Round(value));
            }
        }

        /// <summary>How often should be raised StickMove event in distance units</summary>
        public double DistanceStep
        {
            get { return Convert.ToDouble(GetValue(DistanceStepProperty)); }
            set
            {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(DistanceStepProperty, value);
            }
        }

        /// <summary>How often should be raised StickMove event in distance units</summary>
        public int ClickProxy
        {
            get { return Convert.ToInt16(GetValue(ClickProxyProperty)); }
            set
            {
                if (value < 1) value = 0; else if (value > 100) value = 100;
                SetValue(ClickProxyProperty, value);
            }
        }

        /// <summary>Indicates whether the joystick knob resets its place after being released</summary>
        //public bool ResetKnobAfterRelease
        //{
        //    get { return Convert.ToBoolean(GetValue(ResetKnobAfterReleaseProperty)); }
        //    set { SetValue(ResetKnobAfterReleaseProperty, value); }
        //}

        /// <summary>Delegate holding data for joystick state change</summary>
        /// <param name="sender">The object that fired the event</param>
        /// <param name="args">Holds new values for angle and distance</param>
        public delegate void OnScreenJoystickEventHandler(OnScreenJoystick sender, VirtualJoystickEventArgs args);

        /// <summary>Delegate for joystick events that hold no data</summary>
        /// <param name="sender">The object that fired the event</param>
        public delegate void EmptyJoystickEventHandler(OnScreenJoystick sender);

        /// <summary>This event fires whenever the joystick moves</summary>
        public event OnScreenJoystickEventHandler Moved;


        /// <summary>Delegate holding data for joystick state change</summary>
        /// <param name="sender">The object that fired the event</param>
        /// <param name="args">Holds new values for angle and distance</param>
        public delegate void OnScreenButtonClickEventHandler(OnScreenJoystick sender, VirtualJoystickClickEventArgs args);

        /// <summary>This event fires whenever the direction Button is click
        public event OnScreenButtonClickEventHandler StepMoved;

        /// <summary>This event fires once the joystick is released and its position is reset</summary>
        public event EmptyJoystickEventHandler Released;

        /// <summary>This event fires once the joystick is captured</summary>
        public event EmptyJoystickEventHandler Captured;

        private Point _startPos;
        private double _prevAngle, _prevDistance;
        private readonly Storyboard centerKnob;

        public OnScreenJoystick()
        {
            InitializeComponent();

            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            Knob.MouseMove += Knob_MouseMove;

            //Step move
            UpButton.MouseLeftButtonDown += UpButton_MouseLeftButtonDown;
            DownButton.MouseLeftButtonDown += DownButton_MouseLeftButtonDown;
            RightButton.MouseLeftButtonDown += RightButton_MouseLeftButtonDown;
            LeftButton.MouseLeftButtonDown += LeftButtonButton_MouseLeftButtonDown;



            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPos = e.GetPosition(Base);
            _prevAngle = _prevDistance = 0;
            if (Captured != null)
                
                Captured.Invoke(this);

            //Captured?.Invoke(this);
            Knob.CaptureMouse();

            centerKnob.Stop();
        }

        //Step movement
        private void UpButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 1 for up
            ClickProxy = 1;

            if(StepMoved != null)
            {
                StepMoved.Invoke(this, new VirtualJoystickClickEventArgs { ClickProxy = ClickProxy });
            }
        }

        private void DownButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 2 for down
            ClickProxy = 2;

            if (StepMoved != null)
            {
                StepMoved.Invoke(this, new VirtualJoystickClickEventArgs { ClickProxy = ClickProxy });
            }
        }

        private void RightButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 4 for right
            ClickProxy = 4;

            if (StepMoved != null)
            {
                StepMoved.Invoke(this, new VirtualJoystickClickEventArgs { ClickProxy = ClickProxy });
            }
        }

        private void LeftButtonButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 8 for left
            ClickProxy = 8;

            if (StepMoved != null)
            {
                StepMoved.Invoke(this, new VirtualJoystickClickEventArgs { ClickProxy = ClickProxy });
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Knob.IsMouseCaptured) return;

            KnobBase.Focus();

            Point newPos = e.GetPosition(Base);

            Point deltaPos = new Point(newPos.X - _startPos.X, newPos.Y - _startPos.Y);

            double angle = Math.Atan2(deltaPos.Y, deltaPos.X) * 180 / Math.PI;
            if (angle > 0)
                angle += 90;
            else
            {
                angle = 270 + (180 + angle);
                if (angle >= 360) angle -= 360;
            }

            double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y) / 135 * 100);
            if (distance <= 100)
            {
                Angle = angle;
                Distance = distance;

                knobPosition.X = deltaPos.X;
                knobPosition.Y = deltaPos.Y;

                if (Moved == null ||
                    (!(Math.Abs(_prevAngle - angle) > AngleStep) && !(Math.Abs(_prevDistance - distance) > DistanceStep)))
                    return;
                if (Moved !=null)
                    Moved.Invoke(this, new VirtualJoystickEventArgs { Angle = Angle, Distance = Distance });
                _prevAngle = Angle;
                _prevDistance = Distance;
            }
        }

        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
            Angle = Distance = _prevAngle = _prevDistance = 0;
            if (Moved != null)
                Moved.Invoke(this, new VirtualJoystickEventArgs { Angle = Angle, Distance = Distance });
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
            Angle = Distance = _prevAngle = _prevDistance = 0;
            if (Released!=null)
                Released.Invoke(this);
        }
    }
}