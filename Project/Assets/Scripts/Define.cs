namespace SheepSheep
{
    public static class Define
    {
        // 卡牌宽度
        public const int CARD_X = 45;
        // 卡牌高度
        public const int CARD_Y = 50;

        // 一张卡是 45*50
        // 地图大概是 325*425
        // x重叠部分最小为5，卡面露出最大45
        // maxX = (325-45)/5 + 1
        public const int MAX_X = 57;

        // y重叠部分为一半，即25
        // maxY = 425 / 50 * 0.5
        public const int MAX_Y = 17;

        // z随便猜的
        public const int MAX_Z = 5;

        // 卡牌种类
        public const int NUM_TYPE_COUNT = 13;
    }
}