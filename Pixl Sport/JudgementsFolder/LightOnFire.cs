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
                    id = "Ignite Team";
                    break;
                case JudgementType.Global:
                    id = "Ignite All";
                    break;
                case JudgementType.TeamMember:
                    id = "Ignite One";
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
            TM.Ignite();
            if(TM.HasBall) TM.Drop();
        
        }


        public override void Execute(Team T)
        {
            foreach (TeamMember TM in T.Members)
            {
                TM.Ignite();
                TM.CantCatch = true;

                if (TM.HasBall) TM.Drop();
            }

        }




    }
}
