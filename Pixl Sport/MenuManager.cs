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


        private Menu pauseMenu;
        public List<Player> Players;

        public GameManager Manager;

        public MenuManager(GameManager M)
        {
            Manager = M;
        }


        public void Initialize(){
            Players = Manager.Players;
            pauseMenu = new Menu(this);
            pauseMenu.addMenuItem(new MenuItem("Test Item 1"));
            pauseMenu.addMenuItem(new MenuItem("Test Item 2"));
        
        }


        public void Load(ContentManager CM)
        {
            pauseMenu.Load(CM);   
        }



        public void CloseMenu()
        {
            currentMenus.Pop();
            
        }

        public void Pause()
        {

            currentMenus.Push(pauseMenu);

        }

        public void Pause(Team team)
        {

            OpenMenu(pauseMenu);

        }

        public void OpenMenu(Menu menu)
        {
            Manager.AudioM.PauseSounds();
            currentMenus.Push(menu);
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
