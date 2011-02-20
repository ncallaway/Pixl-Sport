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



    }
}
