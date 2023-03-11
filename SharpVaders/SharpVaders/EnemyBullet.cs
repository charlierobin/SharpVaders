using System;
using AppKit;
using CoreGraphics;
using SpriteKit;

namespace SharpVaders
{
	public class EnemyBullet : SKSpriteNode
    {
        private SKAction action;

        public EnemyBullet(SKSpriteNode enemy)
		{
            SKShapeNode circle = SKShapeNode.FromCircle(10);
            circle.FillColor = NSColor.White;
            this.Add(circle);

            this.action = SKAction.MoveToY(0, 0.4);

            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(10);
            //this.PhysicsBody.Dynamic = true;
            this.PhysicsBody.AffectedByGravity = false;
            this.PhysicsBody.CategoryBitMask = (uint)Types.EnemyBullet;
            this.PhysicsBody.CollisionBitMask = 0;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.Shield + (uint)Types.Player;

            this.Position = enemy.Position;

            this.RunAction(this.action, () =>
            {
                this.RemoveFromParent();
            });
        }
    }
}

