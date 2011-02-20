using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pixl_Sport
{
    class TeamAI
    {
        public enum Instruction
        {
            /* NEUTRAL ACTIONS! */
            AcquireBall, /* NO DATA */
            CheckPlayer, /* TeamMember */

            /* DEFENSIVE ACTIONS! */
            DefendArea, /* Position, Radius */
            DefendPlayer, /* TeamMember */

            /* OFFENSIVE ACTIONS! */
            GetOpen /* Position, Radius */
        }

        public enum PlayMode
        {
            Offensive,
            Defensive,
            Neutral,
        }

        private Team team;

        public TeamAI(Team team)
        {
            this.team = team;
        }

        public void Update(GameTime t)
        {
            foreach (TeamMember m in team.Members) {
                m.AI.InstructionBall = team.Manager.Ball;
                m.AI.Instruction = Instruction.AcquireBall;
            }
        }
    }
}
