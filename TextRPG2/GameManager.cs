using System;
using System.IO;
using System.Text.Json;

namespace TextRPG2
{
    internal class GameManager
    {
        static void Main(string[] args)
        {
            bool isFinished = false;
            do
            {
                Player player;
                Inventory inventory;
                Item item;

                // JSON 저장데이터 있다면 로딩
                if (File.Exists("backupData.json"))
                {
                    string loadJson = File.ReadAllText("backupData.json");
                    // 빈파일인지 아닌지 검증
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("이전 정보를 불러옵니다....");
                    Thread.Sleep(2000);
                    if (string.IsNullOrWhiteSpace(loadJson))
                    {
                        player = new Player();
                        item = new Item("init");
                        inventory = new Inventory();
                    }
                    else
                    {
                        var loadData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(loadJson);
                        // 역직렬화
                        player = loadData["Player"].Deserialize<Player>();
                        item = loadData["item"].Deserialize<Item>();
                        inventory = loadData["Inventory"].Deserialize<Inventory>();
                    }
                } else
                {
                    player = new Player();
                    item = new Item("init");
                    inventory = new Inventory();
                }

                // GameIntro
                while (player.Name == "Unknown" && player.Job == "Unknown")
                {
                    Console.Clear();
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("원하시는 이름을 설정해주세요.");
                    Console.WriteLine();
                    Console.Write(">>");
                    string name = Console.ReadLine();
                    if (name.Length > 0)
                    {
                        player.Name = name;
                    }
                    else
                    {
                        ErrorMessage("잘못된 입력입니다.");
                        Thread.Sleep(2000);
                    }

                    Console.WriteLine();
                    Console.WriteLine($"입력하신 이름은 {name} 입니다.");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 직업을 선택해주세요.");
                    Console.WriteLine();
                    Console.WriteLine("1. 전사");
                    Console.WriteLine("2. 도적");
                    Console.WriteLine();
                    Console.Write(">>");

                    int selectJob = int.TryParse(Console.ReadLine(), out var input) ? input : -1;
                    string job = player.JobName(selectJob);

                    if (job.Length > 0)
                    {
                        player.Job = job;
                    }
                    else
                    {
                        ErrorMessage("잘못된 입력입니다.");
                        Thread.Sleep(2000);
                    }
                }

                // GameStart
                
                
                Shop shop = new Shop();
                Dunjeon dunjeon = new Dunjeon();
                bool isGameOff = false;
                while (!isGameOff)
                {
                    Console.Clear();
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine("1. 상태 보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine("4. 던전입장");
                    Console.WriteLine("5. 휴식하기");
                    Console.WriteLine("0. 게임종료");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.WriteLine();
                    Console.Write(">>");

                    int nextAction = int.TryParse(Console.ReadLine(), out var input) ? input : -1;
                    if (nextAction >= 0 && nextAction < 6)
                    {
                        switch (nextAction)
                        {
                            case 0:
                                // 저장할 데이터는 player와 inventory 정도
                                var backupData = new Dictionary<string, object>()
                                {
                                    {nameof(Player), player },
                                    {nameof(Inventory), inventory},
                                    {nameof(Item), item}
                                };

                                var options = new JsonSerializerOptions { WriteIndented = true };
                                string json = JsonSerializer.Serialize(backupData, options);
                                File.WriteAllText("backupData.json", json);
                                isGameOff = true;
                                isFinished = true;
                                break;
                            case 1:
                                // 상태보기 메소드
                                player.PlayerInfoMenu();
                                break;
                            case 2:
                                // 인벤토리 메소드
                                inventory.InventoryMenu(player);
                                break;
                            case 3:
                                // 상점 메소드
                                shop.ShopMenu(player, item, inventory);
                                break;
                            case 4:
                                // 던전입장 메소드
                                dunjeon.DunjeonMenu(player);
                                break;
                            case 5:
                                // 휴식 메소드
                                player.PlayerRestMenu();
                                break;
                        }
                    }
                    else
                    {
                        ErrorMessage("잘못된 입력입니다.");
                    }
                }
            } while (!isFinished);
        }

        // 공통 - 잘못 입력시 문구 출력 문세지
        public static void ErrorMessage(string message)
        {
            Console.WriteLine(message);
            Thread.Sleep(2000);
        }
    }
}
