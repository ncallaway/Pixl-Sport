using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Pixl_Sport;

using Microsoft.Xna.Framework;

namespace UserMenus
{
    class RuleMenu:Menu
    {
        public Rule ruleTarget;
        public Judgement judgementTarget;
        List<Player> owners = new List<Player>();

        public RuleMenu(MenuManager M, Team team)
        {   
            parent = M;

            owners = team.Players;
            font = parent.Font;
            image = parent.Image;
            current_index = 0;
            x_position = 200;
            y_position = 200;
            y_spacing = 20;
            x_spacing = 0;

            

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

            addMenuItem(new JudgementItem(this, new BigExplosion()));
            addMenuItem(new JudgementItem(this, new Explosion()));
            addMenuItem(new JudgementItem(this, new KickOff()));
            addMenuItem(new JudgementItem(this, new LightOnFire(Judgement.JudgementType.Team)));
            addMenuItem(new JudgementItem(this, new LightOnFire(Judgement.JudgementType.TeamMember)));
            addMenuItem(new JudgementItem(this, new LightOnFire(Judgement.JudgementType.Global)));
            addMenuItem(new JudgementItem(this, new ScoreChange(parent.Manager, 5)));



        }


        public override void Update(GameTime gametime)
        {
            foreach (Player player in owners)
            {
                if (current == null) current = menuitems.ElementAt(0);

                player.Input.Update();
                if (player.Input.IsMoveUpNewlyPressed() || player.Input.IsDPadUpNewlyPressed()) current_index--;
                if (player.Input.IsMoveDownNewlyPressed() || player.Input.IsDPadDownNewlyPressed()) current_index++;
                if (player.Input.IsBButtonNewlyPressed()) parent.CloseMenu();
                if (current_index >= menuitems.Count) current_index = 0;
                if (current_index < 0) current_index = menuitems.Count - 1;
                current = menuitems.ElementAt(current_index);
                if (player.Input.IsAButtonNewlyPressed())
                {
                    player.Input.Update();
                    current.execute();
                }
            }
                }
        }








    }






