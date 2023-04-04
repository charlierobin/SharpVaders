using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;

namespace SharpVaders
{
    public class SceneIntroMenu : Scene
    {
        //private SKTexture[] textures = new SKTexture[2];




        public SceneIntroMenu(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            base.DidMoveToView(view);

            SKSpriteNode title = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/title", "png"));
            title.SetScale(0.5f);
            title.Position = new CGPoint(this.Frame.GetMidX(), this.Frame.GetMaxY() - title.Size.Height);
            title.BlendMode = SKBlendMode.Screen;
            AddChild(title);

            this.AddButton(new Button("buttonNewGame", this.Frame.GetMidX(), this.Frame.Height - 300));

            this.AddButton(new Button("buttonHighScores", this.Frame.GetMidX(), this.Frame.Height - 400));

            this.AddButton(new Button("buttonQuit", this.Frame.GetMidX(), 80));






            //this.textures[0] = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/enemies/enemy-1", "png"));
            //this.textures[1] = SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/enemies/enemy-1-alt", "png"));


            //SKSpriteNode test = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/enemies/enemy-1", "png"));

            //test.SetScale(0.08f);

            //this.Add(test);


            //test.Position = new CGPoint(200,200);

            //SKAction animation = SKAction.AnimateWithTextures(this.textures, 0.5);

            //test.RunAction(SKAction.RepeatActionForever(animation));


        }
    }
}







