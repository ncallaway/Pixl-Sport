using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pixl_Sport.AI
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
            DefendClosestPlayer, /* NO DATA */
            DefendToGoal, /* Position */

            /* OFFENSIVE ACTIONS! */
            GetOpen, /* Position, Radius */
        }

        public enum PlayMode
        {
            Offensive,
            Defensive,
            Neutral,
        }

        private static readonly List<Vector2> HOME_OFF_POSITIONS = new List<Vector2>() { 
            new Vector2(275, 108),
            new Vector2(275, 324),
            new Vector2(200, 216),
            new Vector2(400, 175),
            new Vector2(400, 257)
        };

        private static readonly List<Vector2> AWAY_OFF_POSITIONS = new List<Vector2>() { 
            new Vector2(275, 108),
            new Vector2(275, 324),
            new Vector2(200, 216),
            new Vector2(400, 175),
            new Vector2(400, 257)
        };

        private static readonly List<Vector2> HOME_DEF_POSITIONS = new List<Vector2>() { 
            new Vector2(275, 108),
            new Vector2(275, 324),
            new Vector2(200, 216),
            new Vector2(400, 175),
            new Vector2(400, 257)
        };

        private static readonly List<Vector2> AWAY_DEF_POSITIONS = new List<Vector2>() { 
            new Vector2(425, 108),
            new Vector2(425, 324),
            new Vector2(500, 216),
            new Vector2(300, 175),
            new Vector2(300, 257)
        };

        private Dictionary<TeamMember, Post> assignments;

        private static readonly Vector2 HOME_KICKOFF_START = new Vector2(150, 50);
        private static readonly Vector2 HOME_KICKOFF_END = new Vector2(225, 382);

        private static readonly Vector2 AWAY_KICKOFF_START = new Vector2(550, 50);
        private static readonly Vector2 AWAY_KICKOFF_END = new Vector2(475, 382);

        public bool Home;

        public Team Team;
        public Team Opposition;
        private PlayMode mode;
        private PlayMode prevMode;

        private List<Post> defensivePosts;

        public TeamAI(Team team)
        {
            this.Team = team;
            prevMode = PlayMode.Neutral;
        }

        public void SetupKickoff()
        {
            setOpposition();
            Vector2 start = Home ? HOME_KICKOFF_START : AWAY_KICKOFF_START;
            Vector2 end = Home ? HOME_KICKOFF_END : AWAY_KICKOFF_END;
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

            prevMode = mode;
        }

        private void setOpposition()
        {
            if (Team == Team.Manager.Team1) {
                Home = true;
                Opposition = Team.Manager.Team2;
            } else {
                Home = false;
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

        private void initDefense()
        {
            assignments = new Dictionary<TeamMember, Post>();
            defensivePosts = new List<Post>();

            foreach (Vector2 position in Home ? HOME_DEF_POSITIONS : AWAY_DEF_POSITIONS) {
                Post p = new Post();
                p.Assignee = null;
                p.Position = position;
                defensivePosts.Add(p);
            }
        }

        private void defensivePlayModeUpdate(GameTime t)
        {
            if (prevMode != PlayMode.Defensive) {
                initDefense();
            }

            TeamMember carrier = null;
            foreach (TeamMember m in Opposition.Members) {
                if (m.HasBall) {
                    carrier = m;
                }
            }

            List<TeamMember> members = Team.Members;
            HashSet<TeamMember> unassigned = new HashSet<TeamMember>(members);
            List<Post> defPosts = SortByProximity(defensivePosts, Team.Manager.Ball.Position);
            

            List<TeamMember> getters = GetClosestPlayersToPoint(Team.Members, 1, Team.Manager.Ball.Position);
            foreach (TeamMember m in getters) {
                if (assignments.ContainsKey(m)) {
                    /* Abandon post! */
                    assignments[m].Assignee = null;
                    assignments.Remove(m);
                }
                unassigned.Remove(m);
                m.AI.InstructionTeamMember = carrier;
                m.AI.Instruction = Instruction.DefendPlayer;
            }

            Dictionary<TeamMember, Post> mapping = MapPlayersToPosts(members, defPosts);

            float ballThreat = getBrickyBallThreat();
            foreach (KeyValuePair<TeamMember, Post> kvp in mapping) {
                if (unassigned.Contains(kvp.Key)) {
                    unassigned.Remove(kvp.Key);

                    if (kvp.Key.AI.Instruction != Instruction.DefendArea) {
                        Vector2 value = kvp.Value.Position;
                        value.X = kvp.Value.Position.X + (ballThreat) * (Home ? 50 + kvp.Value.Position.X : 650 - kvp.Value.Position.X);
                        kvp.Key.AI.InstructionPosition = value;
                        kvp.Key.AI.InstructionRadius = 100;
                        kvp.Key.AI.Instruction = Instruction.DefendArea;
                        unassigned.Remove(kvp.Key);
                    }
                }
            }

            foreach (TeamMember m in members) {
                if (unassigned.Contains(m)) {
                    if (assignments.ContainsKey(m)) {
                        /* Abandon post! */
                        assignments[m].Assignee = null;
                        assignments.Remove(m);
                    }

                    m.AI.Instruction = Instruction.DefendClosestPlayer;
                }
            }
        }

        private float getBrickyBallThreat()
        {
            float bbt = 0f;
            if (Home) {
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

        public static List<Post> SortByProximity(List<Post> positions, Vector2 point)
        {
            List<Post> points = new List<Post>(positions);

            points.Sort(delegate(Post a, Post b) {
                return Comparer<float>.Default.Compare(Vector2.DistanceSquared(a.Position, point), Vector2.DistanceSquared(b.Position, point));
            });

            return points;
        }

        public static Dictionary<TeamMember, Post> MapPlayersToPosts(List<TeamMember> players, List<Post> points)
        {
            Dictionary<TeamMember, Post> mapping = new Dictionary<TeamMember, Post>();

            List<TeamMember> unassigned = new List<TeamMember>(players);
            foreach (Post p in points) {
                if (p.Assignee != null) {
                    TeamMember c = p.Assignee;
                    unassigned.Remove(c);
                    mapping.Add(c, p);
                    continue;
                }

                List<TeamMember> closest = GetClosestPlayersToPoint(unassigned, 1, p.Position);
                if (closest.Count > 0) {
                    TeamMember c = closest[0];
                    unassigned.Remove(c);
                    mapping.Add(c, p);
                    p.Assignee = c;
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
