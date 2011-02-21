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
          

            if(manager.Ball.Possessor != null)  if ((manager.Ball.Position.Y < 0 || manager.Ball.Position.Y > 432)) Enforce(manager.Ball.Possessor.Team, manager.Ball.Possessor);



        }

  



    }
}
