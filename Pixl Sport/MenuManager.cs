using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserMenus;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Pixl_Sport;

namespace UserMenus
{
    class MenuManager
    {
        private Stack<Menu> currentMenus = new Stack<Menu>();
        public bool OpenMenus { get { return currentMenus.Count != 0; } }

        private SpriteFont font;
        public SpriteFont Font { get { return font; } }

        private Texture2D image;
        public Texture2D Image { get {return image;}}


        private TeamMenu pauseMenu1;
        private TeamMenu pauseMenu2;
        public List<Player> Players;

        public GameManager Manager;

        private RuleMenu ruleMenu1;

        private RuleMenu ruleMenu2;

        private MainMenu mainMenu;



        public MenuManager(GameManager M)
        {
            Manager = M;
            mainMenu = new MainMenu(this);
        }


        public void Initialize(){
            Players = Manager.Players;
            pauseMenu1 = new TeamMenu(Manager.Team1, this);
            pauseMenu2 = new TeamMenu(Manager.Team2, this);
            ruleMenu1 = new RuleMenu(this, Manager.Team1);
            ruleMenu2 = new RuleMenu(this, Manager.Team2);
            OpenMenu(mainMenu);
        
        }


        public void Load(ContentManager CM)
        {
            pauseMenu1.Load(CM);
            pauseMenu2.Load(CM);
            ruleMenu1.Load(CM);
            ruleMenu2.Load(CM);
            mainMenu.Load(CM);
        }



        public void CloseMenu()
        {
            currentMenus.Pop();
            
        }

        public void Pause()
        {

            currentMenus.Push(pauseMenu1);

        }

        public void Pause(Team team)
        {

            if (team == Manager.Team1) OpenMenu(pauseMenu1);
            else OpenMenu(pauseMenu2);

        }

        public void MainMenu()
        {
            OpenMenu(mainMenu);

        }

        public void OpenMenu(Menu menu)
        {
            Manager.AudioM.PauseSounds();
            currentMenus.Push(menu);
        }

     
        public void OpenRuleMenu(bool left)
        {
            
            Manager.AudioM.PauseSounds();
            if (left) currentMenus.Push(ruleMenu1);
            else currentMenus.Push(ruleMenu2);
        }


        public void Update(GameTime t)
        {
            currentMenus.Peek().Update(t);
        }

        public void Draw(SpriteBatch batch)
        {
            currentMenus.Peek().Draw(batch);

        }




    }
}
