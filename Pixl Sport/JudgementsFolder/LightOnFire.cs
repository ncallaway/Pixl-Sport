using System;
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
                    id = "Let the world BURN!";
                    break;
                case JudgementType.TeamMember:
                    id = "You are on FIRE!";
                    break;
                default:
                    throw new NotImplementedException("I can't start fires if you don't tell me who to burn");

                    break;
            }
        
        }


        public override void Execute(TeamMember TM) 
        {
            //TM.IsONFIRE!!!!   
        
        }


        public override void Execute(Team T)
        {
            foreach (TeamMember TM in T.Members)
            {
                //TM.IsONFIRE!!!!

            }

        }




    }
}
