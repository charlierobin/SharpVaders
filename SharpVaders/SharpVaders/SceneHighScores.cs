using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;

namespace SharpVaders
{
    public class SceneHighScores : Scene
    {
        public SceneHighScores(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            SKSpriteNode title = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/buttonHighScores", "png"));
            title.SetScale(0.5f);
            title.Position = new CGPoint(this.Frame.GetMidX(), this.Frame.GetMaxY() - title.Size.Height);
            title.BlendMode = SKBlendMode.Screen;
            AddChild(title);

            this.AddButton(new Button("buttonNewGame", this.Frame.GetMidX(), 180));

            this.AddButton(new Button("buttonQuit", this.Frame.GetMidX(), 80));

            HighScores highscores = new HighScores();

            double offset = this.Frame.Height - title.Size.Height - 140;

            foreach (HighScoreEntry entry in highscores.entries)
            {
                SKLabelNode label = SKLabelNode.FromFont("RetroBitmap");
                label.FontSize = 42;
                label.Position = new CGPoint(this.Frame.GetMidX(), offset);
                label.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Center;

                label.Text = entry.name + " " + entry.score.ToString();

                this.AddChild(label);

                offset = offset - ( label.FontSize + 15 );
            }
        }
    }
}
