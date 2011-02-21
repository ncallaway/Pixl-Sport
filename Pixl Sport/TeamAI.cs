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

        private static readonly List<Vector2> HOME_DEF_BRICKY_POSITIONS = new List<Vector2>() { 
            new Vector2(30, 30),
            new Vector2(30, 300),
            new Vector2(50, 300)
        };

        private static readonly List<Vector2> AWAY_DEF_BRICKY_POSITIONS = new List<Vector2>() { 
            new Vector2(480, 30),
            new Vector2(600, 216),
            new Vector2(480, 400)
        };

        private static readonly Vector2 HOME_KICKOFF_START = new Vector2(150, 50);
        private static readonly Vector2 HOME_KICKOFF_END = new Vector2(225, 382);

        private static readonly Vector2 AWAY_KICKOFF_START = new Vector2(550, 50);
        private static readonly Vector2 AWAY_KICKOFF_END = new Vector2(475, 382);

        private bool home;

        private Team team;
        private Team opposition;
        private PlayMode mode;

        public TeamAI(Team team)
        {
            this.team = team;
        }

        public void SetupKickoff()
        {
            setOpposition();
            Vector2 start = home ? HOME_KICKOFF_START : AWAY_KICKOFF_START;
            Vector2 end = home ? HOME_KICKOFF_END : AWAY_KICKOFF_END;
            for (int i = 0; i < team.Members.Count; i++) {
                TeamMember m = team.Members[i];
                m.Position = start + (float)i / (float)team.Members.Count * (end - start);
            }
        }

        public void Update(GameTime t)
        {
            setOpposition();
            setPlayMode();

            switch (mode) {
                case PlayMode.Defensive:
                    defensivePlayModeUpdate(t);
                    break;
                case PlayMode.Offensive:
                    offensivePlayModeUpdate(t);
                    break;
                case PlayMode.Neutral:
                default:
                    neutralPlayModeUpdate(t);
                    break;
            }
        }

        private void setOpposition()
        {
            if (team == team.Manager.Team1) {
                home = true;
                opposition = team.Manager.Team2;
            } else {
                home = false;
                opposition = team.Manager.Team1;
            }
        }

        private void neutralPlayModeUpdate(GameTime t)
        {
            /* Closest two members converge on ball! */
            List<TeamMember> getters = getClosestPlayersToPoint(team.Members, 2, team.Manager.Ball.Position);

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

        private void offensivePlayModeUpdate(GameTime t)
        {
            foreach (TeamMember m in team.Members) {
                m.AI.Instruction = Instruction.GetOpen;
            }
        }

        private void defensivePlayModeUpdate(GameTime t)
        {
            List<TeamMember> members = team.Members;
            List<TeamMember> bricky = filterByPosition(members, Team.Position.Bricky);
            List<Vector2> brickyPositions = sortByProximity(home ? HOME_DEF_BRICKY_POSITIONS : AWAY_DEF_BRICKY_POSITIONS, team.Manager.Ball.Position);
            Dictionary<TeamMember, Vector2> mapping = mapPlayersToPoints(bricky, brickyPositions);
            foreach (KeyValuePair<TeamMember, Vector2> kvp in mapping) {
                kvp.Key.AI.InstructionPosition = kvp.Value;
                kvp.Key.AI.Instruction = Instruction.DefendArea;
            }

            foreach (TeamMember m in members) {
                if (bricky.Contains(m) == false) {
                    m.AI.Instruction = Instruction.GetOpen;
                }
            }
        }

        private List<TeamMember> getClosestPlayersToPoint(List<TeamMember> list, int number, Vector2 point)
        {
            List<TeamMember> members = new List<TeamMember>(list);

            members.Sort(delegate (TeamMember a, TeamMember b) {
                return Comparer<float>.Default.Compare(Vector2.DistanceSquared(a.Position, point), Vector2.DistanceSquared(b.Position, point));
            });

            while (members.Count > number) {
                members.RemoveAt(number);
            }

            return members;
        }

        private List<Vector2> sortByProximity(List<Vector2> positions, Vector2 point)
        {
            List<Vector2> points = new List<Vector2>(positions);

            points.Sort(delegate(Vector2 a, Vector2 b) {
                return Comparer<float>.Default.Compare(Vector2.DistanceSquared(a, point), Vector2.DistanceSquared(b, point));
            });

            return points;
        }

        private Dictionary<TeamMember, Vector2> mapPlayersToPoints(List<TeamMember> players, List<Vector2> points)
        {
            Dictionary<TeamMember, Vector2> mapping = new Dictionary<TeamMember, Vector2>();

            List<TeamMember> unassigned = new List<TeamMember>(players);
            foreach (Vector2 p in points) {
                List<TeamMember> closest = getClosestPlayersToPoint(unassigned, 1, p);
                if (closest.Count > 0) {
                    TeamMember c = closest[0];
                    unassigned.Remove(c);
                    mapping.Add(c, p);
                }
            }

            return mapping;
        }

        private List<TeamMember> filterByPosition(List<TeamMember> members, Team.Position position) {
            List<TeamMember> tm = new List<TeamMember>();
            foreach (TeamMember m in members) {
                if (m.Profession == position) {
                    tm.Add(m);
                }
            }
            return tm;
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
