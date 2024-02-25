---- Gara Rosales Gonzalez Zong Open Test ----

1- 3rd Party Assets used:

  A- Used the new URP package´s Garden Scene (included in repository)
  B- Used particle effects from AllIn1VFXToolkit (included in repository)
  C- used generated 8 bit SFX .WAV files

2- Requirements approach:

  - No VR set aviable to test, so game interaction happens with 1st person view controls with some features:
    - the white point is to interact with 3D objects in the scenario, once the white pointer is on a interactable object, it will appear the object´s description. Click to interact with the object
    - Mouse pointer to interact with the games UI
    - Camera can be moved freely while travelling from one place to another.
   

  - Game UI is a 3D World UI Canvas due to the VR Nature of the project
    - due to inventory´s nature (objects were used automatically), to open or close the weapons and instrument inventory, just hover with the mouse pointer weapons or instrument text to check aviable items in those inventories
    - points are shown but does not have any logic implemented (the requirements didn´t said anything about implementing that system).
    - a pickup notification can be shown
    - game UI can appear when required
    - Game UI is shown after interacting with an Interactable object.
    - when the Game UI is shown a button to go to the next place or to restart the scene is shown (the last one has been made to test all 3 box interactions quickly and shown after any box interaction).
   

  - Interactable objects can
    - add an item OR require an item to interact with
    - disappear or not when interacted
    - show particles when interacted
    - play a sound when interacted
    - trigger events when interacted
   

  - The way the player moves, due to efficiency, is by using Animator and playing animations when required. (It could be possible use Timelime too, but due to the test simplicity, via Animator was chosen).

3- Missing Requirement actions:
  - approach the item animation before pick it up was not included, due to object proximity it just picks it up.
