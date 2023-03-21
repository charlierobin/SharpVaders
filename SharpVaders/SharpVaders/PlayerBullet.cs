using System;
using AppKit;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class PlayerBullet : SKSpriteNode
    {
        private static SKAction action;

        private float time = 1;

        private SceneGame game;

        private Player player;

        public PlayerBullet(SceneGame game, Player player) : base(texture: SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/bullet", "png")))
        {
            this.game = game;
            this.player = player;

            this.Name = "Bullet";

            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(100);
            this.PhysicsBody.AffectedByGravity = false;

            this.PhysicsBody.CategoryBitMask = (uint)Types.PlayerBullet;
            this.PhysicsBody.CollisionBitMask = (uint)Types.Nothing;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.Enemy + (uint)Types.FlyingSaucer + (uint)Types.Shield + (uint)Types.EnemyBullet;

            this.SetScale(0.08f);

            if (PlayerBullet.action == null)
            {
                PlayerBullet.action = SKAction.MoveToY(this.game.Frame.Height, this.time);
            }

            this.RunAction(PlayerBullet.action, () =>
            {
                this.player.bulletDestroyed();

                this.RemoveFromParent();
            });
        }

        public void hit()
        {
            this.player.bulletDestroyed();
        }
    }
}

