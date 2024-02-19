using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    private static bool sound, music, vibrate;
    private const string KEY_SOUND = "SETTING_SOUND";
    private const string KEY_MUSIC = "SETTING_MUSIC";
    private const string KEY_VIBRATE = "SETTING_VIBRATE";



    private static void Awake()
    {
        SOUND = PlayerPrefs.GetInt(KEY_SOUND, 1) == 1;
        MUSIC = PlayerPrefs.GetInt(KEY_MUSIC, 1) == 1;
        VIBRATE = PlayerPrefs.GetInt(KEY_VIBRATE, 1) == 1;
    }

    public static Action<bool> onChangeSoundValue = delegate { };
    public static bool SOUND
    {
        get { return sound; }
        set
        {
            sound = value;
            PlayerPrefs.SetInt(KEY_SOUND, sound ? 1 : 0);
            onChangeSoundValue(value);
        }
    }

    public static Action<bool> onChangeMusicValue;
    public static bool MUSIC
    {
        get { return music; }
        set
        {
            music = value;
            PlayerPrefs.SetInt(KEY_MUSIC, music ? 1 : 0);
            onChangeMusicValue?.Invoke(value);
        }
    }

    public static bool VIBRATE
    {
        get { return vibrate; }
        set
        {
            vibrate = value;
            PlayerPrefs.SetInt(KEY_VIBRATE, vibrate ? 1 : 0);
        }
    }
}
