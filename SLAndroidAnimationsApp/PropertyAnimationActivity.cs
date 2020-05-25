
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [Activity(Label = "PropertyAnimationActivity")]
    public class PropertyAnimationActivity : Activity
    {
        private ImageView _imageView;
        private Animator _animator;
        private Random _random;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_view_property_animation);

            _imageView = FindViewById<ImageView>(Resource.Id.activity_view_animation_imageView);
            _imageView.Click += OnImageViewClick;

            _animator = AnimatorInflater.LoadAnimator(this, Resource.Animator.property_animation);
            _animator.SetTarget(_imageView);

            _random = new Random();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _animator.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _animator.Cancel();
        }

        private void OnImageViewClick(object sender, EventArgs e)
        {
            _imageView.SetBackgroundColor(new Color(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255)));
        }
    }
}
