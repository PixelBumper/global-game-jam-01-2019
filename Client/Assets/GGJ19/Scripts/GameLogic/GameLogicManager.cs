using GeneratedServerAPI;
using HalfBlind.ScriptableVariables;
using UnityEditor;
using UnityEngine;

namespace GGJ19.Scripts.GameLogic
{
    [CreateAssetMenu(fileName = nameof(GameLogicManager), menuName = "Tools/CreateGameLogicManager")]
    public class GameLogicManager : ScriptableSingleton<GameLogicManager>
    {
        public string PlayerId => SystemInfo.deviceUniqueIdentifier;

        [Header("Game Events")]
        public ScriptableGameEvent onPlayerCountChanged;
        public ScriptableGameEvent onEmojisChanged;
        public ScriptableGameEvent onSelectableRoleDisabledChanged;
        public ScriptableGameEvent onFailedThreatsChanged;
        public ScriptableGameEvent onThreatsChanged;
        public ScriptableGameEvent onTurnChanged;
        public ScriptableGameEvent onGameWon;
        public ScriptableGameEvent onGameLost;

        [Header("Room Events")]
        public ScriptableGameEvent onRoomInfoChanged;
        
        [Header("Room Variables")]
        public GlobalString serverRoomName;
        public GlobalString myPlayerId;

        private Playing previousPlayingState;
        private Room previousRoomState;
        private Playing currentPlayingState;
        private Room currentRoomState;

        private void Awake()
        {
            myPlayerId.Value = PlayerId;
        }

        public void UpdateGameState(Playing playingState, Room roomState)
        {
            previousPlayingState = currentPlayingState;
            currentPlayingState = playingState;

            previousRoomState = currentRoomState;
            currentRoomState = roomState;

            // TODO (slumley): We should actually check what changed, for now we just reload everything
            if (/*previousPlayingState == null*/ true)
            {
                onPlayerCountChanged.SendEvent();
                onEmojisChanged.SendEvent();
                onSelectableRoleDisabledChanged.SendEvent();
                onFailedThreatsChanged.SendEvent();
                onThreatsChanged.SendEvent();
                onTurnChanged.SendEvent();
                onGameWon.SendEvent();
                onGameLost.SendEvent();
            }
            
            if (previousRoomState != currentRoomState
                || previousRoomState.Name != currentRoomState.Name
                || previousRoomState.Owner != currentRoomState.Name
                || previousRoomState.Players.Count != currentRoomState.Players.Count
                || previousRoomState.PossibleThreats.Count != currentRoomState.PossibleThreats.Count)
            {
                serverRoomName.Value = currentRoomState.Name;
                onRoomInfoChanged.SendEvent();
            }
        }
    }
}