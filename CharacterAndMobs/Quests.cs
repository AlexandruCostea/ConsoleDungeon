using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.CharacterAndMobs
{
    class Quests
    {
        public static void QuestReset(ref bool fishingQ, ref bool tournamentQ, ref bool gamblingQ, ref bool arenaQ, ref int fishingQRank, ref int tournamentQRank, ref int gamblingQRank, ref int arenaQRank, ref int fishingQProg, ref int tournamentQProg, ref int gamblingQProg, ref int arenaQProg){
            Random randomNumber = new Random();
            int quest1, quest2;
            quest1 = randomNumber.Next(1, 4);
            quest2 = quest1;
            while (quest2 == quest1)
                quest2 = randomNumber.Next(1, 4);
            switch(quest1)
            {
                case 1: { fishingQ = true; tournamentQ = gamblingQ = arenaQ = false;
                        fishingQRank++; if (fishingQRank > 5) fishingQRank = randomNumber.Next(3, 5);
                        fishingQProg = 0;
                    } break;
                case 2: {
                        tournamentQ = true; fishingQ = gamblingQ = arenaQ = false;
                        tournamentQRank++; if (tournamentQRank > 5) tournamentQRank = randomNumber.Next(3, 5);
                        tournamentQProg = 0;
                    } break;
                case 3: {
                        gamblingQ = true; tournamentQ = fishingQ = arenaQ = false;
                        gamblingQRank++; if (gamblingQRank > 5) gamblingQRank = randomNumber.Next(3, 5);
                        gamblingQProg = 0;
                    } break;
                case 4: {
                        arenaQ = true; tournamentQ = gamblingQ = fishingQ = false;
                        arenaQRank++; if (arenaQRank > 5) arenaQRank = randomNumber.Next(3, 5);
                        arenaQProg = 0;
                    } break;
            }
            switch(quest2)
            {
                case 1: { fishingQ = true; fishingQRank++; if (fishingQRank > 5) fishingQRank = randomNumber.Next(3, 5); fishingQProg = 0; } break;
                case 2: { tournamentQ = true; tournamentQRank++; if (tournamentQRank > 5) tournamentQRank = randomNumber.Next(3, 5); tournamentQProg = 0; } break;
                case 3: { gamblingQ = true; gamblingQRank++; if (gamblingQRank > 5) gamblingQRank = randomNumber.Next(3, 5); gamblingQProg = 0; } break;
                case 4: { arenaQ = true; arenaQRank++; if (arenaQRank > 5) arenaQRank = randomNumber.Next(3, 5); arenaQProg = 0; } break;
            }
        }
    }
}
