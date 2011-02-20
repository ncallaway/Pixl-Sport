using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport.RulesFolder
{
    class Goal:Rule
    {
        public Goal()
        {
            ruleName = "GOOOOAAALLL!!!";

        }

        public override bool Check()
        {
            if ( /* ball is in the endzone*/ false) return true;

            return false;
        }

        public override void Enforce()
        {
            Team Scoringteam = new Team() ;

            Scoringteam.Score += 5; 


        }

    }
}
