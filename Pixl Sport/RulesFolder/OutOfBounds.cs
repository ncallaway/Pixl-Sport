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

            if(manager.Ball.Possessor != null)  if ((manager.Ball.Position.Y < 0 || manager.Ball.Position.Y > 432)) Enforce(manager.Ball.Possessor.Team, null);



        }

        public override void Enforce(Team T, TeamMember TM)
        {
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
