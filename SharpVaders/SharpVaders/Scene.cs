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

        private Button _focussed;
        
        private Button focussed
        {
            get { return this._focussed; }
            set
            {
                this.focusHighlight?.RemoveFromParent();

                this._focussed = value;

                this.focusHighlight = SKShapeNode.FromRect(this.focussed.Frame.Size);

                this.focusHighlight.FillColor = NSColor.White;

                this.focusHighlight.LineWidth = 4;

                this.focusHighlight.Alpha = 0.2f;

                this.Add(this.focusHighlight);

                this.focusHighlight.Position = this.focussed.Position;

                this.focusHighlight.ZPosition = -100;
            }
        }

        private Button _rolledOver;

        private Button rolledOver
        {
            get { return this._rolledOver; }
            set
            {
                if (value != this._rolledOver)
                {
                    if (this._rolledOver != null)
                    {
                        this._rolledOver.highlighted = false;
                    }

                    this._rolledOver = value;

                    if (this._rolledOver != null)
                    {
                        this._rolledOver.highlighted = true;
                    }
                }
            }
        }

        private List<Button> buttons = new List<Button>();

        private SKShapeNode focusHighlight;

        public Scene(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            base.DidMoveToView(view);

            NSTrackingAreaOptions options = NSTrackingAreaOptions.MouseMoved | NSTrackingAreaOptions.ActiveInKeyWindow;

            NSTrackingArea trackingArea = new NSTrackingArea(view.Frame, options, this, null);

            view.AddTrackingArea(trackingArea);
        }

        public void AddButton(Button button)
        {
            this.AddChild(button);

            this.buttons.Add(button);

            if (this.buttons.Count == 1)
            {
                this.focussed = this.buttons[0];
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

                default:
                    {
                        Console.WriteLine("Unhandled in PerformAction: " + name);

                        break;
                    }
            }
        }

        public override void KeyDown(NSEvent theEvent)
        {
            //Console.WriteLine(theEvent.KeyCode.ToString());

            if (theEvent.KeyCode == 36 || theEvent.CharactersIgnoringModifiers == "/" || theEvent.CharactersIgnoringModifiers == " ")
            {
                // return

                this.PerformAction(this.focussed.Name);
            }
            else if (theEvent.CharactersIgnoringModifiers == "z" || theEvent.KeyCode == 126 || theEvent.KeyCode == 123)
            {
                // up arrow, left arrow

                this.moveFocusToPrevious();
            }
            else if (theEvent.CharactersIgnoringModifiers == "x" || theEvent.KeyCode == 125 || theEvent.KeyCode == 124 || theEvent.KeyCode == 48)
            {
                // down arrow, right arrow, tab

                this.moveFocusToNext();
            }
        }

        public override void MouseMoved(NSEvent theEvent)
        {
            //base.MouseMoved(theEvent);

            CGPoint loc = theEvent.LocationInNode(this);

            SKNode node = this.GetNodeAtPoint(loc);

            if(node is Button)
            {
                this.rolledOver = (Button)node;
            }
            else
            {
                this.rolledOver = null;
            }

            //Button test = node as Button;

            //this.rolledOver = test;

        }
    }
}

