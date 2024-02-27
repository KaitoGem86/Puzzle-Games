using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace BallSortQuest{
    public enum TypeItem{
        Tube,
        Background
    }
    
    public class ShopItem: MonoBehaviour{
        [Header("Elements")]
        [SerializeField] private Image _icon;

        public void Init(){
            
        }
    }
}