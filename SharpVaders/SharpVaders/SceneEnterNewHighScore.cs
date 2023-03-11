using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;
using CoreVideo;
using System.Timers;

namespace SharpVaders
{
    public class SceneEnterNewHighScore : Scene
    {
        private string name = "";
        private int score = 0;
        private SKLabelNode nameEntryLabel;

        private System.Timers.Timer cursorTimer;
        private bool blink = false;

        public SceneEnterNewHighScore(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            this.score = int.Parse(this.data["score"]);

            if (this.score == 0) throw new Exception("There must be an incoming new high score for SceneEnterNewHighScore");

            SKSpriteNode title = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/buttonHighScores", "png"));
            title.SetScale(0.5f);
            title.Position = new CGPoint(this.Frame.GetMidX(), this.Frame.GetMaxY() - title.Size.Height);
            title.BlendMode = SKBlendMode.Screen;
            AddChild(title);

            this.nameEntryLabel = SKLabelNode.FromFont("RetroBitmap");
            this.nameEntryLabel.FontSize = 64;
            this.nameEntryLabel.Position = new CGPoint(this.Frame.GetMidX(), this.Frame.GetMidY());
            this.nameEntryLabel.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;

            this.AddChild(this.nameEntryLabel);

            this.render();

            this.cursorTimer = new System.Timers.Timer(700);

            this.cursorTimer.Elapsed += this.timer;
            this.cursorTimer.AutoReset = true;
            this.cursorTimer.Enabled = true;
        }

        private void timer(Object source, ElapsedEventArgs e)
        {
            this.blink = !this.blink;

            this.render();
        }

        private void render()
        {
            this.nameEntryLabel.Text = this.name;

            this.nameEntryLabel.Position = new CGPoint(this.Frame.GetMidX() - (this.nameEntryLabel.Frame.Width / 2), this.Frame.GetMidY());

            if (this.blink)
            {
                this.nameEntryLabel.Text = this.nameEntryLabel.Text + "_";
            }
        }

        public override void KeyDown(NSEvent theEvent)
        {
            //Console.WriteLine(theEvent.KeyCode);

            if (theEvent.KeyCode == 36 || theEvent.KeyCode == 48)
            {
                // return, tab

                HighScores highScores = new HighScores();

                highScores.add(this.name, this.score);

                highScores.write();

                SKTransition transition = SKTransition.CrossFadeWithDuration(2);

                Scene scene = SKNode.FromFile<SceneHighScores>("SceneHighScores");

                scene.ScaleMode = SKSceneScaleMode.AspectFill;

                this.View.PresentScene(scene, transition);
            }
            else if (theEvent.KeyCode == 51)
            {
                // delete

                this.name = this.name.Substring(0, this.name.Length - 1);

                this.render();
            }
            else if (theEvent.KeyCode == 53)
            {
                // esc

                SKTransition transition = SKTransition.CrossFadeWithDuration(2);

                Scene scene = SKNode.FromFile<SceneHighScores>("SceneHighScores");

                scene.ScaleMode = SKSceneScaleMode.AspectFill;

                this.View.PresentScene(scene, transition);
            }
            else
            {
                this.name = this.name + theEvent.CharactersIgnoringModifiers;

                this.render();
            }
        }
    }
}
