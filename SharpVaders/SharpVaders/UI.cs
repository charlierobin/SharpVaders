using System;
using CoreGraphics;
using Foundation;
using GameKit;
using SpriteKit;

namespace SharpVaders
{
    public class UI : SKNode
    {
        private SKLabelNode scoreUI;
        private SKLabelNode livesUI;

        public UI(SceneGame game, int score, int lives)
        {
            this.scoreUI = SKLabelNode.FromFont("RetroBitmap");

            this.scoreUI.Text = "Score: ";
            this.scoreUI.FontSize = 18;
            this.scoreUI.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;
            this.scoreUI.Position = new CGPoint(10, game.Frame.Height - 30);
            this.scoreUI.FontColor = AppKit.NSColor.White;

            this.AddChild(this.scoreUI);

            this.livesUI = SKLabelNode.FromFont("RetroBitmap");
            this.livesUI.Position = new CGPoint(game.Frame.Width - 10, game.Frame.Height - 30);
            this.livesUI.Text = "Lives: ";
            this.livesUI.FontSize = 18;
            this.livesUI.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Right;
            this.livesUI.FontColor = AppKit.NSColor.White;

            this.AddChild(this.livesUI);

            this.updateScore(score);

            this.updateLives(lives);
        }

        private void updateScore(int score)
        {
            this.scoreUI.Text = "Score: " + score.ToString();
        }

        private void updateLives(int lives)
        {
            if (lives >= 0)
            {
                this.livesUI.Text = "Lives: " + lives.ToString();
            }
            else
            {
                this.livesUI.Text = "Lives: 0";
            }
        }

        public void update(int score, int lives)
        {
            this.updateScore(score);
            this.updateLives(lives);
        }
    }
}

