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
            pixelFont = content.Load<SpriteFont>("MediumPixlFont");
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
            int thinThickness = (int)sizeMultiplier * 2;
            int thickThickness = (int)sizeMultiplier * 4;

            Vector2 origin = Position + padding;

            int fieldHeight = 432 * (int)sizeMultiplier;
            int endZoneWidth = 50 * (int)sizeMultiplier;
            int halfField = 300 * (int)sizeMultiplier;
            int fullField = 600 * (int)sizeMultiplier;

            /* Vertical lines */

            Rectangle backLine = new Rectangle((int)origin.X, (int)origin.Y, thickThickness, fieldHeight);
            Rectangle goalLine = new Rectangle((int)origin.X + endZoneWidth - thickThickness, (int)origin.Y, thickThickness, fieldHeight);
            Rectangle midLine = new Rectangle((int)origin.X + endZoneWidth + halfField, (int)origin.Y, thinThickness, fieldHeight);

            batch.Draw(pixels, backLine, COLOR_LINES);
            batch.Draw(pixels, goalLine, COLOR_LINES);
            batch.Draw(pixels, midLine, COLOR_LINES);

            goalLine = new Rectangle((int)origin.X + endZoneWidth + fullField + thickThickness, (int)origin.Y, thickThickness, fieldHeight);
            backLine = new Rectangle((int)origin.X + endZoneWidth + fullField + endZoneWidth, (int)origin.Y, thickThickness, fieldHeight);

            batch.Draw(pixels, goalLine, COLOR_LINES);
            batch.Draw(pixels, backLine, COLOR_LINES);

            /* Horizontal Lines */

            Rectangle topLine = new Rectangle((int)origin.X, (int)origin.Y, fullField + (2 * endZoneWidth), thickThickness);
            Rectangle bottomLine = new Rectangle((int)origin.X, (int)origin.Y + fieldHeight - thickThickness, fullField + (2 * endZoneWidth), thickThickness);

            batch.Draw(pixels, topLine, COLOR_LINES);
            batch.Draw(pixels, bottomLine, COLOR_LINES);
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
    }
}
