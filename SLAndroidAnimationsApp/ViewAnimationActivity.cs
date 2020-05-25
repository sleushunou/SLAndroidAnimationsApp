using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views.Animations;
using Android.Widget;

namespace SLAndroidAnimationsApp
{
    [Activity(Label = "ViewAnimationActivity")]
    public class ViewAnimationActivity : Activity
    {
        private ImageView _imageView;
        private Animation _animation;
        private Random _random;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_view_property_animation);

            _imageView = FindViewById<ImageView>(Resource.Id.activity_view_animation_imageView);
            _imageView.Click += OnImageViewClick;

            _animation = AnimationUtils.LoadAnimation(this, Resource.Animation.view_animation);

            _random = new Random();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _imageView.StartAnimation(_animation);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _imageView.ClearAnimation();
        }

        private void OnImageViewClick(object sender, EventArgs e)
        {
            _imageView.SetBackgroundColor(new Color(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255)));
        }
    }
}
