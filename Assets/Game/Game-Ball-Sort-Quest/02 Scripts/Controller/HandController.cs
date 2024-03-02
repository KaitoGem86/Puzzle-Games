using System.Collections.Generic;
using UnityEngine;
namespace BallSortQuest{
    public class HandController : MonoBehaviour{
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        
        private Vector3 _defaultOffset = new Vector3(1f, -1f, 0);

        public void Init(List<TubeController> tubes){
            if(tubes == null) throw new System.ArgumentNullException(nameof(tubes));
            if(tubes.Count != 2) return;
            _startPosition = tubes[0].transform.position;
            _endPosition = tubes[tubes.Count - 1].transform.position;
            this.transform.position = _startPosition + _defaultOffset;
        }

        public void OnMouseDown(){
            this.transform.position = _endPosition + _defaultOffset;
        }
    }
}