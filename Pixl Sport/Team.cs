using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pixl_Sport.AI;

namespace Pixl_Sport
{
    class Team
    {
        public enum Position
        {
            Decimator,
            Ballista,
            Bricky,
            Goalie
        }

        private String teamName;
        public String TeamName { get { return teamName; } set { teamName = value; } }

        private Color teamColor;
        public Color Color { get { return teamColor; } set { teamColor = value; } }

        private TeamAI ai;
        public TeamAI AI { get { return ai;  } }

        public List<TeamMember> Members = new List<TeamMember>();

        private GameManager manager;
        public GameManager Manager { get { return manager; } set { manager = value; } }

        public List<Player> Players = new List<Player>();

        public int Score;

        public Team(String name, GameManager manager)
        {
            teamName = name;
            Score = 0;
            this.manager = manager;
            ai = new TeamAI(this);
            
        }

        public void Initialize()
        {
            Members.Clear();

            for (int i = 0; i < 7; i++) {
                TeamMember m = new TeamMember(this);
               m.OnField = true;
                m.Number = i;
                if (i < 3) {
                    m.Profession = Position.Bricky;
                } else if (i == 3) {
                    m.Profession = Position.Decimator;
                } else {
                    m.Profession = Position.Ballista;
                }
                Members.Add(m);
            }
        }

        public void SetupKickoff(bool left)
        {
            ai.SetupKickoff();
        }



        public void Update(GameTime T)
        {
            ai.Update(T);

            foreach (TeamMember TM in Members)
            {
                TM.Update(T);
            }
        }


        }
    }

