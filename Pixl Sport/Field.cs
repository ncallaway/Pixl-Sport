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
        private Texture2D pixels;
        private bool loaded;

        public Vector2 Position { get { return position; } set { position = value; } }
        public uint SizeMultiplier { get { return sizeMultiplier; } set { if (value > 0) { sizeMultiplier = value; } } }
        public Vector2 Size { get { return size; } }

        private Vector2 position;
        private Vector2 size;
        private uint sizeMultiplier;

        public Field()
        {
            loaded = false;
            sizeMultiplier = 1;
        }

        public void Load(ContentManager content) {
            pixels = content.Load<Texture2D>("line");
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

        }

        private void drawLines(SpriteBatch batch)
        {
        }

        private void drawEndzone(SpriteBatch batch)
        {
        }
    }
}
