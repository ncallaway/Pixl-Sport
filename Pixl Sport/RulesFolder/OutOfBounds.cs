using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class OutOfBounds:Rule
    {
        public OutOfBounds(GameManager Manager)
        {
            ruleName = "Out of Bounds";
            manager = Manager;

        }

        public OutOfBounds(GameManager Manager, Judgement punishment)
        {
            ruleName = "Out of Bounds";
            manager = Manager;
            assignedJudgement = punishment;
        }

        public override void Check()
        {
         



            //These will be replaced with the field dimensions provided for later.


            int fieldMinX = 0 ;
            int fieldMinY = 0;
            int fieldMaxX = 100;
            int fieldMaxY = 100;


            // End of PlaceHolder Variables


            foreach (Team T in manager.BothTeams)
            {
                foreach (TeamMember TM in T.Members) if (TM.Position.X < fieldMinX || TM.Position.X > fieldMaxX || TM.Position.Y < fieldMinY || TM.Position.Y > fieldMaxY)
                    {
                        Enforce(T, TM);

                    }
                
            }



        }

        public override void Enforce(Team T, TeamMember TM)
        {
            manager.StopClock();
            
             switch(assignedJudgement.Judged){
                 case Judgement.JudgementType.Team:
                     assignedJudgement.Execute(T);
                         break;
                 case Judgement.JudgementType.TeamMember:
                         assignedJudgement.Execute(TM);
                         break;

                     }

            ///RAWR~!!!!!!


        }



    }
}
