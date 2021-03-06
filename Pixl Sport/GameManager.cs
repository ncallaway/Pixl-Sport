﻿using System;
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

using Pixl_Sport.RulesFolder;
using Pixl_Sport.JudgementsFolder;
using UserMenus;

namespace Pixl_Sport
{



    class GameManager
    {
        Dictionary<String, Rule> rulesList = new Dictionary<String,Rule>();


        // These variables encompass the gameclock.

        //private static int QUARTERTIME = 120000;
        private static int QUARTERTIME = 3000;
      
        private int time;
        public int Time { get { return time; } set { time = Math.Max(time - value, 0); } }
        public int MinTime { get { return time / 60000; } }
        public int SecTime { get { return (time / 1000) % 60; } }
        private bool running;
        public void StopClock() { running = false;}





        public List<Player> Players = new List<Player>();

        public Team Team1;
        public Team Team2;
        public Ball Ball;
        public Field PlaySpace;
        public Scoreboard Scoreboard;
        public List<Team> BothTeams = new List<Team>();

        public IAsyncResult KeyboardResult;



        private Texture2D pixels;

        private PixlGame pixlGame;

        public AudioManager AudioM;

        public MenuManager MenuM;

     


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

            Team1 = new Team("Broadway Bisons", this);
            Team2 = new Team("New Jersey Devils", this);
            Ball = new Ball();

            Team1.Initialize();
            Team2.Initialize();

            /*

            KeyboardResult = Guide.BeginShowKeyboardInput(PlayerIndex.One, "TeamName entry", "Enter the team's name here", "Boradway Bisons", null, null);
            Team1.TeamName = Guide.EndShowKeyboardInput(KeyboardResult);
            
            */


            Team1.Color = Color.Cyan;
            Team2.Color = new Color(167, 167, 167);

            


            AudioM = new AudioManager();
            AudioM.Initialize();

           

            time = QUARTERTIME;

            AddRule(new OutOfBounds(this, new LightOnFire(Judgement.JudgementType.TeamMember)));
            AddRule(new OutOfBounds(this, new Rebound(this)));
            AddRule(new RunInGoal(this, new ScoreExchange(this)));
            AddRule(new ThroughThePostsGoal(this, new ScoreChange(this, 7)));
            AddRule(new OutTheBackGoal(this, new Rebound(this)));
            AddRule(new PassInGoal(this, new ScoreChange(this, 5)));

            AddPlayer(Team1, InputController.InputMode.Player1);
            AddPlayer(Team2, InputController.InputMode.Player2);


            MenuM = new MenuManager(this);
            MenuM.Initialize();
            
        }

        

        public void SetupKickoff()
        {
            Team1.SetupKickoff(true);
            Team2.SetupKickoff(false);
            Ball.Position = new Vector2(352f, 432f / 2f);
            Ball.Clear();
            running = true;
            AudioM.StopSounds();
        }


        public void StartGame()
        {
            Scoreboard.Qtr = 0;
            Scoreboard.AwayScore = 0;
            Scoreboard.HomeScore = 0;
            quarterChange();

        }


        public void AddPlayer(Team team, InputController.InputMode spot)
        {
            Player temp = new Player(team, spot);
            Players.Add(temp);
            team.Players.Add(temp);


        }

         public void Load(ContentManager content)
        {
            pixels = content.Load<Texture2D>("line");

            AudioM.Load(content);
            PlaySpace.Load(content);
            Scoreboard.Load(content);
            MenuM.Load(content);
        }


         public void AddRule(Rule newRule)
         {  
             if(rulesList.ContainsKey(newRule.CallName)) rulesList.Remove(newRule.CallName);
             else rulesList.Add(newRule.CallName, newRule);
         }


        //This itterates throught the current Rules and checks them. If they are broken it enforces them.
        public void RulesCheck()
        {

            foreach (Rule R in rulesList.Values)
            {
                R.Check();

            }
        }

        public void Update(GameTime T)
        {
            if (MenuM.OpenMenus) MenuM.Update(T);
            else
            {
                if (running) Time = T.ElapsedGameTime.Milliseconds;

                TimeSpan remaining = new TimeSpan(0, MinTime, SecTime);

                Scoreboard.HomeTeam = Team1.TeamName;
                Scoreboard.AwayTeam = Team2.TeamName;
                Scoreboard.HomeScore = Team1.Score;
                Scoreboard.AwayScore = Team2.Score;
                Scoreboard.TimeRemaining = remaining;


                foreach (Player p in Players)
                {
                    p.Update(T);

                }
                Team1.Update(T);
                Team2.Update(T);

                
                            
                foreach (TeamMember TM in Team1.Members)
                {
                    if (TM.Tackling) foreach (TeamMember V in Team2.Members) if (TM.Bounds.Intersects(V.Bounds)) TM.Hit(V);
                    if (Ball.Bounds.Intersects(TM.Bounds) && Ball.State != Ball.BallState.Held && !TM.Equals(Ball.Possessor) && !Ball.HotBall) TM.GrabBall(Ball);

                }
                foreach (TeamMember TM in Team2.Members)
                {
                    if (TM.Tackling) foreach (TeamMember V in Team1.Members) if (TM.Bounds.Intersects(V.Bounds)) TM.Hit(V);
                    if (Ball.Bounds.Intersects(TM.Bounds) && Ball.State != Ball.BallState.Held && !TM.Equals(Ball.Possessor) && !Ball.HotBall) TM.GrabBall(Ball);

                }

                Ball.Update(T);
                RulesCheck();

                if (time <= 0) quarterChange();
            }
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

            if(MenuM.OpenMenus) MenuM.Draw(b);

        }

        private void quarterChange()
        {
            if (Scoreboard.Qtr == 4) MenuM.MainMenu();
            else
            {
                if (Scoreboard.Qtr % 2 == 0 || Team1.Score == Team2.Score) { MenuM.OpenRuleMenu(false); MenuM.OpenRuleMenu(true); }
                else MenuM.OpenRuleMenu(Team1.Score < Team2.Score);
                Scoreboard.Qtr++;
                time = QUARTERTIME;
                SetupKickoff();
            }
        }


        



    }
}
