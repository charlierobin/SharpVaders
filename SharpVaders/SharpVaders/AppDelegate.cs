using System;

using AppKit;
using SpriteKit;
using Foundation;

namespace SharpVaders
{
    public partial class AppDelegate : NSApplicationDelegate
    {
        public override void DidFinishLaunching(NSNotification notification)
        {
            SKScene scene = SKNode.FromFile<SceneGame>("SceneGame");

            scene.ScaleMode = SKSceneScaleMode.AspectFit;

            MyGameView.PresentScene(scene);

            MyGameView.IgnoresSiblingOrder = true;

            MyGameView.ShowsFPS = true;
            MyGameView.ShowsNodeCount = true;

            MyGameView.ShowsPhysics = true;
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }

        public override void WillTerminate(NSNotification notification) { }
    }
}

