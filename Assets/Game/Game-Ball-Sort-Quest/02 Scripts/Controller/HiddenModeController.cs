using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest{
    public class HiddenModeController {
        public HiddenModeController(){
        }

        public void HiddenRandomBall(List<TubeController> tubes)
        {
            int tubeRandomIndex = Random.Range(0, tubes.Count);
            bool isComplete = false;
            for (int i = 0;  i < tubes.Count; i++)
            {
                if (tubes[tubeRandomIndex].State == StateTube.Empty || tubes[tubeRandomIndex].State == StateTube.Complete){
                    tubeRandomIndex += 1;
                    continue;
                } 
                var balls = tubes[tubeRandomIndex].Balls;
                if (balls.Count < 1) {
                    Debug.LogWarning("Exist Tube not empty but has no ball " + i);
                    tubeRandomIndex += 1;
                    continue;
                }
                var randomIndex = Random.Range(0, balls.Count);
                for(int j = 0; j < balls.Count; j ++){
                    if(balls[j].IsHiddenWithNoMode){
                        randomIndex += 1;
                        continue;
                    }
                    balls[randomIndex].HideBall();
                    isComplete = true;
                    break;
                }
                if (isComplete) break;
            }
        }
    }
}