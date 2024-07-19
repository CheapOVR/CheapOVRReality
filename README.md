[README НА РУССКОМ](./READMERUS.md)

# Hey! 
## This is my AR project, working only on a Android smartphone. 
(and VR glasses)

It was written in Unity, by one schoolboy, so I recommend you to using my guide.

## If you want to just use it...
Select the "Releases" menu on the right side of your screen and download APK file from last release. Then install it on your phone and run. Interface is in russian now, so, maybe you need a translator ;)

## If you want to compile this project...

# Doesn't work to compile for now. Should be updated in the 2-3 days. Unitypackage, that you can import in your project can be found at releases page. I don't sure that can work.
# Else, you can set up your unity, and just wait...

!!USE UNITY 2022.3.35f1, INSTALLED WITH ANDROID BUILDING TOOLS!!
Import project to Unity, by oppening it from Unity Hub, then connect your phone with USB cable, and turn USB debug mode on it. Allow installing apps from ADB.

Set Unity config like this:

### Project Settings
### Player
#### Resolution and Presentation
![image](https://github.com/ZernovTechno/AR/assets/90546939/a37b0eda-85c2-4c09-a83c-4e5bcf3da646)

#### Other Settings
![image](https://github.com/ZernovTechno/AR/assets/90546939/6ccac38f-c521-406d-8782-dbe65974547b)

#### Publishing Settings
![image](https://github.com/ZernovTechno/AR/assets/90546939/07f3d81a-a2b9-4af5-9bde-126a721199a9)

And then open scene in explorer in the bottom of Unity. Scene located in Assets->Samples->MediaPipe Unity Plugin->0.14.4->Official Solutions->Scenes->Hand Tracking->Hand Tracking.unity

Then click File->Build Settings->Add Open Scenes, check added scene.

Now you can build it and run on a phone. Check your phone connection, and click "Build And Run" in Build Settings/File menu.

## Congrats!

