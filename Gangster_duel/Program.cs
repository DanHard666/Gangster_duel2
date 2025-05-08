using System;

namespace Gangster_duel
{
    internal class Program
    {
        static public Random random = new Random();

        static void Main(string[] args)
        {
            bool Is_bloods = false;           
            bool disassembly = false;
            int gang = 0;

            int[] scores = new int[2];
            ref int bloods_score = ref scores[0];
            ref int crips_score = ref scores[1];
            bool move = random.Next(2) == 0;

            Attacker[] bloodsAttackers = AllGangsters.BloodsAttackers();
            Defender[] bloodsDefenders = AllGangsters.BloodsDefenders();
            Attacker[] cripsAttackers = AllGangsters.CripsAttackers();
            Defender[] cripsDefenders = AllGangsters.CripsDefenders();

            //Gangster[,] allBloods = bloodsAttackers.Cast<Gangster>().Concat(bloodsDefenders.Cast<Gangster>()).ToArray();
            //Gangster[] allBloods = bloodsAttackers.Cast<Gangster>().Concat(bloodsDefenders.Cast<Gangster>()).ToArray();

            while (gang != 1 && gang != 2)
            {
                Console.WriteLine($"Выберите свою группировку: \n1) Бладс (Агрессивные:+15% к атаке, но +20% усталость) \n2) Крипс (Осторожные:+15% к защите, но -10% к атаке)");
                gang = int.Parse(Console.ReadLine());
                if (gang != 1 && gang != 2)
                {
                    Console.WriteLine("Неккоректное значение");
                }
            }
            //gang--;

            if (gang == 1)
                Is_bloods = true;

            if (move)
            {
                Console.WriteLine("Первым атакуете вы");
                WhichGang(bloodsAttackers, cripsDefenders, cripsAttackers, bloodsDefenders, scores, Is_bloods, false, 0);
            }
            else
            {
                Console.WriteLine("Первым атакует противник");
                WhichGang(bloodsAttackers, cripsDefenders, cripsAttackers, bloodsDefenders, scores, !Is_bloods, false, 0);
            }
            Console.WriteLine($"Счет Бладс:{bloods_score}  Крипс:{crips_score} \n");

            while ((bloods_score < 5 && crips_score < 5) && (bloods_score != 4 || crips_score != 4))
            {
                move = !move;
                if (move)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ваш ход");
                    Console.ResetColor();
                    WhichGang(bloodsAttackers, cripsDefenders, cripsAttackers, bloodsDefenders, scores, Is_bloods, false, 0);

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вражеский ход");
                    Console.ResetColor();
                    WhichGang(bloodsAttackers, cripsDefenders, cripsAttackers, bloodsDefenders, scores, !Is_bloods, false, 0);
                }
                Console.WriteLine($"Счет Бладс:{bloods_score}  Крипс:{crips_score} \n");
            }


            if (bloods_score == 4 && crips_score == 4)
            {
                int which_member_attack = 0;
                int additional = 0;
                disassembly = true;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Разборка");
                Console.ResetColor();
                while (bloods_score < 5 && crips_score < 5)
                {
                    if (additional == 2)
                    {
                        which_member_attack++;
                        additional = 0;
                    }
                    if (which_member_attack == 4)
                    {
                        which_member_attack = 0;
                    }
                    move = !move;
                    if (move)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ваш ход");
                        Console.ResetColor();
                        WhichGang(bloodsAttackers, cripsDefenders, cripsAttackers, bloodsDefenders, scores, Is_bloods, disassembly, which_member_attack);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Вражеский ход");
                        Console.ResetColor();
                        WhichGang(bloodsAttackers, cripsDefenders, cripsAttackers, bloodsDefenders, scores, !Is_bloods, disassembly, which_member_attack);
                        Console.WriteLine();
                    }
                    additional++;
                }
            }

            if (bloods_score >= 5)
            {
                Console.WriteLine("Выиграли бладс");
            }
            else
            {
                Console.WriteLine("Выиграли крипс");
            }
        }

        static internal void WhichGang(Attacker[] bloodsAttackers, Defender[] cripsDefenders, Attacker[] cripsAttackers, Defender[] bloodsDefenders, int[] scores, bool Is_bloods, bool disassembly, int which_member_attack)
        {
            if (Is_bloods == true)
            {
                Attacking(bloodsAttackers, cripsDefenders, scores, 0, disassembly, which_member_attack);
            }
            else
            {
                Attacking(cripsAttackers, bloodsDefenders, scores, 1, disassembly, which_member_attack);
            }
        }

        static internal void Attacking(Attacker[] attackers, Defender[] defenders, int[] scores, int gang, bool disassembly, int which_member_attack)
        {
            Attacker attacker = attackers[random.Next(attackers.Length)];
            Defender defender = defenders[random.Next(defenders.Length)];
            if (disassembly == true)
            {
                attacker = attackers[which_member_attack];
            }

            attacker.Characteristic(gang);
            defender.Characteristic(gang);

            if (gang == 0)
            {
                Console.WriteLine($"Атакующий {attacker.name} (стамина:{attacker.stamina} - 20,атака:{attacker.damage} + 15,точность:{attacker.accuracy}) атакует защищающегося " +
                                $"{defender.name} (стамина:{defender.stamina},защита:{defender.defence} + 15,реакция:{defender.reaction})");
            }
            else
            {
                Console.WriteLine($"Атакующий {attacker.name} (стамина:{attacker.stamina},атака:{attacker.damage} - 10,точность:{attacker.accuracy}) атакует защищающегося " +
                                $"{defender.name} (стамина:{defender.stamina},защита:{defender.defence} ,реакция:{defender.reaction})");
            }


            Shoot_or_not(attacker.damage, defender.defence, scores, gang, attacker.accuracy, defender.reaction);
            if (disassembly == false)
            {
                attacker.Checking_stamina(random.Next(8, 13));
                defender.Checking_stamina(random.Next(8, 13));
            }
        }

        static internal void Shoot_or_not(int damage, int defence, int[] scores, int gang, int accuracy, int reaction)
        {


            if (random.Next(1, 101) <= 15)
            {
                Console.WriteLine("Осечка!");
                damage -= 20;
            }
            if (random.Next(1, 101) <= 15)
            {
                Console.WriteLine("Потеря позиции!");
                defence -= 20;
            }

            if (damage > defence)
            {
                if (random.Next(1, 101) <= accuracy)
                {
                    scores[gang]++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Попадание");
                    Console.ResetColor();
                }
            }
            else
            {
                if (random.Next(1, 101) > reaction)
                {
                    scores[gang]++;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Попадание");
                    Console.ResetColor();
                }
            }
        }
    }


    
}
