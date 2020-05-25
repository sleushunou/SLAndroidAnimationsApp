
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SLAndroidAnimationsApp
{
    [Activity(Label = "LayoutTransitionActivity")]
    public class LayoutTransitionActivity : Activity
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
            _viewGroup.LayoutTransition.EnableTransitionType(LayoutTransitionType.Changing);

            _viewGroup.LayoutTransition.SetAnimator(LayoutTransitionType.Appearing, ObjectAnimator.OfFloat(null, "rotationY", 0f, 90f, 180f, 270f, 359.9999f));
            _viewGroup.LayoutTransition.SetAnimator(LayoutTransitionType.Disappearing, ObjectAnimator.OfFloat(null, "rotationX", 0f, 90f, 180f, 270f, 359.9999f));

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
            _viewGroup.AddView(button);
        }

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            var index = _viewGroup.ChildCount - 1;
            if (index > -1)
            {
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
                    child.LayoutParameters = layoutParams;
                }
            });
        }
    }
}
