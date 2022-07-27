# Gameboard SDK Changelog

## 1.0.11

- serialization changes for event args

## 1.0.10

- add dice template assets for customizing companion dice
- added support for changing the companion dice selector options

## 1.0.9

- attempt to resolve unity package manager dependencies for com.unity.nuget.newtonsoft-json

## 1.0.8

- removed the Newtonsoft JSON dll's and replaced it with the unity package com.unity.nuget.newtonsoft-json version 3.0.1
- added some guidance for asset aspect ratios for loading custom companion app assets

## 1.0.7

- added EngagementController and supporting examples for reporting session and ranking metrics
- added rankings to send data to be used in future website reports for rankings/leaderboards

## 1.0.6

- added asset store tools unity asset
- fixed issues outlined by asset store tools package validator

## 1.0.5

- add support for more granular and customizable dice roll events via the DiceGroup object
- fixed dice tint color format to resolve the specified color not appearing on the companion app

## 1.0.4

- add top label property and supporting methods for adding a persistent label to the companion screen via DeviceEventController.cs -> SetTopLabel

## 1.0.3

- add support for setting button texture via a loaded assetId

## 1.0.2

- added some more error messages in controller examples response text
- check for companion user type when deleting and loading assets
- bug fix - only attempt to load assets if a user presence is a companion type.
- add support for retrieving linked gameboard account email address

## 1.0.1

- add companion card highlighting functionality
- add support for changing existing card assets
- add ability to set dice background
- add functionality for changing the companion template assets including the card navigation buttons, background, and card template type
- add functionality for toggling the dice selector visibility

## 1.0.0

Gameboard now accessible using `GetComponent<Gameboard>`

### Gameboard Menu Item

- Menu item called Gameboard is added to Unity when the SDK is added to the project.
- The Gameboard Menu contains two sub items: Add SDK, and Documentation
- Clicking on “Add SDK” will add an instance of the Gameboard prefab to the scene.
- Clicking on “Documentation” will open a web browser to take the user to our official developer docs

### Drawer Controller

- DrawerController is now integrated in the SDK
- Gameboard prefab is updated to have a DrawerController

### Companion Button Controller

- CompanionButtonController is integrated in the SDK
- Gameboard prefab is updated to have a CompanionButtonController

### Dice Roll Controller

- DiceController is integrated in the SDK
- Gameboard prefab is updated to have a DiceController

### Card Controller

- CardController is integrated in the SDK
- Gameboard prefab is updated to have a CardController

### Asset Controller

- AssetController is integrated in the SDK
- Gameboard prefab is updated to have a AssetController

### Device Event Controller

- DeviceEventController is integrated in the SDK
- Gameboard prefab is updated to have a DeviceEventController

### Create Prefab for Gameboard object Tagged with Gameboard

- SDK now includes a prefab called Gameboard with the tag “Gameboard” that creators can drag and drop into their games for quick integration.
- This object will expose all the controllers for the different features as they become available and be the go-to place for Creators

Some of the more interesting improvements are the creation of a single Gameboard prefab that Creators can use as the main point of access for all of our functions and the creations of the Gameboard Menu Item.

As of this version when Creators include our SDK a new option will become available named "Gameboard" in the top menu which will allow them to quickly add our SDK into their project's bootstrap scene. Our Gameboard object will then remain alive throughout their entire game and accessible at any time with the tag "Gameboard"

This first release adds a lot of stability improvements and exposes our functions via controllers on the Gameboard object which will be the way we support features moving forward

## 0.0.1067

- First Package Manager build.
