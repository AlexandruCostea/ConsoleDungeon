using System;
using System.Collections.Generic;
using System.Text;
using ConsoleDungeon.CharacterAndMobs;

namespace ConsoleDungeon.MenuAndText
{
    class JobName
    {
        public static void SetName(Character.Jobs job, int jobRank, ref string jobName){
            switch(job)
            {
                case Character.Jobs.None: { jobName = "None"; }break;
                case Character.Jobs.Fishing: { 
                        switch(jobRank)
                        {
                            case 1: { jobName = "Fishing noob"; }break;
                            case 2: { jobName = "Fish Catcher"; }break;
                            case 3: { jobName = "Fishing Pro"; }break;
                            case 4: { jobName = "Water King"; }break;
                            case 5: { jobName = "Wet Boii"; }break;
                        }
                    }break;
                case Character.Jobs.Blacksmith: {
                        switch(jobRank)
                        {
                            case 1: { jobName = "BS Novice"; }break;
                            case 2: { jobName = "Steel Adept"; } break;
                            case 3: { jobName = "Sword Maker"; } break;
                            case 4: { jobName = "Bayblade Boi"; } break;
                            case 5: { jobName = "Iron Man"; } break;
                        }
                    }break;
                case Character.Jobs.Guard: {
                        switch(jobRank)
                        {
                            case 1: { jobName = "Babysitter"; } break;
                            case 2: { jobName = "Garden Guard"; } break;
                            case 3: { jobName = "Palace guard"; } break;
                            case 4: { jobName = "Bodyguard"; } break;
                            case 5: { jobName = "A Fortress"; } break;
                        }
                    }break;
                case Character.Jobs.Banker: {
                        switch(jobRank)
                        {
                            case 1: { jobName = "Service Agent"; } break;
                            case 2: { jobName = "Bank Manager"; } break;
                            case 3: { jobName = "Bank Director"; } break;
                            case 4: { jobName = "Bank CEO"; } break;
                            case 5: { jobName = "CEO of Earth"; } break;
                        }
                    }break;
            }
        }
    }
}
