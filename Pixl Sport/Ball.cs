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
        
        public BoundingBox Bounds
        {
            get
            {
                return new BoundingBox(new Vector3(position - Vector2.One, height), new Vector3(position + 2 * Vector2.One,height = 2f));
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
            height = 0f;
            State = BallState.Dead;
        }



        public void Draw(SpriteBatch batch, Texture2D pixels, Vector2 fieldOrigin, uint scaleSize)
        {
            Vector2 drawLocation = (fieldOrigin + position) * scaleSize;
            Rectangle destination = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, 2 * (int)scaleSize, 2 * (int)scaleSize);

            batch.Draw(pixels, destination, COLOR_BALL);
        }


        public void Update ( GameTime t) 
        {



            switch (State){
                case BallState.Flying:
                    position += direction*velocity;
                   /* if(height <= 1f) 
                    {
                  // state = BallState.Bouncing;
                    Random rand = new Random();
                    direction = new Vector2((float)rand.NextDouble(), (float)(rand.NextDouble()));
                    direction.Normalize();
                    velocity /= 2;
                   }*/
                    break;
                case BallState.Held:
                    break;
                case BallState.Bouncing:
                    break;

            
            }
       }


        public void SendFlying(Vector2 direction,float time)
        {
            this.direction = direction;
            this.direction.Normalize();

            velocity = direction.Length() / time;

            State = BallState.Flying;
        }



    }
}
