using System;
using AppKit;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class PlayerBullet : SKSpriteNode
    {
        private SKAction action;

        private SceneGame game;

        public PlayerBullet(SKSpriteNode player, SceneGame game)
        {
            this.game = game;

            this.action = SKAction.MoveToY(this.game.Frame.Height, 0.5);

            this.Add(SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/Bullet", "png")));

            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(60);
            //this.PhysicsBody.Dynamic = true;
            this.PhysicsBody.AffectedByGravity = false;
            this.PhysicsBody.CategoryBitMask = (uint)Types.Bullet;
            this.PhysicsBody.CollisionBitMask = 0;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.Enemy + (uint)Types.Shield;

            this.Position = player.Position;

            this.SetScale(0.5f);

            this.RunAction(this.action, () =>
            {
                this.RemoveFromParent();

                this.game.PlayerBulletActionDone();
            });
        }
    }
}

