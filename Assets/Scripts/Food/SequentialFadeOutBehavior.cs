using UnityEngine;


namespace Food
{
    enum FadeOutState
    {
        NotStarted,
        FadingToWhite,
        FadingOut,
        Finished
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class SequentialFadeOutBehavior : MonoBehaviour
    {
        public delegate void FadeOutEvent();
        public FadeOutEvent OnCompleted;

        public float FadeToWhiteTime = 0.1f;
        public AnimationCurve FadeToWhiteCurve;
        
        public float FadeOutTime = 0.2f;
        public AnimationCurve FadeOutCurve;

        public float SequentialDelay = 0.1f;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            // Animation state machine
            switch (currentState)
            {
                case FadeOutState.NotStarted:
                default:
                    break;

                case FadeOutState.FadingToWhite:
                    currentTime += Time.deltaTime;
                    if (currentTime >= FadeToWhiteTime)
                    {
                        currentTime = 0f;
                        currentState = FadeOutState.FadingOut;
                    }
                    else
                    {
                        float t = currentTime / FadeToWhiteTime;
                        float alpha = FadeToWhiteCurve.Evaluate(t);
                        spriteRenderer.color = Color.Lerp(originalColor, Color.white, alpha);
                    }
                    break;

                case FadeOutState.FadingOut:
                    currentTime += Time.deltaTime;
                    if (currentTime >= FadeOutTime)
                    {
                        currentTime = 0f;
                        currentState = FadeOutState.Finished;
                    }
                    else
                    {
                        float t = currentTime / FadeOutTime;
                        float alpha = FadeOutCurve.Evaluate(t);
                        spriteRenderer.color = Color.Lerp(Color.white, Color.clear, alpha);
                    }
                    break;

                case FadeOutState.Finished:
                    currentState = FadeOutState.NotStarted;
                    OnCompleted?.Invoke();
                    break;
            }
        }

        public void FadeOut()
        {
            currentTime = 0f;
            currentState = FadeOutState.FadingToWhite;
        }

        private FadeOutState currentState = FadeOutState.NotStarted;
        private SpriteRenderer spriteRenderer;
        
        private Color originalColor;
        private float currentTime;
    }
}