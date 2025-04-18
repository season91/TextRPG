using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextRPG2
{
    public class Item
    {
        public List<Dictionary<string, string>> Items { get; set; }
        public int itemCount = 6; // 하드코딩

        public Item() { 
            this.Items = new List<Dictionary<string, string>>();
        }
        public Item(string init)
        {
            this.Items = new List<Dictionary<string, string>>();
            string itemNo = "";
            string name = "";
            string type = "";
            string power = "";
            string descript = "";
            string gold = "";
            string equip = "";

            for (int i = 0; i < itemCount; i++)
            {
                Dictionary<string, string> itemInfo = new Dictionary<string, string>();
                switch (i)
                {
                    case 0:
                        itemNo = "0";
                        name = "수련자 갑옷";
                        type = "DefensePower";
                        power = "5";
                        descript = "수련에 도움을 주는 갑옷입니다.";
                        gold = "1000";
                        break;
                    case 1:
                        itemNo = "1";
                        name = "무쇠 갑옷";
                        type = "DefensePower";
                        power = "9";
                        descript = "무쇠로 만들어져 튼튼한 갑옷입니다.";
                        gold = "2000";
                        break;
                    case 2:
                        itemNo = "2";
                        name = "스파르타의 갑옷";
                        type = "DefensePower";
                        power = "15";
                        descript = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
                        gold = "3500";
                        break;
                    case 3:
                        itemNo = "3";
                        name = "낡은 검";
                        type = "AttackPower";
                        power = "2";
                        descript = "쉽게 볼 수 있는 낡은 검 입니다.";
                        gold = "600";
                        break;
                    case 4:
                        itemNo = "4";
                        name = "청동 도끼";
                        type = "AttackPower";
                        power = "5";
                        descript = "어디선가 사용됐던거 같은 도끼입니다.";
                        gold = "1500";
                        break;
                    case 5:
                        itemNo = "5";
                        name = "스파르타의 창";
                        type = "AttackPower";
                        power = "7";
                        descript = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
                        gold = "4000";
                        break;
                }

                itemInfo.Add("itemNo", itemNo);
                itemInfo.Add("name", name);
                itemInfo.Add("type", type);
                itemInfo.Add("power", power);
                itemInfo.Add("descript", descript);
                itemInfo.Add("gold", gold);
                itemInfo.Add("equip", equip);

                this.Items.Add(itemInfo);
            }
        }

        // Item과 Inventory 보유 중인지 비교
        public bool IsSameItem(Dictionary<string, string> item, Inventory inventory)
        {
            bool result = false;
            foreach (var inven in inventory.InventoryItems)
            {
                if (item["itemNo"] == inven["itemNo"])
                {
                    result = true;
                }
            }
            return result;
        }
        
    }
}
