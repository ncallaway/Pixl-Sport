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
        private Team opposition;
        private PlayMode mode;

        public TeamAI(Team team)
        {
            this.team = team;
            
        }

        public void Update(GameTime t)
        {
            setOpposition();
            setPlayMode();

            switch (mode) {
                case PlayMode.Neutral:
                default:
                    neutralPlayModeUpdate(t);
                    break;
            }
        }

        private void setOpposition()
        {
            if (team == team.Manager.Team1) {
                opposition = team.Manager.Team2;
            } else {
                opposition = team.Manager.Team1;
            }
        }

        private void neutralPlayModeUpdate(GameTime t)
        {
            /* Closest two members converge on ball! */
            TeamMember closest = null;
            TeamMember closest2 = null;
            Ball ball = team.Manager.Ball;

            foreach (TeamMember m in team.Members) {
                if (closest == null ||
                    Vector2.DistanceSquared(m.Position, ball.Position) < Vector2.DistanceSquared(closest.Position, ball.Position)) {
                    closest2 = closest;
                    closest = m;
                } else {
                    if (closest2 == null ||
                        Vector2.DistanceSquared(m.Position, ball.Position) < Vector2.DistanceSquared(closest2.Position, ball.Position)) {
                        closest2 = m;
                    }
                }
            }

            closest.AI.InstructionBall = ball;
            closest.AI.Instruction = Instruction.AcquireBall;

            closest2.AI.InstructionBall = ball;
            closest2.AI.Instruction = Instruction.AcquireBall;
        }

        private void setPlayMode()
        {
            mode = PlayMode.Neutral;

            foreach (TeamMember m in team.Members) {
                if (m.HasBall) {
                    mode = PlayMode.Offensive;
                }
            }

            foreach (TeamMember m in opposition.Members) {
                if (m.HasBall) {
                    mode = PlayMode.Defensive;
                }
            }
        }
    }
}
