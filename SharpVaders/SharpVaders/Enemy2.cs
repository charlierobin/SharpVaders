using System;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class Enemy2 : Enemy
    {
        public Enemy2(SceneGame game, Wave wave, nfloat x, nfloat y) : base(game, wave, x, y, 10, "graphics/enemies/enemy-2") { }
    }
}

