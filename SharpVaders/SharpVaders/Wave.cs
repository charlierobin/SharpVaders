using System;
using SpriteKit;
using CoreGraphics;
using AppKit;

namespace SharpVaders
{
    public class Wave : SKNode
    {
        public int value = 1000;

        private SceneGame game;

        private int waveCount;

        private Type[] types = { typeof(Enemy1), typeof(Enemy1), typeof(Enemy2), typeof(Enemy2), typeof(Enemy3) };

        private int numberAcross = 11;
        private int numberDown = 0;

        private nfloat horizSpacing = 120;
        private nfloat vertSpacing = 120;

        private nfloat gridWidth = 0;
        private nfloat gridHeight = 0;

        private nfloat offset = 0;

        private nfloat horizMove = 0;
        private nfloat vertMove = 70;

        private nfloat horizMoveTime = 6;
        private nfloat vertMoveTime = 1;

        public Wave(SceneGame game)
        {
            this.game = game;

            this.Name = "Wave";

            this.game.Add(this);

            this.spawn();
        }

        public override void KeyDown(NSEvent theEvent)
        {
            if (theEvent.CharactersIgnoringModifiers == "r")
            {
                this.reset();
            }
        }

        public void spawn()
        {
            this.numberDown = this.types.Length;

            this.gridWidth = (this.numberAcross - 1) * this.horizSpacing;
            this.gridHeight = (this.numberDown - 1) * this.vertSpacing;

            this.offset = (this.game.Frame.Width - this.gridWidth) / 2;

            this.horizMove = this.offset * 2;

            for (int y = 0; y < this.numberDown; y++)
            {
                Type enemyType = this.types[y];

                nfloat yLevel = this.game.Frame.Height + 200 + (y * this.vertSpacing);

                for (int x = 0; x < this.numberAcross; x++)
                {
                    Enemy e = (Enemy)Activator.CreateInstance(enemyType, this.game, this, this.offset + (x * this.horizSpacing), yLevel);

                    SKAction behaviour = SKAction.Sequence(new[]
                    {
                        SKAction.WaitForDuration(1),
                        SKAction.MoveBy(0, -200 - this.gridHeight - this.vertSpacing, 2),
                        SKAction.MoveBy(this.offset, 0, this.horizMoveTime / 2)
                    });

                    e.RunAction(behaviour, () =>
                    {
                        SKAction b = SKAction.Sequence(new[]
                        {
                            SKAction.MoveBy(0, -this.vertMove, this.vertMoveTime),
                            SKAction.MoveBy(-this.horizMove, 0, this.horizMoveTime),
                            SKAction.MoveBy(0, -this.vertMove, this.vertMoveTime),
                            SKAction.MoveBy(this.horizMove, 0, this.horizMoveTime)
                        });

                        e.RunAction(SKAction.RepeatActionForever(b));
                    });

                    this.waveCount++;
                }
            }
        }

        public void reset()
        {
            this.game.EnumerateChildNodes("Enemy", (SKNode node, out bool stop) =>
            {
                node.RemoveAllActions();

                Enemy e = (Enemy)node;

                SKAction behaviour = SKAction.Sequence(new[]
                {
                        SKAction.MoveBy(0, e.startY - node.Position.Y, 2),
                        SKAction.MoveTo(new CGPoint(e.startX, e.startY), 0.1),
                        SKAction.MoveBy(0, -200 - this.gridHeight - this.vertSpacing, 2),
                        SKAction.MoveBy(this.offset, 0, this.horizMoveTime / 2)
                });

                node.RunAction(behaviour, () =>
                {
                    SKAction b = SKAction.Sequence(new[]
                    {
                            SKAction.MoveBy(0, -this.vertMove, this.vertMoveTime),
                            SKAction.MoveBy(-this.horizMove, 0, this.horizMoveTime),
                            SKAction.MoveBy(0, -this.vertMove, this.vertMoveTime),
                            SKAction.MoveBy(this.horizMove, 0, this.horizMoveTime)
                    });

                    node.RunAction(SKAction.RepeatActionForever(b));
                });

                stop = false;
            });
        }

        public void hit(Enemy enemy)
        {
            this.waveCount--;

            if (this.waveCount == 0)
            {
                this.game.waveDestroyed();
            }
        }
    }
}

