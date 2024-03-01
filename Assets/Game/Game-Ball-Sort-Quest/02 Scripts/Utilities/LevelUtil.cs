namespace BallSortQuest{
    public class LevelUtil{
        public static bool IsLevelHidden(int level){
            if (level < 15) return level == 8 || level == 12;
            return level % 4 == 1;
        }
    }
}