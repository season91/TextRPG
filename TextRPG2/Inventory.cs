using System;
using System.Xml.Linq;

namespace TextRPG2
{
    public class Inventory
    {
        public List<Dictionary<string, string>> InventoryItems { get; set; }
        public Inventory() {
            this.InventoryItems = new List<Dictionary<string, string>>();
        }

        public void InventoryMenu(Player player)
        {
            bool isFinished = false;
            List<Dictionary<string, string>> printItems = InventoryItems;
            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("인벤토리");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                for (int i = 0; i < printItems.Count; i++)
                {
                    Console.Write($"- ");
                    Console.Write($"{(printItems[i]["equip"] == "check" ? "[E]" : "")}");
                    Console.Write($" {printItems[i]["name"]} " +
                        $"| {(printItems[i]["type"] == "DefensePower" ? "방어력" : "공격력")} + {printItems[i]["power"]} " +
                        $"| {printItems[i]["descript"]} " +
                        $"| {(printItems[i]["gold"] + "G")}");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
                Console.Write(">>");

                int nextAction = int.TryParse(Console.ReadLine(), out var input) ? input : -1;
                if (nextAction == 0)
                {
                    isFinished= true;
                }
                else if (nextAction == 1)
                {
                    EquipItemManage(player, InventoryItems);
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        public void EquipItemManage(Player player, List<Dictionary<string, string>> inventoryItems)
        {
            bool isFinished = false;
            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                Console.WriteLine();

                for (int i = 0; i < InventoryItems.Count; i++)
                {
                    Console.Write($"- {(i + 1)}");
                    Console.Write($"{(inventoryItems[i]["equip"] == "check" ? " [E]" : "")}");
                    Console.Write($" {inventoryItems[i]["name"]} " +
                        $"| {(inventoryItems[i]["type"] == "DefensePower" ? "방어력" : "공격력")} + {inventoryItems[i]["power"]} " +
                        $"| {inventoryItems[i]["descript"]} ");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
                Console.Write(">>");

                int nextAction = int.TryParse(Console.ReadLine(), out var input) ? input : -1;
                if (nextAction == 0)
                {
                    isFinished  = true;
                }
                else if (nextAction > 0 && nextAction <= inventoryItems.Count)
                {
                    TryEquipItem(player, nextAction - 1, inventoryItems);
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        public void SwapEquipAttackItem(Player player, int idx, List<Dictionary<string, string>> inventoryItems)
        {
            int power = int.Parse(inventoryItems[idx]["power"]);

            var equipAttackItem = (from x in inventoryItems.Select((item, index) => new { item, index })
                                        where x.item["equip"] == "check" && x.item["type"] == "AttackPower"
                                        select (x.index, x.item)).FirstOrDefault();
            if (equipAttackItem.item != null)
            {
                // 기존 장착 해제
                inventoryItems[equipAttackItem.index]["equip"] = "";
                player.AttackBonus -= int.Parse(inventoryItems[equipAttackItem.index]["power"]);

                Console.WriteLine("기존 무기 장착 해제했습니다!");
            }
            
            // 장착 처리
            inventoryItems[idx]["equip"] = "check";
            player.AttackBonus += power;

            Console.WriteLine("장착을 완료했습니다!");
            Thread.Sleep(2000);
        }

        public void SwapEquipDefenseItem(Player player, int idx, List<Dictionary<string, string>> inventoryItems)
        {
            int power = int.Parse(inventoryItems[idx]["power"]);

            var equipDefenseItem = (from x in inventoryItems.Select((item, index) => new { item, index })
                                        where x.item["equip"] == "check" && x.item["type"] == "DefensePower"
                                        select (x.index, x.item)).FirstOrDefault();
            if (equipDefenseItem.item != null)
            {
                // 기존 장착 해제
                inventoryItems[equipDefenseItem.index]["equip"] = "";
                player.DefenseBonus -= int.Parse(inventoryItems[equipDefenseItem.index]["power"]);

                Console.WriteLine("기존 방어구 장착 해제했습니다!");
            }

            // 장착 처리
            inventoryItems[idx]["equip"] = "check";
            player.DefenseBonus += power;

            Console.WriteLine("장착을 완료했습니다!");
            Thread.Sleep(2000);
        }

        // 장착 개선 적용
        public void TryEquipItem(Player player, int index, List<Dictionary<string, string>> inventoryItems)
        {
            string type = inventoryItems[index]["type"];
            int power = int.Parse(inventoryItems[index]["power"]);

            if (inventoryItems[index]["equip"] == "")
            {
                if (type == "AttackPower")
                {
                    SwapEquipAttackItem(player, index, inventoryItems);
                } 

                if (type == "DefensePower")
                {
                    SwapEquipDefenseItem(player, index, inventoryItems);
                }
            }
            else
            {
                if (type == "AttackPower")
                {
                    player.AttackBonus -= power;
                }
                else if (type == "DefensePower")
                {
                    player.DefenseBonus -= power;
                }

                inventoryItems[index]["equip"] = "";
                Console.WriteLine("장착 해제 했습니다!");
            }

            Thread.Sleep(2000);
        }
    }
}
