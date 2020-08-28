Go to Tools->Screenshot to open the window. Settings are saved in the operating system's registry, on a per-project basis.

Preferences:
--------------------
In Unity 2019+, go to Edit->Preferences->Editor->Screenshot tab

In Unity <2019, go to Edit->Preferences->Screenshot tab

Here you can configure the filename- and date format for screenshots.

Cinemachine:
---------------------
If your main camera has a CinemachineBrain component attached to it, the camera cannot be temporarly moved for a scene-view screenshot.

Open the ScreenshotTool.cs file and uncomment the first line (#define CINEMACHINE). 
This will temporarily disable the CinemachineBrain component when capturing.