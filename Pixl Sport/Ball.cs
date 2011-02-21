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
        private float height;
        private float arcApex;
        private bool apexReached;

        private float h;


        public BoundingBox Bounds
        {
            get
            {
                return new BoundingBox(new Vector3(position - Vector2.One, h), new Vector3(position + 2 * Vector2.One,h + 2f));
            }
        }

        private Vector2 direction;
        public Vector2 Direction { get { return direction; } }


        private float velocity;

        public enum BallState
        {
            Held,
            Flying,
            Bouncing,
            Dead


        }

        public BallState State;
        public TeamMember Possessor;

        public Ball()
        {
            apexReached = false;
            height = 0f;
            h = 1f;
            velocity = .5f;
            arcApex = 20f;
            Bounce();
        }



        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 2 * (int)scaleSize* (int) (h /5 +1), 2 * (int)scaleSize *(int) (h /5+1));

            batch.Draw(pixels, destination, COLOR_BALL);
        }


        public void Update ( GameTime t)
        {
            h -= .1f;
            h = Math.Max(h, 0);

            
            switch (State){
                case BallState.Flying:

                    position += direction*velocity;
                    if (!apexReached) h+=.3f;
                    if (h > arcApex) apexReached = true;
                    if (h < .2f) Bounce();



                    break;
                case BallState.Held:
                    break;
                case BallState.Bouncing:
                        
                    break;

            
            }
       }

        public void Clear()
        {
            direction = Vector2.Zero;
            apexReached = true;
            height = 20f;

        }


        public void SendFlying(Vector2 direction,float strength, float apex)
        {
            apexReached = false;
            h = 2.1f;
            arcApex = apex;
            velocity = strength;
            this.direction = direction;
            this.direction.Normalize();

            

            State = BallState.Flying;
        }

        public void Bounce()
        {
            Random rand = new Random();
            int deviation = 0;
          
              deviation = (int)(rand.NextDouble() * 120) % 30 - 15;
              
              direction = new Vector2((float)Math.Cos(deviation), (float)Math.Sin(deviation));
              direction.Normalize();
              SendFlying(direction, velocity / 2, arcApex / 2);
        }

    }
}
