# SharpVaders
 
Learning some more about SpriteKit, Visual Studio and C#, all rolled into a simple Space Invaders type game.

There are still some rough edges to be sanded down: the final pixellations on the graphics, final label graphics, and the null reference crash that will pop up if you play it long enough because sprites are not being correctly removed from the game scene. (Basically, they are being removed during the physics contact callback, which as far as I can make out is wrong. Best practice seems to be create a list of items to be removed, add to it, then go through once everything else has been done, ie: after update, removing those items.)

https://user-images.githubusercontent.com/10506323/229731222-f9e971f0-4d5f-4a36-8d9b-fc7a7b020e0e.mp4

But it broadly encompasses everything I wanted to look at when I started out: buttons (SKSpriteNodes) with clicks and rollovers, buttons with simple keyboard navigation, entry and storing of high scores: the basics for a simple arcade-type game.
