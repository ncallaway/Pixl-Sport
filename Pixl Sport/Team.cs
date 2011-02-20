using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixl_Sport
{
    class Team
    {
        private String teamName;
        public String TeamName { get { return teamName; } }

        private Color teamColor;
        public Color Color { get { return teamColor; } set { teamColor = value; } }

        public List<TeamMember> Members = new List<TeamMember>();

        public int Score;

        public Team(String name)
        {
            teamName = name;
            Score = 0;
        }

        public void Initialize()
        {
            Members.Clear();

            for (int i = 0; i < 7; i++) {
                TeamMember m = new TeamMember(this);
                m.OnField = true;
                Members.Add(m);
            }
        }

        public void SetupKickoff(bool left)
        {
            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(350, 432);

            Vector2 rightOffset = new Vector2(350, 0);

            if (!left) {
                min += rightOffset;
                max += rightOffset;
            }

            Random rand = new Random();

            int offFieldOffset = 0;

            foreach (TeamMember m in Members) {
                if (m.OnField) {
                    float posX = (float)rand.NextDouble();
                    posX *= (max.X - min.X);
                    posX += min.X;

                    float posY = (float)rand.NextDouble();
                    posY *= (max.Y - min.Y);
                    posY += min.Y;

                    m.Position = new Vector2(posX, posY);
                } else {
                    m.Position = new Vector2(min.X + offFieldOffset * 5, min.Y - 10);
                }
            }
        }



        public void Update(GameTime T)
        {
            foreach (TeamMember TM in Members)
            {
                TM.Update(T);

            }
        }


        }
    }

