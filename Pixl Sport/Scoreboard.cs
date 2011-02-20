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
        private const int NAME_SCORE_PADDING = 30;
        private const int SCORE_SCORE_PADDING = 100;

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

            drawTopLine(batch, baseX, width, baseY);
        }

        private void drawTopLine(SpriteBatch batch, float baseX, float width, float baseY)
        {
            /* Calc top line width! */
            Vector2 measurement = sbMedium.MeasureString(HomeTeam);
            float topLineWidth = measurement.X;
            measurement = sbMedium.MeasureString(AwayTeam);
            topLineWidth += measurement.X;
            topLineWidth += 2 * NAME_SCORE_PADDING + SCORE_SCORE_PADDING;

            float xPos = baseX + (width - topLineWidth) / 2f;
            float yPos = baseY;

            xPos += NAME_SCORE_PADDING;

            batch.DrawString(sbMedium, HomeTeam, new Vector2(xPos, yPos), COLOR_SB_TEXT);
        }
    }
}
