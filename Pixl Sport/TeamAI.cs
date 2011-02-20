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
            List<TeamMember> getters = getClosestTeamMembersToPoint(team, 2, team.Manager.Ball.Position);

            foreach (TeamMember m in getters) {
                m.AI.InstructionBall = team.Manager.Ball;
                m.AI.Instruction = Instruction.AcquireBall;
            }

            foreach (TeamMember m in team.Members) {
                if (getters.Contains(m) == false) {
                    m.AI.InstructionBall = team.Manager.Ball;
                    m.AI.Instruction = Instruction.GetOpen;
                }
            }
        }

        private List<TeamMember> getClosestTeamMembersToPoint(Team team, int number, Vector2 point)
        {
            List<TeamMember> members = new List<TeamMember>(team.Members);

            members.Sort(delegate (TeamMember a, TeamMember b) {
                return Comparer<float>.Default.Compare(Vector2.DistanceSquared(a.Position, point), Vector2.DistanceSquared(b.Position, point));
            });

            while (members.Count > number) {
                members.RemoveAt(number);
            }

            return members;
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
