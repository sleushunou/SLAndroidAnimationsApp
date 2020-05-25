using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Widget;

namespace SLAndroidAnimationsApp
{
    [Activity(Label = "DrawableAnimationActivity")]
    public class DrawableAnimationActivity : Activity
    {
        private ImageView _imageView;
        private AnimationDrawable _alarmDrawable;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_drawable_animation);

            _alarmDrawable = (AnimationDrawable)Resources.GetDrawable(Resource.Drawable.alarm_animation_24);

            _imageView = FindViewById<ImageView>(Resource.Id.activity_drawable_animation_imageView);
            _imageView.SetImageDrawable(_alarmDrawable);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _alarmDrawable.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _alarmDrawable.Stop();
        }
    }
}
