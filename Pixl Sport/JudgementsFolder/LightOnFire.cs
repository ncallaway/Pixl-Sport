﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class LightOnFire: Judgement
    {



        public LightOnFire(JudgementType which)
        {   
            Judged = which;
            switch (which)
            {

                case JudgementType.Team:
                    id = "You can tell by the way we're sparking that we're burning alive!";
                    break;
                case JudgementType.Global:
                    id = "Let the world BURN!";
                    break;
                case JudgementType.TeamMember:
                    id = "You are on FIRE!";
                    break;
                default:
                    throw new NotImplementedException("I can't start fires if you don't tell me who to burn");

                  
            }
        
        }


        public override void Execute(GameManager M) 
        {
            //TM.IsONFIRE!!!!   
        
        }
        
        public override void Execute(TeamMember TM) 
        {
            TM.CantCatch = true;
            TM.IsOnFire = true;
            if(TM.HasBall) TM.Drop();
        
        }


        public override void Execute(Team T)
        {
            foreach (TeamMember TM in T.Members)
            {
                TM.IsOnFire = true;

            }

        }




    }
}
