﻿using Gameboard.EventArgs;
using Gameboard.Objects;
using Gameboard.Objects.Dice;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Gameboard.Examples
{
    public class DiceControllerExample : MonoBehaviour
    {
        private UserPresenceController userPresenceController;
        private DiceRollController diceController;
        private AssetController assetController;
        private bool diceSelectorVisibility = true;
        public Texture2D CompanionBackground;
        public Texture2D ModifiedD6Texture;
        public Text Results;

        private int d6Count; // TODO: remove after functional testing

        private void Awake()
        {
            GameObject gameboardObject = GameObject.FindWithTag("Gameboard");
            Gameboard gameboard = gameboardObject.GetComponent<Gameboard>();
            gameboard.GameboardShutdownBegun += OnGameboardShutdown;

            diceController = gameboardObject.GetComponent<DiceRollController>();
            userPresenceController = gameboardObject.GetComponent<UserPresenceController>();
            assetController = gameboardObject.GetComponent<AssetController>();
        }

        private void Start()
        {
            GameboardLogging.Verbose("DiceControllerExample Start");
            diceController.OnDiceRolled += OnDiceRolled;
        }

        private void OnDestroy()
        {
            GameboardLogging.Verbose("DiceControllerExample OnDestroy"); 
            diceController.OnDiceRolled -= OnDiceRolled;
        }
        
        private void OnGameboardShutdown()
        {
            diceController.OnDiceRolled -= OnDiceRolled;
        }

        /// <summary>
        /// Handle the dice roll event generated by the companion user
        /// </summary>
        /// <param name="diceRolledEvent"></param>
        void OnDiceRolled(CompanionDiceRollEventArgs diceRolledEvent)
        {
            // Handle Dice Rolls
            GameboardLogging.Verbose($"received dice roll event: {diceRolledEvent}");
            Results.text = $"{diceRolledEvent}";
            GameboardLogging.Verbose(Results.text);
        }

        public async void SetDiceBackground()
        {
            // Get the user id of the first companion user from the UserPresenceController
            var userId = Utils.GetFirstCompanionUserId(userPresenceController);
            if (userId == string.Empty)
            {
                Results.text = "There are no companion users connected.";
                GameboardLogging.Error("There are no companion users connected.");
                return;
            }

            Results.text = $"Loading Background Asset...";

            // Create texture assets to load into the companion app
            var textureDelegate = new AssetController.AddAsset<CompanionTextureAsset, Texture2D>(assetController.CreateTextureAsset);
            var backgroundAsset = textureDelegate(CompanionBackground);

            if (backgroundAsset == null)
            {
                GameboardLogging.Error("Failed to create background asset.");
                Results.text = "Failed to create background asset.";
                return;
            }

            var loadAssetResponse = await backgroundAsset.LoadAssetToCompanion(userPresenceController, userId);
            Results.text += $"\nLoad background asset response: {loadAssetResponse}";

            // Set the background of the dice area on the companion for the first user
            var setBackgroundResponse = await diceController.SetDiceBackgroundAsset(userId, backgroundAsset.AssetGuid.ToString());
            Results.text += $"\nSet Dice Background response: {setBackgroundResponse}";
            return;
        }

        public async void ToggleDiceSelectorVisibility()
        {
            // Get the user id of the first companion user from the UserPresenceController
            var userId = Utils.GetFirstCompanionUserId(userPresenceController);
            if (userId == string.Empty)
            {
                Results.text = "There are no companion users connected.";
                GameboardLogging.Error("There are no companion users connected.");
                return;
            }

            // toggle the dice selector visibility on the companion for the first user
            diceSelectorVisibility = !diceSelectorVisibility;
            var response = await diceController.SetDiceSelectorVisibility(userId, diceSelectorVisibility);
            Results.text = $"Dice Selector Visibility set: {response}";
        }

        /// <summary>
        /// Ask the first companion user to roll dice with the specified inputs.
        /// </summary>
        /// <param name="dice"></param>
        /// <param name="notation"></param>
        /// <param name="overallModifier"></param>
        /// <returns></returns>
        public async Task RollDice(DieGroup[] dice, string notation, int overallModifier = 0)
        {
            var userId = Utils.GetFirstCompanionUserId(userPresenceController);
            if (userId == string.Empty)
            {
                Results.text = "There are no companion users connected.";
                return;
            }

            // Ask the first joined user to roll dice on their companion app.
            CompanionMessageResponseArgs responseArgs = await diceController.RollDice(userId, dice, overallModifier, notation);
            Results.text = responseArgs.wasSuccessful
                ? $"Asked companion user {responseArgs.ownerId} to roll."
                : $"Error: {responseArgs.errorResponse.Message}";
            GameboardLogging.Log(Results.text);
        }

        /// <summary>
        /// Ask the first joined user to roll a d20
        /// </summary>
        public async void RollD20()
        {
            await RollDice(new DieGroup[]
            {
                new DieGroup
                {
                    sides = 20,
                }
            }, "1d20");
        }

        /// <summary>
        /// Ask the first joined user to roll a clear d20
        /// </summary>
        public async void RollClearD20()
        {
            await RollDice(new DieGroup[]
            {
                new DieGroup
                {
                    sides = 20,
                    count = 1,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.clear)}"
                }
            }, "1d20");
        }

        /// <summary>
        /// Ask the first joined user to roll a lot of dice.
        /// </summary>
        public async void RollALotOfDice()
        {
            await RollDice(new DieGroup[]
            {
                new DieGroup
                {
                    sides = 20,
                    count = 2,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.cyan)}"
                },
                new DieGroup
                {
                    sides = 6,
                    count = 2,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.red)}"
                },
                new DieGroup
                {
                    sides = 4,
                    count = 1,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.blue)}"
                },
                new DieGroup
                {
                    sides = 10,
                    count = 1,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.gray)}"
                },
                new DieGroup
                {
                    sides = 12,
                    count = 1,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.magenta)}"
                },
                new DieGroup
                {
                    sides = 100,
                    count = 1,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.white)}"
                },
            }, "2d20+2d6+1d4+1d10+1d12+1d100");
        }

        /// <summary>
        /// Ask the first joined user to roll a modified d6
        /// </summary>
        public async void RollModifiedD6()
        {
            await RollDice(new DieGroup[]
            {
                new DieGroup
                {
                    sides = 6,
                    count = 1,
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.blue)}",
                }
            }, "1d6+1000", 1000);
        }

        /// <summary>
        /// Set the selectable dice for the first user's companion app
        /// </summary>
        public async void SetSelectableDice()
        {
            // Get the user id of the first companion user from the UserPresenceController
            var userId = Utils.GetFirstCompanionUserId(userPresenceController);
            if (userId == string.Empty)
            {
                Results.text = "There are no companion users connected.";
                GameboardLogging.Error("There are no companion users connected.");
                return;
            }

            Results.text = $"Loading modified d6 Asset...";

            // Create the modified d6 texture asset to load into the companion app
            //      Additional default dice texture templates are included in the Resources -> DiceTemplates folder.
            var textureDelegate = new AssetController.AddAsset<CompanionTextureAsset, Texture2D>(assetController.CreateTextureAsset);
            var modifiedD6 = textureDelegate(ModifiedD6Texture);

            if (modifiedD6 == null)
            {
                GameboardLogging.Error("Failed to create modified d6 asset.");
                Results.text = "Failed to create modified d6 asset.";
                return;
            }

            // Load the asset onto the companion for the first user id
            var loadAssetResponse = await modifiedD6.LoadAssetToCompanion(userPresenceController, userId);
            Results.text = $"Load Die asset response: {loadAssetResponse}";

            // Create a DiceGroup[] to send to the companion user for their dice selector choices
            // The companion app will only display these, so be sure to include all dice that the user can choose.
            DieGroup[] dice = new DieGroup[] {
                new DieGroup {
                    id = "d6-textured",
                    sides = 6,
                    label = "Modified d6",
                    textureId = modifiedD6.AssetGuid.ToString(),
                },

                new DieGroup {
                    id = "d6-textured-green",
                    sides = 6,
                    label = "Modified GREEN d6",
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.green)}",
                    textureId = modifiedD6.AssetGuid.ToString(),
                },

                new DieGroup {
                    id = "d6-standard",
                    sides = 6,
                    label = "d6",
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.cyan)}",
                },

                // You can omit the textureId to use the default texture if a standard number of sides is included.
                new DieGroup {
                    id = "d20",
                    sides = 20,
                    label = "Magenta d20",
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.magenta)}",
                },

                // You can omit the id as well if you do not care to change it in the future.
                new DieGroup {
                    sides = 10,
                    label = "Blue d10",
                    diceTintHexColor = $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.blue)}",
                }
            };
            
            // Set the selectable dice on the companion for the first user
            var response = await diceController.SetSelectableDice(userId, dice);
            Results.text = $"Dice Selector Visibility response: {response}";
        }

        /// <summary>
        /// Set the selectable dice to include a d6 with a modified die texture and request the first companion user to roll it
        /// increment number of dice each time the button is clicked.
        /// </summary>
        public async void SetSelectableDiceAndRollD6Incremented()
        {
            // Get the user id of the first companion user from the UserPresenceController
            var userId = Utils.GetFirstCompanionUserId(userPresenceController);
            if (userId == string.Empty)
            {
                Results.text = "There are no companion users connected.";
                GameboardLogging.Error("There are no companion users connected.");
                return;
            }

            Results.text = $"Loading modified d6 Asset...";

            // Create the modified d6 texture asset to load into the companion app
            //      Additional default dice texture templates are included in the Resources -> DiceTemplates folder.
            var textureDelegate = new AssetController.AddAsset<CompanionTextureAsset, Texture2D>(assetController.CreateTextureAsset);
            var modifiedD6 = textureDelegate(ModifiedD6Texture);

            if (modifiedD6 == null)
            {
                GameboardLogging.Error("Failed to create modified d6 asset.");
                Results.text = "Failed to create modified d6 asset.";
                return;
            }

            // Load the asset onto the companion for the first user id
            var loadAssetResponse = await modifiedD6.LoadAssetToCompanion(userPresenceController, userId);
            Results.text = $"Load Die asset response: {loadAssetResponse}";

            // Create a DiceGroup[] to send to the companion user for their dice selector choices
            // The companion app will only display these, so be sure to include all dice that the user can choose.
            DieGroup[] dice = new DieGroup[] {
                new DieGroup {
                    id = "d6-textured",
                    sides = 6,
                    label = "Modified d6",
                    textureId = modifiedD6.AssetGuid.ToString(),
                },
            };

            // Set the selectable dice on the companion for the first user
            var response = await diceController.SetSelectableDice(userId, dice);
            Results.text = $"Dice Selector Visibility response: {response}";

            // Request the companion user to roll dice with count incrementing with each button press
            GameboardLogging.Log($"{dice[0]}");
            dice[0].count = d6Count;
            await RollDice(dice, $"{d6Count}d6");
            d6Count++;
        }
    }
}