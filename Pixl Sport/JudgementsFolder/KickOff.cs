using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class KickOff: Judgement
    {
        public KickOff()
        {
            id = "Kick Off";
            Judged = JudgementType.Global;


        }



        public override void Execute(TeamMember TM){ throw new NotImplementedException();}

        public override void Execute(Team T) { throw new NotImplementedException(); }

        public override void Execute(GameManager M) { M.SetupKickoff(); }





    }
}
