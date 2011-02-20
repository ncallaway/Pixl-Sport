using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class RunInGoal:Rule
    {
        public RunInGoal(GameManager M, Judgement judge)
        {
            manager = M;
            ruleName = "GOOOOAAALLL!!!";
            assignedJudgement = judge;
        }

        public override void Check()
        {

        }
        

    }

    class OutTheBackGoal : Rule
    {
        public OutTheBackGoal(GameManager M, Judgement judge)
        {
            manager = M;
            ruleName = "GOOOOAAALLL!!!";
            assignedJudgement = judge;
        }

        public override void Check()
        {

        }


    }

    class PassInGoal : Rule
    {
        public PassInGoal(GameManager M, Judgement judge)
        {
            manager = M;
            ruleName = "GOOOOAAALLL!!!";
            assignedJudgement = judge;
        }

        public override void Check()
        {

        }


    }
}
