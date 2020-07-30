---
layout: post
title:  "My First Post"
date:   2020-07-29 17:15:22 -0700
categories: jekyll update
---

## Welcome to my first post! 

Here I'm going to talk about my journey through the land of game development, specifically for my game <u>Tiny Turtle Tanks</u>. In making this blog, I hope to pass along some of the knowledge that I've learned and document the process of the making of this game. (Mostly so I don't forget it.)

I also want to share tips and tricks to all of the different 'islands' that lie around game dev land. There is so much to creating a game, besides just making it. For example making music, marketing, and even creating this here website. 

## Steps for creating this devblog
First a little info. This website is created using Jekyll for layouts, and hosted using Github Pages. I decided to use this path since I wanted to fast way to create new blog posts, and I wanted to host it for free. Besides a few painful installation moments, GH-Pages and Jekyll work extremely well together and matched all my requirements, so here we go.

### Resources
1. [GitHub Pages][gh-p]
2. [Jekyll][jekyll-link]
3. [Installation Video][install-vid]

### Creating a new site
1. The first thing I had to do was set up the environment where I would store all of the files for this site. This site it going to be used primarily for my game and my game alone. And since I already had a repository setup on GitHub for it, I decided just to continue using that. 
2. If you go under your repo, to **Settings**
3. Under **Settings** find the GitHub Pages section
    - You can select a source to determine where your files should be stored on your repo. 
        - Master branch for when your entire repo is the site
        - /docs folder for when you want to seperate your site from your other info (My method)
        - Alt branch for complete code seperation
    - Don't change the theme here for now (this button is for the basic GH-Pages users)
4. After a link should pop up with your freely hosted site. (Ex: [https://joesobo.github.io/TinyTurtleTanks/](https://joesobo.github.io/TinyTurtleTanks/))
    - Currently the site is empty, however
5. You can of course put normal HTML and CSS in the /docs folder and treat it like any normal website
6. To make it easier on myself, though we will use Jekyll
    - Go through their install process, installing Ruby, gems, and bundle as you go
7. Once install navigate to your /docs folder in the terminal
8. Run this command to instantiate jekyll:  
``jekyll new .``
9. This should populate your /docs folder with all of the nessesary info to start your site!
10. Comment out ``gem "jekyll", "~> 4.1.1"``
11. Uncomment ``gem "github-pages", group: :jekyll_plugins`` to active GH-Pages ability to build your site
12. You can push the new code up to your repo to see it on your GH-Pages (This process can take a few minutes)
13. Alternitively for faster coding iterations, you can host the site locally using:  
``bundle exec jekyll serve --watch``
    - Navigate to localhost:4000/{repo}/ 
    - (Ex: localhost:4000/TinyTurtleTanks/)
14. Congrats! Go to your URL and see your working site now. (It should just be the default Jekyll homepage)


### Overview of the file structure:
1. _config.yml - A configuration file used for storing basic information used in templates
    - Requires link to base url - Ex: /TinyTurtleTanks
    - Requires link to url - Ex: https://joesobo.github.io
2. _layouts - Holds templates of base HTML pages for easy customization
3. _posts - Markdown files used for quickly creating blog posts
4. _site - Holds the generated files that are actually rendered
    - Don't edit these! They are only for the server.
    - If this folder isn't showing try running:  
    ``jekyll build``
5. Gem file - Where you manage which versions you are supposed to run

### Editing the site
1. Navigate to your gems installation folder (Ex: C:\Ruby27-x64\lib\ruby\gems\2.7.0\gems)
2. You'll be able to see a list of folders with possible themes. I chose architect (jekyll-theme-architect-0.1.1)
3. In _config.yml change the theme to the proper name
    - theme: jekyll-theme-architect
4. In the gem file comment out:  
``gem "minima", "~> 2.5"``
5. Replace the commented out line with your chosen theme and its version:  
``gem "jekyll-theme-architect", "~> 0.1.1"``
6. Most of the themes are old and require a little bit of editing, so copy the entire contents of the theme from the /gems folder and paste it into your /docs
    - This also helps since you need to upload the theme to GitHub in order to actually see it on your GH-Pages
    - Should contain a _layouts folder, _sass folder, and an assets folder as well as a README.md
7. Since the _layouts folder is outdated, I copied in the home, page, and post.html files from the minima folder (The default theme).
    - This helps give the layouts so the pages can actually be loaded
8. Within /assets/css you can find all of the styles used to format your site
    - For the `architect theme` the print.scss contains all of the styles
    - The style.scss file is for you to make custom edits and changes to make the theme your own
9. Now you have all the pieces you need to get started. Try playing around with style.scss file, design new layouts, and create posts quickly in markdown.
    

[gh-p]:         https://pages.github.com/
[jekyll-link]:  https://jekyllrb.com/
[install-vid]:  https://www.youtube.com/watch?v=EmSrQCDsMv4