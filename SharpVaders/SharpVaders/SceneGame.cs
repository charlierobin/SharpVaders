using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;

namespace SharpVaders
{
    public class SceneGame : Scene
    {
        private Random random = new Random();

        private int score;
        private int lives = 10;

        private SKLabelNode scoreUI;
        private SKLabelNode livesUI;

        private SKSpriteNode player;
        private SKSpriteNode bullet;

        private bool leftPressed = false;
        private bool rightPressed = false;
        private bool firePressed = false;

        private SKSpriteNode enemy;

        private float speed = 600;

        private double lastTime;

        public SceneGame(IntPtr handle) : base(handle) { }

        public override void DidMoveToView(SKView view)
        {
            this.PhysicsWorld.ContactDelegate = new ContactDelegate(this);

            this.setupUI();

            this.refreshShields();

            this.spawnNewPlayer();

            this.spawnNewEnemy();
        }

        private void setupUI()
        {
            this.scoreUI = SKLabelNode.FromFont("RetroBitmap");

            this.scoreUI.Text = "Score: " + this.score.ToString();
            this.scoreUI.FontSize = 18;
            this.scoreUI.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Left;

            this.scoreUI.Position = new CGPoint(10, this.Frame.Height - 30);

            this.scoreUI.FontColor = AppKit.NSColor.White;

            this.AddChild(this.scoreUI);


            this.livesUI = SKLabelNode.FromFont("RetroBitmap");

            this.livesUI.Text = "Lives: " + this.lives.ToString();
            this.livesUI.FontSize = 18;
            this.livesUI.HorizontalAlignmentMode = SKLabelHorizontalAlignmentMode.Right;

            this.livesUI.Position = new CGPoint(this.Frame.Width - 10, this.Frame.Height - 30);

            this.livesUI.FontColor = AppKit.NSColor.White;

            this.AddChild(this.livesUI);
        }

        private void spawnNewEnemy()
        {
            this.enemy = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/Enemy", "png"));
            this.enemy.Name = "Enemy";

            this.enemy.PhysicsBody = SKPhysicsBody.CreateCircularBody(100);
            //this.enemy.PhysicsBody.Dynamic = true;
            this.enemy.PhysicsBody.AffectedByGravity = false;
            this.enemy.PhysicsBody.CategoryBitMask = (uint)Types.Enemy;
            this.enemy.PhysicsBody.CollisionBitMask = 0;
            this.enemy.PhysicsBody.ContactTestBitMask = (uint)Types.Bullet;

            this.enemy.Position = new CGPoint(0, this.Frame.Height - 40);
            this.enemy.SetScale(0.5f);


            SKAction moveLeft = SKAction.MoveToX(this.Frame.Width, 5);
            SKAction moveRight = SKAction.MoveToX(0, 5);

            SKAction moveDown = SKAction.MoveBy(0, -50, 0.5);

            SKAction loop = SKAction.Sequence(new SKAction[] { moveLeft, moveDown, moveRight, moveDown });

            this.enemy.RunAction(SKAction.RepeatActionForever(loop));

            this.cueNextEnemyShoot();

            this.AddChild(this.enemy);
        }

        private void cueNextEnemyShoot()
        {
            this.enemy.RunAction(SKAction.WaitForDuration(random.NextDouble() * 2.0f), () =>
            {
                this.AddChild(new EnemyBullet(this.enemy));

                this.cueNextEnemyShoot();
            });
        }

        private void refreshShields()
        {
            this.Add(new Shield(200, 170));
            this.Add(new Shield(600, 170));
            this.Add(new Shield(1000, 170));
            this.Add(new Shield(1400, 170));
            this.Add(new Shield(1800, 170));
        }

        private void spawnNewPlayer()
        {
            this.player = SKSpriteNode.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/Spaceship", "png"));
            this.player.Name = "Player";

            this.player.PhysicsBody = SKPhysicsBody.CreateCircularBody(100);
            //this.player.PhysicsBody.Dynamic = true;
            this.player.PhysicsBody.AffectedByGravity = false;
            this.player.PhysicsBody.CategoryBitMask = (uint)Types.Player;
            this.player.PhysicsBody.CollisionBitMask = 0;
            this.player.PhysicsBody.ContactTestBitMask = (uint)Types.EnemyBullet;

            this.player.Position = new CGPoint(this.Frame.Width / 2, 50);
            this.player.SetScale(0.5f);

            this.AddChild(this.player);
        }

        public override void KeyDown(NSEvent theEvent)
        {
            if (theEvent.CharactersIgnoringModifiers == "z")
            {
                this.leftPressed = true;
            }

            if (theEvent.CharactersIgnoringModifiers == "x")
            {
                this.rightPressed = true;
            }

            if (theEvent.CharactersIgnoringModifiers == "/")
            {
                this.firePressed = true;
            }

            if (theEvent.CharactersIgnoringModifiers == "q")
            {
                this.gameOver();
            }
        }

        public override void KeyUp(NSEvent theEvent)
        {
            if (theEvent.CharactersIgnoringModifiers == "z")
            {
                this.leftPressed = false;
            }

            if (theEvent.CharactersIgnoringModifiers == "x")
            {
                this.rightPressed = false;
            }

            if (theEvent.CharactersIgnoringModifiers == "/")
            {
                this.firePressed = false;
            }
        }

        public override void Update(double currentTime)
        {
            double delta = currentTime - this.lastTime;

            this.lastTime = currentTime;

            if (this.leftPressed)
            {
                this.player.Position = new CGPoint(this.player.Position.X - (this.speed * delta), this.player.Position.Y);

                if (this.player.Position.X < 0)
                {
                    this.player.Position = new CGPoint(0, this.player.Position.Y);
                }
            }
            else if (this.rightPressed)
            {
                this.player.Position = new CGPoint(this.player.Position.X + (this.speed * delta), this.player.Position.Y);

                if (this.player.Position.X > this.Frame.Width)
                {
                    this.player.Position = new CGPoint(this.Frame.Width, this.player.Position.Y);
                }
            }

            if (this.bullet == null && this.firePressed)
            {
                this.bullet = new PlayerBullet(this.player, this);

                this.AddChild(this.bullet);
            }
        }

        public void PlayerBulletActionDone()
        {
            this.bullet = null;
        }

        private void EnemyBulletContact(SKPhysicsContact contact)
        {
            contact.BodyB.Node.RemoveFromParent();
            contact.BodyA.Node.RemoveFromParent();

            this.bullet = null;

            this.score = this.score + 100;

            this.scoreUI.Text = "Score: " + this.score.ToString();

            this.spawnNewEnemy();
        }

        private void EnemyBulletShieldContact(SKPhysicsContact contact)
        {
            contact.BodyB.Node.RemoveFromParent();
            contact.BodyA.Node.RemoveFromParent();
        }

        private void EnemyBulletPlayerContact(SKPhysicsContact contact)
        {
            if (contact.BodyA.CategoryBitMask == (uint)Types.EnemyBullet) contact.BodyA.Node.RemoveFromParent();
            if (contact.BodyB.CategoryBitMask == (uint)Types.EnemyBullet) contact.BodyB.Node.RemoveFromParent();

            this.lives--;

            if (this.lives < 0)
            {
                this.gameOver();
            }
            else
            {
                this.livesUI.Text = "Lives: " + this.lives.ToString();
            }
        }

        private void BulletShieldContact(SKPhysicsContact contact)
        {
            contact.BodyB.Node.RemoveFromParent();
            contact.BodyA.Node.RemoveFromParent();

            this.bullet = null;
        }

        private bool test(SKPhysicsContact contact, Types t1, Types t2)
        {
            return (contact.BodyA.CategoryBitMask == (uint)t1 && contact.BodyB.CategoryBitMask == (uint)t2) || (contact.BodyA.CategoryBitMask == (uint)t2 && contact.BodyB.CategoryBitMask == (uint)t1);
        }

        public void DidBeginContact(SKPhysicsContact contact)
        {
            if (this.test(contact, Types.Enemy, Types.Bullet))
            {
                this.EnemyBulletContact(contact);
            }

            if (this.test(contact, Types.EnemyBullet, Types.Shield))
            {
                this.EnemyBulletShieldContact(contact);
            }

            if (this.test(contact, Types.Bullet, Types.Shield))
            {
                this.BulletShieldContact(contact);
            }

            if (this.test(contact, Types.EnemyBullet, Types.Player))
            {
                this.EnemyBulletPlayerContact(contact);
            }
        }

        public override void DidSimulatePhysics() { }

        private void gameOver()
        {
            // TODO play a little animation, lock input while it plays?

            HighScores highscores = new HighScores();

            if (highscores.isNewHighScore(this.score))
            {
                SKTransition transition = SKTransition.CrossFadeWithDuration(2);

                Scene scene = SKNode.FromFile<SceneEnterNewHighScore>("SceneEnterNewHighScore");

                scene.data["score"] = this.score.ToString();

                scene.ScaleMode = SKSceneScaleMode.AspectFill;

                this.View.PresentScene(scene, transition);
            }
            else
            {
                this.PerformAction("buttonHighScores");
            }
        }
    }
}

