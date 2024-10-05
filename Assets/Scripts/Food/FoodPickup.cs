using System;
using UnityEngine;

namespace Food
{
    public class FoodPickup : MonoBehaviour
    {
        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            transform.position = Vector3.zero;
        }
    }
}