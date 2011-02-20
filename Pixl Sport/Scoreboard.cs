using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pixl_Sport
{
    class Scoreboard
    {
        public String HomeTeam;
        public String AwayTeam;

        public int HomeScore;
        public int AwayScore;

        public int Qtr;
        public String Mode;
        public TimeSpan timeRemaining;
    }
}
