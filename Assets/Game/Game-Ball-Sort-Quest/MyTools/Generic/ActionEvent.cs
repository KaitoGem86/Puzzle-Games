using System;

namespace BallSortQuest
{
    public static class ActionEvent
    {
        public static Action OnResetGamePlay;

        public static Action OnProcessComplete;

        #region Booster
        public static Action OnUseBoosterRevoke;
        public static Action OnUserBoosterAdd;
        #endregion

        #region Select Shop
        public static Action OnSelectShopBackground;
        public static Action OnSelectShopTube;
        #endregion
    }
}