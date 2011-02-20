using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class OutOfBounds:Rule
    {
        public OutOfBounds(GameManager Manager)
        {
            ruleName = "Out of Bounds";
            manager = Manager;

        }

        public override bool Check()
        {
            bool violated = false;



            //These will be replaced witht he actual numbers


            int fieldMinX = 0 ;
            int fieldMinY = 0;
            int fieldMaxX = 100;
            int fieldMaxY = 100;


            // End of PlaceHolder Variables


            foreach (Team T in manager.BothTeams)
            {
                foreach (TeamMember TM in T.Members) if (TM.Position.X < fieldMinX || TM.Position.X > fieldMaxX || TM.Position.Y < fieldMinY || TM.Position.Y > fieldMaxY) violated = true;
                
            }



            return violated;
        }

        public override void Enforce()
        {

            ///RAWR~!!!!!!


        }



    }
}
