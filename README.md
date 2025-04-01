# VertigoCase

This project is an example ui flow for a roulette game where the player clicks a button that spins a gun's chamber to earn rewards while trying to avoid hitting a bomb that removes all earned rewards and ends the game.

**APK file is in the release info**

## General Flow
- Game starts when the player clicks the start button in the middle of the screen which opens the roulette game ui and initializes it.
- The game consists of regular zones, safe zones and super zones. Safe zone and super zone has no bomb risk and the player can leave the game, earning the rewards they've won so far while on these zones.
- When the player clicks spin, the chamber spins rapidly and stops on a random reward.
- If the reward is not a bomb the player temporarily collects the reward.
- If the reward is the bomb, player is presented with the option to leave and lose all rewards or spend some currency to keep playing.
- Once on a safe zone the player can leave earning all the rewards so far or keep playing and try to reach the end zone.
- When the game ends one way or another the game pieces get cleared and roulette game ui closes.
- Player can play again by clicking the start button.

## Content Summary
* To achieve a somewhat realistic flow, the project utilizes **Service Locator Pattern**.
* The Service Locator holds references to Manager classes such as UIManager, EventManager, ObjectPoolManager etc.
* The SceneInstaller class works with InitializeBeforeSceneLoad to get the managers up and running.
* The roulette game ui is separated into four main parts as following:
   - Zone progress bar; which is at the top of the screen. It shows current progress of the game.
   - Upcoming zone bar; which is at the top right side of the screen. It shows the upcoming safe zones.
   - Temporary reward bar; which is at the left side of the screen, showing the rewards collected so far.
   - Roulette spinner; which is at the center of the screen and handles the spinning animation and reward selection of the gun chamber.
* These pieces are all initialized and monitored by RouletteGameController which holds references for each of these items and oversees the general progression.
* There are also smaller more generalized classes that could be used else where on the UI controlled by these pieces such as the WheelOfFortuneController, RadialLayoutGroup, CenteralizedScrollRect etc.
* There are also a collection of sprite atlasses (2k resolution) and seperated canvases for performance optimization.

### Below Figma file contains my scribblings before starting the project to understand the general flow and have a rough idea of the requirements and architecture
https://www.figma.com/board/weeU5XHxucsJd0gO460Kam/Vertigo-Case-Notes?node-id=0-1&t=IOAqo8wFJMDINtzC-1
### Trello used for progress tracking
https://trello.com/invite/b/67e2d112605701e5272cc892/ATTIf8fb2475128866b0af82e3fdd34171edAF61FCBB/vertigo-case

### Code Structure
![](https://github.com/Trasqual/VertigoCase/blob/main/Recordings/CodeStructure.png)

## GIFs of different resolutions

### 20x9
![](https://github.com/Trasqual/VertigoCase/blob/main/Recordings/movie_20x9.gif)

### 16x9
![](https://github.com/Trasqual/VertigoCase/blob/main/Recordings/movie_16x9.gif)

### 4x3
![](https://github.com/Trasqual/VertigoCase/blob/main/Recordings/movie_4x3.gif)

### Bomb Sequence
![](https://github.com/Trasqual/VertigoCase/blob/main/Recordings/Bomb_1920x1080.gif)

## What could be improved:
- The Roulette Game Controller shouldn't hold concrete references to it's elements, so they could be easily changed.
- It also doesn't fit in the MVP/MVC pattern.
- Could use addressables for better memory management and avoid using resources.
- Zone progress bar should be a pooled horizontal scroll, considering there could be 100+ zones
- Rewards and items use sort of a flyweight pattern but they could still be improved
- Currently each zone is using a fixed set of rewards. These could be randomized if necessary.
- Could add better particle effects and polish overall.
  
