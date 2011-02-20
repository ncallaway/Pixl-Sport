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
            TeamMember

        }

        public JudgementType Judged;
        protected String id;
        public String Id { get { return id; } }


    


        public void Execute(TeamMember TM);
  
        public void Execute(Team T);




    }
}
