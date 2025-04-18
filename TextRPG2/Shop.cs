namespace TextRPG2
{
    public class Shop
    {
        public void ShopMenu(Player player, Item item, Inventory inventory)
        {
            bool isFinished = false;
            List<Dictionary<string, string>> printItems = item.items;

            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상점");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < printItems.Count; i++)
                {
                    Console.Write($"- ");
                    Console.Write($" {printItems[i]["name"]} " +
                        $"| {(printItems[i]["type"] == "DefensePower" ? "방어력" : "공격력")} + {printItems[i]["power"]} " +
                        $"| {printItems[i]["descript"]} " +
                        $"| {(inventory.InventoryItems.Contains(printItems[i]) ? "구매완료" : printItems[i]["gold"] + "G")}");
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("2. 아이템 판매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
                Console.Write(">>");

                int nextAction = int.TryParse(Console.ReadLine(), out var input) ? input : -1;
                if (nextAction == 0)
                {
                    isFinished = true;
                }
                else if (nextAction == 1)
                {
                    ShopBuyItem(player, item, inventory);
                }
                else if (nextAction == 2)
                {
                    ShopSaleItem(player, item, inventory);
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
            
        }

        // 아이템 구매 메뉴로 이동
        public void ShopBuyItem(Player player, Item item, Inventory inventory)
        {
            List<Dictionary<string, string>> printItems = item.items;
            bool isFinished = false;
            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상점 - 아이템 구매");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < printItems.Count; i++)
                {
                    Console.Write($"- {(i + 1)}");
                    Console.Write($" {printItems[i]["name"]} " +
                        $"| {(printItems[i]["type"] == "DefensePower" ? "방어력" : "공격력")} + {printItems[i]["power"]} " +
                        $"| {printItems[i]["descript"]} " +
                        $"| {( inventory.InventoryItems.Contains(printItems[i])  ? "구매완료" : printItems[i]["gold"] + "G")}");

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
                    isFinished = true;
                }
                else if (nextAction > 0 && printItems.Count >= nextAction)
                {
                    TryShopBuyItem(player, printItems[nextAction -1], inventory);
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        // 아이템 구매 시도
        public void TryShopBuyItem(Player player, Dictionary<string, string> itemInfo, Inventory inventory)
        {
            int price = int.Parse(itemInfo["gold"]);

            if (inventory.InventoryItems.Contains(itemInfo))
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
                
            }
            else if (player.Gold >= price)
            {
                player.Gold -= price;
                inventory.InventoryItems.Add(itemInfo);
                Console.WriteLine($"구매를 완료했습니다! 남은 Gold : {player.Gold} G");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            
            Thread.Sleep(2000);
        }

        // 아이템 판매 메뉴로 이동
        public void ShopSaleItem(Player player, Item item, Inventory inventory)
        {
            List<Dictionary<string, string>> printItems = inventory.InventoryItems;
            bool isFinished = false;
            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상점 - 아이템 판매");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < printItems.Count; i++)
                {
                    Console.Write($"- {(i + 1)}");
                    Console.Write($" {printItems[i]["name"]} " +
                        $"| {(printItems[i]["type"] == "DefensePower" ? "방어력" : "공격력")} + {printItems[i]["power"]} " +
                        $"| {printItems[i]["descript"]} " +
                        $"| {printItems[i]["gold"] + "G"}");

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
                    isFinished = true;
                }
                else if (nextAction > 0 && printItems.Count >= nextAction)
                {
                    TryShopSaleItem(player, printItems[nextAction - 1], inventory);
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        // 아이템 판매 시도
        public void TryShopSaleItem(Player player, Dictionary<string, string> itemInfo, Inventory inventory)
        {
            int price = int.Parse(itemInfo["gold"]);
            
            // 장착중이라면 상태 조정
            if (itemInfo["equip"] == "check")
            {
                string type = itemInfo["type"];
                int power = int.Parse(itemInfo["power"]);
                if (type == "AttackPower")
                {
                    player.AttackBonus -= power;
                }
                else if (type == "DefensePower")
                {
                    player.DefenseBonus -= power;
                }
                itemInfo["equip"] = "";
            }
            player.Gold += (int)(price * 0.85);
            inventory.InventoryItems.Remove(itemInfo);

            Console.WriteLine($"판매 완료! 남은 Gold : {player.Gold} G");
            Thread.Sleep(2000);
        }
    }
}
