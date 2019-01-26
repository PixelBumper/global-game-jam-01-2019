using System.Collections.Generic;
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
        //public ScriptableGameEvent onFailedThreatsChanged;
        public ScriptableGameEvent onThreatsChanged;
        public ScriptableGameEvent onTurnChanged;
        public ScriptableGameEvent onGameWon;
        public ScriptableGameEvent onGameLost;

        [Header("Room Events")]
        public ScriptableGameEvent onRoomInfoChanged;

        [Header("Threats")]
        public ScriptableVariable threat1;
        public ScriptableVariable threat2;
        public ScriptableVariable threat3;
        public ScriptableVariable threat4;
        public ScriptableVariable threat5;
        public ScriptableVariable threat6;

        public RoleThreat[] allThreats;
        // public List<RoleThreat> easyThreats;  // Add difficulty later

        [Header("Room Variables")]
        public GlobalString serverRoomName;
        public GlobalString myPlayerId;

        private Playing previousPlayingState;
        private Room previousRoomState;
        private Playing currentPlayingState;
        private Room currentRoomState;

        private long lastVersionReceived = long.MinValue;

        private void Awake()
        {
            myPlayerId.Value = PlayerId;
            allThreats = new RoleThreat[6] {
                (RoleThreat)threat1.GetValue(),
                (RoleThreat)threat2.GetValue(),
                (RoleThreat)threat3.GetValue(),
                (RoleThreat)threat4.GetValue(),
                (RoleThreat)threat5.GetValue(),
                (RoleThreat)threat5.GetValue()
            };
        }

        public void UpdateGameState(Playing playingState, Room roomState)
        {
            if (playingState != null)
            {
                if (playingState.Version < lastVersionReceived)
                {
                    return;
                }

                lastVersionReceived = playingState.Version;
            }

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
                //onFailedThreatsChanged.SendEvent();
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