using UnityEngine;


namespace Food
{
    enum FadeOutState
    {
        NotStarted,
        FadingToWhite,
        FadingOut
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class SequentialFadeOutBehavior : MonoBehaviour
    {
        public delegate void FadeOutEvent();
        public FadeOutEvent OnCompleted;
        public FadeOutEvent OnStagger;

        public float FadeToWhiteTime = 0.1f;
        public AnimationCurve FadeToWhiteCurve;
        
        public float FadeOutTime = 0.2f;
        public AnimationCurve FadeOutCurve;

        public float SequentialDelay = 0.05f;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (staggerTime >= SequentialDelay)
            {
                staggerTime = -1f;
                OnStagger?.Invoke();
            }
            if (staggerTime >= 0f)
            {
                staggerTime += Time.deltaTime;
            }

            // Animation state machine
            switch (currentState)
            {
                case FadeOutState.NotStarted:
                default:
                    break;

                case FadeOutState.FadingToWhite:
                    if (animationTime >= FadeToWhiteTime)
                    {
                        animationTime = 0f;
                        currentState = FadeOutState.FadingOut;
                    }
                    else
                    {
                        float t = animationTime / FadeToWhiteTime;
                        float alpha = FadeToWhiteCurve.Evaluate(t);
                        spriteRenderer.color = Color.Lerp(originalColor, Color.white, alpha);
                    }
                    animationTime += Time.deltaTime;
                    break;

                case FadeOutState.FadingOut:
                    if (animationTime >= FadeOutTime)
                    {
                        currentState = FadeOutState.NotStarted;
                        animationTime = -1f;
                        OnCompleted?.Invoke();
                    }
                    else
                    {
                        float t = animationTime / FadeOutTime;
                        float alpha = FadeOutCurve.Evaluate(t);
                        spriteRenderer.color = Color.Lerp(Color.white, Color.clear, alpha);
                    }
                    animationTime += Time.deltaTime;
                    break;
            }
        }

        public void FadeOut()
        {
            animationTime = 0f;
            staggerTime = 0f;
            currentState = FadeOutState.FadingToWhite;
        }

        private FadeOutState currentState = FadeOutState.NotStarted;
        private SpriteRenderer spriteRenderer;
        
        private Color originalColor;
        private float animationTime = -1f;
        private float staggerTime = -1f;
    }
}