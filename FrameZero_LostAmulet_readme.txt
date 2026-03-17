Team Name: Frame Zero
Team Emails: zluo317@gatech.edu; sagarwal442@gatech.edu; kbhanderi6@gatech.edu; jli3307@gatech.edu;  nkibreab3@gatech.edu

Team Canvas Account Names: Ziwen Luo; Soham Agarwal, Krishna Bhanderi, Jiayi Li, Nahom Kibreab

For Alpha Demo:
Main Scenes: MainMenu, WorldMap, LavaLevel, WaterLevel, EarthLevel
1.  Our Demo Start Scene: MainMenu

2.  How to play and what parts of the level to observe technology requirements

Starting the game
The demo begins in the MainMenu scene, which contains the primary navigation interface for starting or exiting the game. To start the Game, Click Start Game on the main menu. This will load the WorldMap scene, which functions as the hub that connects all levels. When the player enters the WorldMap for the first time, a short intro narrative sequence appears. This introduction demonstrates the UI dialogue system, which includes: Character portrait display, a Dialogue text box, Next button to progress dialogue and Skip button to bypass the narrative with keyboard shortcuts (Space / Enter → advance dialogue; Escape → skip the intro). The narrative only plays on the first entry to the WorldMap. When the player returns from levels, the map will load directly without replaying the intro.

Navigating the World Map
The WorldMap scene serves as the central hub connecting all levels. Players control a map avatar using: W A S D. Movement is restricted to the map plane, allowing players to explore the world map and approach different level nodes. Each level node visually represents a biome: Fire (Lava Level), Water Level, Earth Level, Boss Level. Each node also displays its progression state: Grey → Locked; Yellow → Unlocked; Green → Completed. Players can approach an unlocked node and press: E to enter that level.
Locked nodes cannot be entered until the previous level is completed.

Level Progression System
The game follows a structured progression system: Fire → Earth → Water → Boss. Completing a level will: Mark the level as completed; Unlock the next level on the world map; Return the player to the WorldMap scene. For testing purposes during the Alpha demo, each level includes a temporary shortcut:
Press L. This will simulate level completion by: Completing the current level; Unlocking the next level; Returning to the WorldMap scene. 

Lava Level (Fire Biome)
The Lava Level demonstrates several core gameplay technologies: Players control a third-person character with movement relative to the camera. The system includes: Smooth third-person camera follow
; Character rotation based on movement direction; Jump mechanics including multi-jump capability; Platform Traversal Mechanics. The main gameplay mechanic of the level is an “avoid the floor” traversal challenge. Players must: Navigate elevated platforms; Perform precise jumps; Avoid falling into lava. If the player touches lava, the character is reset. The lava level also demonstrates environmental technologies such as: Sculpted volcanic terrain; Lava river environment and Terrain texturing using Unity terrain painting tools. The level includes several audio elements: Jump sound effects; Background ambient music; Environmental lava sounds. The level is completed when the player successfully reaches the final platform.

Earth Level
The objective of the Earth level is to avoid thorn bushes and root enemies while collecting wood in order to craft a shield. Movement and physics calculations are processed in FixedUpdate, ensuring consistent speed and gravity. The Earth Level includes audio elements in interactions with the environment such as a sound effect which indicates that a player has collected a wood item and a sound effect when the shield is crafted.

Water Level
The Water Level focuses on interaction systems, NPC logic, and environmental gameplay mechanics. It has a Day–Night Time System The level includes a time constraint mechanic, where players must complete tasks before night falls. A timer is displayed in the upper-right corner of the screen. If night falls before the objective is completed, the game transitions to a Game Over screen, where the player can: Restart the level; Return to the World Map; NPC Interaction System. The level includes an NPC (Wizard) who assigns tasks to the player. The NPC demonstrates: Patrol behavior; Idle state when the player approaches; Dialogue-based task assignment; Item Collection System. Players must collect items from the environment to complete the assigned task. Example task:Collect three fish from the pond; Fish can be collected by walking into them. Inventory System: The collected items are displayed in the inventory UI located in the upper-left corner. Once the player brings the required items back to the NPC: The NPC removes the items from the inventory; A potion item is granted. The water crystal spawns at the player spawn point.Collecting the crystal completes the level.

Boss Level
For the Alpha demo, the Boss level node is included in the WorldMap but mainly serves as a placeholder. It becomes accessible only after all three biome levels have been completed. 


3.  Known problem areas
WaterLevel: Hard to get out of the pond, terrain needs to be edited. The pond can be exited to the right of the ice extrusion currently.
Earth Level: Some visual clipping when player passes through hilly terrain.


4.  Manifest of which files authored by each teammate:

a) Detail who on the team did what

Ziwen: 
Worked on the MainMenu Scene and the World Map Scene. Created game buttons so that start game leads to the WorldMap scene and exit leads to quitting the game. Implemented the WorldMap scene with basic visuals and four level nodes: Fire; Earth;Water;Boss. Each node visually shows its state: Locked: Grey; Unlocked: Yellow; Completed: Green. Added a map Player with Movement control. The player can move on the world map using: W A S D. Movement is restricted to the map plane. Added a node interaction system which the user can enter a level by approaching an unlocked node and pressing E. Locked nodes cannot be entered. Added a Level Progression System: The map now automatically updates progression based on completed levels. Completed levels are marked visually on the map. Added an Intro Narrative (First Entry Only). When the game starts and the player enters the World Map for the first time, a short narrative intro appears. The intro narrative features a Dialogue panel with character portrait; Next / Skip buttons; and Keyboard shortcuts (Space / Enter → Next; Escape → Skip). Intro only plays the first time entering WorldMap. If the player returns from a level back to the map, the intro will not play again. For testing purposes, I also added a Simulate Level Completion feature. At each level scene, pressing the key L on the keyboard will simulate: Complete the current level; Unlock the next level and Return to WorldMap. 

Krishna:
Worked on Water Level Scene. Included day to night feature that creates a time constraint for the player to complete the level. Players can move through a scene using WASD or arrows. Terrain constrained the player to a set map. The start of the scene will present instructions and the player can press SPACE to exit. An NPC patrols a set area of the terrain until a player comes near, then it will idle. Players can interact with NPC in multiple ways. The NPC can give instructions, an item, or just say hello. Once a task is assigned by the NPC, the player must navigate the map to complete said tasks. The player will collect items, such as 3 fish from the pond, and bring them back to the NPC. The player can collect fish through walking into them head on. The NPC will then add a potion to the players inventory and remove the items needed for the potion. Once the potion is collected, the water crystal will appear at spawn point. The player can collect the crystal to complete the round. If night falls, the game will show a game over scene in which the player can choose to restart or go back to the main map menu. Pressing ESC will allow the player to pause the game and give the same options. A timer is shown in the upper right, an inventory list in the upper left, and dialogue with the NPC will show up at the bottom.

Soham: 
I was primarily responsible for designing and implementing the lava level in a 3D third-person environment, where the core gameplay revolves around an “avoid the floor” mechanic that requires players to navigate across elevated platforms, perform precise jumps, and reach the end goal without falling into lava. This involved developing the underlying game logic for player progression, lava interactions, and win conditions, including resetting the player upon contact with lava and triggering completion states. I implemented a full player controller system using Unity’s Input System, enabling smooth third-person movement relative to the camera, directional rotation, and responsive jump mechanics, including multi-jump functionality. In addition, I worked on integrating and configuring the character model, setting up a humanoid rig and Animator Controller to support walking and jump animations, and ensuring that these animations synchronized correctly with gameplay inputs. I also implemented a third-person camera system that maintains a consistent offset from the player and follows movement smoothly to provide a stable gameplay perspective. On the environment side, I created the lava-themed terrain by sculpting a volcanic landscape, designing a lava river that flows from an elevated, volcano-like structure, and structuring the level layout to support platform-based traversal. I applied and configured lava-based textures sourced from the Unity Asset Store, using terrain painting tools to achieve a cohesive visual style across the level. I further enhanced the atmosphere by integrating daylight and lighting settings to improve visibility and overall aesthetic. Additionally, I incorporated audio elements into the level, including jump sound effects tied to player actions, background music for ambient immersion, and spatial lava sound effects to enhance realism as the player moves through the environment. Beyond the level itself, I also contributed to setting up the initial main menu UI using Unity’s Canvas system, helping establish the basic flow of the game. Overall, my work focused on building a complete and cohesive gameplay experience by combining mechanics, animation, environment design, audio integration, and UI elements into a fully playable lava level.

Jiayi:
Worked on the shared Player System and character integration. Created a reusable Player setup with movement, camera follow, and interaction logic so the same player structure can be used across scenes. Added a character model as a child of the Player root object while keeping the root object responsible for gameplay logic such as movement, collision, interaction, and inventory. Implemented an equipment system with three attachment points: SwordSocket, ShieldSocket, and SpellSocket. Set up separate ground pickups for Sword, Shield, and Spell, so that when the player touches a pickup, the corresponding inventory state updates, the pickup disappears, and the matching item appears on the character. Also worked on testing and prototyping character animation integration, including attempts to connect walking animations and replace the placeholder T-pose model with an animated humanoid model for use in the actual game scenes.

Nahom:
Worked on the Earth Level scene. Added hilly terrain with trees and root enemies to chase the player. Implemented simple enemy AI, which only currently has one state. Implemented game logic so that the player can craft a shield when all wood items are collected. Incorporated audio elements that correspond with interactions with the environment, such as the pop sound effect for picking up an item.

b) Each asset implemented for each team member

Ziwen:
MainMenu Scene: 
Canvas - Panel - StartButton and ExitButton
WorldMap Scene: 
WorldMap
Canvas - Boss Icon and IntroPanel - CharacterImage; NameText; DialogueText; NextButton; SkipButton
MapPlayer
MapNodes - FireNode; EarthNode; WaterNode; BossNode
WorldMapManager
WorldMapIntroController
Materials for Node State Indication


Krishna:
InventoryCanvas
GameOverCanvas - RestartButton and ExitButton
GamePauseCanvas - RestartButton and ExitButton
InstructionCanvas
WinCanvas
Fishs
NPC (Wizard)
Player
TimeController
GameManager
Moon and Daylight
Terrain
Pond

Soham:
LavaMat
Music/
Prefabs/Platform
PlayerAnimator
CharacterModel
Shader/
InputControllerManager
InputController
GalaxyFire
Kevin Iglesais/Human Animations
NPC or Player/Effort Sounds
Input System_Actions

Jiayi: 
Animator
Art
Material 
Prefabs 
Scenes 
Scripts
TextMesh Pro

Nahom:
Earth Scene
RootEnemy
EarthLevelPlayerController
Wooden Shield
Grassy Terrain
Trees
Thorn bushes
Pop Sound effect (pick up item)
Craft sound effect


3). List of C# script files individually

Ziwen:
MenuManager.cs
MapPlayerController.cs
LevelNode.cs
LevelType.cs
WorldMapManager.cs
WorldMapIntroController.cs
LevelCompleteTester.cs
GameProgress.cs

Krishna:
GameOverMenu
GameQuitter
TimeController
InventoryManager
InstructionManager
NPCTextTrigger
CrystalPickup
FishPickup
FishSwim
PlayerMovement
CameraFollower
WizardAI

Soham:
Input System_Actions
PlayerController
CameraFollow
MenuManager(Initial)


Jiayi:
AbilityPickup
AbilityType
CameraFollow
InteractableObject
PlayerAbilityVisual
PlayerInteraction
PlayerInventory
PlayerMovement
SwordPickup

Nahom:
EarthLevelPlayerController
RootEnemy
