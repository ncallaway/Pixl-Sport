using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Pixl_Sport
{
    class Field
    {
        private static readonly Vector2 PADDING_SIZE = new Vector2(20, 20);
        private static readonly Color COLOR_GRASS = new Color(0, 139, 0);
        private static readonly Color COLOR_LINES = Color.Black;
        private static readonly Color COLOR_ENDZONE = new Color(167, 167, 167);
        private static readonly Color COLOR_ENDZONE_TEXT = Color.Black;

        private const String ENDZONE_TEXT_1 = "BROADWAY";
        private const String ENDZONE_TEXT_2 = "BISONS";

        private Texture2D pixels;
        private SpriteFont pixelFont;
        private bool loaded;

        public Vector2 Position { get { return position; } set { position = value; } }
        public Vector2 FieldOrigin { get { return position + PADDING_SIZE; } }
        
        public Vector2 RenderSize { get { return sizeMultiplier * (size + (2 * PADDING_SIZE)); } }
        public Vector2 Size { get { return sizeMultiplier * size; } }
        public uint SizeMultiplier { get { return sizeMultiplier; } set { if (value > 0) { sizeMultiplier = value; } } }

        private Vector2 position;
        private Vector2 size;
        private uint sizeMultiplier;        

        public Field()
        {
            loaded = false;
            sizeMultiplier = 1;
            position = new Vector2(100, 250);
            size = new Vector2(700, 432);
        }

        public void Load(ContentManager content) {
            pixels = content.Load<Texture2D>("line");
            pixelFont = content.Load<SpriteFont>("Fonts/MediumPixlFont");
            loaded = true;
        }

        public void Draw(SpriteBatch batch)
        {
            System.Diagnostics.Debug.Assert(loaded, "Attempting to draw before loading!");

            drawBackground(batch);
            drawLines(batch);
            drawEndzone(batch);
        }

        private void drawBackground(SpriteBatch batch)
        {
            Rectangle destination = new Rectangle((int)Position.X, (int)Position.Y, (int)RenderSize.X, (int)RenderSize.Y);

            batch.Draw(pixels, destination, COLOR_GRASS);
        }

        private void drawLines(SpriteBatch batch)
        {
            Vector2 padding = sizeMultiplier * PADDING_SIZE;
            int thickThickness = (int)sizeMultiplier * 4;

            Vector2 origin = Position + padding;

            int fieldHeight = 432 * (int)sizeMultiplier;
            int endZoneWidth = 50 * (int)sizeMultiplier;
            int fieldWidth = 700 * (int)sizeMultiplier;

            /* Vertical lines */
            Vector2 thickLineWidthOffset = new Vector2(thickThickness, 0);
            Vector2 thickLineHeightOffset = new Vector2(0, thickThickness);
            Vector2 fieldHeightOffset = new Vector2(0, fieldHeight);
            Vector2 goalHeightOffset = new Vector2(0, 80);
            Vector2 endzoneWidthOffset = new Vector2(endZoneWidth, 0);
            Vector2 fieldWidthOffset = new Vector2(fieldWidth, 0);
            Vector2 openSpaceSixthWidthOffset = (fieldWidthOffset - 2 * endzoneWidthOffset) / 6f;

            Vector2 topLeft = origin;
            Vector2 bottomLeft = topLeft + fieldHeightOffset;
            Vector2 centreLeft = topLeft + fieldHeightOffset / 2f;
            Vector2 topRight = topLeft + fieldWidthOffset;
            Vector2 bottomRight = bottomLeft + fieldWidthOffset;
            Vector2 centreRight = topRight + fieldHeightOffset / 2f;

            Vector2 end = origin;
            end.Y += fieldHeight;

            /* BACK LINES */
            drawLine(batch, COLOR_LINES, topLeft, bottomLeft, 2, 2);
            drawLine(batch, COLOR_LINES, topRight, bottomRight, 2, 2);
            
            /* GOAL LINES */
            drawLine(batch, COLOR_LINES, topLeft + endzoneWidthOffset - thickLineWidthOffset, 
                                        bottomLeft + endzoneWidthOffset - thickLineWidthOffset, 2, 2);
            drawLine(batch, COLOR_LINES, topRight - endzoneWidthOffset + thickLineWidthOffset,
                                        bottomRight - endzoneWidthOffset + thickLineWidthOffset, 2, 2);

            /* CENTER LINE */
            drawLine(batch, COLOR_LINES, topLeft + fieldWidthOffset / 2f, bottomLeft + fieldWidthOffset / 2f, 2, 1);

            /* Horizontal Lines */
            drawLine(batch, COLOR_LINES, topLeft, topRight, 2, 2);
            drawLine(batch, COLOR_LINES, bottomLeft - thickLineHeightOffset, bottomRight - thickLineHeightOffset, 2, 2);
            drawLine(batch, COLOR_LINES, centreLeft - goalHeightOffset / 2f, centreRight - goalHeightOffset / 2f,
                2, 1, true);

            drawLine(batch, COLOR_LINES, centreLeft + goalHeightOffset / 2f, centreRight + goalHeightOffset / 2f,
                2, 1, true);


            /* Diagonal Lines!! */
            drawLine(batch, COLOR_LINES, topLeft + endzoneWidthOffset + openSpaceSixthWidthOffset,
               bottomLeft + endzoneWidthOffset + 2 * openSpaceSixthWidthOffset - thickLineHeightOffset, 2, 1);

            drawLine(batch, COLOR_LINES,
               bottomLeft + endzoneWidthOffset + 4 * openSpaceSixthWidthOffset - thickLineHeightOffset,
               topLeft + endzoneWidthOffset + 5 * openSpaceSixthWidthOffset, 2, 1);
        }

        private void drawEndzone(SpriteBatch batch)
        {
            Vector2 padding = sizeMultiplier * PADDING_SIZE;
            int thinThickness = (int)sizeMultiplier * 2;
            int thickThickness = (int)sizeMultiplier * 4;

            Vector2 origin = Position + padding;

            int fieldHeight = 432 * (int)sizeMultiplier;
            int endZoneWidth = 50 * (int)sizeMultiplier;
            int halfField = 300 * (int)sizeMultiplier;
            int fullField = 600 * (int)sizeMultiplier;

            Rectangle endZone = new Rectangle((int)origin.X + thickThickness, (int)origin.Y + thickThickness, endZoneWidth - (2 * thickThickness), fieldHeight - (2 * thickThickness));

            batch.Draw(pixels, endZone, COLOR_ENDZONE);

            endZone = new Rectangle((int)origin.X + endZoneWidth + fullField + (2 * thickThickness), (int)origin.Y + thickThickness, endZoneWidth - (2 * thickThickness), fieldHeight - (2 * thickThickness));

            batch.Draw(pixels, endZone, COLOR_ENDZONE);

            /* end zone text */
            Vector2 originalTextSize = pixelFont.MeasureString(ENDZONE_TEXT_1);
            Vector2 rotatedTextSize = new Vector2(originalTextSize.Y, originalTextSize.X);
            Vector2 endZoneSize = new Vector2(endZoneWidth + 2 * thickThickness, fieldHeight - 2 * thickThickness);
            Vector2 centered = (endZoneSize - rotatedTextSize) / 2f;

            Vector2 position = new Vector2(origin.X + thickThickness + centered.X - thinThickness, (int)origin.Y + fieldHeight - thickThickness - centered.Y);

            batch.DrawString(pixelFont, ENDZONE_TEXT_1, position, COLOR_ENDZONE_TEXT, -(float)Math.PI / 2f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            originalTextSize = pixelFont.MeasureString(ENDZONE_TEXT_2);
            rotatedTextSize = new Vector2(originalTextSize.Y, originalTextSize.X);
            centered = (endZoneSize - rotatedTextSize) / 2f;

            position = new Vector2(origin.X + 2 * endZoneWidth + fullField + centered.X - thickThickness + thinThickness, (int)origin.Y + thickThickness + centered.Y);

            batch.DrawString(pixelFont, ENDZONE_TEXT_2, position, COLOR_ENDZONE_TEXT, (float)Math.PI / 2f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }


        private void drawLine(SpriteBatch batch, Color color, Vector2 start, Vector2 end, int pixelThickness, int lineThickness)
        {
            drawLine(batch, color, start, end, pixelThickness, lineThickness, false);
        }

        private void drawLine(SpriteBatch batch, Color color, Vector2 start, Vector2 end, int pixelThickness, int lineThickness, bool dashed)
        {
            /* Guarantee start X is less than end X w/ swap */
            if (end.X < start.X) {
                Vector2 tmp = start;
                start = end;
                end = tmp;
            }

            int startCellX = (int)start.X;
            int startCellY = (int)start.Y;

            int endCellX = (int)end.X;
            int endCellY = (int)end.Y;

            if (endCellX == startCellX) {
                /* VERTICAL LINE!! */
                Rectangle dest = new Rectangle(startCellX, Math.Min(endCellY, startCellY), lineThickness * pixelThickness, Math.Abs(endCellY - startCellY));
                batch.Draw(pixels, dest, color);
                return;
            }


            int currentCellX = startCellX;
            int currentCellY = startCellY + pixelThickness * lineThickness;

            int dashCount = 0;

            while (currentCellX <= endCellX) {
                /* Find intersection with line and X value */
                float rightX = currentCellX + (lineThickness * pixelThickness);

                float t = (rightX - start.X) / (end.X - start.X);
                if (t < 0) { t = 0; }
                if (t > 1) { t = 1; }
                float intY = ((t * end.Y) + ((1 - t) * start.Y));

                bool goingUp = startCellY < endCellY;

                int steps = 0;

                steps = (int)((currentCellY - intY) / pixelThickness);
                
                if (steps >= 0 && steps < lineThickness) {
                    steps = lineThickness;
                }

                if (steps < 0 && steps > -lineThickness) {
                    steps = lineThickness;
                }

                int currentStopCellY = currentCellY - steps * pixelThickness;

                int yStart = (goingUp) ? currentCellY : currentStopCellY;
                int height = Math.Abs(currentCellY - currentStopCellY);

                Rectangle dest = new Rectangle(currentCellX, yStart, lineThickness * pixelThickness, height);
                batch.Draw(pixels, dest, color);

                currentCellX += pixelThickness * lineThickness;
                dashCount += pixelThickness;

                if (dashed && dashCount > 10) {
                    dashCount = 0;
                    currentCellX += 20 * pixelThickness;
                }

                if (currentStopCellY >= currentCellY) {
                    currentCellY = currentStopCellY - pixelThickness * lineThickness;
                } else {
                    currentCellY = currentStopCellY + pixelThickness * lineThickness;
                }
            }
        }
    }
}
