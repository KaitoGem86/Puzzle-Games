using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    [Serializable]
    public class BallData
    {
        public int index;
        public Sprite avatarSpr;
    }

    public class BallController : MonoBehaviour
    {
        public BallData Data;
        [SerializeField] SpriteRenderer _avarSpr;
        [SerializeField] private SpriteRenderer _hiddenMaskBall;
        [SerializeField] float _heighBall;
        [SerializeField]
        public int Id => Data.index;
        [SerializeField] private ParticleSystem _particle;

        private bool _isMoving;
        private bool _isHidden;


        #region Unity Medthod
        private void Awake()
        {
            _heighBall = _avarSpr.bounds.size.y;
        }

        private void OnEnable()
        {
            // Reset();
            ActionEvent.OnSelectShopBall += SetSpriteTemplate;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_isMoving)
            {
            }
        }

        private void Reset()
        {
            this.Data = null;
        }

        private void OnDisable()
        {
            Reset();
            ActionEvent.OnSelectShopBall -= SetSpriteTemplate;
        }
        #endregion

        public void Init(BallData data, Vector2 originalPos, Vector3 startMovePos, int index)
        {
            this.Data = data;
            this.transform.DOKill();

            //ShowInfor();
            SetSpriteTemplate();
            
            SetPosition(originalPos, startMovePos, index);
            _isHidden = false;
            _hiddenMaskBall.gameObject.SetActive(false);
        }

        public void Init(BallData data, Vector2 originalPos, Vector3 startMovePos, int index, int tubeSlots, bool isHidden = false)
        {
            this.Data = data;

            //ShowInfor();
            SetSpriteTemplate();

            SetPosition(originalPos, startMovePos, index);
            if(isHidden){
                _hiddenMaskBall.gameObject.SetActive(index != tubeSlots - 1);
                _isHidden = index != tubeSlots - 1;
            }
        }

        private void SetSpriteTemplate(){
            var currentBallSpriteData = BallSortQuest.GameManager.Instance.Datamanager.ListBallDataSO.BallDatas[PlayerData.UserData.CurrentBallIndex];
            Data = currentBallSpriteData.BallDataList[Id];
            ShowInfor();
            _hiddenMaskBall.sprite = currentBallSpriteData.BallDataList[12].avatarSpr;
            _heighBall = _avarSpr.bounds.size.y;
        }

        private void ShowInfor()
        {
            _avarSpr.sprite = Data.avatarSpr;
        }

        public void ShowItself()
        {
            _hiddenMaskBall.gameObject.SetActive(false);
            _isHidden = false;
        }

        public void HideBall(){
            _hiddenMaskBall.gameObject.SetActive(true);
            _isHidden = true;
        }

        public void SetPosition(Vector2 originalPos, Vector2 startMovePos, int index)
        {
            this.transform.position = new Vector2(originalPos.x, originalPos.y + index * _heighBall);
        }

        public void StartMove(TubeController tube, bool value, float originalChildCount = 0, int index = 0)
        {
            tube.ChangeState(StateTube.Active);
            float duration = getDuration(tube.StartPosMove);
            if (value)
            {
                if(_isHidden)
                    _hiddenMaskBall.gameObject.SetActive(false);
                this.transform.DOMoveY(tube.StartPosMove.y, duration).SetEase(Ease.OutQuad).OnComplete(() =>
                 {
                    tube.ChangeState(StateTube.Incomplete); // Co the gay loi
                 });
                if(PlayerData.UserData.IsSoundOn)
                    SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("Bottle_Active0"));
            }
            else
            {
                
                Vector2 target = new Vector2(0, (originalChildCount + index) * _heighBall + tube.SpawnPos.y);
                this.transform.DOMoveY(target.y, duration).SetEase(Ease.InQuad).OnComplete(() =>
                {
                    if(PlayerData.UserData.IsSoundOn)
                        SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("Bottle_Active2"));
                    this.transform.DOJump(new Vector2(this.transform.position.x, target.y), 0.5f, 1, 0.1f).OnComplete(() =>
                    {
                        if(_isHidden)
                            _hiddenMaskBall.gameObject.SetActive(true);
                        tube.ChangeState(StateTube.Incomplete); // Co the gay loi
                    });
                    _particle.Play();
                });
            }
        }

        public void Movement(TubeController from, TubeController to, float originalChildCount, int index, Action onComplete)
        {
            this.transform.SetParent(to.transform);

            Vector2 target = new Vector2(to.SpawnPos.x, (originalChildCount + index) * _heighBall + to.SpawnPos.y);

            //Vector2 thisPos = this.transform.position;
            //float height = (thisPos - tube.StartPosMove).magnitude / 4;
            //Vector2 direc = (thisPos - tube.StartPosMove).normalized;
            //Vector2 topPos = (thisPos + tube.StartPosMove) / 2 + new Vector2(-direc.y, direc.x) * height;

            Vector3[] path = new Vector3[3] { from.StartPosMove/*, topPos*/, to.StartPosMove, target };

            float duration = 0;
            for (int i = 1; i < path.Length; i++)
            {
                duration += getDuration(path[i]);
            }
            //duration = getDuration(target, 0.002f);
            //  Debug.Log($"Ball {index}---Duration: {duration}");

            this.transform.DOPath(path, duration).SetEase(Ease.InQuad).SetDelay(0.1f * index)
              .OnComplete(() =>
              {
                  this.transform.DOJump(new Vector2(this.transform.position.x, target.y), 0.5f, 1, 0.1f);

                  onComplete?.Invoke();
                  if(PlayerData.UserData.IsSoundOn)
                    SoundManager.Instance.PlaySfxRewind(GlobalSetting.GetSFX("Bottle_Active2"));
                  _particle.Play();
              });
            // });
        }

        private float getDuration(Vector2 target, float timer = 0.001f)
        {
            double _distance = (target - (Vector2)this.transform.position).magnitude;
            float duration = (float)(0.1f + Math.Round(_distance * timer, 2));

            return duration;
        }

        public bool IsHidden => _isHidden && GameManager.Instance.GameModeController.CurrentGameMode.Equals(TypeChallenge.None);
        public bool IsHiddenWithNoMode => _isHidden;
        public SpriteRenderer HiddenMaskBall => _hiddenMaskBall;
    }
}