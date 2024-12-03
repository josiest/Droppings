using Pi.Subsystems;
using UnityEngine;

namespace Board
{
    public class GameBoardSystem : SubsystemBase
    {
        public static T FindOrRegister<T>() where T : GameBoardSubsystem
        {
            if (!instance) { Debug.LogError("[Droppings.GameBoardSystem] Game Board System instance doesn't exist"); }
            var subsystem = instance?.GetComponent<T>();
            return subsystem? subsystem : instance?.gameObject.AddComponent<T>();
        }
        public static T Find<T>() where T : GameBoardSubsystem
        {
            return instance?.GetComponent<T>();
        }

        public static void RegisterBoard(GameBoard newBoard)
        {
            var system = GetOrCreateInstance();
            if (!system) { return; }

            system.Board = newBoard;
            system.RegisterSubsystems();
        }

        public static GameBoard CurrentBoard => instance?.Board;
        public GameBoard Board { get; private set; }

        private static GameBoardSystem GetOrCreateInstance()
        {
            if (instance) { return instance; }
            var instanceObject = new GameObject("GameBoardSystem", typeof(GameBoardSystem));
            instance = instanceObject.GetComponent<GameBoardSystem>();
            return instance;
        }

        //
        // Unity Events
        //

        private void Awake()
        {
            if (instance && instance != this) { Destroy(this); }
        }

        private void RegisterSubsystems()
        {
            foreach (var subsystemType in SubsystemLocators.GetAllSubsystemTypes<GameBoardSubsystem>())
            {
                if (!GetComponent(subsystemType)) { gameObject.AddComponent(subsystemType); }
            }
        }

        private void OnDestroy()
        {
            if (instance == this) { instance = null; }
        }

        private static GameBoardSystem instance;
    }
}