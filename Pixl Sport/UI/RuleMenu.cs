using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserMenus
{
    class RuleMenu:Menu
    {
        public Rule ruleTarget;
        public Judgement judgementTarget;
        List<Player> owners = new List<Player>();
        List<MenuItem> judgementItems = new List<MenuItem>();
        Color teamColor;
        public bool leftMenu;
        

        public RuleMenu(MenuManager M, Team team)
        {   
            parent = M;

            owners = team.Players;
            font = parent.Font;
            image = parent.Image;
            current_index = 0;
            x_position = 62;
            y_position = 250;
            y_spacing = 20;
            x_spacing = 0;

            teamColor = team.Color;

            ruleTarget = new OutOfBounds(parent.Manager);
            judgementTarget = new ScoreChange(parent.Manager, -3);

            title = team.TeamName + "'s Rule Selection";

            addMenuItem(new RuleImplementerItem(parent.Manager, this));
            addMenuItem(new RuleItem(this, new HotPotato(parent.Manager)));
            addMenuItem(new RuleItem(this, new OutOfBounds(parent.Manager)));
            addMenuItem(new RuleItem(this, new NoFighting(parent.Manager)));
            addMenuItem(new RuleItem(this, new NoKicking(parent.Manager)));
            addMenuItem(new RuleItem(this, new NoPassing(parent.Manager)));
            addMenuItem(new RuleItem(this, new TimeBall(parent.Manager)));
            addMenuItem(new RuleItem(this, new TimeBall(parent.Manager, 15)));
            addMenuItem(new RuleItem(this, new TimeBall(parent.Manager, 10)));
            addMenuItem(new RuleItem(this, new RunInGoal(parent.Manager)));

            judgementItems.Add(new RuleImplementerItem(parent.Manager, this));
            judgementItems.Add(new JudgementItem(this, new BigExplosion()));
            judgementItems.Add(new JudgementItem(this, new Explosion()));
            judgementItems.Add(new JudgementItem(this, new KickOff()));
            judgementItems.Add(new JudgementItem(this, new LightOnFire(Judgement.JudgementType.Team)));
            judgementItems.Add(new JudgementItem(this, new LightOnFire(Judgement.JudgementType.TeamMember)));
            judgementItems.Add(new JudgementItem(this, new LightOnFire(Judgement.JudgementType.Global)));
            judgementItems.Add(new JudgementItem(this, new ScoreChange(parent.Manager, 5)));



        }


        public override void Update(GameTime gametime)
        {
            foreach (Player player in owners)
            {
                if (current == null) current = menuitems.ElementAt(0);

                player.Input.Update();
                if (player.Input.IsMoveUpNewlyPressed() || player.Input.IsDPadUpNewlyPressed()) current_index--;
                if (player.Input.IsMoveDownNewlyPressed() || player.Input.IsDPadDownNewlyPressed()) current_index++;

                if (player.Input.IsMoveRightNewlyPressed() || player.Input.IsDPadRightNewlyPressed() || player.Input.IsDPadLeftNewlyPressed() || player.Input.IsMoveLeftNewlyPressed()) leftMenu = !leftMenu;

                if (player.Input.IsBButtonNewlyPressed()) parent.CloseMenu();
                
                

                if (leftMenu && current_index >= menuitems.Count) current_index = 0;                    
                if (!leftMenu && current_index >= judgementItems.Count) current_index = 0;
                
                if (leftMenu && current_index < 0) current_index = menuitems.Count - 1;
                if (!leftMenu && current_index < 0) current_index = judgementItems.Count - 1;
                
                if ( leftMenu) current = menuitems.ElementAt(current_index);
                else current = judgementItems.ElementAt(current_index);

                
               

                if (player.Input.IsAButtonNewlyPressed())
                {
                    player.Input.Update();
                    current.execute();
                }

                if (player.Input.IsXButtonNewlyPressed() || player.Input.IsPauseMenuNewlyPressed())
                {
                    player.Input.Update();
                    current = menuitems.ElementAt(0);
                    current.execute();
                }

            }
      }



        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(image, new Rectangle(287, 110, 460, y_spacing * (3) + 10), Color.Gray);
            batch.Draw(image, new Rectangle(292, 115, 450, y_spacing * (3)), Color.Black);
            batch.DrawString(font, title, new Vector2(292, 115), teamColor);
            

            batch.Draw(image, new Rectangle(x_position - 5, y_position - 5, 410, y_spacing * (menuitems.Count) + 10), Color.Gray);
            batch.Draw(image, new Rectangle(x_position, y_position, 400, y_spacing * (menuitems.Count)), Color.Black);
            batch.DrawString(font, "Rules List:", new Vector2(x_position, y_position), Color.Gold);

            batch.DrawString(font, "(X)", new Vector2(372, 140), Color.Blue);

            foreach (MenuItem x in menuitems)
            {
                int i = menuitems.IndexOf(x);
                if (i == 0) x.Draw(batch, font, 412, 140);
                else if (x==current) x.Draw(batch, font, x_position + (i + 1) * x_spacing + 15, y_position + y_spacing * (i));
                else x.Draw(batch, font, x_position + (i + 1) * x_spacing, y_position + y_spacing * (i));
            }


            batch.Draw(image, new Rectangle(x_position - 5 + 500, y_position - 5, 410, y_spacing * (judgementItems.Count) + 10), Color.Gray);
            batch.Draw(image, new Rectangle(x_position + 500, y_position, 400, y_spacing * (judgementItems.Count)), Color.Black);
            batch.DrawString(font, "Judgement List:", new Vector2(x_position + 500, y_position), Color.Gold);


            foreach (MenuItem x in judgementItems)
            {
                int i = judgementItems.IndexOf(x);
                if (i == 0) x.Draw(batch, font, 412, 140);
                else if (x == current) x.Draw(batch, font, x_position + 500 + (i + 1) * x_spacing + 15, y_position + y_spacing * (i));
                else x.Draw(batch, font, x_position + 500 + (i + 1) * x_spacing, y_position + y_spacing * (i));
            }





        }



        }








    }






