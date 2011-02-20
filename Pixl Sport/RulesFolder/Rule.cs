using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
     abstract class Rule
    {
         protected String ruleName;
         public String RuleName { get { return ruleName; } }
         protected GameManager manager;

         protected Judgement assignedJudgement;


         public Rule() { }


         public abstract void Check();



         public abstract void Enforce(Team T, TeamMember TM);
         
     

    }
}
