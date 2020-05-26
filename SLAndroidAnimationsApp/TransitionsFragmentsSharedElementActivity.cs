using System;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;

namespace SLAndroidAnimationsApp.Resources
{
    [Activity(Label = "TransitionsFragmentsSharedElementActivity")]
    public class TransitionsFragmentsSharedElementActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_transition_fragments_shared_element);

            SupportFragmentManager
                .BeginTransaction()
                .Add(Resource.Id.frameLayout1, new Fragment1())
                .Commit();
        }
    }

    public class Fragment1 : Fragment
    {
        private Button _navigateButton;
        private ImageView _imageView;
        private TextView _textView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_1, container, false);

            _navigateButton = view.FindViewById<Button>(Resource.Id.button1);
            _navigateButton.Click += OnNavigateButtonClick;

            _imageView = view.FindViewById<ImageView>(Resource.Id.imageView1);

            _textView = view.FindViewById<TextView>(Resource.Id.textView1);

            return view;
        }

        public override void OnDestroyView()
        {
            _navigateButton.Click -= OnNavigateButtonClick;
            base.OnDestroyView();
        }

        private void OnNavigateButtonClick(object sender, EventArgs e)
        {
            FragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.frameLayout1, new Fragment2
                {
                    SharedElementEnterTransition = new ChangeBounds(),
                    SharedElementReturnTransition = new ChangeBounds()
                })
                .AddSharedElement(_imageView, "transition_image")
                .AddSharedElement(_textView, "transition_title")
                .AddToBackStack(null)
                .Commit();
        }
    }

    public class Fragment2 : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_2, container, false);

            return view;
        }
    }
}
