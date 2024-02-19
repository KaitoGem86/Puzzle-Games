using System;
using System.Collections;
using UnityEngine;

public static class CounterTime
{
    public static IEnumerator CounterDown(int duration, float delay = 1, Action<int> OnCounter = null, Action OnComplete = null)
    {
        int counter = duration;
        OnCounter?.Invoke(counter);
        while (counter > 0)
        {
            yield return new WaitForSeconds(delay);
            counter--;
            OnCounter?.Invoke(counter);
        }
        OnComplete?.Invoke();
    }

    public static IEnumerator CounterUp(int
         duration, float delay = 1, Action<int> OnCounter = null, Action OnComplete = null)
    {
        int counter = 0;
        OnCounter?.Invoke(counter);
        while (counter < duration)
        {
            yield return new WaitForSeconds(delay);
            counter++;
            OnCounter?.Invoke(counter);
        }
        OnComplete.Invoke();
    }
}
