using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport.JudgementsFolder
{
    class Rebound :  Judgement
    {
        public Rebound(GameManager M)
        {
            manager = M;
            Judged = JudgementType.Global;
        }

        public override void Execute(GameManager M)
        {
            manager.Ball.SendFlying(-manager.Ball.Direction, manager.Ball.Velocity, manager.Ball.VerticalF);
        }



        public override void Execute(TeamMember TM) { throw new NotImplementedException(); }

        public override void Execute(Team T) { throw new NotImplementedException(); }
    }
}
