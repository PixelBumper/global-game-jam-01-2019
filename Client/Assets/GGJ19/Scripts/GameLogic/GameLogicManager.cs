using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeneratedServerAPI;
using GGJ19.Scripts.Server_Api;
using HalfBlind.ScriptableVariables;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace GGJ19.Scripts.GameLogic
{
    [CreateAssetMenu(fileName = nameof(GameLogicManager), menuName = "Tools/CreateGameLogicManager")]
    public class GameLogicManager : ScriptableSingleton<GameLogicManager>
    {
        public string MyPlayerId => SystemInfo.deviceUniqueIdentifier;

        [Header("Game Events")]
        public ScriptableGameEvent onPlayerCountChanged;
        public ScriptableGameEvent onEmojisChanged;
        public ScriptableGameEvent onSelectableRoleDisabledChanged;
        public ScriptableGameEvent onFailedThreatsChanged;
        public ScriptableGameEvent onThreatsChanged;
        public ScriptableGameEvent onTurnChanged;
        public ScriptableGameEvent onGameStart;
        public ScriptableGameEvent onGameWon;
        public ScriptableGameEvent onGameLost;
        public ScriptableGameEvent onGamePhaseChanged;
        public ScriptableGameEvent OnEmojiPhaseStarted;
        public ScriptableGameEvent OnEmojiPhaseEnded;

        [Header("Game Variables")] 
        public GlobalString player1Id;
        public GlobalString player2Id;
        public GlobalString player3Id;
        public GlobalString player4Id;
        public GlobalFloat lengthOfTurnInSeconds;
        public GlobalFloat amountOfTurns;
        public GlobalFloat currentTurn;
        public GlobalString currentGamePhase;

        public GlobalString emojiIconPlayer1;
        public GlobalString emojiIconPlayer2;
        public GlobalString emojiIconPlayer3;
        public GlobalString emojiIconPlayer4;

        [Header("Runtime Variables")]
        public GlobalFloat CurrentPhaseRemainingTimeInSeconds;

        [Header("Room Events")]
        public ScriptableGameEvent onRoomInfoChanged;

        [Header("Threats")]
        public ScriptableVariable threat1;
        public ScriptableVariable threat2;
        public ScriptableVariable threat3;
        public ScriptableVariable threat4;
        public ScriptableVariable threat5;
        public ScriptableVariable threat6;

        [Header("CurrentThreats")]
        public GlobalFloat currentTheatsAmount;
        public GlobalString currentThreat1;
        public GlobalString currentThreat2;
        public GlobalString currentThreat3;
        public GlobalString currentThreat4;
        public GlobalString currentThreat5;
        public GlobalString currentThreat6;
        public GlobalString currentThreat7;

        [Header("Room Variables")]
        public GlobalString serverRoomName;
        public GlobalString serverOwnerId;

        [Header("Server Variables")]
        public GlobalFloat serverPollTimeMs; // How many ms between server requests

        [CanBeNull] private Playing previousPlayingState;
        [CanBeNull] private Room previousRoomState;
        private Playing currentPlayingState;
        private Room currentRoomState;

        private long lastVersionReceived = long.MinValue;

        public void ResetGameVars()
        {
            player1Id.Value = null;
            player2Id.Value = null;
            player3Id.Value = null;
            player4Id.Value = null;

            serverRoomName.Value = null;
            serverOwnerId.Value = null;
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

            if(currentPlayingState == null)
            {
                // Stuff to do durring Room Creation
                UpdatePlayers();
            }

            // TODO (slumley): We should actually check what changed, for now we just reload everything
            if (/*previousPlayingState == null*/ currentPlayingState != null)
            {
                UpdatePhaseTime();
                UpdateEmojiIcons();
                UpdateThreats();
                onSelectableRoleDisabledChanged.SendEvent();
                onFailedThreatsChanged.SendEvent();
                onThreatsChanged.SendEvent();

                UpdateTurn();
            }
            
            if (currentPlayingState != null)
            {
                if (currentPlayingState != null &&
                    currentPlayingState.CurrentPhase.ToString() != currentGamePhase.Value)
                {
                    UpdateGamePhase();
                }
                
                if (currentPlayingState.CurrentRoundNumber >= currentPlayingState.MaxRoundNumber)
                {
                    currentGamePhase.Value = "NOT_STARTED";
                    onGameWon.SendEvent();
                }
                else if (currentPlayingState.OpenThreats.Count >= currentPlayingState.MaximumThreats)
                {
                    currentGamePhase.Value = "NOT_STARTED";
                    onGameLost.SendEvent();
                }
            }

            if (currentRoomState != null)
            {
                bool hasRoomChanged = false;
                if (currentRoomState.Name != serverRoomName.Value)
                {
                    hasRoomChanged = true;
                    serverRoomName.Value = currentRoomState.Name;
                }

                serverOwnerId.Value = currentRoomState.Owner; 
                
                if (previousRoomState != null && previousRoomState.Players.Count != currentRoomState.Players.Count || previousRoomState == null)
                {
                    hasRoomChanged = true;
                }
                

                if (hasRoomChanged)
                {
                    onRoomInfoChanged.SendEvent();
                }
            }
        }

        private void UpdatePhaseTime() {
            var RoundEndingTimeMilliseconds = currentPlayingState.RoundEndingTime;
            var serverCurrentMilliseconds = currentPlayingState.CurrentTime;
            var remainingMilliseconds = RoundEndingTimeMilliseconds - serverCurrentMilliseconds;
            CurrentPhaseRemainingTimeInSeconds.Value = remainingMilliseconds * 0.001f;
            Debug.Log($"[{DateTime.Now:HH:mm:ss}]Game will finish in {remainingMilliseconds*.001f}s at {RoundEndingTimeMilliseconds} were NOW: {serverCurrentMilliseconds}");
        }

        private void UpdateGamePhase()
        {
            var previousValue = currentGamePhase.Value;
            currentGamePhase.Value = currentPlayingState.CurrentPhase.ToString();
            if (string.IsNullOrEmpty(previousValue) || previousValue == "NOT_STARTED") {
                if(currentGamePhase.Value == "PHASE_EMOJIS") {
                    OnEmojiPhaseStarted.SendEvent();
                }
            }

            if (!string.IsNullOrEmpty(previousValue) && previousValue == "PHASE_EMOJIS") {
                OnEmojiPhaseEnded.SendEvent();
            }
            
            onGamePhaseChanged.SendEvent();
        }

        private void UpdateThreats() {
            if(currentPlayingState == null || currentPlayingState.PossibleThreats == null) {
                return;
            }
            currentTheatsAmount.Value = currentPlayingState.PossibleThreats.Count;
            var threatsList = new List<string>(currentPlayingState.PossibleThreats.Count);
            foreach (RoleThreat threat in currentPlayingState.PossibleThreats) {
                threatsList.Add(threat.Value);
            }

            currentThreat1.Value = threatsList.Count > 0 ? threatsList[0] : string.Empty;
            currentThreat2.Value = threatsList.Count > 1 ? threatsList[1] : string.Empty;
            currentThreat3.Value = threatsList.Count > 2 ? threatsList[2] : string.Empty;
            currentThreat4.Value = threatsList.Count > 3 ? threatsList[3] : string.Empty;
            currentThreat5.Value = threatsList.Count > 4 ? threatsList[4] : string.Empty;
            currentThreat6.Value = threatsList.Count > 5 ? threatsList[5] : string.Empty;
            currentThreat7.Value = threatsList.Count > 6 ? threatsList[6] : string.Empty;
        }

        private void UpdateEmojiIcons()
        {
            emojiIconPlayer1.Value = null;
            emojiIconPlayer2.Value = null;
            emojiIconPlayer3.Value = null;
            emojiIconPlayer4.Value = null;


            if(currentPlayingState == null)
            {
                // Gmae Not Started Yet.
                return;
            }

            ICollection<Emoji> emojiCollection;
            if (player1Id.Value != null && currentPlayingState.PlayerEmojis.TryGetValue( player1Id.Value, out emojiCollection) && emojiCollection.Count > 0)
            {
                foreach (var emoji in emojiCollection)
                {
                    emojiIconPlayer1.Value = emoji.Emoji1;
                    break;
                }
            }
            
            if (player2Id.Value != null && currentPlayingState.PlayerEmojis.TryGetValue(player2Id.Value, out emojiCollection) && emojiCollection.Count > 0)
            {
                foreach (var emoji in emojiCollection)
                {
                    emojiIconPlayer2.Value = emoji.Emoji1;
                    break;
                }
            }
            
            if (player3Id.Value != null && currentPlayingState.PlayerEmojis.TryGetValue(player3Id.Value, out emojiCollection) && emojiCollection.Count > 0)
            {
                foreach (var emoji in emojiCollection)
                {
                    emojiIconPlayer3.Value = emoji.Emoji1;
                    break;
                }
            }
            
            if (player4Id.Value != null && currentPlayingState.PlayerEmojis.TryGetValue(player4Id.Value, out emojiCollection) && emojiCollection.Count > 0)
            {
                foreach (var emoji in emojiCollection)
                {
                    emojiIconPlayer4.Value = emoji.Emoji1;
                    break;
                }
            }

            onEmojisChanged.SendEvent();
        }

        private void UpdatePlayers()
        {
            if(currentRoomState == null) {
                return;
            }

            Debug.Log("UPDATING PLAYERS");
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

        public async Task SendRoomInfoRequest()
        {
            string roomId = serverRoomName.Value;
            if (!string.IsNullOrEmpty(roomId))
            {
                try
                {
                    var serverApi = ServerApi.Instance;
                    RoomInformation roomInfoResponse = await serverApi.RoomInformationAsync(roomId);
                    Debug.Log($"[{DateTime.Now:HH:mm:ss}]{nameof(roomInfoResponse)}:{roomInfoResponse.ToJson()}");
                    UpdateGameState(roomInfoResponse.Playing, roomInfoResponse.Waiting);
                }
                catch (Exception e)
                {
                    LogException(e);
                }
            }
        }

        public void LogException(Exception e)
        {
            Debug.LogError(string.Format("Caught Error: {0} : {1}", e.Message, e.StackTrace));
        }
    }
}