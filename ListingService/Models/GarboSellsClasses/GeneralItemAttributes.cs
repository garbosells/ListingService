namespace GarboSellsClasses.Models
{
    /// <summary>
    /// A set if Attributes that applies to all items.
    /// </summary>
    public class GeneralItemAttributes
    {
        /// <summary>
        /// Represents the primary color attribute and its recommended values.
        /// OPTIONAL
        /// </summary>
        public ItemAttribute primaryColor { get; set; }
        /// <summary>
        /// Represents the secondary color attribute and its recommended values.
        /// OPTIONAL
        /// </summary>
        public ItemAttribute secondaryColor { get; set; }
        /// <summary>
        /// Represents the era attribute and its recommended values. The era is whenever the item was manufactured. It is used by the Ebay/Etsy services to determine whether the item can be categorized as "vintage."
        /// REQUIRED     
        /// </summary>
        public ItemAttribute era { get; set; }
        /// <summary>
        /// Represents the material attribute and its recommended values.That is, what is the item made of?
        /// OPTIONAL     
        /// </summary>
        public ItemAttribute material { get; set; }
    }
}