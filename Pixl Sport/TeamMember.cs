using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;

namespace Pixl_Sport
{
    class TeamMember
    {
        private String name;
        public String Name { get { return name; } }

        private String profession;
        public String Profession { get { return Profession; } }

        public bool OnField;

        private Vector2 position;
        public Vector2 Position { get { return position; } }


        private Team team;
        public Team Team { get { return team; } }



        public TeamMember(Team team)
        {
            this.team = team;

        }


    }
}
