using Food;
using Scene;
using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(Pickup))]
    public class DivineTally : MonoBehaviour
    {
        [SerializeField] private int score = 5;
        private DivineAbacus _divineAbacus;

        public void Start()
        {
            _divineAbacus = SceneSubsystems.Find<DivineAbacus>();
            var pickupComponent = GetComponent<Pickup>();
            pickupComponent.OnConsumed += OnConsumed;
        }

        private void OnConsumed()
        {
            _divineAbacus.Score += score;
        }
    }
    
    
}
