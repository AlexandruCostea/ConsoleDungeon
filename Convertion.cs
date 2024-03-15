using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleDungeon
{
    class Convertion
    {
        public static int ToInt(string option, int usage)
        {
            int optionNumber;
            try
            {
                optionNumber = Convert.ToInt32(option);
            }
            catch
            {
                if (usage == 1)
                    optionNumber = 1;
                else
                    optionNumber = 0;
            }
            return optionNumber;
        }
    }
}
