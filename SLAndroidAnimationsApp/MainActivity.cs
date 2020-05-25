using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace SLAndroidAnimationsApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button _drawableAnimationButton;
        private Button _transitionManagerButton;
        private Button _viewAnimationButton;
        private Button _propertyAnimationButton;
        private Button _layoutTransitionAnimationButton;
        private Button _transitionManagerScenesButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _drawableAnimationButton = FindViewById<Button>(Resource.Id.animation_drawable_button);
            _drawableAnimationButton.Click += OnDrawableButtonClick;

            _viewAnimationButton = FindViewById<Button>(Resource.Id.animation_view_button);
            _viewAnimationButton.Click += OnViewAnimationButtonClick;

            _propertyAnimationButton = FindViewById<Button>(Resource.Id.animation_property_button);
            _propertyAnimationButton.Click += OnPropertyAnimationButtonClick;

            _layoutTransitionAnimationButton = FindViewById<Button>(Resource.Id.layout_transition_button);
            _layoutTransitionAnimationButton.Click += OnLayoutTransitionAnimationButtonClick;

            _transitionManagerButton = FindViewById<Button>(Resource.Id.transition_manager_button);
            _transitionManagerButton.Click += OnTransitionManagerButtonClick;

            _transitionManagerScenesButton = FindViewById<Button>(Resource.Id.transition_manager_scenes_button);
            _transitionManagerScenesButton.Click += OnTransitionManagerScenesButtonClick;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void OnDrawableButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(DrawableAnimationActivity)));
        }

        private void OnViewAnimationButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(ViewAnimationActivity)));
        }

        private void OnPropertyAnimationButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(PropertyAnimationActivity)));
        }

        private void OnLayoutTransitionAnimationButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(LayoutTransitionActivity)));
        }

        private void OnTransitionManagerButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(TransitionManagerActivity)));
        }

        private void OnTransitionManagerScenesButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(this, typeof(TransitionManagerScenesActivity)));
        }
    }
}