namespace GarboSellsClasses.Models
{
    /// <summary>
    /// Represents a measurement of an item. The area that is to be measured is provided by the template loaded from the CategoryDataService and is identifiable by the ID provided in the template.
    /// </summary>
    public class ItemMeasurement
    {
        /// <summary>
        /// The ID of the measurement field (IE, chest, length, etc), provided by the template.
        ///
        /// <setby>The Android app when the user sets a value for a measurement. Assigned to correspond to the ID provided by the template.</setby>
        /// <setwhen>Saving a new item.</setwhen>
        /// <modifywhen>Never.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public long categoryMeasurementId { get; set; }
        /// <summary>
        /// The ID of the measurement. Set by the database and used to relate the measurement to the Item.
        /// <p>
        /// NOTE: This will be NULL when creating a new item in the app.It is not set until the item is saved.
        /// </p>
        /// <setby>The database (should be autoincremented).</setby>
        /// <setwhen>Saving a new item.</setwhen>
        /// <modifywhen>Never.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public long? itemMeasurementId { get; set; }
        /// <summary>
        /// The value of the measurement as provided by the user.
        /// Assumed to be inches.
        /// 
        /// <setby>The user.</setby>
        /// <setwhen>Creating an item in the app.</setwhen>
        /// <modifywhen>Editing an existing item in the app.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public double itemMeasurementValue { get; set; }

        /// <summary>
        /// Constructor. All three fields are required.
        /// </summary>
        /// <param name="categoryMeasurementId"></param>
        /// <param name="itemMeasurementId"></param>
        /// <param name="itemMeasurementValue"></param>
        public ItemMeasurement(long categoryMeasurementId, long? itemMeasurementId, double itemMeasurementValue)
        {
            this.categoryMeasurementId = categoryMeasurementId;
            this.itemMeasurementId = itemMeasurementId;
            this.itemMeasurementValue = itemMeasurementValue;
        }
        public ItemMeasurement() { }
    }
}