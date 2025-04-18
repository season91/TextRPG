using System;
using System.Numerics;

namespace TextRPG2
{
    public class Dunjeon
    {
        private int donjeonDefense;
        private int defaultGold;

        public Dunjeon()
        {
            this.donjeonDefense = 0;
            this.defaultGold = 0;
        }
        // 3가지 난이도 
        // 난이도는 방어력으로 판단 
        // easy 방어력 5이상 - 보상 1천골드 + 추가보상 (공격력10이면 10~20퍼 추가보상 / 공격력 15면 15~30퍼 추가보상. 공격력~공격력*2 범위내
        // normal 방어력 11이상 - 보상 1700골드
        // hard 방어력 17 이상 - 2500 골드

        // 권장방어력보다 낮다면 40% 확률로 실패 - 보상없고 체력 반 감소

        // 권장방어력보다 놎다면 클리어 - 권장방어력에 따라 종료시 체력 감소
        // 기본 체력 감소량 20~35 랜덤 
        // 공식 : 내 방어력 - 권장 방어력 = 숫자 만큼 증갑폭 랜덤값에 설정
        // 차이가 양수 2라면 (내 방어력이 더 높을 때) 기본 체력 감소량 20(-2) ~ 35(-2) 사이 랜덤 값
        // 차이가 음수 -6이라면 (내 방어력이 낮을 때) 기본 체력 감소량 20(+6) ~ 35(+6) 사이 랜덤 값

        public void DunjeonMenu(Player player)
        {
            bool isFinished = false;
            while (!isFinished)
            {
                Console.Clear();
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("던전입장");
                Console.ForegroundColor = originalColor;
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전   | 방어력 17 이상 권장");
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
                else if (nextAction >= 1 && nextAction <= 3)
                {
                    TryDunjeonPlay(nextAction, player);
                }
                else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }
        }

        public void TryDunjeonPlay(int donjeonLevel, Player player)
        {
            bool isFinished = false;
            while (!isFinished)
            {
                string difficulty = "";
                int beforeHealth = player.Health;
                int beforeGold = player.Gold;
                if (donjeonLevel == 1)
                {
                    donjeonDefense = 5;
                    defaultGold = 1000;
                    difficulty = "쉬운 던전";
                }
                else if (donjeonLevel == 2)
                {
                    donjeonDefense = 11;
                    defaultGold = 1700;
                    difficulty = "일반 던전";
                }
                else if (donjeonLevel == 3)
                {
                    donjeonDefense = 17;
                    defaultGold = 2500;
                    difficulty = "어려운 던전";
                }

                int totalDefense = player.Defense + player.DefenseBonus;
                int diffDefense = totalDefense - donjeonDefense;

                if (diffDefense >= 0)
                {
                    // 100퍼 성공
                    DunjeonClear(player, diffDefense, difficulty);
                }
                else
                {
                    int succesPercent = RandomeNumber(1, 10);
                    if (succesPercent >= 6)
                    {
                        // 60퍼 확률로 성공
                        DunjeonClear(player, diffDefense, difficulty);
                    }
                    else
                    {
                        DunjeonFail(player, difficulty);
                    }
                        
                }

                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"체력 {beforeHealth} -> {player.Health}");
                Console.WriteLine($"Gold {beforeGold} -> {player.Gold}");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.Write(">>");
                int nextAction = int.TryParse(Console.ReadLine(), out var input) ? input : -1;

                if (nextAction == 0)
                {
                    isFinished = true;
                } else
                {
                    GameManager.ErrorMessage("잘못된 입력입니다.");
                }
            }

        }

        // 랜섬 수치 리턴 (int)
        public int RandomeNumber(int min, int max)
        {
            int randomeNumber = 0;
            Random random = new Random();

            randomeNumber = random.Next(min, max+1);

            return randomeNumber;
        }

        // 랜섬 수치 리턴 (double)
        public double RandomeDouble(double min, double max)
        {
            double randomeDouble = 0.0;
            Random random = new Random();

            randomeDouble = Math.Round( random.NextDouble() * (max - min) + min , 0) / 100;

            return randomeDouble;
        }

        // 레벨업
        public void LevelUpValidation(Player player)
        {
            player.ClearCount += 1;

            if (player.Level == 1 && player.ClearCount == 1)
            {
                player.Level += 1;
                player.Attack += 0.5;
                player.Defense += 1;
                player.ClearCount = 0;
            }
            else if (player.Level == 2 && player.ClearCount == 2)
            {
                player.Level += 1;
                player.Attack += 0.5;
                player.Defense += 1;
                player.ClearCount = 0;
            }
            else if (player.Level == 3 && player.ClearCount == 3)
            {
                player.Level += 1;
                player.Attack += 0.5;
                player.Defense += 1;
                player.ClearCount = 0;
            }
            else if (player.Level == 4 && player.ClearCount == 4)
            {
                player.Level += 1;
                player.Attack += 0.5;
                player.Defense += 1;
                player.ClearCount = 0;
            }
        }

        // 던전 클리어
        public void DunjeonClear(Player player, int diffDefense, string difficulty)
        {
            // 체력 감소 수치
            int minusHealth = (RandomeNumber(20, 35)) - diffDefense;
            // 추가 골드 보상 퍼센트
            double totalAttack = player.Attack + player.AttackBonus;
            double bonusGoldPercent = RandomeDouble(totalAttack, (totalAttack * 2));
            // 추가 골드
            int bonusGold = (int)(Math.Round(defaultGold * bonusGoldPercent, 0));

            // 레벨업 처리
            if (player.Level < 5)
            {
                LevelUpValidation(player);
            } 

            // 체력 감소 처리
            if (player.Health >= 0)
            {
                player.Health -= minusHealth;

                // 예외처리
                if(player.Health < 0)
                {
                    player.Health = 0;
                }
            }
            else
            {
                player.Health = 0;
            }

            // Gold 증가 처리
            player.Gold += defaultGold + bonusGold;

            Console.Clear();
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("던전 클리어");
            Console.ForegroundColor = originalColor;
            Console.WriteLine("축하합니다!!");
            Console.WriteLine($"{difficulty}을 클리어 하였습니다.");
        }

        // 던전 실패
        public void DunjeonFail(Player player, string difficulty)
        {
            // 체력 감소 처리
            if (player.Health >= 0)
            {
                player.Health -= (player.Health / 2);
                player.Health = player.Health < 0 ? 0 : player.Health;
            }
            else
            {
                player.Health = 0;
            }

            Console.Clear();
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("던전 실패");
            Console.ForegroundColor = originalColor;
            Console.WriteLine($"{difficulty}을 실패하였습니다.");
        }

    }
}
