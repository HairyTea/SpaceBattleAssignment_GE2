# SpaceBattleAssignment_GE2
A lego space battle simulation created in Unity

# Initial Plan:
The plan is to create a unique lego space battle that involves a story. Adding in multiple behaviours such as Path follow, Seek, Pursue and having the ability to control the
ships using keyboard & controller.

I decided to go with a lego theme as I don't watch a lot of sci-fi movies/shows but loved lego as kid.
I decided to go with a Good vs Evil theme and have multiple camera's set up, each with thier own ships with different behaviours, so Cam 1 would be path follow, Cam 2 would be Seek, etc.....
I've also wanted to add a gigantic end sequence where it is an all out battle against good and evil, with a giant overlord looking over the battlefield.

# Scripts Created & Used:
- [x] Animation Controller - This controls the animations used by the lego man. Using the keycodes ("Q,W,E,R"), each one set to a bool that is controlled by the      animator to perform different expression animations when prompted. 

- [x] Bullet - This script tells the bullet prefab what to do when hitting an object or being fired, when it hits either a ship tagged good or a ship tagged bad, this scripts tells it what to destory.

- [x] Camera Controller - This controlls the cameras for each ship using the numpad from (1-5). For example, pressing 2 will turn off all cameras except for Cam2 and then pressing 3 will set active Cam3 and set active Cam2 to false. This script also resets the scene by pressing the keycode ("X") and triggers the easter egg on Cam5 with the keycode ("Space").

- [x] Lego Path - This scripts draws a path for the lego ship to follow using Vector3 as a waypoint, adding them to a list and having a ship follow this looped waypoint list.

- [x] Lego PathFollow - This script is mainly what we learned in the last few weeks put together by using Arrive, Seek and a PathFollow behaviour then calculating the force to help the ships in the scene fly around.

- [x] Lego Ship Controller - This scripts allows you to take over the ship and control them using the keycodes ("W,A,S,D"). This player steering works on the horizontal and vertical axis.

- [x] Lego Ship Health - This give the ship health using an integer to track the ships health. If the health reaches 0 by an enemy bullet colliding with the ship itself, the ship gets destroyed, playing particle effects and sounds in it's death position.

- [x] Lego Ship Shoot - This script instantiates 2 bullet prefabs each coming from the 2 guns on the ship and adds force to it's rigidbody so it moves in the direction it is pointing. If an enemy ship comes into close contact with the ship that this script is connected to then this script will start instantiating bullets to fire at the enemey ship. If the instantiated bullets don't hit anything after a few seconds of instantiation, then they will be destroyed.

- [x] Rotation - This script is used for debris and the ending easter egg and rotates the objects on thier transform.Rotate. This script also uses bools such as ("PlusZ, MinusZ") and when true will rotate a desired direction needed using moveSpeed with Time.deltaTime.

- [x] BattleShip - This script adds a patrol behavior aswell as a seek behaviour to help with the ending battle. When there is not an enemy near a ship they will continue to patrol, but when getting into contact with an enemy ship they will start to seek and persue the enemy ship eventually trying to destory it, which it will then go back into seek/patrol. 

# Final Video:
[GE2022 Lego Space Battle Video - Space:2420 (A Lego Space Adventure)](https://youtu.be/F9w7EzKUXTo "GE2022 Lego Space Battle Video-Space:2420 (A Lego Space Adventure)")

# Video StoryBoard:
![image](https://user-images.githubusercontent.com/58917936/156204979-d3bf9f4f-990f-488e-9727-4082062d3b56.png)

# References:
-https://www.myinstants.com/instant/lego-breaking-57805/

-https://www.turbosquid.com/3d-models/free-lego-space-3d-model/477885

-https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-particle-pack-127325

-https://assetstore.unity.com/packages/2d/characters/moody-skies-lego-microgame-add-ons-179859#content

-https://free3d.com/3d-model/lego-man-8986.html

-https://stock.adobe.com/uk/search?k=spaceship%20cockpit

-https://www.myinstants.com/instant/among-us-roundstart-17570/

-https://www.myinstants.com/instant/bikini-bottom-just-got-real/

-https://www.youtube.com/watch?v=ziFxpjRbfJc&ab_channel=GravitySound

-https://www.turbosquid.com/FullPreview/616773
