using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class Team
    {
        private String teamName;
        public String TeamName { get { return teamName; } }

        public List<TeamMember> Members = new List<TeamMember>();

        public int Score;



    }
}
