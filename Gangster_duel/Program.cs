using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

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



    internal static class AllGangsters
    {
        internal static Attacker[] BloodsAttackers()
        {
            return new Attacker[]
            {
        new Attacker
        {
            name = "Red_Joe",
            stamina = 100,
            damage = 70,
            accuracy = 80
        },
        new Attacker
        {
            name = "Small_Tim",
            stamina = 100,
            damage = 68,
            accuracy = 82
        },
        new Attacker
        {
            name = "Crazy_Franklin",
            stamina = 95,
            damage = 72,
            accuracy = 78
        },
        new Attacker
        {
            name = "Fast_Lamar",
            stamina = 95,
            damage = 74,
            accuracy = 76
        }
            };
        }

        internal static Defender[] BloodsDefenders()
        {
            return new Defender[]
            {
        new Defender
        {
            name = "Big_CJ",
            stamina = 90,
            defence = 85,
            reaction = 80
        },
        new Defender
        {
            name = "Bad_Kai",
            stamina = 90,
            defence = 82,
            reaction = 83
        }
            };
        }

        internal static Attacker[] CripsAttackers()
        {
            return new Attacker[]
            {
        new Attacker
        {
            name = "Chill_Tony",
            stamina = 100,
            damage = 65,
            accuracy = 88
        },
        new Attacker
        {
            name = "Angry_George",
            stamina = 100,
            damage = 70,
            accuracy = 80
        },
        new Attacker
        {
            name = "Lil_T",
            stamina = 95,
            damage = 75,
            accuracy = 72
        },
        new Attacker
        {
            name = "Black_M",
            stamina = 95,
            damage = 78,
            accuracy = 70
        }
            };
        }

        internal static Defender[] CripsDefenders()
        {
            return new Defender[]
            {
        new Defender
        {
            name = "James",
            stamina = 95,
            defence = 90,
            reaction = 85
        },
        new Defender
        {
            name = "Blue_D",
            stamina = 90,
            defence = 85,
            reaction = 80
        }
            };
        }
    }

    internal abstract class Gangster
    {
        internal string name;
        internal int stamina;
        private bool _isInitialized;
        int charact;

        internal void Adjunction(int const_charact, int charact)
        {
            if (!_isInitialized)
            {
                const_charact = charact;
                _isInitialized = true;
            }
        }

        internal void Checking_stamina(int negative_stamina)
        {
            stamina -= negative_stamina;
            if (stamina < 0)
            {
                stamina = 0;
            }
        }
        internal virtual void Minus_Characteristic(int characteristic, int const_charact)
        {
            characteristic = const_charact - (100 - stamina) / 18 * 12;
        }
        internal abstract void Additional_Chancge_Characteristic(int gang);

        internal virtual void Check_Characteristic(int characteristic, int minimum, int max)
        {
            if (characteristic < minimum)
                characteristic = minimum;
            else
            {
                if (characteristic > max)
                    characteristic = max;
            }
        }
        internal virtual void Characteristic(int gang)
        {
            Adjunction(1, 2);
            Minus_Characteristic(charact, charact);
            Additional_Chancge_Characteristic(gang);
            Check_Characteristic(charact, 1, 2);
        }
    }

    internal class Attacker : Gangster
    {
        internal int damage;
        internal int accuracy;
        internal int const_damage;
        internal override void Additional_Chancge_Characteristic(int gang)
        {
            const_damage = damage;
            if (gang == 0)
            {
                damage += 15;
                if (stamina > 19)
                    stamina -= 20;
            }
            else
            {
                damage -= 10;
            }
        }

        internal override void Characteristic(int gang)
        {
            Adjunction(const_damage, damage);
            Minus_Characteristic(damage, const_damage);
            Additional_Chancge_Characteristic(gang);
            Check_Characteristic(damage, 50, 90);
        }
    }

    internal class Defender : Gangster
    {
        internal int defence;
        internal int reaction;
        internal int const_defender;

        internal override void Additional_Chancge_Characteristic(int gang)
        {
            if (gang == 0)
            {
                if (stamina > 19)//
                    stamina -= 20;//
            }
            else
            {
                defence += 15;
            }
        }

        internal override void Characteristic(int gang)
        {
            Adjunction(const_defender, defence);
            Minus_Characteristic(defence, const_defender);
            Additional_Chancge_Characteristic(gang);
            Check_Characteristic(defence, 60, 100);
        }
    }
}
