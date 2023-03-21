using System;
using SpriteKit;
using CoreGraphics;

namespace SharpVaders
{
    public class Shields
    {
        private SceneGame game;

        public Shields(SceneGame game)
        {
            this.game = game;

            this.refreshShields();
        }

        public void refreshShields()
        {
            this.game.EnumerateChildNodes("Shield", (SKNode node, out bool stop) =>
            {
                node.RemoveFromParent();

                stop = false;
            });

            float y = 140;

            Shield shield1 = new Shield(this.game, 0, y);
            Shield shield2 = new Shield(this.game, 0, y);
            Shield shield3 = new Shield(this.game, 0, y);
            Shield shield4 = new Shield(this.game, 0, y);
            Shield shield5 = new Shield(this.game, 0, y);

            double spacing = shield1.CalculateAccumulatedFrame().Width + shield2.CalculateAccumulatedFrame().Width + shield3.CalculateAccumulatedFrame().Width + shield4.CalculateAccumulatedFrame().Width + shield5.CalculateAccumulatedFrame().Width;

            spacing = this.game.Frame.Width - spacing;

            spacing = spacing / 5;

            shield1.Position = new CGPoint(spacing / 2, y);
            shield2.Position = new CGPoint(shield1.Position.X + spacing + shield1.CalculateAccumulatedFrame().Width, y);
            shield3.Position = new CGPoint(shield2.Position.X + spacing + shield2.CalculateAccumulatedFrame().Width, y);
            shield4.Position = new CGPoint(shield3.Position.X + spacing + shield3.CalculateAccumulatedFrame().Width, y);
            shield5.Position = new CGPoint(shield4.Position.X + spacing + shield4.CalculateAccumulatedFrame().Width, y);

            this.game.Add(shield1);
            this.game.Add(shield2);
            this.game.Add(shield3);
            this.game.Add(shield4);
            this.game.Add(shield5);
        }
    }
}

