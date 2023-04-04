using System;
using AppKit;
using CoreGraphics;
using Foundation;
using SpriteKit;
using static CoreFoundation.DispatchSource;

namespace SharpVaders
{
    public class Player : SKSpriteNode
    {
        private float speed = 600;

        private SceneGame game;

        private PlayerBullet bullet;

        private bool leftPressed = false;
        private bool rightPressed = false;
        private bool firePressed = false;

        public Player(SceneGame game) : base(texture: SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/player", "png")))
        {
            this.Texture.UsesMipmaps = true;

            this.game = game;

            this.Name = "Player";

            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(500);
            this.PhysicsBody.AffectedByGravity = false;

            this.PhysicsBody.CategoryBitMask = (uint)Types.Player;
            this.PhysicsBody.CollisionBitMask = 0;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.EnemyBullet;

            this.SetScale(0.08F);

            this.game.AddChild(this);

            this.Position = new CGPoint(this.game.Frame.Width / 2, 50);
        }

        public override void KeyDown(NSEvent theEvent)
        {
            if (theEvent.CharactersIgnoringModifiers == "z")
            {
                this.leftPressed = true;
            }
            else if (theEvent.CharactersIgnoringModifiers == "x")
            {
                this.rightPressed = true;
            }
            else if (theEvent.CharactersIgnoringModifiers == "/")
            {
                this.firePressed = true;
            }
            else if (theEvent.CharactersIgnoringModifiers == "s")
            {
                this.explode();
            }
        }

        public override void KeyUp(NSEvent theEvent)
        {
            if (theEvent.CharactersIgnoringModifiers == "z")
            {
                this.leftPressed = false;
            }
            else if (theEvent.CharactersIgnoringModifiers == "x")
            {
                this.rightPressed = false;
            }
            else if (theEvent.CharactersIgnoringModifiers == "/")
            {
                this.firePressed = false;
            }
        }

        public void update(double delta)
        {
            if (this.leftPressed)
            {
                this.Position = new CGPoint(this.Position.X - (this.speed * delta), this.Position.Y);

                if (this.Position.X < 0)
                {
                    this.Position = new CGPoint(0, this.Position.Y);
                }
            }
            else if (this.rightPressed)
            {
                this.Position = new CGPoint(this.Position.X + (this.speed * delta), this.Position.Y);

                if (this.Position.X > this.Scene.Frame.Width)
                {
                    this.Position = new CGPoint(this.Scene.Frame.Width, this.Position.Y);
                }
            }

            if (this.bullet == null && this.firePressed)
            {
                this.bullet = new PlayerBullet(this.game, this);

                this.bullet.Position = this.Position;

                this.game.AddChild(this.bullet);
            }
        }

        public void bulletDestroyed()
        {
            this.bullet = null;
        }

        public void explode()
        {
            ExplosionPlayer.Spawn(this);

            this.RemoveFromParent();

            this.game.playerWasDestroyed();
        }
    }
}

