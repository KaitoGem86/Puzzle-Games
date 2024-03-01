using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    [CreateAssetMenu(fileName = "Tube Datas", menuName = "Data SO/Tube Datas", order = 0)]
    public class TubeDataSO : ScriptableObject
    {
        public List<TubeSpriteData> tubeSpriteDatas = new List<TubeSpriteData>();
    }

    [Serializable]
    public class TubeSpriteData
    {
        public Sprite topSprite;
        public Sprite middleSprite;
        public Sprite bottomSprite;
    }
}