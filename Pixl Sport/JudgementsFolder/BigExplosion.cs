using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace Pixl_Sport
{
    class BigExplosion : Judgement
    {

        public BigExplosion()
        {
            Judged = JudgementType.Global;
            id = "KA-BOOMM!";
        }


        public override void Execute(GameManager M)
        {
            Random rand = new Random();
            int degree = (int)(rand.NextDouble()*10000) % 360;
            int degree2 = (int)(rand.NextDouble() * 10000) % 45 +45;
            
            M.Ball.HotBall=true;
            M.AudioM.PlayEffect("Bomb");
            if (M.Ball.Possessor != null)
            {
                M.Ball.Possessor.Stun(5000);
                M.Ball.Possessor = null;
            }
            M.Ball.SendFlying(new Vector2((float)Math.Cos(degree), (float)Math.Sin(degree)), (float)Math.Sin(degree2), (float)Math.Cos(degree2));
            foreach (TeamMember TM2 in M.Team1.Members) if (TM2.Bounds.Intersects(new BoundingSphere(new Vector3(M.Ball.Position, 0f), 30f))){ TM2.Stun(4000); TM2.IsOnFire = true;}
            foreach (TeamMember TM2 in M.Team2.Members) if (TM2.Bounds.Intersects(new BoundingSphere(new Vector3(M.Ball.Position, 0f), 30f))) { TM2.Stun(4000); TM2.IsOnFire = true; }
           


        }

        public override void Execute(Team T) { throw new NotImplementedException(); }

        public override void Execute(TeamMember TM) { throw new NotImplementedException(); }



    }
}
