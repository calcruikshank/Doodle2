namespace Gameboard.Objects.Dice
{
    [System.Serializable]
    public class DieGroup
    {
        public DieGroup() { }

        /// <summary>
        /// Number of sides on the dice
        /// </summary>
        public int sides;

        /// <summary>
        /// Number of dice to roll
        /// </summary>
        public int count;

        /// <summary>
        /// id associated with the roll
        /// </summary>
        /// <remarks>defaults to Guid.NewGuid if none specified.</remarks>
        public string id;

        /// <summary>
        /// The tint color hex value assigned to this die group.
        /// </summary>
        /// <remarks>
        /// HEX - "#ff0000"
        /// You can easily specify the hex format with Unity's ColorUtility:
        ///     $"#{UnityEngine.ColorUtility.ToHtmlStringRGB(UnityEngine.Color.green)}"
        /// </remarks>
        public string diceTintHexColor;

        /// <summary>
        /// TextureId of a previously loaded FBX asset to use for the dice model.
        /// </summary>
        /// <remarks>If a textureId is not included then the default texture will be used so long as sides is included.</remarks>
        public string textureId;

        /// <summary>
        /// Text to be displayed in the companion app when the user is selecting dice
        /// </summary>
        public string label;

        //public int[] result?; //Phase 2
    }
}
