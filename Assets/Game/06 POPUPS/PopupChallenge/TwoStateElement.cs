using UnityEngine;

namespace BallSortQuest{
    public class TwoStateElement{
        private readonly Transform _onObject;
        private readonly Transform _offObject;
        public TwoStateElement(Transform trans){
            _onObject = trans.GetChild(0);
            _offObject = trans.GetChild(1);
        }

        public void SetState(bool isOn){
            _onObject.gameObject.SetActive(isOn);
            _offObject.gameObject.SetActive(!isOn);
        }
    }
}