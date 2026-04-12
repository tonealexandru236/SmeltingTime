# Smelting Time

### Game Title: "Smelting Time"
### Team: "Echipa Team"
### Made for the Fiicode 2026 Qualifier Game Jam

### Logo:
![Logo](Assets/CreativeAssets/Sprites/KindaLogo.png)

<br>
<br>
<br>

# Credits

### Developers

1. Tudose Dragos - Game Progammer, Designer and Artist
2. Tone Alexandru - Game Progammer, Designer, Artist and Sound Engineer
3. Morariu Tudor - Sound Designer, Composer and Tester

### Other resources

1. Hosting & Version Control: Github
2. SFX: Freesound + Audacity
3. Sprites: Aseprite

<br>
<br>
<br>

# The game itself

### Sprites

We Used Aseprite to hand draw all sprites. We've had our fair share of inspirations and a lot of trial and error. Most if not all items have multiple sprites depending on the direction the player is holding them. They are located in ` Assets\CreativeAssets\Sprites `.

### Animations

Most animations were done with the built-in Unity Animation/Animator components (a notorious exception being the walking animation). They are located in ` Assets\CreativeAssets\Animations `.

### Font

For the font, we used the Jersey10 Regular font, provided by Google Fonts. It's located in ` Assets\CreativeAssets\Font `.

### Scenes

We have a total of 8 scenes: Title screen, Credits, Tutorial and one scene for each of the 5 levels. They're located in ` Assets\CreativeAssets\Scenes `.

### Sounds

As tracks, we have a Main Bass, recorded on-site by our Composer, Morariu Tudor, as well as a 1 minute long ` .mp3 ` of rain sounds. As sounds, we have a bunch of ` .wav ` files, some provided by the ` https://freesound.org/ ` platform under Creative Commons 0, others manually edited or recorded with Audacity.

# Scripts
1. Player Movement - Basic movement yap yap
2. FPS - Counts fps and displays it in the TMP_TEXT where the script is attached; In build, capped at monitor refresh rate.
3. Item Script - The script attached to items (pickable items, like on the ground n stuff). Contains info about the sprites and id (tag)
4. Player Hand - Responsible for managing what the player has in hand / with what items/stations to interact + use animations
5. Station Script - Main script for stations (creafting, trash, smelting etc...). Used to find the stations and to interact with them easier (for Player Hand)
6. Crafting Table - Responsible with interactions for crafting table + the logic for crafting
7. Conveyor Belt - Conveyor animation + instantiating / moving items on the conveyor
8. Trash Can - A script that its just happy to be included 
9. Pause - Pauses the game if you press "esc"
10. PlayersManager - Script that manages both players (responsible of action order)
11. ItemDatabase - Script responsible to match items ID with prefab (used in Crafting/Smelting)
12. FurnaceScript - Resplonsible with interactions with Furnace + smelting