using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SS.View
{
    public class CameraDestroyer : MonoBehaviour
    {
        private void OnDestroy()
        {
            Manager.Object.ActivateBackgroundCamera(true);
        }
    }
}
