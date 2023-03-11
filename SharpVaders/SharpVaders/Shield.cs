using System;
using AppKit;
using CoreGraphics;
using SpriteKit;

namespace SharpVaders
{
    public class Shield : SKSpriteNode
    {
        public Shield(int x, int y)
        {
            SKShapeNode shield = SKShapeNode.FromRect(new CGRect(0, 0, 200, 10));
            shield.FillColor = NSColor.White;
            shield.Position = new CGPoint(-100, -5);

            this.Add(shield);

            this.PhysicsBody = SKPhysicsBody.CreateRectangularBody(shield.Frame.Size);
            //this.PhysicsBody.Dynamic = true;
            this.PhysicsBody.AffectedByGravity = false;
            this.PhysicsBody.CategoryBitMask = (uint)Types.Shield;
            this.PhysicsBody.CollisionBitMask = 0;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.EnemyBullet + (uint)Types.Bullet;

            this.Position = new CGPoint(x, y);
        }
    }
}

