namespace TextRPG2
{
    public class Item_arr
    {
        public int itemNo;
        public string name;
        public string type;
        public int power;
        public string descript;
        public int gold;

        public Item_arr()
        {
            string[] names = { "수련자 갑옷", "무쇠 갑옷", "스파르타의 갑옷", "낡은 검", "청동 도끼", "스파르타의 창"};
            string[] types = { "DefensePower", "AttackPower"};
            int[] powers = { };
        }
    }
}
