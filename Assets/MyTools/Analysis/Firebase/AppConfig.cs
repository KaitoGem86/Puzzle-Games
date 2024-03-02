using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AppConfig : SingletonMonoBehaviour<AppConfig>
{
    [Header("~~~~~VALUE DEFAULT~~~~~")]
        [SerializeField] int _bannerAdLevel;
        [SerializeField] int _interAdLevel;
        [SerializeField] int _interFrequencyTime;
        [SerializeField] bool _isShowInterWithClosePopupRemoveAds;
        [Space(10)]
        [Header("~~~~~~~VALUE IS FETCH~~~~~~")]
        [SerializeField] string ValueIsFetch;

        public int BannerAdLevel
        {
            get => _bannerAdLevel;
            set
            {
                _bannerAdLevel = value;
                AppendValueIsFetch(value.ToString());
            }
        }

        public int InterAdLevel
        {
            get => _interAdLevel;
            set
            {
                _interAdLevel = value;
                AppendValueIsFetch(value.ToString());
            }
        }

        public int InterFrequencyTime
        {
            get => _interFrequencyTime;
            set
            {
                _interFrequencyTime = value;
                AppendValueIsFetch(value.ToString());
            }
        }

        public bool IsShowInterWithClosePopupRemoveAds
        {
            get => _isShowInterWithClosePopupRemoveAds;
            set
            {
                _isShowInterWithClosePopupRemoveAds = value;
                AppendValueIsFetch(value.ToString());
            }
        }

        private void AppendValueIsFetch(string value)
        {
            ValueIsFetch += $"|{value}|";
        }
}

