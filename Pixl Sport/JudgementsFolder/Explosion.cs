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
            Judged = JudgementType.Global;
            id = "Explosion";
        }


  public override void Execute(TeamMember TM)
  {   Random rand = new Random();
      int degree = (int)rand.NextDouble()*360%360;
      TM.HeldBall.HotBall = true;
      TM.Team.Manager.AudioM.PlayEffect("Bomb");
      TM.TimeWithBall = 0;
      TM.HeldBall.Possessor = null;
      TM.BallMovement( .5f, 1f);
      TM.Stun(5000);
  
  
  }

  public override void Execute(Team T){ throw new NotImplementedException();}
  public override void Execute(GameManager M)
  {
      Random rand = new Random();
      int degree = (int)(rand.NextDouble() * 10000) % 360;
      int degree2 = (int)(rand.NextDouble() * 10000) % 45 + 45;

      M.Ball.HotBall = true;
      M.AudioM.PlayEffect("Bomb");
      if (M.Ball.Possessor != null)
      {
          M.Ball.Possessor.Stun(5000);
          M.Ball.Possessor = null;
      }
      M.Ball.SendFlying(new Vector2((float)Math.Cos(degree), (float)Math.Sin(degree)), (float)Math.Sin(degree2), (float)Math.Cos(degree2));
     


  }


    }
}
