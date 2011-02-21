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
            DefendToGoal, /* Position */

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
            new Vector2(425, 108),
            new Vector2(500, 216),
            new Vector2(425, 324)
        };

        private static readonly Vector2 HOME_KICKOFF_START = new Vector2(150, 50);
        private static readonly Vector2 HOME_KICKOFF_END = new Vector2(225, 382);

        private static readonly Vector2 AWAY_KICKOFF_START = new Vector2(550, 50);
        private static readonly Vector2 AWAY_KICKOFF_END = new Vector2(475, 382);

        private bool home;

        public Team Team;
        public Team Opposition;
        private PlayMode mode;

        public TeamAI(Team team)
        {
            this.Team = team;
        }

        public void SetupKickoff()
        {
            setOpposition();
            Vector2 start = home ? HOME_KICKOFF_START : AWAY_KICKOFF_START;
            Vector2 end = home ? HOME_KICKOFF_END : AWAY_KICKOFF_END;
            for (int i = 0; i < Team.Members.Count; i++) {
                TeamMember m = Team.Members[i];
                m.Position = start + (float)i / (float)Team.Members.Count * (end - start);
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
            if (Team == Team.Manager.Team1) {
                home = true;
                Opposition = Team.Manager.Team2;
            } else {
                home = false;
                Opposition = Team.Manager.Team1;
            }
        }

        private void neutralPlayModeUpdate(GameTime t)
        {
            /* Closest two members converge on ball! */
            List<TeamMember> getters = GetClosestPlayersToPoint(Team.Members, 2, Team.Manager.Ball.Position);

            foreach (TeamMember m in getters) {
                m.AI.InstructionBall = Team.Manager.Ball;
                m.AI.Instruction = Instruction.AcquireBall;
            }

            foreach (TeamMember m in Team.Members) {
                if (getters.Contains(m) == false) {
                    m.AI.InstructionBall = Team.Manager.Ball;
                    m.AI.Instruction = Instruction.GetOpen;
                }
            }
        }

        private void offensivePlayModeUpdate(GameTime t)
        {
            foreach (TeamMember m in Team.Members) {
                m.AI.Instruction = Instruction.GetOpen;
            }
        }

        private void defensivePlayModeUpdate(GameTime t)
        {
            List<TeamMember> members = Team.Members;
            List<TeamMember> bricky = FilterByPosition(members, Team.Position.Bricky);
            List<Vector2> brickyPositions = SortByProximity(home ? HOME_DEF_BRICKY_POSITIONS : AWAY_DEF_BRICKY_POSITIONS, Team.Manager.Ball.Position);
            Dictionary<TeamMember, Vector2> mapping = MapPlayersToPoints(bricky, brickyPositions);
            float ballThreat = getBrickyBallThreat();
            foreach (KeyValuePair<TeamMember, Vector2> kvp in mapping) {
                Vector2 value = kvp.Value;
                value.X = kvp.Value.X + (ballThreat) * (home ? 50 + kvp.Value.X : 650 - kvp.Value.X);
                kvp.Key.AI.InstructionPosition = value;
                kvp.Key.AI.Instruction = Instruction.DefendArea;
            }

            foreach (TeamMember m in members) {
                if (bricky.Contains(m) == false) {
                    m.AI.Instruction = Instruction.GetOpen;
                }
            }
        }

        private float getBrickyBallThreat()
        {
            float bbt = 0f;
            if (home) {
                /* BBT */
                bbt = ((700 - Team.Manager.Ball.Position.X) - 225f) / 475f;
            } else {
                bbt = ((Team.Manager.Ball.Position.X) - 225f) / 475f;
            }

            bbt = Math.Min(bbt, 1f);
            bbt = Math.Max(bbt, 0f);
            return bbt;
        }

        public static List<TeamMember> GetClosestPlayersToPoint(List<TeamMember> list, int number, Vector2 point)
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

        public static List<Vector2> SortByProximity(List<Vector2> positions, Vector2 point)
        {
            List<Vector2> points = new List<Vector2>(positions);

            points.Sort(delegate(Vector2 a, Vector2 b) {
                return Comparer<float>.Default.Compare(Vector2.DistanceSquared(a, point), Vector2.DistanceSquared(b, point));
            });

            return points;
        }

        public static Dictionary<TeamMember, Vector2> MapPlayersToPoints(List<TeamMember> players, List<Vector2> points)
        {
            Dictionary<TeamMember, Vector2> mapping = new Dictionary<TeamMember, Vector2>();

            List<TeamMember> unassigned = new List<TeamMember>(players);
            foreach (Vector2 p in points) {
                List<TeamMember> closest = GetClosestPlayersToPoint(unassigned, 1, p);
                if (closest.Count > 0) {
                    TeamMember c = closest[0];
                    unassigned.Remove(c);
                    mapping.Add(c, p);
                }
            }

            return mapping;
        }

        public static List<TeamMember> FilterByPosition(List<TeamMember> members, Team.Position position) {
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

            foreach (TeamMember m in Team.Members) {
                if (m.HasBall) {
                    mode = PlayMode.Offensive;
                }
            }

            foreach (TeamMember m in Opposition.Members) {
                if (m.HasBall) {
                    mode = PlayMode.Defensive;
                }
            }
        }
    }
}
