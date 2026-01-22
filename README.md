# Unity Ball Collector – New Input System Demo

## Overview
This project is a small beginner Unity game created to demonstrate foundational knowledge of Unity’s **New Input System**, **Rigidbody-based movement**, and **basic physics interactions**. The game is intentionally simple and focuses on learning core mechanics rather than visual polish or advanced systems.

---

## Game Description
The player controls a ball that can move around a simple level while interacting with basic obstacles. The environment includes walls, ramps, and objects that require controlled movement and jumping to navigate. Coins are placed throughout the level and can be collected by the player.

The objective is to move through the environment and collect coins while interacting with physics-based obstacles.

---

## Learning Goals Demonstrated
This project demonstrates beginner-level understanding of:

- Unity New Input System  
  - Action Maps  
  - Vector2 composite movement (WASD)  
  - Button-based input actions (Jump)  

- Rigidbody Physics  
  - Force-based movement using `AddForce`  
  - Controlled acceleration and braking  
  - Physics interactions with walls and ramps  

- Colliders  
  - SphereCollider for the player  
  - BoxCollider for walls and obstacles  
  - Trigger colliders for collectible objects  

- Basic gameplay systems  
  - Player movement  
  - Jumping  
  - Coin collection  

---

## Controls
| Action | Input |
|------|------|
| Move | WASD |
| Jump | Space |

---

## Features
- Physics-based rolling movement  
- Walls and boundaries using colliders  
- Ramps and slopes that respect Rigidbody physics  
- Jumping implemented using impulse force  
- Collectible coins using trigger detection  

---

## Technical Notes
- Built using Unity 6  
- Uses Unity’s New Input System exclusively  
- Movement implemented with Rigidbody and force-based control  
- No legacy input API usage  
- No third-party assets or packages required  

---

## Scope and Intent
This project is not intended to be a complete game. Its purpose is to:

- Practice Unity fundamentals  
- Learn proper usage of the New Input System  
- Gain experience working with Rigidbody physics and colliders  
- Serve as a foundation for more complex Unity projects  

---

## Possible Future Improvements
- Score or UI display  
- Win condition when all coins are collected  
- Audio feedback for movement and collection  
- Camera follow and smoothing  

---

## Author
Created as a learning project to explore Unity fundamentals, physics-based movement, and input handling.

---

## Gifs

![GameStart](https://github.com/user-attachments/assets/9e846aff-9e22-4f82-8b82-a3abf366147c)

![CollectCoins](https://github.com/user-attachments/assets/e34dd44f-1a10-454d-968f-cecf3f1fd562)


![GameOver](https://github.com/user-attachments/assets/bf96462a-c670-4261-b13b-00ddf6ad8e48)


