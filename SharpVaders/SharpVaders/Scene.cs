using System;
using AppKit;
using SpriteKit;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;

namespace SharpVaders
{
    public class Scene : SKScene
    {
        public Dictionary<string, string> data = new Dictionary<string, string>();

        private Button focussed;
        //private Button rolledOver;

        private List<Button> buttons = new List<Button>();

        private SKShapeNode focusHighlight;

        public Scene(IntPtr handle) : base(handle) { }

        public void AddButton(Button button)
        {
            if (this.focusHighlight == null)
            {
                this.focusHighlight = SKShapeNode.FromRect(new CGRect(0, 0, 200, 100));

                this.focusHighlight.FillColor = NSColor.White;

                this.focusHighlight.Alpha = 0.2f;

                this.Add(this.focusHighlight);
            }

            this.AddChild(button);

            this.buttons.Add(button);

            if (this.buttons.Count == 1)
            {
                this.focussed = this.buttons[0];

                this.focusHighlight.Position = CGPoint.Subtract(this.focussed.Position, new CGSize(100, 50));
            }
        }

        private void moveFocusToPrevious()
        {
            int pos = this.buttons.IndexOf(this.focussed);

            pos--;

            if (pos < 0)
            {
                pos = this.buttons.Count - 1;
            }

            this.focussed = this.buttons[pos];

            this.focusHighlight.Position = CGPoint.Subtract(this.focussed.Position, new CGSize(100, 50));
        }

        private void moveFocusToNext()
        {
            int pos = this.buttons.IndexOf(this.focussed);

            pos++;

            if (pos > this.buttons.Count - 1)
            {
                pos = 0;
            }

            this.focussed = this.buttons[pos];

            this.focusHighlight.Position = CGPoint.Subtract(this.focussed.Position, new CGSize(100, 50));
        }

        public void PerformAction(string name, int score = 0)
        {
            switch (name)
            {
                case "buttonNewGame":
                    {
                        SKTransition transition = SKTransition.CrossFadeWithDuration(2);

                        SKScene scene = SKNode.FromFile<SceneGame>("SceneGame");

                        scene.ScaleMode = SKSceneScaleMode.AspectFit;

                        this.View.PresentScene(scene, transition);

                        break;
                    }

                case "buttonHighScores":
                    {
                        SKTransition transition = SKTransition.CrossFadeWithDuration(2);

                        SKScene scene = SKNode.FromFile<SceneHighScores>("SceneHighScores");

                        scene.ScaleMode = SKSceneScaleMode.AspectFit;

                        this.View.PresentScene(scene, transition);

                        break;
                    }

                case "buttonQuit":
                    {
                        NSApplication.SharedApplication.Terminate(null);

                        break;
                    }

                case "enterNewHighScore":
                    {
                        SKTransition transition = SKTransition.CrossFadeWithDuration(2);

                        Scene scene = SKNode.FromFile<SceneEnterNewHighScore>("SceneEnterNewHighScore");

                        scene.data["score"] = score.ToString();

                        scene.ScaleMode = SKSceneScaleMode.AspectFit;

                        this.View.PresentScene(scene, transition);

                        break;
                    }
            }
        }

        public override void KeyDown(NSEvent theEvent)
        {
            Console.WriteLine(theEvent.KeyCode.ToString());

            if (theEvent.KeyCode == 36 || theEvent.KeyCode == 48 || theEvent.CharactersIgnoringModifiers == "/")
            {
                // return, tab

                this.PerformAction(this.focussed.Name);
            }
            else if (theEvent.CharactersIgnoringModifiers == "z" || theEvent.KeyCode == 126 || theEvent.KeyCode == 123)
            {
                // up arrow, left arrow

                this.moveFocusToPrevious();
            }
            else if (theEvent.CharactersIgnoringModifiers == "x" || theEvent.KeyCode == 125 || theEvent.KeyCode == 124)
            {
                // down arrow, right arrow

                this.moveFocusToNext();
            }
        }
    }
}

