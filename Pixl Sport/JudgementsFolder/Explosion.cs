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
      TM.HeldBall.h = 5f;
      TM.HeldBall.SendFlying(new Vector2((float)Math.Cos(degree), (float)Math.Sin(degree)), .5f, 2f);
      TM.Team.Members.Remove(TM);
  
  
  }

  public override void Execute(Team T){ throw new NotImplementedException();}

  public override void Execute(GameManager M){ throw new NotImplementedException();}



    }
}
