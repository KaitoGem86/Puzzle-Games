using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallSortQuest
{
    [CreateAssetMenu(fileName = "Tube Datas", menuName = "Data SO/Tube Datas", order = 0)]
    public class TubeDataSO : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] GameObject _tubePrefab;
    }
}