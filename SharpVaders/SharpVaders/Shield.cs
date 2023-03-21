using System;
using AppKit;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class Shield : SKSpriteNode
    {
        public Shield(SceneGame game, int x, int y) : base()
        {
            this.Name = "Shield";

            //SKSpriteNode brick = SKSpriteNode.FromTexture(SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("pixel", "png")));

            //brick.PhysicsBody = SKPhysicsBody.CreateRectangularBody(brick.Frame.Size);
            //brick.PhysicsBody.AffectedByGravity = false;

            //brick.PhysicsBody.CategoryBitMask = (uint)Types.Shield;
            //brick.PhysicsBody.CollisionBitMask = 0;

            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    //SKSpriteNode b = (SKSpriteNode)brick.Copy();

                    SKSpriteNode b = SKSpriteNode.FromTexture(SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("pixel", "png")));

                    b.PhysicsBody = SKPhysicsBody.CreateCircularBody(b.Frame.Size.Width / 2);
                    b.PhysicsBody.AffectedByGravity = false;

                    b.PhysicsBody.CategoryBitMask = (uint)Types.Shield;
                    b.PhysicsBody.CollisionBitMask = 0;

                    b.SetScale(0.3f);

                    this.Add(b);

                    b.Position = new CGPoint(column * b.Frame.Width, row * b.Frame.Height);
                }
            }

            //game.Add(this);

            this.Position = new CGPoint(x, y);
        }
    }
}

