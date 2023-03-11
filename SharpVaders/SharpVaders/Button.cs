using System;
using System.Reflection;
using AppKit;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class Button : SKSpriteNode
    {
        public Button(string name, double x, double y)
        {
            this.Name = name;
            this.SetScale(0.5f);
            this.Position = new CGPoint(x, y);
            this.BlendMode = SKBlendMode.Screen;
            this.UserInteractionEnabled = true;
            this.RunAction(SKAction.SetTexture(SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/" + name, "png")), resize: true));
        }

        public override void MouseDown(NSEvent theEvent)
        {
            Type typeOfParent = this.Parent.GetType();

            MethodInfo methodRef = typeOfParent.GetMethod("PerformAction");

            methodRef.Invoke(this.Parent, new object[] { this.Name });
        }
    }
}

