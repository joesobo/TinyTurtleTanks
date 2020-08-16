---
layout: post
title:  "Weekly Update #1"
date:   2020-08-16 13:52:00 -0700
categories: TTT Update
img: Fly2.png
---

## Changing up the flow

So I've been thinking about this devlog that I've begun and the effort that must go into maintaining it. Obviously it's not that hard to write out a couple of short paragraphs, although I despise writing. However, I am really enjoying the process of describing exactly what I did and how. 

So my thought for this devlog, is to try to give it a little structure. My goal will be to upload a post once a week on Sundays. This gives me the perfect opportunity to get plenty of work done during the week, then take a free day to create a writeup. Also it should prevent me from getting too burned out on writing. Ugh...

## Speaking of structure

Since I've become a developer, I've started to develop a small arsenol of tools and programs that I like to use under various situations. Some are just my favorite IDE. (Visual Studio Code extensions for the win!) Others are tools like Git and Github. Which I've talked about a little in the past. It's what I use for all my projects, including this game and this blog.

Now I know to some Git might not seem like a really *advanced* tool, but it is amazingly suprising how many people are completely ignorant to using a VCS. Version Control System. Now I won't get into it too much because I'm worried I'll start ranting, but if you've never heard or used Git, please go check it out now. You have no idea of the amount of pain and all of the different tools that come available with Git. It's very worth it, just use it.

Back to the topic at hand. Another big tool that I regularly use is Trello. It happens to be a great resource for organizing and planning out big projects. If you want to write a Hello World, you can probably fit the scope in your head. But try to develop a multi-month video game with art, music, code, and more? Tack on the possibility of working in a team and you have a recipe for disaster without some form of guide. 

So if I were to sum up my crazy ramblings I'd recommend three things.

1. Check out an IDE for writing your code. It make's it 10x easier. I'd presonally recommend Visual Studio Code just for the vast library of community extensions. 
2. Checkout an task manager like Trello. Git also has a task manager to a certain extent with issues.
3. Please for the love of god use a damn VCS. It doesn't have to be Git, but it should.

## Meat and Potatoes

Now for the real shit you guys have all been waiting for. The weekly update log.

Disclaimer: It's currently over 100 degrees here so progress has been slow. (Not that its ever not slow)

As I said in the last post, I've been working on revamping the weapons system for the game. It was a little boring in the beginning with just a single method of attack, and don't even get me started on the enemies. 

<img src="../../../../../assets/images/Explosion.png" alt="Explosion" width="400">

I've now upgraded the system, so you the player have a weapon and an alternate weapon by default. Initially this will be your lazer and the ability to drop mines. The enemy is structured on the same system so I can easily drag and drop out Serialized Objects representing the different weapons. Basically what this means is that I can very quickly modify and create new enemies with a variety of weapons and number of weapons! Very cool.

Modify my old pickup code for player effects, and now weapons will drop from crates. This means you can switch out weapons in game. However only for a limited amount of time. Giving the player unlimited rockets might be a little... destructive

Of course once I got the player weapons into the game I started to get confused. Every time you switch weapons you had no idea what you layout was, or how long the timer lasted until you went back to your normal weapons. The only solution I could think to this issue was the add in UI. Not my favorite thing in the world trust me. Originally I wanted to have as few UI on screen as possible. Even the healthbar was built to float above the players head. However, at this point it really didn't seem worth the effort to fight it, so in it goes.

<img src="../../../../../assets/images/WeaponUI.png" alt="Weapon UI">

Trust me. I already now. It's butt ass ugly. Don't worry, everything you see here is (hopefully) placeholder. 

Side note: I also got to realize how shit my pixel art is. Fantastic! Another skill to start working on. 
In serious though, I have always loved pixel, and voxel art. I would seriously loved to make a 2D pixel art game as my next project. I've already got so many great ideas, it's killing me not to work on them.

Finally just as a little fun aside in the game, I made it so all explosions effect the environment obstacles. Go blow up some trees and flowers.

**Warning: Explosions may vary. Use with caution. If sent to lower orbit, good luck.**

<img src="../../../../../assets/images/Fly2.png" alt="Flying Turtle" width="400">

[gh-p]:         https://pages.github.com/