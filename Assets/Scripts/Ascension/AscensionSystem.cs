using System.Collections.Generic;
using Board;
using UnityEngine;

namespace Ascension
{
    public class AscensionSystem : GameBoardSubsystem
    {
        public void Awake()
        {
           var settings = Resources.Load<AscensionSettings>(AscensionSettings.ResourcePath);
           if (!settings)
           {
                Debug.LogWarning("[Droppings.AscensionSystem] Unable to ascension settings at " +
                                 $"{AscensionSettings.ResourcePath}, using default settings instead");
                settings = ScriptableObject.CreateInstance<AscensionSettings>();
           }

           pointsPerTier = new List<int>(settings.PointsPerTier);
           currentPoints = CreateCurrentPoints(pointsPerTier);
        }

        public delegate void PointEvent(int numPoints);

        /**
         * \brief Called every time the ascension point level changes.
         * \note numPoints is the current amount of points for the current tier
         */
        public PointEvent OnPointsAdded;
        
        /**
         * \brief Called every time a glyph is completed.
         * \not numPoints is the tier which was completed
         */
        public PointEvent OnGlyphCompleted;

        /** The current ascension points counting toward the next tier-1 glyph */
        public int AscensionPoints
        {
            // Ascension points are kept track in the first index
            get
            {
                if (currentPoints.Count != pointsPerTier.Count)
                {
                    currentPoints = CreateCurrentPoints(pointsPerTier);
                }
                return currentPoints.Count > 0? currentPoints[0] : 0;
            }
            set
            {
                if (value == currentPoints[0]) { return; }
                
                // Make sure that overflow points go toward next glyph
                currentPoints[0] = value % pointsPerTier[0];

                // broadcast glyph and point events accordingly
                if (value >= pointsPerTier[0])
                {
                    currentPoints[1] += 1;
                    Debug.Log("Completed a glyph!");
                    OnGlyphCompleted?.Invoke(currentPoints[1] >= pointsPerTier[1] ? 2 : 1);
                }
                Debug.Log("Added a point!");
                OnPointsAdded?.Invoke(currentPoints[0]);
            }
        }

        /** The total number of ascension points gained, glyphs included */
        public int TotalPoints
        {
            get
            {
                int sum = 0;
                int currentTierPoints = 1;

                for (int i = 0; i < pointsPerTier.Count; i++)
                {
                    sum += currentPoints[i] * currentTierPoints;
                    currentTierPoints *= pointsPerTier[i];
                }
                return sum;
            }
        }

        private static List<int> CreateCurrentPoints(List<int> newPointsPerTier)
        {
            var newCurrentPoints = new List<int>();
            foreach (int _ in newPointsPerTier) { newCurrentPoints.Add(0); }
            return newCurrentPoints;
        }

        private List<int> pointsPerTier = new();
        private List<int> currentPoints = new();
    }
}