using UnityEngine;
using PopupSystem;

namespace BallSortQuest
{
    public class ShopPanel : BasePopup
    {
        [Header("Shop Elements")]
        [SerializeField] private Transform _marketNavigateButton;
        [SerializeField] private Transform _tubeNavigateButton;
        [SerializeField] private Transform _backgroundNavigateButton;

        [Space(10),Header("Purchase Boards")]
        [SerializeField] private GameObject _marketBoard;
        [SerializeField] private GameObject _listSlideBoard;

        private TwoStateElement _marketButton;
        private TwoStateElement _backgroundButton;
        private TwoStateAtMidElement _tubeButton;

        public void Show(){
            base.Show();
            _marketButton ??= new TwoStateElement(_marketNavigateButton);
            _backgroundButton ??= new TwoStateElement(_backgroundNavigateButton);
            _tubeButton ??= new TwoStateAtMidElement(_tubeNavigateButton);
            OnMarketNavigateButtonClick();
        }

        public void Close(){
            base.Hide();
        }

        public void OnMarketNavigateButtonClick(){
            _marketButton.SetState(true);
            _backgroundButton.SetState(false);
            _tubeButton.SetState(false, true);
            _marketBoard.SetActive(true);
            _listSlideBoard.SetActive(false);
        }

        public void OnTubeNavigateButtonClick(){
            _marketButton.SetState(false);
            _backgroundButton.SetState(false);
            _tubeButton.SetState(true, true);
            _marketBoard.SetActive(false);
            _listSlideBoard.SetActive(true);
        }

        public void OnBackgroundNavigateButtonClick(){
            _marketButton.SetState(false);
            _backgroundButton.SetState(true);
            _tubeButton.SetState(false, false);
            _marketBoard.SetActive(false);
            _listSlideBoard.SetActive(true);
        }
    }
}