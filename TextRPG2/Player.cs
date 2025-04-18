
namespace TextRPG2
{
    public class Player
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public double Attack {  get; set; }
        public int Defense { get; set; }
        public int Health {  get; set; }
        public int Gold { get; set; }
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public int ClearCount { get; set; }

        // 정보 없는 생성자
        public Player()
        {
            this.Name = "Unknown";
            this.Job = "Unknown";
            this.Level = 1;
            this.Attack = 10.0;
            this.Defense = 5;
            this.Health = 70;
            this.Gold = 100000;
            this.AttackBonus = 0;
            this.DefenseBonus = 0;
            this.ClearCount = 0;
        }

        public Player(string name, string job)
        {
            this.Name = name;
            this.Job = job;
            this.Level = 1;
            this.Attack = 10.0;
            this.Defense = 5;
            this.Health = 70;
            this.Gold = 100000;
            this.AttackBonus = 0;
            this.DefenseBonus = 0;
            this.ClearCount = 0;
        }

        // Intro 직업 int -> string 변환 메소드
        public string JobName(int selectJob)
        {
            string jobName = "";
            switch (selectJob)
            {
                case 1:
                    jobName = "전사";
                    break;
                case 2:
                    jobName = "도적";
                    break;
                default:
                    break;
            }
            return jobName;
        }

        // 1번 메뉴 출력 메소드(PlayerInfo)
        public void PlayerInfoMenu()
        {
            bool isFinished = false;
            while (!isFinished)
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"Name : {Name}");
                Console.WriteLine($"Lv. {Level:00}");
                Console.WriteLine($"Chad ({Job})");
                Console.WriteLine($"공격력 : {Attack}{(AttackBonus > 0 ? " (+" + AttackBonus + ")" : "")}");
                Console.WriteLine($"방어력 : {Defense}{(DefenseBonus > 0 ? " (+" + DefenseBonus + ")" : "")}");
                Console.WriteLine($"체  력 : {Health}");
                Console.WriteLine($"Gold : {Gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
                Console.Write(">>");

                int nextAction = int.TryParse(Console.ReadLine(), out var input) ? input : -1;

                if (nextAction == 0) { 
                    isFinished = true;
                } else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        // 캐릭터 휴식
        public void PlayerRestMenu()
        {
            bool isFinished = false;
            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("휴식하기");
                Console.ForegroundColor = originalColor;
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {Gold} G)");
                Console.WriteLine();
                Console.WriteLine("1. 휴식하기");
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
                    TryPlayerRest();
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        public void TryPlayerRest()
        {
            int price = 500;

            if (Gold >= 500 && Health < 100)
            {
                Health = 100;
                Gold -= price;
                Console.WriteLine($"휴식을 완료했습니다. 남은 골드 {Gold} G");
            }
            else if (Health == 100)
            {
                Console.WriteLine("체력이 이미 100 입니다.");
            }
            else
            {
                Console.WriteLine("Gold가 부족합니다.");
            }

            Thread.Sleep(2000);
        }
    }
}
