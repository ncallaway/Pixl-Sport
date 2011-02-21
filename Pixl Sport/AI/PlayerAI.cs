using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Pixl_Sport.AI
{
    class PlayerAI
    {
        public enum Action
        {
            NoAction,
            MoveNearPosition,
            MoveNearTeamMember,
            MoveToPosition,
            MoveToTeamMember,
            MoveToBall,

            CreateClearLineBetweenBall,
            CreateClearLineBetweenGoal,
            DefendPlayer,

            Stunned,

            Wait,
        }

        public TeamAI.Instruction Instruction { get { return instruction; }
            set
            {
                if (instruction != value) {
                    action = Action.NoAction;
                }
                instruction = value;
            }
        }

        public PlayerAI(TeamMember player)
        {
            this.player = player;
            instruction = TeamAI.Instruction.GetOpen;
            action = Action.NoAction;

            parentAi = player.Team.AI;
        }

        public void Stun(TimeSpan duration)
        {
            action = Action.Stunned;
            actionTimeSpan = duration;
        }

        private TeamAI.Instruction instruction;
        private Ball instructionBall;
        private Vector2 instructionPosition;
        private float instructionRadius;
        private TeamMember instructionTeamMember;

        

        public Ball InstructionBall { get { return instructionBall; } set {
            if (instructionBall != value) {
                action = Action.NoAction;
            }
            instructionBall = value; }
        }
        public Vector2 InstructionPosition
        {
            get { return instructionPosition; }
            set
            {
                if (instructionPosition != value) {
                    action = Action.NoAction;
                }
                instructionPosition = value;
            }
        }
        public float InstructionRadius
        {
            get { return instructionRadius; }
            set
            {
                if (instructionRadius != value) {
                    action = Action.NoAction;
                }
                instructionRadius = value;
            }
        }
        public TeamMember InstructionTeamMember
        {
            get { return instructionTeamMember; }
            set
            {
                if (instructionTeamMember != value) {
                    action = Action.NoAction;
                }
                instructionTeamMember = value;
            }
        }
        private Action action;
        private Ball actionBall;
        private Vector2 actionPosition;
        private float actionRadius;
        private TimeSpan actionTimeSpan;
        private TeamMember actionTeamMember;

        private TeamAI parentAi;

        private TeamMember player;

        public void Update(GameTime t)
        {
            setActionForInstruction();

            performAction(t);
        }

        private void setActionForInstruction()
        {
            /* SPECIAL ACTIONS!! */
            if (action == Action.Stunned) { return; }

            switch (instruction) {
                case TeamAI.Instruction.AcquireBall:
                    selectActionForAcquireBall();
                    break;
                case TeamAI.Instruction.DefendArea:
                    selectActionForDefendArea();
                    break;
                case TeamAI.Instruction.DefendPlayer:
                    selectActionForDefendPlayer();
                    break;
                case TeamAI.Instruction.DefendClosestPlayer:
                    selectActionForDefendClosestPlayer();
                    break;
                case TeamAI.Instruction.GetOpen:
                    action = Action.Wait;
                    break;
            }
        }

        private void selectActionForAcquireBall()
        {
            if (player.HasBall && player.HeldBall == instructionBall) {
                /* GOT IT! */
                action = Action.Wait;
            }

            action = Action.MoveToBall;
            actionBall = instructionBall;
        }

        private void selectActionForDefendPlayer()
        {
            action = Action.DefendPlayer;
            actionTeamMember = instructionTeamMember;
        }

        private void selectActionForDefendClosestPlayer()
        {
            Vector2 ballPos = parentAi.Team.Manager.Ball.Position;
            TeamMember closest = null;
            foreach (TeamMember m in parentAi.Opposition.Members) {
                if (closest == null
                    || Vector2.DistanceSquared(m.Position, ballPos) < Vector2.DistanceSquared(closest.Position, ballPos)) {
                    closest = m;
                }
            }

            action = Action.DefendPlayer;
            actionTeamMember = closest;
        }

        private void selectActionForDefendArea()
        {
            if (Vector2.DistanceSquared(player.Position, instructionPosition) < instructionRadius * instructionRadius) {
                /* Where should we go? */
                Vector2 ballPos = parentAi.Team.Manager.Ball.Position;
                TeamMember closest = null;
                foreach (TeamMember m in parentAi.Opposition.Members) {
                    if (Vector2.DistanceSquared(m.Position, actionPosition) < actionRadius * actionRadius) {
                        if (closest == null 
                            || Vector2.DistanceSquared(m.Position, ballPos) < Vector2.DistanceSquared(closest.Position, ballPos)) {
                            closest = m;
                        }
                    }
                }

                if (closest == null) {
                    /* Move to Point nearest ball-carrier within our radius */
                    Vector2 direction = (ballPos - instructionPosition);
                    direction.Normalize();

                    actionPosition = instructionPosition + direction * instructionRadius;
                    action = Action.MoveToPosition;
                    return;
                }
                
                /* GOT IT! */
                if (closest.HasBall) {
                    actionTeamMember = closest;
                    action = Action.DefendPlayer;
                }
                return;
            }

            action = Action.MoveToPosition;
            actionPosition = instructionPosition;
        }

        private void performAction(GameTime t)
        {
            switch (action) {
                case Action.MoveToBall:
                    performMoveToBall();
                    break;
                case Action.MoveToPosition:
                    performMoveToPosition();
                    break;
                case Action.DefendPlayer:
                    performDefendPlayer();
                    break;
                case Action.Stunned:
                    performStunned(t);
                    break;
                case Action.NoAction:
                case Action.Wait:
                default:
                    performWait();
                    break;
            }
        }

        private void performStunned(GameTime t)
        {
            actionTimeSpan -= t.ElapsedGameTime;

            if (actionTimeSpan.TotalMilliseconds < 0) {
                /* NO LONGER STUNNED */
                action = Action.NoAction;
            }
        }

        private void performDefendPlayer()
        {
            Vector2 direction;
            Vector2 target;
            float distance = Vector2.Distance(actionTeamMember.Position, player.Position);
            //target = actionTeamMember.Position + actionTeamMember.Velocity * distance;
            target = actionTeamMember.Position;
            if (actionTeamMember.HasBall) {
                /* Get between him and the goal! */
                direction = parentAi.Home ? Vector2.UnitX : -Vector2.UnitX;
                
                if ( Math.Abs(actionTeamMember.Position.Y - player.Position.Y) > 10f ) {
                    target = target + direction * 15f;
                }
            } else {
                /* Get between him and the ball! */
                direction = parentAi.Team.Manager.Ball.Position - actionTeamMember.Position;
                target = target + direction * 15f;
            }


            Vector2 myDirection = (target - player.Position);

            if (myDirection.LengthSquared() < TeamMember.PLAYER_SPEED * TeamMember.PLAYER_SPEED) {
                player.Position = target;
                return;
            }

            myDirection.Normalize();
            player.Position += myDirection;
        }

        private void performMoveToBall()
        {
            if (player.HasBall && player.HeldBall == actionBall) {
                /* ACTION ACCOMPLISHED! */
                action = Action.Wait;
                return;
            }

            /* Get Vector between us and ball! */
            Vector2 direction = actionBall.Position - player.Position;

            if (direction.LengthSquared() < TeamMember.PLAYER_SPEED * TeamMember.PLAYER_SPEED) {
                player.Position = actionBall.Position;
                return;
            }

            direction.Normalize();

            direction *= TeamMember.PLAYER_SPEED;

            player.Position += direction;
        }

        private void performMoveToPosition()
        {
            if (player.Position == actionPosition) {
                /* ACTION ACCOMPLISHED! */
                action = Action.Wait;
                return;
            }

            /* Get Vector between us and ball! */
            Vector2 direction = actionPosition - player.Position;

            if (direction.LengthSquared() < TeamMember.PLAYER_SPEED * TeamMember.PLAYER_SPEED) {
                player.Position = actionPosition;
                return;
            }

            direction.Normalize();

            direction *= TeamMember.PLAYER_SPEED;

            player.Position += direction;
        }

        private void performWait()
        {
            return;
        }
    }
}
