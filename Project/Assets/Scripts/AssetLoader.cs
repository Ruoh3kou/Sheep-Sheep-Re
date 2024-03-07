using UnityEngine;

namespace SheepSheep
{
    public static class AssetLoader
    {
        private static string cardPath = "Sprites/Card/";
        private static string grayCardPath = "Sprites/GrayCard/";

        public static Sprite LoadCard(int typeId)
        {
            var path = cardPath + (typeId + 1);
            var res = Resources.Load<Sprite>(path);
            return res;
        }
        
        public static Sprite LoadGrayCard(int typeId)
        {
            var path = grayCardPath + (typeId + 1);
            var res = Resources.Load<Sprite>(path);
            return res;
        }
    }
}