using System.Collections.Generic;
using Board;
using Game;
using Score;

namespace Ascension
{
    public class AscensionSystem : GameBoardSubsystem
    {
        private void Awake()
        {
            var settings = AscensionSettings.LoadOrCreateDefault();
            pointsPerTier = new List<int>(settings.PointsPerTier);
            currentPoints = CreateCurrentPoints(pointsPerTier);
        }

        private void Start()
        {
            abacus = GameBoardSystem.FindOrRegister<DivineAbacus>();
            abacus.OnScoreChanged += OnScoreChanged;

            var resetSystem = GameBoardSystem.FindOrRegister<ResetSystem>();
            resetSystem.OnReset += Reset;
        }

        public delegate void PointEvent();

        /** * \brief Called every time a tally is added to the score */
        public PointEvent OnPointAdded;
        
        /** * \brief Called every time a glyph is completed. */
        public PointEvent OnGlyphCompleted;

        /** \brief Called every time the score is reset - namely when the game is reset */
        public PointEvent OnScoreReset;

        private void OnScoreChanged(int newScore)
        {
            // need to refactor, but for now assume new score always changes by two rules
            // 1. A point was added
            // 2. The score was reset

            if (newScore == 0) { Reset(); }
            else { AscensionPoints += 1; }
        }

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
                OnPointAdded?.Invoke();

                // broadcast glyph and point events accordingly
                if (value >= pointsPerTier[0])
                {
                    currentPoints[1] += 1;
                    OnGlyphCompleted?.Invoke();
                }
            }
        }

        public int PointsForTier(int tier)
        {
            return tier < pointsPerTier.Count ? pointsPerTier[tier] : 0;
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

        private void Reset()
        {
            for (int i = 0; i < currentPoints.Count; ++i)
            {
                currentPoints[i] = 0;
            }
            OnScoreReset?.Invoke();
        }

        private DivineAbacus abacus;
        private List<int> pointsPerTier = new();
        private List<int> currentPoints = new();
    }
}