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
        private const int BOX_PADDING = 35;

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
            drawSecondLine(batch, baseX, width, baseY);
        }

        private void drawTopLine(SpriteBatch batch, float baseX, float width, float baseY)
        {
            bool scoreThree = false;
            if (HomeScore > 100 || HomeScore < -100 || AwayScore > 100 || AwayScore < -100) {
                scoreThree = true;
            }

            String homeScoreString = scoreThree ? string.Format("{0:d3}", HomeScore) : string.Format("{0:d2}", HomeScore);
            String awayScoreString = scoreThree ? string.Format("{0:d3}", AwayScore) : string.Format("{0:d2}", AwayScore);

            /* Calc top line width! */
            float xPos = 0; /* Will do centering below */
            float yPos = baseY + 20 ;

            Vector2 measurement = sbMedium.MeasureString(HomeTeam);
            Vector2 homeTeamPos = new Vector2(xPos, yPos);
            xPos = measurement.X;
            xPos += NAME_SCORE_PADDING;

            measurement = sbXtraLarge.MeasureString(homeScoreString);
            Vector2 homeScorePos = new Vector2(xPos, baseY);

            xPos += measurement.X;
            xPos += SCORE_SCORE_PADDING;

            measurement = sbXtraLarge.MeasureString(awayScoreString);
            Vector2 awayScorePos = new Vector2(xPos, baseY);

            xPos += measurement.X;
            xPos += NAME_SCORE_PADDING;

            measurement = sbMedium.MeasureString(AwayTeam);
            Vector2 awayTeamPos = new Vector2(xPos, yPos);

            xPos += measurement.X;

            float topLineWidth = xPos;

            Vector2 centerOffset = new Vector2(baseX + (width - topLineWidth) / 2f, 0f);

            batch.DrawString(sbMedium, HomeTeam, homeTeamPos + centerOffset, COLOR_SB_TEXT);
            batch.DrawString(sbXtraLarge, homeScoreString, homeScorePos + centerOffset, COLOR_SB_TEXT);
            batch.DrawString(sbXtraLarge, awayScoreString, awayScorePos + centerOffset, COLOR_SB_TEXT);
            batch.DrawString(sbMedium, AwayTeam, awayTeamPos + centerOffset, COLOR_SB_TEXT);
        }

        private void drawSecondLine(SpriteBatch batch, float baseX, float width, float baseY)
        {
            String timeString = string.Format("{0:d2}", TimeRemaining.Minutes);
            timeString += ":";
            timeString += string.Format("{0:d2}", TimeRemaining.Seconds);

            String qtrString = string.Format("{0:d1}", Qtr);

            float qtrWidth = sbSmall.MeasureString("QTR").X;
            float timeTextWidth = sbSmall.MeasureString("TIME").X;
            float timeWidth = sbLarge.MeasureString(timeString).X;
            float modeWidth = sbSmall.MeasureString("MODE").X;

            float line2Width = qtrWidth + BOX_PADDING + timeWidth + BOX_PADDING + modeWidth;

            float posX = (width - line2Width) / 2f + baseX;
            batch.DrawString(sbSmall, "QTR", new Vector2(posX, baseY + 100), COLOR_SB_TEXT);

            float qtrStrW = sbLarge.MeasureString(qtrString).X;
            posX = (qtrWidth - qtrStrW) / 2f + posX;
            batch.DrawString(sbLarge, qtrString, new Vector2(posX, baseY + 120), COLOR_SB_TEXT);

            posX = (width - timeTextWidth) / 2f + baseX;
            batch.DrawString(sbSmall, "TIME", new Vector2(posX, baseY + 100), COLOR_SB_TEXT);

            posX = (width - timeWidth) / 2f + baseX;
            batch.DrawString(sbLarge, timeString, new Vector2(posX, baseY + 120), COLOR_SB_TEXT);

            posX = (width + line2Width) / 2f - modeWidth + baseX;
            batch.DrawString(sbSmall, "MODE", new Vector2(posX, baseY + 100), COLOR_SB_TEXT);

            float modeStrW = sbLarge.MeasureString(Mode).X;
            posX = (modeWidth - modeStrW) / 2f + posX;
            batch.DrawString(sbLarge, Mode, new Vector2(posX, baseY + 120), COLOR_SB_TEXT);

        }
    }
}
