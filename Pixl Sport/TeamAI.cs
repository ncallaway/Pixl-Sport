using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pixl_Sport
{
    class TeamAI
    {
        public enum PlayMode
        {
            Offensive,
            Defensive,
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
                m.AI.Goal = PlayerAI.Instruction.AcquireBall;
            }
        }
    }
}
