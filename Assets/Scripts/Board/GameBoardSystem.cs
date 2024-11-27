using System.Reflection;
using Pi.Subsystems;
using UnityEngine;

namespace Board
{
    public class GameBoardSystem : SubsystemBase
    {
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

        private void RegisterSubsystems()
        {
            foreach (var subsystemType in SubsystemLocators.GetAllSubsystemTypes<GameBoardSubsystem>())
            {
                var subsystem = gameObject.AddComponent(subsystemType);
                var onRegisterMethod = subsystemType.GetMethod("OnRegisterGameBoard", BindingFlags.Instance);
                if (onRegisterMethod == null) { continue; }

                var methodParams = onRegisterMethod.GetParameters();
                if (methodParams.Length == 1 && methodParams[0].ParameterType == typeof(GameBoard))
                {
                    onRegisterMethod.Invoke(subsystem, new[] { (object)Board });
                }
                else if (methodParams.Length == 0)
                {
                    onRegisterMethod.Invoke(subsystem, null);
                }
            }
        }

        private void OnDestroy()
        {
            if (instance == this) { instance = null; }
        }

        private static GameBoardSystem instance;
    }
}