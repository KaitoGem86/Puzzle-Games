using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace GamePlay
{
    public enum TilePositionEnum{
        InTopLayer,
        InBehindLayer,
    }

    public enum TileStateEnum{
        Normal,
        Linked,
        Locked,
        InSlot,
    }

    public class TileController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseDown(){
            MoveToPosition(new Vector3(-1,-1,0), true);
        }

        private void MoveToPosition( Vector3 targetPos, bool isJump = false){
            if(!isJump){
                transform.DOMove(targetPos, 0.2f);
            }
            else{
                transform.DOJump(targetPos, 0.5f, 1, 0.2f);
            }
        }
    }
}
