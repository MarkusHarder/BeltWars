# BeltWars

## Asset folder structure

Assets
---2D Space Kit
---Prefabs (contains nothing)
---Resources (basically all in-game used assets except scenes, scripts and sounds are stored here)
   ---Asteroids
   ---Backgrounds
   ---EmptyObjects (contains empty game objects which own global scripts)
   ---Planets
   ---Ships
   ---ShootAndDestroy
---Scenes
   ---Main (in game scene)
   ---Menu (menu scene)
   ---Test (scenes which you used to test your stuff)
---Scripts
   ---Events (right now only support ship event)
   ---Global (scrips which run global on empty game objects or scripts which are not attached to a object but called during runtime)
   ---Ships (Movement,...)
   ---ShootAndDestroy (Explosion, Shoot,...)
---Sounds
   ---Menu


## Rules before merging into develop branch

- be sure your project has the same folder structure inluding the same files
- create an own folder (name: your name) within the resource folder if you develop for the single device game mode (Tomy, Patrick, Daniel)
   - new or modified assets (each asset except scenes, scripts and sounds) are stored in that folder
- create a scene (name of the scene should contain your name) that contains your developed and running stuff (Tomy, Patrick, Daniel, Markus)
   - best thing would be if you use a copy of the MainScene (name it different from the MainScene!)
   - store the scene in Assets/Scenes/Test
- Store your scripts in a matching script folder (Assets/Scripts/...) if it's a new script or create a matching folder
