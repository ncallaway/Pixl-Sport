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

         public Rule() { }


         public abstract bool Check();



         public abstract void Enforce();
     

    }
}
