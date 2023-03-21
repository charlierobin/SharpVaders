using System;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public static class ExtensionsToRandom
    {
        public static bool NextBoolean(this Random r)
        {
            return r.Next(0, 2) == 0;
        }

        public static double Next(this Random r, double v1, double v2)
        {
            double range = v2 - v1;

            return v1 + (r.NextDouble() * range);
        }
    }
}

// TODO delete this

namespace SharpVaders
{
    public class CallLater
    {
        public static void afterDEAD(Action action, double delay)
        {
            System.Timers.Timer time = new System.Timers.Timer();

            time.Interval = delay;

            time.AutoReset = false;

            time.Elapsed += new System.Timers.ElapsedEventHandler((object sender, System.Timers.ElapsedEventArgs e) =>
            {
                action();
            });

            time.Start();
        }
    }
}




