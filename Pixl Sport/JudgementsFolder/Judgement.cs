using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    abstract class Judgement
    {
        public enum JudgementType
        {
            Team,
            TeamMember,
            Global

        }

        public JudgementType Judged;
        protected String id;
        public String Id { get { return id; } }


    


        public abstract void Execute(TeamMember TM);
  
        public abstract void Execute(Team T);

        public abstract void Execute(GameManager M);

        /*
         * 

        public override void Execute(TeamMember TM){ throw new NotImplementedException();}

        public override void Execute(Team T){ throw new NotImplementedException();}

        public override void Execute(GameManager M){ throw new NotImplementedException();}


         */


    }
}
