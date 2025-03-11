Start scene file: GameStartScene

How to play the game:
    - Click "Start" to start the game from the beginning or "Select Level" to start from a specific level. 
    - WASD for movement of player
    - Move the player from checkpoint to checkpoint using the path as guidance.
    - Do NOT skip any checkpoints or you will not be able to progress to the next level.
    
    - Click "esc" to open the pause menu
        - You can then restart the level or exit the game

    - The game is over once you complete all three levels.

To observe the AI: 
    - This is implemented in the final level. There is an AI storm cloud that has a set patrol and a state in which it targets the player and attempts to obstruct their view with a lightning blast.

Known problem areas: 
    - In the final/volcano level, it is possible to fall out the map. We believe this to be a bug in our player movement system.

Manifest of Assets/Scripts:
    - Sal
        - Contributions
            - Designed Forest Map
            - Designed checkpoint system with plugins for ui system and player
            - Integrated checkpoint system into the levels
            - Performed integration tests with player and checkpoint, ui, and ai system
            - Performed bug patches on ui and ai system regarding project integration
            - Managed merge conflicts and GitHub repo
            - Scripted and performed Alpha video
        - Assets implemented/found
            - Forst Map Assets
            - ForestMap.unity
            - Checkpoint.prefab 
        - Scripts 
            - CheckpointCounter.cs

    - Zamaria
        - Contributions
            - Designed the Beach Map
            - Created UI elements (Start Menu, Pause Menu, Win/Lose States)
            - Developed Code for the associated elements and integrated it into all Level maps
        - Assets implemented/found
            - Beach Map Assets
            - BeachScene.unity
            - GaneStartScene.unity
        - Scripts
            - GameManager.cs
            - PauseMenuToggle.cs 

    - Vonte
        - Contributions
            - Created Project Storytelling Narrative
            - Created Volcano Terrain Design
            - Incorporated UV-based Material Design in Volcanic Terrain 
            - Developed AI-based tracking within Scene
            - Implemented Particle System Rain within Scene
        - Assets implemented/found
            - Volcano Map Assets
            - VolcanoScene.unity
        - Scripts 
            - CloudAI.cs

    - Kevin
        - Contributions
            - Sourced models and animations for Player
            - Scripted player control and movement
            - Configured Mecanim animations to player movement with blend trees
        - Assets implemented/found
            - Player Asset
        - Scripts 
            - PlayerMovement.cs

    - Kayla
        - Contributions
            - Sourced models for Beach & Forest maps
            - Sourced models for various powerup objects
            - Played around with powerup objects within a blank scene to work on “player” reactions to objects
            - Made an in-depth plan for the future implementation of powerup & negatively charged objects
        - Assets implemented/found
            - found asset packs for beach and forest scene
            - found asset packs for collectibles
            - SampleScene.unity (branch: kayla-collectibles)
            - ProjectSettings Assets
