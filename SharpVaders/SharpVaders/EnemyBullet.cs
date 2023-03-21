using System;
using AppKit;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class EnemyBullet : SKSpriteNode
    {
        //private static SKAction action;

        private float time = 1.5f;

        public EnemyBullet(SKSpriteNode enemy) : base(texture: SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/enemies/bomb", "png")))
        {
            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(150);
            this.PhysicsBody.AffectedByGravity = false;

            this.PhysicsBody.CategoryBitMask = (uint)Types.EnemyBullet;
            this.PhysicsBody.CollisionBitMask = (uint)Types.Nothing;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.Shield + (uint)Types.Player;

            this.Position = enemy.Position;

            this.SetScale(0.08f);

            //if (EnemyBullet.action == null)
            //{

            // TODO I think this is wrong ...

            nfloat y = (this.Position.Y + (enemy.Scene.Frame.Height / 2)) / enemy.Scene.Frame.Height;

            //    EnemyBullet.action = SKAction.Sequence(SKAction.MoveToY(0, this.time * y), SKAction.RemoveFromParent());
            //}

            //this.RunAction(EnemyBullet.action);

            this.RunAction(SKAction.Sequence(SKAction.MoveToY(0, this.time * y), SKAction.RemoveFromParent()));
        }
    }
}

