using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class Goal:Rule
    {
        public Goal(GameManager M, Judgement judge)
        {
            manager = M;
            ruleName = "GOOOOAAALLL!!!";
            assignedJudgement = judge;
        }

        public override void Check()
        {
          //  if ( /* ball is in the endzone*/ false) Enforce ;

        }
        

    }
}
