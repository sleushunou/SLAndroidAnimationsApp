
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Transitions;
using Android.Views;
using Android.Widget;

namespace SLAndroidAnimationsApp
{
    [Activity(Label = "TransitionManagerActivity")]
    public class TransitionManagerActivity : Activity
    {
        private Button _addButton;
        private Button _removeButton;
        private LinearLayout _viewGroup;
        private Timer _timer;
        private Random _random;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_layout_transition);

            _addButton = FindViewById<Button>(Resource.Id.activity_layout_transition_button_add);
            _removeButton = FindViewById<Button>(Resource.Id.activity_layout_transition_button_remove);

            _viewGroup = FindViewById<LinearLayout>(Resource.Id.activity_layout_transition_viewgroup);

            _addButton.Click += AddButtonClick;
            _removeButton.Click += RemoveButtonClick;

            _random = new Random();

            _timer = new Timer(1000);
            _timer.Elapsed += OnTimerElapsed;
        }

        protected override void OnResume()
        {
            base.OnResume();
            _timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _timer.Stop();
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            var button = new Button(this) { Text = "Button" };
            button.SetBackgroundColor(new Color(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255)));

            TransitionManager.BeginDelayedTransition(_viewGroup, new Slide((int)GravityFlags.Bottom));

            _viewGroup.AddView(button);
        }

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            var index = _viewGroup.ChildCount - 1;
            if (index > -1)
            {
                TransitionManager.BeginDelayedTransition(_viewGroup, new Slide((int)GravityFlags.Top));
                _viewGroup.RemoveViewAt(index);
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                for (int i = 0; i < _viewGroup.ChildCount; i++)
                {
                    var child = _viewGroup.GetChildAt(i);
                    var layoutParams = child.LayoutParameters;
                    layoutParams.Height = (int)(_random.Next(50, 100) * Resources.DisplayMetrics.Density + 0.5f);

                    TransitionManager.BeginDelayedTransition(_viewGroup, new ChangeBounds());
                    child.LayoutParameters = layoutParams;
                }
            });
        }
    }
}
