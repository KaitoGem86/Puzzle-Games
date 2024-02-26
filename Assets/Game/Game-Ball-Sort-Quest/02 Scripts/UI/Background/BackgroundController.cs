using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BallSortQuest
{
    public class BackgroundController : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private DynamicBackgroundController _dynamicBackgroundController;
        public void InitBackground(){
            BackgroundData data = DataManager.Instance.BackgroundDatas.Backgrounds[PlayerData.UserData.CurrentBackgroundIndex];
            SetBackground(data);
        }

        public void SetBackground(BackgroundData data){
            _backgroundImage.sprite = data.SpriteBackground;
            switch(data.TypeBackground){
                case TypeBackgroundEnum.Dynamic:
                    _dynamicBackgroundController.gameObject.SetActive(true);
                    _dynamicBackgroundController.SetParticleBackground(data.ParticleBackground);
                    _dynamicBackgroundController.ResetParticleBackground();
                    _dynamicBackgroundController.InitParticleBackground();
                    break;
                case TypeBackgroundEnum.Static:
                    //Do something
                    _dynamicBackgroundController.gameObject.SetActive(false);
                    break;
                default:
                    throw new System.Exception("Type Background not found!");
            }
        }
    }
}