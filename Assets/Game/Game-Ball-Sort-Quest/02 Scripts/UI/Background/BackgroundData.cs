using System;
using UnityEngine;

namespace BallSortQuest{
    [Serializable]
    public class BackgroundData{
        public TypeBackgroundEnum TypeBackground;
        public Sprite SpriteBackground;
        [SerializeField] private Sprite _particleBackgrounds;

        //Use when background is dynamic
        public Sprite ParticleBackground {
            get {
                if (TypeBackground == TypeBackgroundEnum.Dynamic){
                    return _particleBackgrounds;
                }
                return null;
            }
        }
    }

    public enum TypeBackgroundEnum{
        Dynamic,
        Static,
    }
}