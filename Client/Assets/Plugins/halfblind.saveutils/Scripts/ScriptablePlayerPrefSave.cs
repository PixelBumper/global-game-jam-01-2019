using UnityEngine;

namespace HalfBlind.SaveUtils {
    [CreateAssetMenu(menuName = "Save/PlayerPrefSaver", fileName = "PlayerPrefSaver")]
    public class ScriptablePlayerPrefSave : ScriptableObject, ISave {
        public bool Load<T>(string key, out T outValue) {
            var stringValue = PlayerPrefs.GetString(key, string.Empty);
            outValue = JsonUtility.FromJson<T>(stringValue);
            return true;
        }

        public void Save<T>(string key, T value) {
            var serializedValue = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key, serializedValue);
        }
    }
}
