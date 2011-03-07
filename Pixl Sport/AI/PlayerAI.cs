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
            instruction = TeamAI.Instruction.None;
            action = Action.NoAction;
            actionTimeSpan = TimeSpan.Zero;

            parentAi = player.Team.AI;
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
            if (player.State != PlayerState.Normal) { return; }

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
                    selectActionForGetOpen();
                    break;
                case TeamAI.Instruction.GetOpenNoInfo:
                    selectActionForGetOpenNoInfo();
                    break;
                default:
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

        private void selectActionForGetOpen()
        {
            float distanceS = Vector2.DistanceSquared(player.Position, instructionPosition);

            if (action == Action.CreateClearLineBetweenBall && distanceS < (instructionRadius * instructionRadius)) {
                return;
            }

            if (distanceS < (instructionRadius * instructionRadius) * .6f) {
                action = Action.CreateClearLineBetweenBall;
                actionBall = parentAi.Team.Manager.Ball;
                return;
            }
            action = Action.MoveToPosition;
            actionPosition = instructionPosition;
        }

        private void selectActionForGetOpenNoInfo()
        {
            float ballDistance = Vector2.Distance(player.Position, parentAi.Team.Manager.Ball.Position);

            if (action == Action.CreateClearLineBetweenBall && ballDistance < 75f) {
                return;
            }

            if (ballDistance < 50f) {
                action = Action.CreateClearLineBetweenBall;
                actionBall = parentAi.Team.Manager.Ball;
                return;
            }

            action = Action.MoveToBall;
            actionBall = parentAi.Team.Manager.Ball;
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
            if (player.State == PlayerState.Stunned) { return; }
            if (player.State == PlayerState.OnFire) { performOnFire(t); return; }

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
                case Action.CreateClearLineBetweenBall:
                    performCreateClearLineBetweenBall();
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

        private void performOnFire(GameTime t)
        {
            actionTimeSpan -= t.ElapsedGameTime;

            if (actionTimeSpan < TimeSpan.Zero)
            {
                Random rand = new Random();
                int miliseconds = rand.Next(500, 1000);
                actionTimeSpan = new TimeSpan(0, 0, 0, 0, miliseconds);

                actionPosition = new Vector2((float)rand.NextDouble() * - .5f, (float)rand.NextDouble() - .5f );
                actionPosition.Normalize();
            }

            actionPosition.Normalize();
            player.UpdatePosition(actionPosition * TeamMember.PLAYER_SPEED);
        }

        private void performCreateClearLineBetweenBall()
        {
            Ball ball = parentAi.Team.Manager.Ball;

            Vector2 ballDirection = ball.Position - player.Position;
            ballDirection.Normalize();

            /* Perpendicular! */
            Vector2 upDirection; upDirection.X = -ballDirection.Y; upDirection.Y = ballDirection.X;
            Vector2 downDirection = -upDirection;

            TeamMember tmMin = null;
            float dotAbsMin = 100f;
            foreach (TeamMember m in parentAi.Opposition.Members) {
                if (Math.Abs(Vector2.Dot(m.Position - player.Position, upDirection)) < dotAbsMin) {
                    dotAbsMin = Math.Abs(Vector2.Dot(m.Position - player.Position, upDirection));
                    tmMin = m;
                }
            }

            if (tmMin == null) { return; }
            Vector2 direction = Vector2.Zero;
            
            if (Vector2.Dot(tmMin.Position - player.Position, upDirection) > 0) {
                /* GO DOWN! */
                direction = downDirection;
            } else {
                direction = upDirection;
            }

            player.UpdatePosition(direction * TeamMember.PLAYER_SPEED);
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
            player.UpdatePosition(myDirection);
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

            player.UpdatePosition(direction);
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

            player.UpdatePosition (direction);
        }

        private void performWait()
        {
            return;
        }
    }
}
