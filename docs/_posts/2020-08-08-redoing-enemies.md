---
layout: post
title:  "Redoing Enemies"
date:   2020-08-08 21:06:00 -0700
categories: TTT Update
img: TurtleBright.png
---

## Today I've been working on remaking the base enemy

So today we're talking about redoing our base enemy. I later go on even further with this topic to include it for my base player as well. 

Now, everyone knows redoing stuff *sucks*. Trying to reread old code, always seems to leave me wondering what tiny elves were typing away at my computer while I slept. I certainly couldn't have written this crap. Either way, redoing old sections of your game is a great way to learn refactoring and to implement all of the new knowledge that you have collected. 

## Background on enemies and weapons

So in Tiny Turtle Tanks so far, both the enemy and the player are essentially the same. Same model, same health, same damage, same weapons. You can only shoot a single lazor forward. We have multiple shot points avaiable to effect shooting(not that I used them yet). All in all it seemed kind of boring and didn't really fit the idea for where I wanted my game to go. If you don't know this game was originally inspired by Wii Tanks as well as Astro Bears. In Wii Tanks there are several levels of enemies that can shoot, drop mines, and more. They can also use a variety of ammo. I wanted a game similar to Wii Tanks, but on a small 3D spherical planet, where you had the danger of shooting yourself in the back as bullets wrap around.

Thats also another thing I should mention. Bullets wrap around the planet, very similar to how the player mvoes, but with a constant driving force. Thankfully the surface of the planet isn't smooth or you would be in real danger.

## New enemies and weapons

Pulling from a few ideas I decided to create a new class system to represent the base player. This would determine basic factors like speed, jump force, and weapons. 

Each weapon would contain a clip, that you could fire at a certain rate. Of course each weapon needs to be reloaded, so their is a longer reload time for that as well. And finally of course Ammo.

The ammo is where the fun of these weapons is going to be had. It handles how fast the bullet goes, how much damage, if its explosive or not, and if it is allowed to bounce. This last idea came straight from Wii Tanks. What a legend of a game. My plan for ammo is to have a small variety that you as the player can cycle through by collecting pickups, as you face enemies of varieying weaponry. A few examples would be the standard *lazors*, rockets, mines, and bombs. 

My hope is that through a random selection of weapons combined with multi shot points, we should get some pretty interesting gameplay.

(Of course non of the bouncing or explosiveness has been coded in yet... But it's ready for when I get around to it!)

### Implementing it

To start with this I decided to go with Scriptable Objects. I've been playing with them a bit recently, and they seem like a really good way to create multiple iterations of the same class. Which is exactly what I want. Multiple different versions of the same enemy. I even realized at this point that most of the information in the Base Turtle would be as applicable to the player as it was the enemy. Time to refactor more!

After working up the design for the Base Player and all the sub classes I added the Scriptable Object as a public parameter in both the enemy and player class. At the beginning of the game they will pull all of the information out of the Scriptable Object and use it to control all the aspects of the turtle.

If I want to make a new enemy, it's easy. Just create a new Scriptable Object. Play with the health, add a new weapon. Maybe add a side weapon.
Maybe I need to tweak the player attributes. Thanks to Scriptable Objects, I can do just that **_during runtime, and have it all saved when I exit_**. Can't do that with prefabs.

## Thoughts

I've got a lot more to implement with this Base Class, but I really like the start that I have going. The weapons needed a massive rework, and I believe with this change I've made it a lot more modular for the future. Should be a lot easier to pump out some new enemies. Of course we would need new artwork for those enemies too...

<img src="../../../../../assets/images/Turtle.png" alt="Turtle" width="200">
<img src="../../../../../assets/images/TurtleDead.png" alt="TurtleDead" width="200">
<img src="../../../../../assets/images/TurtleHallow.png" alt="TurtleHallow" width="200">
<img src="../../../../../assets/images/TurtlePink.png" alt="TurtlePink" width="200">
<img src="../../../../../assets/images/TurtlePurple.png" alt="TurtlePurple" width="200">
<img src="../../../../../assets/images/TurtleRed.png" alt="TurtleRed" width="200">
<img src="../../../../../assets/images/TurtleWhite.png" alt="TurtleWhite" width="200">

[gh-p]:         https://pages.github.com/