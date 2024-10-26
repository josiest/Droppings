using Food;
using Subsystems;
using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(Pickup))]
    public class DivineTally : MonoBehaviour
    {
        [SerializeField] private int score = 5;
        private DivineAbacus divineAbacus;

        public void Start()
        {
            divineAbacus = SceneSubsystemLocator.Find<DivineAbacus>();
            var pickupComponent = GetComponent<Pickup>();
            pickupComponent.OnConsumed += OnConsumed;
        }

        private void OnConsumed()
        {
            divineAbacus.Score += score;
        }
    }
    
    
}
