using UnityEngine;

namespace BallSortQuest{
    public class TwoStateElement{
        protected readonly Transform _onObject;
        protected readonly Transform _offObject;
        private bool _isOn;
        public TwoStateElement(Transform trans){
            _onObject = trans.GetChild(0);
            _offObject = trans.GetChild(1);
        }

        public void SetState(bool isOn){
            _isOn = isOn;
            _onObject.gameObject.SetActive(isOn);
            _offObject.gameObject.SetActive(!isOn);
        }

        public bool IsOn{
            get => _isOn;
        }
    }

    public class TwoStateAtMidElement : TwoStateElement {
        // _offObject is the object at the right of the mid object
        // _off1Object is the object at the left of the mid object
        private readonly Transform _off1Object;
        public TwoStateAtMidElement(Transform trans): base(trans){
            _off1Object = trans.GetChild(2);
        }

        public void SetState(bool isOn, bool buttonOnAtLeft = false){
            _onObject.gameObject.SetActive(isOn);
            _offObject.gameObject.SetActive(!isOn && buttonOnAtLeft);
            _off1Object.gameObject.SetActive(!isOn && !buttonOnAtLeft);
        }
    }
}