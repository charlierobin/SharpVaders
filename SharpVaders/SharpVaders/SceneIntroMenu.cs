using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;

namespace SharpVaders
{
    public class SceneIntroMenu : Scene
    {
        public SceneIntroMenu(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            SKSpriteNode title = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/title", "png"));
            title.SetScale(0.5f);
            title.Position = new CGPoint(this.Frame.GetMidX(), this.Frame.GetMaxY() - title.Size.Height);
            title.BlendMode = SKBlendMode.Screen;
            AddChild(title);

            this.AddButton(new Button("buttonNewGame", this.Frame.GetMidX(), this.Frame.Height - 300));

            this.AddButton(new Button("buttonHighScores", this.Frame.GetMidX(), this.Frame.Height - 400));

            this.AddButton(new Button("buttonQuit", this.Frame.GetMidX(), 80));
        }
    }
}
