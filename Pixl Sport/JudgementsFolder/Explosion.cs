using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace Pixl_Sport
{
    class Explosion:Judgement
    {

        public Explosion()
        {
            Judged = JudgementType.TeamMember;
            id = "KA-BOOMM!";
        }


  public override void Execute(TeamMember TM)
  {   Random rand = new Random();
      int degree = (int)rand.NextDouble()*360%360;
      TM.HeldBall.HotBall = true;
      TM.Team.Manager.audioM.PlayEffect("Bomb");
      TM.TimeWithBall = 0;
      TM.HeldBall.Possessor = null;
      TM.BallMovement( .5f, 1f);
      TM.Stun(5000);
  
  
  }

  public override void Execute(Team T){ throw new NotImplementedException();}

  public override void Execute(GameManager M){ throw new NotImplementedException();}



    }
}
