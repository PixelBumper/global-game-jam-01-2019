namespace HalfBlind.SaveUtils {
    public interface ISave {
        bool Load<T>(string key, out T outValue);
        void Save<T>(string key, T value);
    }
}
