# Smelting Time

### Game Title: "Smelting Time"
### Team: "Echipa Team"
### Made for the FIICode 2026 Qualifier Game Jam and updated for the FIICode 2026 Game Dev Finals
### Game Trailer: `https://youtu.be/lnJNCoZv2OQ`
### V1.2.0 trailer: `https://youtu.be/vYz5v9kcMq4`
### Build Download: `github.com/tonealexandru236/SmeltingTime-Build`
### Project Download: `github.com/tonealexandru236/SmeltingTime`

### Logo:
![Logo](Assets/CreativeAssets/Sprites/KindaLogo.png)

<br>
<br>
<br>

# Credits

### Developers

1. Tudose Dragos - Game Progammer, Designer and Artist. Discord: `dragoss_`
2. Tone Alexandru - Game Progammer, Designer, Artist and Sound Engineer. Discord: `theman360`
3. Morariu Tudor - Sound Designer, Composer and Tester. Discord: `morariut`

### Other resources

1. Hosting & Version Control: Github
2. Music: GarageBand + BandLab
3. SFX: Freesound + Audacity
4. Sprites: Aseprite

<br>
<br>
<br>

# V1.2.0 Update - FIICode 2026 Game Dev Finals

The Final challenge was to implement a "a cooperative gameplay mechanic where the two players do not perceive or interact with the world in the same way.". Our idea was to gatekeep the previously universal workstation mechanics.

Instead of the two players having access to all stations, now they are split into two classes (or Jobs). A "Crafter" and a "Blacksmith/Smelter". The Crafter can use the Crafting Table (unlocked in Tutorial) and the Enchanting Table (unlocked on Level 4). The Blacksmith can use the Furnace (unlocked on Level 1) and the Smithing Table (unlocked on Level 7).

This encourages the two players to work together compared to the previously slightly flawed system. To compensate, all levels were made much easier. Other additions this update include:

1. Completely reworked levels and 2 more levels.
2. A new tier of armor, Netherite (very original), that is applied onto diamond gear with a smithing table by a blacksmith.
3. Enchanting. The crafter can insert tools, weapons, armor and golden apples to "enchant" them by holding the action button up to 3 levels. Customers will require different levels of enchants.
4. Night time. Every other levels is at night time, where the new fireflies and torches prosper.
5. Updated visuals, updated tutorial, new transitions between scenes, new settings, UI updates (new buttons, timer animations, etc.) + QOL (such as navigation with 'esc' or confirm to exit).
6. Brand new track, "It's Smelting Time!". Updated Title track.
7. Endless mode, a rare chance to snow, Patch Notes tab, Weather makes customers leave earlier.

+ Bug fixes :)

# The game itself

Disclaimer: The following are refering to build up to V1.2.0. Any further major updates might break or change any of the patterns, info and facts mentioned.

The game was developed in `Unity 6.3 (6000.3.10f1)`. The Repo contains the Unity project itself. A Windows build is located in the `!WINDOWS_BUILD` folder. For the cleanest experience, you can just download the `.exe` file from this repo: `github.com/tonealexandru236/SmeltingTime-Build`.

### Sprites

We Used Aseprite to hand draw all sprites. We've had our fair share of inspirations and a lot of trial and error. Most if not all items have multiple sprites depending on the direction the player is holding them. They are located in ` Assets\CreativeAssets\Sprites `.

### Animations

Some animations were done with the built-in Unity Animation/Animator components, while others were hardcoded. (a notorious example being the walking animation). They are located in ` Assets\CreativeAssets\Animations `.

### Font

For the font, we used the Jersey10 Regular font, provided by Google Fonts. It's located in ` Assets\CreativeAssets\Font `.

### Scenes

We have a total of 12 scenes: Title screen, Credits, Patch Notes, Tutorial, Endless and one scene for each of the 7 levels. They are located in ` Assets\CreativeAssets\Scenes `.

### Sounds

As tracks, we have 2 tracks, both made by Morariu Tudor. "Preheating" is the Title track, while "It's Smelting Time!" (v1.2.0) is the main looping track in-game. The project also contains a 1 minute long ` .mp3 ` of rain sounds. As for other sounds, we have a bunch of ` .wav ` files, some provided by the ` freesound.org ` platform under Creative Commons 0, others manually edited or recorded with Audacity. They are located in ` Assets\CreativeAssets\Sounds `.

### Scripts

We have tens of scripts, each doing its own thing.  They are located in ` Assets\CreativeAssets\Scripts `. Below we explained what some of them do:

#### General
1. Player Movement - Basic movement yap yap
2. Item Script - The script attached to items (pickable items, like on the ground and stuff). Contains info about the sprites and id (tag)
3. Player Hand - Responsible for managing what the player has in hand / with what items/stations to interact + use animations).
4. Station Script - Main script for stations (creafting, trash, smelting etc...). Used to find the stations and to interact with them easier (for Player Hand).
5. Crafting Table - Responsible with interactions for crafting table + the logic for crafting.
6. FurnaceScript - Resplonsible with interactions with Furnace + smelting.
7. Conveyor Belt - Conveyor animation + instantiating / moving items on the conveyor.
8. Trash Can - A script that its just happy to be included (logic done somewhere else).
9. PlayersManager - Script that manages both players (responsible of action order).
10. ItemDatabase - Script responsible to match items ID with prefab (used in Crafting/Smelting).
11. CustomerOrderLine - Manages all of the customers logic.
12. GameManager - Manages the timer and counts served customers.
13. Customer Variation - Applies random colors to customers.
14. Weather - Applies random weather (rain) to spice up gameplay.


#### UI

1. FPS - Counts fps and displays fps; In build, capped at monitor refresh rate.
2. ItemSplashes - Shows splashes with the current item that each player is holding.
3. Pause - Pauses the game if you press `esc`.
4. Exit - Exits the game
5. OpenScene - Opens specified scene
6. SetColor - sets the player's color to the selected one
7. ButtonBehaviour - Uses the DOTWEEN module to make buttons nicely bounce on hover
8. Version - Displays the current version
9. TitleScreenUI - manages Title screen UI (wow)
10. DestroySetting - Destroys stuff in the scene if a setting turned them off
11. BackgroundAnim - manages the cycling background in the Title Screen / Credits.

#### Audio

1. Audio Manager - Manages audio by creating a set amount of Audio Sources (to not abuse Instantiate and tank performace) and assigns/plays sounds dinamically to boost performance (or so it's intended)