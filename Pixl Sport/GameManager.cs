using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Pixl_Sport
{



    class GameManager
    {
        List<Rule> rulesList = new List<Rule>();


        // These variables encompass the gameclock.

        private static int QUARTERTIME = 600000;
        private int time;
        public int Time { get { return time; } set { time = Math.Max(time - value, 0); } }
        public int MinTime { get { return time / 6000; } }
        public int SecTime { get { return (time / 1000) % 60; } }
        private bool running;

        public Team Team1;
        public Team Team2;
        public Ball Ball;
        public Field PlaySpace;
        public Scoreboard Scoreboard;
        public List<Team> BothTeams = new List<Team>();

        private Texture2D pixels;

        private PixlGame pixlGame;


        public GameManager(PixlGame game) {
            pixlGame = game;
            Initialize();
        }

        public void Initialize()
        {
            PlaySpace = new Field();
            Scoreboard = new Scoreboard();

            Vector2 windowSize = new Vector2(pixlGame.GraphicsDevice.Viewport.Width,
                                                 pixlGame.GraphicsDevice.Viewport.Height);
            Vector2 psp;
            psp.X = (windowSize.X - PlaySpace.RenderSize.X) / 2f;
            psp.Y = windowSize.Y - PlaySpace.RenderSize.Y - 50f;

            PlaySpace.Position = psp;

            Team1 = new Team("Broadway Bisons");
            Team2 = new Team("New Jersey Devils");
            Ball = new Ball();

            Team1.Initialize();
            Team2.Initialize();

            Team1.Color = Color.Cyan;
            Team2.Color = new Color(167, 167, 167);

            SetupKickoff();
        }

        public void SetupKickoff()
        {
            Team1.SetupKickoff(true);
            Team2.SetupKickoff(false);
            Ball.Position = new Vector2(352f, 432f / 2f);
        }

        public void Load(ContentManager content)
        {
            pixels = content.Load<Texture2D>("line");
            
            PlaySpace.Load(content);
            Scoreboard.Load(content);
        }


        //This itterates throught the current Rules and checks them. If they are broken it enforces them.
        public void RulesCheck()
        {

            foreach (Rule R in rulesList)
            {
                if (R.Check()) R.Enforce();

            }
        }

        public void Update(GameTime T)
        {
            if (running) Time = T.ElapsedGameTime.Milliseconds;

            Scoreboard.HomeTeam = Team1.TeamName;
            Scoreboard.AwayTeam = Team2.TeamName;
            Scoreboard.HomeScore = Team1.Score;
            Scoreboard.AwayScore = Team2.Score;
        }

        public void Draw(GameTime t, SpriteBatch b)
        {
            Scoreboard.Draw(b, new Rectangle(0, 20, pixlGame.GraphicsDevice.Viewport.Width, 170));
            PlaySpace.Draw(b);

            foreach (TeamMember m in Team1.Members) {
                m.Draw(b, pixels, PlaySpace.FieldOrigin, PlaySpace.SizeMultiplier);
            }

            foreach (TeamMember m in Team2.Members) {
                m.Draw(b, pixels, PlaySpace.FieldOrigin, PlaySpace.SizeMultiplier);
            }

            Ball.Draw(b, pixels, PlaySpace.FieldOrigin, PlaySpace.SizeMultiplier);
        }



    }
}
