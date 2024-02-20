using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    [System.Serializable]
    public class TubeData
    {
        public int Slot;
        public List<BallData> dataBall = new List<BallData>();

        public TubeData(int slot, List<BallData> dataBall)
        {
            Slot = slot;
            this.dataBall = dataBall;
        }
    }

    public class TubeController : MonoBehaviour
    {
        public TubeData data;
        [Header("REFFERENCE")]
        [SerializeField] GameObject _ballPrefab;
        [SerializeField] SpriteRenderer _avaterSpr;
        [SerializeField] Transform _spwanTrans;
        [Space(10)]
        [SerializeField] Transform _startTransMove;
        [Header("VALUE")]
        [SerializeField] float _width, _height;
        private int _slot;
        public int Slot => _slot;
        public float Width => _width;
        public float Height => _height;
        private Vector2 _spwanPos, _startPosMove;
        public Vector2 SpawnPos => _spwanPos;
        public Vector2 StartPosMove => _startPosMove;
        public List<BallController> Balls = new List<BallController>();
        [field: SerializeField] public StateTube State { get; private set; }

        private void Awake()
        {
            _width = _avaterSpr.bounds.size.x;
            _height = _avaterSpr.bounds.size.y;
            //   Debug.LogError($"Width Tube: {_width}--- Height Tube: {_height}");
        }

        private void OnEnable()
        {

        }

        private void Reset()
        {
            this.data = null;
            ChangeState(StateTube.Emty);
            if (Balls.Count < 1) return;
            foreach (var ball in Balls)
            {
                SimplePool.Despawn(ball.gameObject);
            }
            Balls.Clear();
        }

        private void OnDisable()
        {
            Reset();
        }

        private void OnMouseDown()
        {
            if (State.Equals(StateTube.Active)) return;

            GameManager.Instance.GamePlayManager.OnClick(this);
        }

        public void Init(Vector2 target, TubeData data, int slot)
        {
            this.data = data;
            _slot = slot;
            SetPosition(target);


            //  Debug.Log($"spwan pos: {_spwanPos}");

            if (data.dataBall.Count > 1)
                ChangeState(StateTube.Incomplete);

            for (int i = 0; i < data.dataBall.Count; i++)
            {
                SpwanBall(data.dataBall[i], i);
            }
        }

        public void SetPosition(Vector2 target)
        {
            this.transform.position = target;

            _spwanPos = _spwanTrans.position;
            _startPosMove = _startTransMove.position;
        }

        private void SpwanBall(BallData data, int index)
        {
            GameObject ballObj = SimplePool.Spawn(_ballPrefab, Vector2.zero, Quaternion.identity);
            ballObj.transform.SetParent(this.transform);
            BallController ball = ballObj.GetComponent<BallController>();
            ball.Init(data, _spwanPos, _startPosMove, index);
            Balls.Add(ball);
        }

        public void ChangeState(StateTube state)
        {
            this.State = state;
        }

        public void UpdadeTubeBonus()
        {
            _slot++;
        }

        public bool isTubeEmty()
        {
            if (Balls.Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool CanSortBall(TubeController tube)
        {
            if (isTubeEmty())
            {
                return true;
            }
            if (_slot == Balls.Count) return false;

            if (data.Slot == Balls.Count)
            {
                return false;
            }

            bool isSameColor = GetLastBall().Id == tube.GetLastBall().Id;
            if (!isSameColor)
            {
                return false;
            }
            return true;
        }

        public List<BallController> getListSameBall()
        {
            BallController ball = Balls[Balls.Count - 1];

            List<BallController> sameBalls = new List<BallController>() { ball };

            for (int i = Balls.Count - 2; i >= 0; i--)
            {
                if (Balls[i].Id == ball.Id)
                {
                    sameBalls.Add(Balls[i]);
                }
                else
                {
                    break;
                }
            }
            return sameBalls;
        }

        public BallController GetLastBall()
        {
            if (!isTubeEmty())
            {
                BallController ball = Balls[Balls.Count - 1];
                return ball;
            }
            return null;
        }

        public List<BallController> GetCanMoveBalls(){
            if (isTubeEmty()) return null;
            int id = Balls[Balls.Count - 1].Id;
            List<BallController> canMoveBalls = new List<BallController>();
            for (int i = Balls.Count - 1; i >= 0; i--)
            {
                if (i > 0)
                {
                    if (Balls[i].Id == id)
                    {
                        canMoveBalls.Add(Balls[i]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return canMoveBalls;
        }

        public bool isDone()
        {
            if (_slot < data.Slot) return false;
            if (Balls.Count != data.Slot) return false;
            for (int i = Balls.Count - 1; i >= 0; i--)
            {
                if (i > 0)
                {
                    if (Balls[i].Id != Balls[i - 1].Id)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

    public enum StateTube
    {
        Active,
        Incomplete,
        Complete,
        Emty
    }
}