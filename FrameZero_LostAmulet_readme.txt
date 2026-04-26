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
Press L. This will simulate level completion by: Completing the current level; Unlocking the next level; Returning to the WorldMap scene. Otherwise, whenever you complete a level and touch the final crystal, you move on to the next level automatically.

Lava Level (Fire Biome):
The Lava Level demonstrates three main parts of my work: the character system, the lava/platform level, and level progression integration.
Players control a third-person character whose movement is based on the camera direction. The character system includes smooth camera follow, movement-based rotation, jumping, and multi-jump functionality. I also worked on the character model setup, including the sword and shield, and connected basic animations for movement, jumping, pickup, and spell casting. The pickup action is bound to Q, and spell casting is bound to F.
The main gameplay mechanic is an “avoid the floor” platforming challenge. Players must move across elevated platforms, make careful jumps, and avoid touching the lava. If the player touches the lava, they are reset to a safe position.
The level environment includes sculpted volcanic terrain, a lava river, and terrain textures painted using Unity’s terrain tools. The level also includes basic audio elements such as jump sounds, background music, and lava/environment sounds.
For progression, the player must first find and pick up the sword by pressing Q near it. After getting the sword, the player can complete the level by reaching the final platform and touching the crystal. This crystal interaction connects the lava level to the next part of the game.

Earth Level
The objective of the Earth level is to avoid thorn bushes and root enemies while collecting wood in order to craft a shield. Movement and physics calculations are processed in FixedUpdate, ensuring consistent speed and gravity. The Earth Level includes audio elements in interactions with the environment such as a sound effect which indicates that a player has collected a wood item and a sound effect when the shield is crafted.

Water Level
The Water Level focuses on interaction systems, NPC logic, and environmental gameplay mechanics. It has a Day–Night Time System The level includes a time constraint mechanic, where players must complete tasks before night falls. A timer is displayed in the upper-right corner of the screen. If night falls before the objective is completed, the game transitions to a Game Over screen, where the player can: Restart the level; Return to the World Map; NPC Interaction System. The level includes an NPC (Wizard) who assigns tasks to the player. The NPC demonstrates: Patrol behavior; Idle state when the player approaches; Dialogue-based task assignment; Item Collection System. Players must collect items from the environment to complete the assigned task. Example task:Collect three fish from the pond; Fish can be collected by walking into them. Inventory System: The collected items are displayed in the inventory UI located in the upper-left corner. Once the player brings the required items back to the NPC: The NPC removes the items from the inventory; A potion item is granted. The water crystal spawns at the player spawn point.Collecting the crystal completes the level.

Boss Level
This is the final boss level where the 

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
My primary contributions to the project were centered around three core areas: (1) character system implementation (including weapon integration), (2) lava level design and gameplay mechanics, and (3) level-to-level progression and integration.

For the character system, I was responsible for implementing a complete third-person player controller using Unity’s Input System, enabling smooth camera-relative movement, directional rotation, and responsive jump mechanics, including multi-jump functionality. I extended this system to support interaction mechanics such as a pickup system (bound to Q) and a spell-casting system (bound to F). I also handled the integration of the character model, including configuring a humanoid rig and building an Animator Controller that supports walking, jumping, interaction, and spell-casting states. A key part of this work was ensuring proper equipment handling, where the character maintains a consistent default state with a sword equipped in-hand and a shield positioned on the back, and temporarily transitions out of this state during interactions before returning seamlessly. I refined animation blending, resolved directional movement inconsistencies (such as sideways motion issues), and synchronized animations with gameplay inputs. Additionally, I integrated action-based audio cues for jumping, pickups, and spell casting. More so, I added several configuration for each level with different states so that we can use the same character model except with the constraints of that specific level.

For the lava level, I designed and implemented a complete 3D environment built around an “avoid the floor” mechanic, requiring players to traverse elevated platforms and avoid falling into lava. This included developing the core gameplay logic for lava interactions (player reset on contact), traversal challenges, and win conditions. On the environment side, I created a volcanic terrain featuring a lava river originating from an elevated structure, and structured the level layout to support precise platforming. I applied and configured lava-based materials from the Unity Asset Store, resolving shader and rendering issues, and used terrain painting tools to achieve a cohesive visual style. I also implemented a third-person camera system to maintain a stable gameplay perspective and integrated ambient audio and spatial sound effects to enhance immersion. Additionally, I built interactive elements such as a sword pickup system with proximity-based UI prompts and trigger detection, and debugged collider and physics issues to ensure reliable interactions.

For level integration and progression, I implemented the logic that connects gameplay elements across the level, including interaction-driven progression (e.g., crystal triggers), state transitions, and end-condition handling. This involved ensuring that completing objectives within the lava level correctly advances the game state, while maintaining consistency between gameplay systems, UI feedback, and player state. I also contributed to the initial main menu UI setup using Unity’s Canvas system, helping establish the overall flow of the game.

Overall, my work focused on delivering a cohesive gameplay experience by tightly integrating the character system (with weapon handling), the lava-based level design, and the progression logic between game states, ensuring that mechanics, visuals, audio, and interactions functioned seamlessly together.

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
Character
Blink
RPGCrystals
NPCSounds
Sword
Shield
Animator


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
LavaLevelManager
SwordPickup
LevelCrystal
LavaEndTrigger
EarthCrystalScript
MonsterHealth(Edits for the ending)
CrystalPickup(Edits for the queuing)
GameProgress(Edits for queuing)

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
