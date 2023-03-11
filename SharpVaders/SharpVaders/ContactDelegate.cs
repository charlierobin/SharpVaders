using System;
using SpriteKit;

namespace SharpVaders
{
    public class ContactDelegate : SKPhysicsContactDelegate
    {
        private SceneGame scene;

        public ContactDelegate(SceneGame scene)
        {
            this.scene = scene;
        }

        public override void DidBeginContact(SKPhysicsContact contact)
        {
            this.scene.DidBeginContact(contact);
        }

        //public override void DidEndContact(SKPhysicsContact contact)
        //{
        //    base.DidEndContact(contact);
        //}
    }
}

