
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public bool isFirstSession = true;
        public string language = "ru";

        // Игровые сохранения
        public bool isTutorailPassed;
        public int maxPassedlevel;
        public bool[] openLevels = new bool[14];
        public bool isFirstStart = true;
        // Сохранения настроек
        public float soundVolume;
        public float effectVolume;
        public bool isSoundActive;
        public float sensetivity;
    }
}
