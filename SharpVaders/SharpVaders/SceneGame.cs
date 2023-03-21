using System;
using System.Collections.Generic;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;
using GameKit;

namespace SharpVaders
{
    public class SceneGame : Scene
    {
        public Random random = new Random();

        private int _score = 0;
        private int _lives = 1;

        private UI ui;

        public int score
        {
            get { return this._score; }
            set
            {
                this._score = value;
                this.ui.update(this._score, this._lives);
            }
        }

        public int lives
        {
            get { return this._lives; }

            set
            {
                this._lives = value;

                this.ui.update(this._score, this._lives);
            }
        }

        private Player player;
        private Shields shields;
        private Wave wave;

        private double lastTime;

        //private List<SKNode> toRemove = new List<SKNode>();

        public SceneGame(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            this.PhysicsWorld.ContactDelegate = new ContactDelegate(this);

            this.ui = new UI(this, this.score, this.lives);

            this.Add(this.ui);

            this.player = new Player(this);

            this.shields = new Shields(this);

            this.Add(new FlyingSaucers(this));

            this.wave = new Wave(this);
        }

        public override void KeyDown(NSEvent theEvent)
        {
            this.player?.KeyDown(theEvent);
            this.wave?.KeyDown(theEvent);

            if (theEvent.CharactersIgnoringModifiers == "q")
            {
                this.lives = 0;

                this.player.explode();
            }
            else if (theEvent.CharactersIgnoringModifiers == "e")
            {
                Explosion.Spawn(new CGPoint(400, 400), this);
            }
        }

        public override void KeyUp(NSEvent theEvent)
        {
            this.player?.KeyUp(theEvent);
        }

        public override void Update(double currentTime)
        {
            double delta = currentTime - this.lastTime;

            this.lastTime = currentTime;

            this.player?.update(delta);
        }

        //public override void DidFinishUpdate()
        //{
        //    base.DidFinishUpdate();

        //    foreach (SKNode node in this.toRemove)
        //    {
        //        node.RemoveFromParent();
        //    }

        //    this.toRemove.Clear();
        //}

        //

        private void PlayerBullet_FlyingSaucer_Contact(SKPhysicsContact contact)
        {
            PlayerBullet bullet = null;
            FlyingSaucer saucer = null;

            if (contact.BodyA.CategoryBitMask == (uint)Types.FlyingSaucer)
            {
                bullet = (PlayerBullet)contact.BodyB.Node;
                saucer = (FlyingSaucer)contact.BodyA.Node;
            }
            else
            {
                bullet = (PlayerBullet)contact.BodyA.Node;
                saucer = (FlyingSaucer)contact.BodyB.Node;
            }

            this.score = this.score + saucer.value;

            bullet.hit();
            saucer.hit();

            Explosion.Spawn(saucer);

            //this.toRemove.Add(bullet);
            //this.toRemove.Add(saucer);

            bullet.RemoveFromParent();
            saucer.RemoveFromParent();
        }

        private void PlayerBullet_Enemy_Contact(SKPhysicsContact contact)
        {
            PlayerBullet bullet = null;
            Enemy enemy = null;

            if (contact.BodyA.CategoryBitMask == (uint)Types.Enemy)
            {
                bullet = (PlayerBullet)contact.BodyB.Node;
                enemy = (Enemy)contact.BodyA.Node;
            }
            else
            {
                bullet = (PlayerBullet)contact.BodyA.Node;
                enemy = (Enemy)contact.BodyB.Node;
            }

            this.score = this.score + enemy.value;

            bullet.hit();
            enemy.hit();

            Explosion.Spawn(enemy);

            //this.toRemove.Add(bullet);
            //this.toRemove.Add(enemy);

            bullet.RemoveFromParent();
            enemy.RemoveFromParent();
        }

        private void PlayerBullet_Shield_Contact(SKPhysicsContact contact)
        {
            PlayerBullet bullet = null;

            if (contact.BodyA.CategoryBitMask == (uint)Types.PlayerBullet)
            {
                bullet = (PlayerBullet)contact.BodyA.Node;
            }
            else
            {
                bullet = (PlayerBullet)contact.BodyB.Node;
            }

            bullet.hit();

            contact.BodyB.Node.RemoveFromParent();
            contact.BodyA.Node.RemoveFromParent();
        }

        private void PlayerBullet_EnemyBullet_Contact(SKPhysicsContact contact)
        {
            PlayerBullet bullet = null;

            if (contact.BodyA.CategoryBitMask == (uint)Types.PlayerBullet)
            {
                bullet = (PlayerBullet)contact.BodyA.Node;
            }
            else
            {
                bullet = (PlayerBullet)contact.BodyB.Node;
            }

            bullet.hit();

            Explosion.Spawn(bullet);

            contact.BodyB.Node.RemoveFromParent();
            contact.BodyA.Node.RemoveFromParent();
        }

        private void EnemyBullet_Shield_Contact(SKPhysicsContact contact)
        {
            contact.BodyB.Node.RemoveFromParent();
            contact.BodyA.Node.RemoveFromParent();
        }

        private void EnemyBullet_Player_Contact(SKPhysicsContact contact)
        {
            Player player = null;

            if (contact.BodyA.CategoryBitMask == (uint)Types.Player)
            {
                player = (Player)contact.BodyA.Node;

                contact.BodyB.Node.RemoveFromParent();
            }
            else
            {
                player = (Player)contact.BodyB.Node;

                contact.BodyA.Node.RemoveFromParent();
            }

            player.explode();
        }

        private void Enemy_Shield_Contact(SKPhysicsContact contact)
        {
            if (contact.BodyA.CategoryBitMask == (uint)Types.Shield) contact.BodyA.Node.RemoveFromParent();
            else contact.BodyB.Node.RemoveFromParent();
        }

        private void Enemy_Player_Contact(SKPhysicsContact contact)
        {
            Player player = null;

            if (contact.BodyA.CategoryBitMask == (uint)Types.Player)
            {
                player = (Player)contact.BodyA.Node;

                contact.BodyB.Node.RemoveFromParent();
            }
            else
            {
                player = (Player)contact.BodyB.Node;

                contact.BodyA.Node.RemoveFromParent();
            }

            player.explode();

            this.wave.reset();
        }

        private bool test(SKPhysicsContact contact, Types t1, Types t2)
        {
            if (contact.BodyA == null) return false;
            if (contact.BodyB == null) return false;

            if (contact.BodyA.Node == null) return false;
            if (contact.BodyB.Node == null) return false;

            return (contact.BodyA.CategoryBitMask == (uint)t1 && contact.BodyB.CategoryBitMask == (uint)t2) || (contact.BodyA.CategoryBitMask == (uint)t2 && contact.BodyB.CategoryBitMask == (uint)t1);
        }

        public void DidBeginContact(SKPhysicsContact contact)
        {
            if (this.test(contact, Types.PlayerBullet, Types.FlyingSaucer)) this.PlayerBullet_FlyingSaucer_Contact(contact);

            else if (this.test(contact, Types.PlayerBullet, Types.Enemy)) this.PlayerBullet_Enemy_Contact(contact);

            else if (this.test(contact, Types.PlayerBullet, Types.Shield)) this.PlayerBullet_Shield_Contact(contact);

            else if (this.test(contact, Types.PlayerBullet, Types.EnemyBullet)) this.PlayerBullet_EnemyBullet_Contact(contact);

            else if (this.test(contact, Types.EnemyBullet, Types.Shield)) this.EnemyBullet_Shield_Contact(contact);

            else if (this.test(contact, Types.EnemyBullet, Types.Player)) this.EnemyBullet_Player_Contact(contact);

            else if (this.test(contact, Types.Enemy, Types.Shield)) this.Enemy_Shield_Contact(contact);

            else if (this.test(contact, Types.Enemy, Types.Player)) this.Enemy_Player_Contact(contact);

            // https://stackoverflow.com/questions/40593678/what-are-sprite-kits-category-mask-and-collision-mask
        }

        public void waveDestroyed()
        {
            this.score = this.score + this.wave.value;

            this.wave.RemoveFromParent();

            this.wave = new Wave(this);

            this.shields.refreshShields();
        }

        public void playerWasDestroyed()
        {
            this.player = null;

            this.lives--;

            if (this.lives < 0)
            {
                this.RunAction(SKAction.WaitForDuration(3), () =>
                {
                    // TODO play a little animation, lock input while it plays?

                    this.gameOver();
                });
            }
            else
            {
                this.RunAction(SKAction.WaitForDuration(3), () =>
                {
                    this.player = new Player(this);

                    this.shields.refreshShields();
                });
            }
        }

        private void gameOver()
        {
            HighScores highscores = new HighScores();

            if (highscores.isNewHighScore(this.score))
            {
                this.PerformAction("enterNewHighScore", this.score);
            }
            else
            {
                this.PerformAction("buttonHighScores");
            }
        }
    }
}

