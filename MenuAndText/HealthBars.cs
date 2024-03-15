using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon.MenuAndText
{
    class HealthBars
    {
        public static void SetHealthBar(ref string healthBar, int hp, int fullHp){
            healthBar = "[";
            int hpPercentage = 10 * hp / fullHp;
            for (int i = 1; i <= hpPercentage; i++)
                healthBar += "/";
            for (int i = 1; i <= 10 - hpPercentage; i++)
                healthBar += " ";
            healthBar += "]";
        }
    }
}
