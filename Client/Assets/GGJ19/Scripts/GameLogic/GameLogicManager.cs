using System.Collections.Specialized;
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

        public ScriptableGameEvent onPlayerCountChanged;
        public ScriptableGameEvent onEmojisChanged;
        public ScriptableGameEvent onSelectableRoleDisabledChanged;
        public ScriptableGameEvent onFailedThreatsChanged;
        public ScriptableGameEvent onThreatsChanged;
        public ScriptableGameEvent onTurnChanged;
        public ScriptableGameEvent onGameWon;
        public ScriptableGameEvent onGameLost;

        public ScriptableGameEvent onRoomInfoChanged;

        private Playing previousPlayingState;
        private Room previousRoomState;
        private Playing currentPlayingState;
        private Room currentRoomState;

        public void UpdateGameState(Playing playingState, Room roomState)
        {
            previousPlayingState = currentPlayingState;
            currentPlayingState = playingState;

            previousRoomState = currentRoomState;
            currentRoomState = roomState;

            if (previousPlayingState == null)
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
                onRoomInfoChanged.SendEvent();
            }
        }

        public void JoinReadyRoom(RoomInformation roomInfo)
        {
            Playing  playing = roomInfo.Playing;
            Room room = roomInfo.Waiting;
        }


    }
}