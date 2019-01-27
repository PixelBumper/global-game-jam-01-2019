using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GeneratedServerAPI;
using GGJ19.Scripts.Server_Api;
using HalfBlind.ScriptableVariables;
using JetBrains.Annotations;
using UnityEngine;

namespace GGJ19.Scripts.GameLogic
{
    public class GameLogicManager : MonoBehaviour
    {
        public static GameLogicManager currentInstance;

        public static GameLogicManager instance => currentInstance;
        
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
        public ScriptableGameEvent OnRolePhaseStarted;
        public ScriptableGameEvent OnRolePhaseEnded;

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

        private void Awake()
        {
            if (currentInstance != null)
            {
                Destroy(gameObject);
                return;
            }

            currentInstance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void ResetGameVars()
        {
            player1Id.Value = null;
            player2Id.Value = null;
            player3Id.Value = null;
            player4Id.Value = null;
            currentGamePhase.Value = string.Empty;
            lastVersionReceived = -1;

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


            // Stuff to do durring Room Creation
            UpdatePlayers(roomState);

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

        private void UpdateGamePhase() {
            var previousValue = currentGamePhase.Value;
            currentGamePhase.Value = currentPlayingState.CurrentPhase.ToString();

            if (!string.IsNullOrEmpty(previousValue) && previousValue == "PHASE_EMOJIS") {
                OnEmojiPhaseEnded.SendEvent();
            }

            if (currentGamePhase.Value == "PHASE_EMOJIS") {
                OnEmojiPhaseStarted.SendEvent();
            }

            if (currentGamePhase.Value == "PHASE_ROLE") {
                OnRolePhaseStarted.SendEvent();
            }

            if (!string.IsNullOrEmpty(previousValue) && previousValue == "PHASE_ROLE") {
                OnRolePhaseEnded.SendEvent();
            }

            onGamePhaseChanged.SendEvent();
        }

        private void UpdateThreats() {
            if(currentPlayingState == null || currentPlayingState.PossibleThreats == null) {
                return;
            }
            currentTheatsAmount.Value = currentPlayingState.PossibleThreats.Count;
            var threatsList = new List<string>(currentPlayingState.PossibleThreats.Count);
            foreach (String threat in currentPlayingState.PossibleThreats) {
                threatsList.Add(threat);
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

            GetEmojiForPlayerId(player1Id, emojiIconPlayer1);
            GetEmojiForPlayerId(player2Id, emojiIconPlayer2);
            GetEmojiForPlayerId(player3Id, emojiIconPlayer3);
            GetEmojiForPlayerId(player4Id, emojiIconPlayer4);

            onEmojisChanged.SendEvent();
        }

        private void GetEmojiForPlayerId(GlobalString playerId, GlobalString emojiForPlayerId) {
            if (playerId.Value != null) {
                ICollection<string> emojiCollection;
                if (currentPlayingState.PlayerEmojis.TryGetValue(playerId.Value, out emojiCollection) && emojiCollection.Count > 0) {
                    foreach (var emoji in emojiCollection) {
                        emojiForPlayerId.Value = emoji;
                        break;
                    }
                }
            }
        }

        private void UpdatePlayers(Room room)
        {
            if(room == null) {
                return;
            }

            Debug.Log("UPDATING PLAYERS");
            var playerList = new List<string>(room.Players);
            playerList.Sort((left, right) => string.Compare(left, right, StringComparison.Ordinal));

            player1Id.Value = playerList.Count > 0 ? playerList[0] : null;
            player2Id.Value = playerList.Count > 1 ? playerList[1] : null;
            player3Id.Value = playerList.Count > 2 ? playerList[2] : null;
            player4Id.Value = playerList.Count > 3 ? playerList[3] : null;

            onPlayerCountChanged.SendEvent();
        }

        private void UpdateTurn()
        {
            if(currentPlayingState != null && currentTurn != null && currentTurn.Value != currentPlayingState.CurrentRoundNumber)
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
                    Debug.Log($"[{DateTime.Now:HH:mm:ss}]{nameof(SendRoomInfoRequest)}");
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