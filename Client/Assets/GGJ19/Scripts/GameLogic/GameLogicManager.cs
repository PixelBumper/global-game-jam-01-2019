using System;
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
        public ScriptableGameEvent onFailedThreatsChanged;
        public ScriptableGameEvent onThreatsChanged;
        public ScriptableGameEvent onTurnChanged;
        public ScriptableGameEvent onGameWon;
        public ScriptableGameEvent onGameLost;

        [Header("Game Variables")] 
        public GlobalString player1Id;
        public GlobalString player2Id;
        public GlobalString player3Id;
        public GlobalString player4Id;
        public GlobalFloat lengthOfTurnInSeconds;
        public GlobalFloat amountOfTurns;
        public GlobalFloat currentTurn;

        [Header("Room Events")]
        public ScriptableGameEvent onRoomInfoChanged;

        [Header("Threats")]
        public ScriptableVariable threat1;
        public ScriptableVariable threat2;
        public ScriptableVariable threat3;
        public ScriptableVariable threat4;
        public ScriptableVariable threat5;
        public ScriptableVariable threat6;

        [Header("Room Variables")]
        public GlobalString serverRoomName;
        public GlobalString myPlayerId;

        [Header("Server Variables")]
        public GlobalFloat serverPollTimeMs; // How many ms between server requests

        private Playing previousPlayingState;
        private Room previousRoomState;
        private Playing currentPlayingState;
        private Room currentRoomState;

        private long lastVersionReceived = long.MinValue;

        private void Awake()
        {
            myPlayerId.Value = PlayerId;
        }

        public void UpdateGameState(Playing playingState, Room roomState)
        {
           
            Debug.Log("Game State Updating!");
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

            Debug.Log("RoomState Name:" + roomState.Name);
            // TODO (slumley): We should actually check what changed, for now we just reload everything
            if (/*previousPlayingState == null*/ true)
            {
                UpdatePlayers();
                onEmojisChanged.SendEvent();
                onSelectableRoleDisabledChanged.SendEvent();
                onFailedThreatsChanged.SendEvent();
                onThreatsChanged.SendEvent();

                UpdateTurn();
                
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

        private void UpdatePlayers()
        {
            var playerList = new List<PlayerId>(currentRoomState.Players);
            playerList.Sort((left, right) => string.Compare(left.Name, right.Name, StringComparison.Ordinal));

            player1Id.Value = playerList.Count > 0 ? playerList[0].Name : null;
            player2Id.Value = playerList.Count > 1 ? playerList[1].Name : null;
            player3Id.Value = playerList.Count > 2 ? playerList[2].Name : null;
            player4Id.Value = playerList.Count > 3 ? playerList[3].Name : null;

            onPlayerCountChanged.SendEvent();
        }

        private void UpdateTurn()
        {
            if(currentPlayingState != null)
            {
                currentTurn.Value = currentPlayingState.CurrentRoundNumber;
                onTurnChanged.SendEvent();
            }
        }
    }
}