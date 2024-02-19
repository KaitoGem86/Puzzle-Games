using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public static class FunctionCommon
{
    static Camera _cam;
    public static Camera mainCam
    {
        get
        {
            if (!_cam)
            {
                _cam = Camera.main;
            }
            return _cam;
        }
    }

    static float _ratio = 0;
    public static float newRatio
    {
        get
        {
            if (_ratio == 0)
            {
                float oldRatio = 1920f / 1080f;
                _ratio = (float) Screen.height / Screen.width;
                _ratio /= oldRatio; //depend on height
            }
            return _ratio;
        }
    }

    public static float Random(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public static Tweener ChangeValueFloat(float startValue, float endValue, float speed,
        Action<float> onUpdate, TweenCallback onComplete)
    {
        return DOTween.To(() => startValue, x => startValue = x, endValue, speed).
            OnUpdate(delegate
            {
                onUpdate?.Invoke(startValue);
            }).SetEase(Ease.Linear).OnComplete(onComplete);
    }

    public static Tweener ChangeValueInt(int startValue, int endValue, float speed,float delay,
        Action<int> onUpdate)
    {
        return DOTween.To(() => startValue, x => startValue = x, endValue, speed).
            OnUpdate(delegate
            {
                onUpdate?.Invoke(startValue);
            }).SetDelay(delay);
    }

    public static Tween DelayTime(float time, Action onDone)
    {
        return DOVirtual.DelayedCall(time, delegate
        {
            try
            {
                onDone?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                throw;
            }
        }, false);
    }

    public static bool Between(float val, float min, float max)
    {
        return val >= min && val <= max;
    }

    public static void DeleteAllChild<T>(this T target) where T : Transform
    {
        for (int i = target.childCount - 1; i >= 0; i--)
        {
            UnityEngine.Object.DestroyImmediate(target.GetChild(i).gameObject);
        }
    }

    public static int LoadRandomByWeight(IEnumerable<float> lstWeights)
    {
        var lst = lstWeights.ToList();
        int index = -1;
        float sum = 0f;
        for (int i = 0; i < lst.Count(); i++)
        {
            lst[i] += sum;
            sum = lst[i];
            Debug.Log($"{sum} {lst[i]}");
        }
        var rand = Random(0f, lst.Last());
        index = lst.FindIndex(x => x >= rand);
        Debug.Log($"{rand} {index} {lst.Last()} {lst.Count}");
        return index;
    }
    
    public static T LoadRandom<T>(this IList<T> list)
    {
        int rand = UnityEngine.Random.Range(0, list.Count);
        return list[rand];
    }
    
    public static T LoadRandom<T>(this IList<T> list, Func<T, bool> predic)
    {
        var newList = new List<T>(list).Where(predic).ToList();
        if (newList.Count == 0) return default(T);
        int rand = UnityEngine.Random.Range(0, newList.Count - 1);
        return newList[rand];
    }

    public static bool InRange(this float value, float min, float max)
    {
        return value >= min && value <= max;
    }
    
    public static IList<int> ToListIndex<T>(this IList<T> lst)
    {
        List<int> indexs = new List<int>();
        for (int i = 0; i < lst.Count; i++)
        {
            indexs.Add(i);
        }
        return indexs;
    }

    public static IList<T> RandomUnique<T>(this IList<T> lst, int take)
    {
        var result = new List<T>();
        var tmp = lst;
        for (int i = 0; i < take; i++)
        {
            int rand = UnityEngine.Random.Range(0, tmp.Count - 1);
            tmp.RemoveAt(rand);
            result.Add(tmp[rand]);
        }

        return result;
    }

    public static string FormatString<T>(this string value, IList<T> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            value = value.Replace("{" + i + "}", $"{objs[i]}");
        }

        return value;
    }

    public static Vector2 RandomPointInCircle(Vector2 center, float radius)
    {
        var random = Random(0f, 1f);
        var R = radius * 2;
        var r = R * Mathf.Sqrt(random);
        var theta = random * 2 * Mathf.PI;

        return new Vector2(center.x + r * Mathf.Cos(theta), center.y + r * Mathf.Sin(theta));
    }
    
    public static Vector2 GetUnitOnCircle(float angleDegrees, float radius) {
     
        // initialize calculation variables
        float _x = 0;
        float _y = 0;
        float angleRadians = 0;
        Vector2 _returnVector;
     
        // convert degrees to radians
        angleRadians = angleDegrees * Mathf.PI / 180.0f;
     
        // get the 2D dimensional coordinates
        _x = radius * Mathf.Cos(angleRadians);
        _y = radius * Mathf.Sin(angleRadians);
     
        // derive the 2D vector
        _returnVector = new Vector2(_x, _y);
     
        // return the vector info
        return _returnVector;
    }

    public static Tweener ShakeCam(float duration, float strength, int vibrato)
    {
        return mainCam.DOShakePosition(duration, strength, vibrato);
    }
    
    /// <summary>
    /// Make 3D gameobject x axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetTrans">Target trans.</param>
    public static void LookAtAxisX2D(this Transform trans, Transform targetTrans)
    {
        LookAtAxisX2D(trans, targetTrans.position);
    }
    /// <summary>
    /// Make 3D gameobject x axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetPosition">Target position.</param>
    public static void LookAtAxisX2D(this Transform trans, Vector3 targetPosition)
    {
        // It's important to know rotating direction (clock-wise or counter clock-wise)
        // If target is above of gameobject (has y value higher) then rotate counter clock-wise and vice versa
        bool isAboveOfXAxis = targetPosition.y > trans.position.y;
        float angle = (isAboveOfXAxis ? 1 : -1) * Vector3.Angle(Vector3.right, targetPosition - trans.position);
//        trans.localRotation = Quaternion.identity;
        trans.localRotation = Quaternion.Euler(Vector3.forward * angle);
    }
    /// <summary>
    /// Make 3D gameobject y axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetTrans">Target trans.</param>
    public static void LookAtAxisY2D(this Transform trans, Transform targetTrans)
    {
        LookAtAxisY2D(trans, targetTrans.position);
    }
    /// <summary>
    /// Make 3D gameobject y axis look at target in 2D (with object has default rotation like in 3D).
    /// </summary>
    /// <param name="trans">Trans.</param>
    /// <param name="targetPosition">Target position.</param>
    public static void LookAtAxisY2D(this Transform trans, Vector3 targetPosition)
    {
        var position = trans.position;
        bool isLeftOfYAxis = targetPosition.x < position.y;
        float angle = (isLeftOfYAxis ? 1 : -1) * Vector3.Angle(Vector3.up, targetPosition - position);
//        trans.localRotation = Quaternion.identity;
        trans.localRotation = Quaternion.Euler(Vector3.forward * angle);
    }
    /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static void LookAt2D(this Transform transform, Vector2 forward)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }
    
    public static DateTime StartOfDay(this DateTime theDate)
    {
        return theDate.Date;
    }

    public static DateTime EndOfDay(this DateTime theDate)
    {
        return theDate.Date.AddDays(1).AddTicks(-1);
    }

    public static float TotalSecondsInADay()
    {
        return 86400;
    }

    public static string ToJson(this object obj)
    {
        return JsonUtility.ToJson(obj);
    }

    public static T FromJson<T>(this string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
