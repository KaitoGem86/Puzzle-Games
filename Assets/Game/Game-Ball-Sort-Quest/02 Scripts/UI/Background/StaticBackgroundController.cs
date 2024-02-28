using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest{
    public class StaticBackgroundController : MonoBehaviour{
        [SerializeField] private Image _backgroundImage;

        public void SetBackgroundSprite(Sprite sprite){
            _backgroundImage.sprite = sprite;
        }
    }
}