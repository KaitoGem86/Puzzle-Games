using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public SkeletonAnimation _skeAnim;
    [SpineAnimation] public string[] _animName;
    public string CurrentAnim { get; set; }

    public void ChangeSkin(SkeletonAnimation skAnim, string ssSkinChange)
    {
        var skeleton = skAnim.Skeleton;
        var skeletonData = skeleton.Data;
        var newSkin = new Skin("new-skin");

        newSkin.AddSkin(skeletonData.FindSkin(ssSkinChange));
        skeleton.SetSkin(newSkin);
        skeleton.SetSlotsToSetupPose();
        skAnim.AnimationState.Apply(skeleton);
    }

    public void SetAnimation(int index, bool loop = false, float timeScale = 1, Action CompleteCallBack = null, string eventName = null, Action EventCallback = null)
    {
        if (CurrentAnim != _animName[index])
        {
            CurrentAnim = _animName[index];

            var setAnimTrack = _skeAnim.AnimationState.SetAnimation(0, _animName[index], loop);

            setAnimTrack.TimeScale = timeScale;

            setAnimTrack.Complete += (e) => { CompleteCallBack?.Invoke(); };

            if (string.IsNullOrEmpty(eventName)) return;
            setAnimTrack.Event += (e, d) =>
            {
                if (d.Data.Name == eventName)
                {
                    EventCallback?.Invoke();
                }
            };
        }
        else
        {
            //  Debug.LogError("Don't set anim");
        }
    }
}
