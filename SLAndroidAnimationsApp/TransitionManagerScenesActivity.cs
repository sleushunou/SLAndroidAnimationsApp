
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Transitions;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;

namespace SLAndroidAnimationsApp
{
    [Activity(Label = "TransitionManagerScenesActivity")]
    public class TransitionManagerScenesActivity : Activity
    {
        private ViewGroup _sceneRoot;
        private Button _animateButton;
        private Scene _scene1, _scene2;
        private int _currentSceneNumber = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_transition_manager_scenes);

            _animateButton = FindViewById<Button>(Resource.Id.animate_button);
            _animateButton.Click += AnimateButtonClick;

            _sceneRoot = FindViewById<ViewGroup>(Resource.Id.scene_root);

            _scene1 = Scene.GetSceneForLayout(_sceneRoot, Resource.Layout.scene_1, this);
            _scene2 = Scene.GetSceneForLayout(_sceneRoot, Resource.Layout.scene_2, this);
        }

        private void AnimateButtonClick(object sender, EventArgs e)
        {
            _currentSceneNumber = _currentSceneNumber == 1 ? 2 : 1;

            var scene = _currentSceneNumber switch
            {
                1 => _scene1,
                2 => _scene2,
                _ => throw new NotImplementedException()
            };

            var set = new TransitionSet();
            set.AddTransition(new Explode());
            set.AddTransition(new ChangeBounds());
            set.AddTransition(new CustomTransition());
            set.SetOrdering(TransitionOrdering.Together);
            set.SetDuration(500);
            set.SetInterpolator(new DecelerateInterpolator());

            TransitionManager.Go(scene, set);
        }

        private class CustomTransition : Transition
        {
            private const string PROPNAME_BACKGROUND = "com.example.android.customtransition:CustomTransition:background";

            public CustomTransition() : base()
            {
            }

            public CustomTransition(IntPtr pointer, JniHandleOwnership transfer) : base(pointer, transfer)
            {
            }

            public override void CaptureStartValues(TransitionValues transitionValues)
            {
                CaptureValues(transitionValues);
            }

            public override void CaptureEndValues(TransitionValues transitionValues)
            {
                CaptureValues(transitionValues);
            }

            public override Animator CreateAnimator(ViewGroup sceneRoot, TransitionValues startValues, TransitionValues endValues)
            {
                if (null == startValues || null == endValues)
                {
                    return null;
                }

                var view = endValues.View;

                var startBackground = (Drawable)startValues.Values[PROPNAME_BACKGROUND];
                var endBackground = (Drawable)endValues.Values[PROPNAME_BACKGROUND];

                if (startBackground is ColorDrawable && endBackground is ColorDrawable)
                {
                    var startColor = (ColorDrawable)startBackground;
                    var endColor = (ColorDrawable)endBackground;
                    int colorStart = Color.Rgb(startColor.Color.R, startColor.Color.G, startColor.Color.B);
                    int colorEnd = Color.Rgb(endColor.Color.R, endColor.Color.G, endColor.Color.B);

                    if (startColor.Color != endColor.Color)
                    {
                        var animator = ValueAnimator.OfObject(new ArgbEvaluator(), colorStart, colorEnd);
                        animator.Update += (object sender, ValueAnimator.AnimatorUpdateEventArgs e) =>
                        {
                            var value = (int)animator.AnimatedValue;
                            if (!value.Equals(null))
                            {
                                view.SetBackgroundColor(new Color(value));
                            }
                        };

                        return animator;
                    }
                }
                return null;

            }

            private void CaptureValues(TransitionValues transitionValues)
            {
                var view = transitionValues.View;
                transitionValues.Values[PROPNAME_BACKGROUND] = view.Background;
            }
        }
    }
}
