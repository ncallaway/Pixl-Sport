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
        private static readonly Color COLOR_SB_TEXT = new Color (207, 195, 11);

        public String HomeTeam;
        public String AwayTeam;

        public int HomeScore;
        public int AwayScore;

        public int Qtr;
        public String Mode;
        public TimeSpan TimeRemaining;

        private SpriteFont sbSmall;
        private SpriteFont sbMedium;
        private SpriteFont sbLarge;
        private SpriteFont sbXtraLarge;

        public Scoreboard()
        {
            HomeTeam = "BROADWAY BISONS";
            AwayTeam = "NEW JERSEY DEVILS";

            HomeScore = 9;
            AwayScore = 24;

            Qtr = 1;
            Mode = "-";
            TimeRemaining = new TimeSpan();
        }

        public void Load(ContentManager content)
        {
            sbSmall = content.Load<SpriteFont>("Fonts/SmallSBFont");
            sbMedium = content.Load<SpriteFont>("Fonts/MediumSBFont");
            sbLarge = content.Load<SpriteFont>("Fonts/LargeSBFont");
            sbXtraLarge = content.Load<SpriteFont>("Fonts/XtraLargeSBFont");
        }

        public void Draw(SpriteBatch batch, Rectangle region)
        {
            float baseX = region.Left;
            float width = region.Width;

            /* Center vertically within region */
            float baseY = (region.Height - 150f) / 2f + region.Top;
        }
    }
}
