using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pixl_Sport
{
    class TeamAI
    {
        public enum PlayMode
        {
            Offensive,
            Defensive,
        }

        private Team team;

        public TeamAI(Team team)
        {
            this.team = team;
        }


    }
}
