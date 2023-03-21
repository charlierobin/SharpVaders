using System;
using CoreGraphics;
using SceneKit;
using SpriteKit;

namespace SharpVaders
{
    public class Explosion
    {
        private static SKEmitterNode explosion;

        public static void Spawn(CGPoint pos, SKScene scene)
        {
            if (Explosion.explosion == null)
            {
                Explosion.explosion = SKEmitterNode.FromFile<SKEmitterNode>("ExplosionParticleEffect");
            }

            SKEmitterNode e = (SKEmitterNode)Explosion.explosion.Copy();

            scene.AddChild(e);

            e.Position = pos;

            e.RunAction(SKAction.Sequence(new[] { SKAction.WaitForDuration(3), SKAction.RemoveFromParent() }));
        }

        public static void Spawn(SKSpriteNode node)
        {
            Explosion.Spawn(node.Position, node.Scene);
        }


    }

    public class ExplosionPlayer
    {
        private static SKEmitterNode explosion;

        public static void Spawn(CGPoint pos, SKScene scene)
        {
            if (ExplosionPlayer.explosion == null)
            {
                ExplosionPlayer.explosion = SKEmitterNode.FromFile<SKEmitterNode>("PlayerExplosionParticleEffect");
            }

            SKEmitterNode e = (SKEmitterNode)ExplosionPlayer.explosion.Copy();

            scene.AddChild(e);

            e.Position = pos;

            e.RunAction(SKAction.Sequence(new[] { SKAction.WaitForDuration(3), SKAction.RemoveFromParent() }));
        }

        public static void Spawn(SKSpriteNode node)
        {
            ExplosionPlayer.Spawn(node.Position, node.Scene);
        }
    }
}

