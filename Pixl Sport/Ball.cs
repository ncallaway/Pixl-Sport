using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pixl_Sport
{
    class Ball
    {
        private static readonly Color COLOR_BALL = new Color(96, 51, 17);

        private Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }

        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 2 * (int)scaleSize, 2 * (int)scaleSize);

            batch.Draw(pixels, destination, COLOR_BALL);
        }
    }
}
