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
            Judged = JudgementType.TeamMember;
            id = "KA-BOOMM!";
        }


        public override void Execute(TeamMember TM)
        {
            Random rand = new Random();
            int degree = (int)rand.NextDouble() * 360 % 360;
            if(TM.HasBall) TM.HeldBall.HotBall = true;
            TM.TimeWithBall = 0;
            TM.HeldBall.Possessor = null;
            TM.BallMovement(.5f, 1f);
            foreach (TeamMember TM2 in TM.Team.Manager.Team1.Members) if (TM2.Bounds.Intersects(new BoundingSphere(new Vector3(TM.Position, 0f), 30f))){ TM2.Stun(4000); TM2.IsOnFire = true;}
            foreach (TeamMember TM2 in TM.Team.Manager.Team2.Members) if (TM2.Bounds.Intersects(new BoundingSphere(new Vector3(TM.Position, 0f), 30f))) { TM2.Stun(4000); TM2.IsOnFire = true; }
           
            TM.Stun(5000);


        }

        public override void Execute(Team T) { throw new NotImplementedException(); }

        public override void Execute(GameManager M) { throw new NotImplementedException(); }



    }
}
