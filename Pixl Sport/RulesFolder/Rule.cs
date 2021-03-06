﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
     abstract class Rule
    {
         protected String ruleName;
         public String RuleName { get { return ruleName; } }
         public String CallName { get { return ruleName + ": " + assignedJudgement.Id; } }
         protected GameManager manager;

         protected Judgement assignedJudgement;


         public Rule() { }


         public abstract void Check();

         public virtual void SetJudgement(Judgement newJudgement)
         {
             assignedJudgement = newJudgement;
         }

         public virtual void Enforce(Team T, TeamMember TM)
         {
             if (T == null || TM == null)
             {
                 return;
             }

             switch (assignedJudgement.Judged)
             {
                 case Judgement.JudgementType.Team:
                     assignedJudgement.Execute(T);
                     break;
                 case Judgement.JudgementType.TeamMember:
                     assignedJudgement.Execute(TM);
                     break;
                 case Judgement.JudgementType.Global:
                     assignedJudgement.Execute(manager);
                     break;
                 
             }



         }
         
     

    }
}
