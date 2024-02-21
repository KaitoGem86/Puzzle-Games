using UnityEngine;
namespace BallSortQuest
{
    [CreateAssetMenu(fileName = "BackgroundDatas", menuName = "BackgroundDatas", order = 1)]
    public class BackgroundDatas : ScriptableObject
    {
        public BackgroundData[] Backgrounds;
    }
}