using UnityEngine;


namespace Food
{
    [RequireComponent(typeof(Animator))]
    public class SequentialFadeOutBehavior : MonoBehaviour
    {
        public delegate void FadeOutEvent();
        public FadeOutEvent OnCompleted;
        public FadeOutEvent OnStagger;

        public float FadeOutTime = 0.2f;

        public float SequentialDelay = 0.05f;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            anim.speed = 1f / Mathf.Max(0.001f, FadeOutTime);
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
        }

        public void FadeOut()
        {
            staggerTime = 0f;
            anim.enabled = true;
        }

        public void CompleteAnimation()
        {
            OnCompleted?.Invoke();
        }

        private SpriteRenderer spriteRenderer;
        private Animator anim;
        private float staggerTime = -1f;
    }
}