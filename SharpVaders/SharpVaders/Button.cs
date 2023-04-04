using System;
using System.Reflection;
using AppKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class Button : SKSpriteNode
    {
        private static SKAction test;
        private static SKAction test2;

        private Boolean _highlighted;

        public Boolean highlighted
        {
            get { return this._highlighted; }
            set
            {
                this._highlighted = value;

                if (this._highlighted)
                {
                    this.RunAction(SKAction.RepeatActionForever(Button.test));
                }
                else
                {
                    this.RemoveAllActions();

                    this.RunAction(Button.test2);
                }
            }
        }

        public Button(string name, double x, double y) : base(texture: SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/" + name, "png")))
        {
            this.Name = name;
            this.SetScale(0.5f);
            this.Position = new CGPoint(x, y);
            this.BlendMode = SKBlendMode.Screen;
            this.UserInteractionEnabled = true;

            if (Button.test == null)
            {
                Button.test = SKAction.Sequence(SKAction.ColorizeWithColor(NSColor.Red, 1, 0.5), SKAction.ColorizeWithColor(NSColor.Red, 0, 0.5));
            }

            if (Button.test2 == null)
            {
                Button.test2 = SKAction.ColorizeWithColor(NSColor.Red, 0, 0.5);
            }
        }

        public override void MouseDown(NSEvent theEvent)
        {
            Type typeOfParent = this.Scene.GetType();

            MethodInfo methodRef = typeOfParent.GetMethod("PerformAction");

            methodRef.Invoke(this.Parent, new object[] { this.Name, 0 });
        }
    }
}

