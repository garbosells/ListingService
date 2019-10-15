namespace GarboSellsClasses.Models
{
    /// <summary>
    /// Represents the size of an item. Includes the ID of the "type" of sizing (US letter, US numeric, UK sizes, etc) and the ID of the value (S/M/L, 4, etc). The value itself is not contained in this object.
    /// </summary>
    public class ItemSize
    {
        /// <summary>
        /// The ID of the size type selected by the user.
        /// 
        /// <setby>The Android app when the user selects the size type.</setby>
        /// <setwhen>Saving a new item.</setwhen>
        /// <modifywhen>The size type is modified on an existing item.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public long sizeTypeId { get; set; }
        /// <summary>
        /// The ID of the size value selected by the user. This is NOT the size itself - it is the primary key of the size value in the database.
        /// 
        /// <setby>The Android app when the user selects the size.</setby>
        /// <setwhen>Saving a new item.</setwhen>
        /// <modifywhen>The size is modified on an existing item.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public long sizeValueId { get; set; }
        /// <summary>
        /// Constructor, all fields are required.
        /// </summary>
        /// <param name="sizeTypeId"></param>
        /// <param name="sizeValueId"></param>
        public ItemSize(long sizeTypeId, long sizeValueId)
        {
            this.sizeTypeId = sizeTypeId;
            this.sizeValueId = sizeValueId;
        }
        public ItemSize() { }
    }
}