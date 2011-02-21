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
            if (manager.Ball.State == Ball.BallState.Held)
            {
                if (manager.Ball.Position.X < 50 && manager.Ball.Possessor.Team == manager.Team2) Enforce(manager.Team2, null);
                if (manager.Ball.Position.X > 650 && manager.Ball.Possessor.Team == manager.Team1) Enforce(manager.Team1, null);
            }
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
            if (manager.Ball.Position.X <= 0 && (manager.Ball.Position.Y < 176 || manager.Ball.Position.Y > 256) && manager.Ball.Possessor.Team == manager.Team2) Enforce(manager.Team2, manager.Ball.Possessor);
            if (manager.Ball.Position.X >= 700 && (manager.Ball.Position.Y < 176 || manager.Ball.Position.Y > 256) && manager.Ball.Possessor.Team == manager.Team1) Enforce(manager.Team1, manager.Ball.Possessor);
       
        }


    }

    class ThroughThePostsGoal : Rule    
    {
        public ThroughThePostsGoal(GameManager M, Judgement judge)
        {
            manager = M;
            ruleName = "GOOOOAAALLL!!!";
            assignedJudgement = judge;
        }

        public override void Check()
        {
            if (manager.Ball.Position.X <= 0 && (manager.Ball.Position.Y > 176 && manager.Ball.Position.Y < 256)&& manager.Ball.Possessor.Team == manager.Team2) Enforce(manager.Team2, manager.Ball.Possessor);
            if (manager.Ball.Position.X >= 700 && (manager.Ball.Position.Y > 176 && manager.Ball.Position.Y < 256)&& manager.Ball.Possessor.Team == manager.Team1) Enforce(manager.Team1, manager.Ball.Possessor);
       
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
