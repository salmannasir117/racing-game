Start scene file: GameStartScene

How to play the game:
    - Click "Start" to start the game from the beginning or "Select Level" to start from a specific level. 
    - WASD for movement of player
    - Move the player from checkpoint to checkpoint using the path as guidance.
    - Do NOT skip any checkpoints or you will not be able to progress to the next level.
    - collect the cute cats at your own peril...
        - (they serve no function other than to be cute and distract the player)
    
    - Click "esc" to open the pause menu
        - You can then restart the level, exit the game, or return to the main menu

    - The game is over once you complete all three levels.

To observe the AI: 
    - One implementation is the cats on the beach and forest map. They are in an idle state until a player gets close enough to them. They will then follow the player.
    - There is an AI storm cloud on the volcano map that has a set patrol and a state in which it targets the player. It adds to the ambiance of the map.

Known problem areas: 
    - No known problem areas. 

Manifest of Assets/Scripts:
    - Sal
        - Contributions
            - Designed Forest Map (enhanced with particle systems, fog, obstacles)
            - Designed and integrated checkpoint system (enhanced with audio/visual feedback, checkered flag for finish line)
            - Designed and integrated Timer/Objective text prefab 
            - Designed basic speedup powerup functionality 
            - Modifications to Beach Map (skybox, obstacles, decorations)
            - Powerup placement on Forest and Beach Map
            - Performed integration tests & bug patching with player and checkpoint, ui, and ai systems
            - Managed merge conflicts and GitHub repo
            - Scripted and performed Alpha video
	        - Scripted and performed gameplay video
        - Assets implemented/found
            - Forest Map Assets
            - ForestMap.unity
            - Checkpoint.prefab 
	        - Timer UI.prefab
        - Scripts 
            - CheckpointCounter.cs
	        - Timer.cs
            - SpeedUpPowerUp.cs


    - Zamaria
        - Contributions
            - Designed the Beach Map
            - Designed the Start Menu, Pause Menu, Win/Lose states 
            - Developed Code for the associated elements and integrated it into all Level maps
            - Assessed various font assets to alter the text of the UI elements to better fit the game’s theme 
            - Updated UI Elements to reflect the Alpha and playtest feedback
        - Assets implemented/found
            - Beach Map Assets
            - JazzCreateBubble.tff (Font of Storm Surge title)
            - BeachScene.unity
            - GaneStartScene.unity
        - Scripts
            - GameManager.cs
            - PauseMenuToggle.cs 

    - Vonte
        - Contributions
            - Created Project Storytelling Narrative
            - Created Volcano Terrain Design/Map
            - Incorporated UV-based Material Design in Volcanic Terrain 
            - Developed AI-based tracking within Scene
            - Implemented Particle System Rain within Scene
            - Created response audio for Player Movement 
            - Curated individual Environment Sound playback for each level.
            - Developed decorative AI Cloud feature that roams using waypoints. 
            - Implement Lightning Strike onto volcanic terrain using programmatic C# timing
        - Assets implemented/found
            - Volcano Map Assets
            - VolcanoScene.unity
        - Scripts 
            - CloudAI.cs

    - Kevin
        - Contributions
            - Sourced models and animations for Player
            - Configured player input and character movement
            - Imported character model and animation, configured via Mecanim/Blend Trees
            - Configured kitty movement, including multi-state AI that waits for the player to near, then runs around, somewhat following the player through the level
            - Imported kitty model and animation, configured via Mecanim/Blend Trees
            - Imported meow sound effect, set to play when player nears any kitty
        - Assets implemented/found
            - Player Character
            - Kitty
            - Meow sound effect
        - Scripts 
            - PlayerMovement_Comp.cs
            - KittenController.cs
            - KittenSpawner.cs


    - Kayla
        - Contributions
            - Designed functionality and individual prefabs for the Short Burst, Slow Down, and Freeze Powerups
            - Made modifications to the tutorial screen to include powerup descriptions
            - Added various audios to be triggered upon the collection of any power-up item
            - Edited speed up powerup to make changes to the degree of speedup after feedback from playtests
            - Powerup placement on the Volcano map to place negative and positive powerups more strategically
        - Assets Implemented / Found
            - Short Burst Powerup.prefab
            - Speed Up Powerup.prefab
            - Slow Down Powerup.prefab
            - Freeze Powerup.prefab
            - Ice-Crack sound effect (Freeze Powerup)
            - Rapid-Wind sound effect (Short Burst Powerup)
            - Slow-Motion sound effect (Slow Down Powerup)
            - Speed-Up sound effect (Speed Up Powerup)
        Scripts
            - ShortBurstPowerup.cs
            - SlowDownPowerUp.cs
            - FreezePowerUp.cs

