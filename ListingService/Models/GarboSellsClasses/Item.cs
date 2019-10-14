using System;
using System.Collections.Generic;

namespace GarboSellsClasses.Models
{
    /// <summary>
    ///  Represents an "Item" or "Listing" as it is represented in the app.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The date/time when the item was originally created.
        ///
        /// <setby>The frontend Android app when saving a new item, using <code>new Date()</code></setby>
        /// <setwhen>Creating a new item in the frontend Android application.</setwhen>
        /// <modifywhen>Never. This should not be modified once set.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public DateTime createdDateTime { get; set; }
        /// <summary>
        /// The date/time when the item was last modified.
        ///
        /// <setby>The frontend Android app when saving a new item, using <code>new Date()</code></setby>
        /// <setwhen>Creating a new item in the frontend Android application.</setwhen>
        /// <modifywhen>Saving an existing item in the frontend Android application.</modifywhen>
        /// <modifiedby>The Android application.</modifiedby>
        /// </summary>
        public DateTime updatedDateTime { get; set; }
        /// <summary>
        /// The ID of the user who created the item originally.
        /// <p>
        /// NOTE: this is not the <code>userLoginId</code> (which is a<code> GUID</code> or<code>string</code>). It is the<code> userId</code>, which is a<code>long</code>.
        /// </p>
        /// <setby>The frontend Android app when saving a new item, using <code>new Date()</code></setby>
        /// <setwhen>Creating a new item in the frontend Android application.</setwhen>
        /// <modifywhen>Never. This should not be modified once set.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public long createdByUserId { get; set; }
        /// <summary>
        /// The ID of the user who last modified the item.
        /// <p>
        /// NOTE: this is not the <code>userLoginId</code> (which is a<code> GUID</code> or<code>string</code>). It is the<code> userId</code>, which is a<code>long</code>.
        /// </p>
        /// <setby>The frontend Android app when saving a new item, using <code>new Date()</code></setby>
        /// <setwhen>Creating a new item in the frontend Android application.</setwhen>
        /// <modifywhen>Saving an existing item in the frontend Android application.</modifywhen>
        /// <modifiedby>The Android application.</modifiedby>
        /// </summary>
        public long updatedByUserId { get; set; }
        /// <summary>
        /// The ID of the item. This is the primary key identifying the item in the database.
        /// <p>
        /// NOTE: This will be NULL when creating a new item in the app. It is not set until the item is saved.
        /// </p>
        /// <setby>The database (should be set to autoincrement)</setby>
        /// <setwhen>A new item is saved to the database</setwhen>
        /// <modifywhen>Never.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public long? id { get; set; }
        /// <summary>
        /// The ID of the category that the item belongs to.
        ///
        /// <setby>The Android app on item creation (the Category is set by the user, the ID is saved).</setby>
        /// <setwhen>Creating an item in the Android app.</setwhen>
        /// <modifywhen>Never. Once the category is set, it should not be modified.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public long categoryId { get; set; }
        /// <summary>
        /// The ID of the subcategory that the item belongs to.
        ///
        /// <setby>The Android app on item creation (the Subcategory is set by the user, the ID is saved).</setby>
        /// <setwhen>Creating an item in the Android app.</setwhen>
        /// <modifywhen>Never. Once the subcategory is set, it should not be modified.</modifywhen>
        /// <modifiedby>No one.</modifiedby>
        /// </summary>
        public long subcategoryId { get; set; }
        /// <summary>
        /// This field will become the title of the listing once it is posted to Ebay/Etsy.
        ///
        /// <setby>The user in the frontend Android app.</setby>
        /// <setwhen>Creating an item in the Android app.</setwhen>
        /// <modifywhen>Editing an item in the Android app.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public string shortDescription { get; set; }
        /// <summary>
        /// This field will become the text body of the listing once it is posted to Ebay/Etsy.
        ///
        /// <setby>The user in the frontend Android app.</setby>
        /// <setwhen>Creating an item in the Android app.</setwhen>
        /// <modifywhen>Editing an item in the Android app.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public string longDescription { get; set; }
        /// <summary>
        /// NOT IN USE.
        /// Extended feature that may be implemented after basic functionality exists.
        /// </summary>
        public string[] tags { get; set; }
        /// <summary>
        /// The measurements of the item.Only applies if <code>category.hasMeasurements == TRUE</code> May be empty.See ItemMeasurement for details.
        ///
        /// OPTIONAL FIELDS
        ///
        /// <setby>The Android app when the user enters measurements.</setby>
        /// <setwhen>Saving a new item.</setwhen>
        /// <modifywhen>The measurements are modified on an existing item.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public List<ItemMeasurement> measurements { get; set; }
        /// <summary>
        /// The size of the item. See ItemSize for details.
        ///
        /// REQUIRED FIELD if <code>category.hasSizing == TRUE</code>
        ///
        /// <setby>The Android app when the user selects the size type and size value.</setby>
        /// <setwhen>Saving a new item.</setwhen>
        /// <modifywhen>The size type and size value are modified on an existing item.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public ItemSize size { get; set; }
        /// <summary>
        /// A set of attributes that apply to the particular subcategory selected. A given subcategory may have 0 or more of these.
        ///
        /// <setby>The user when completing the item creation wizard.</setby>
        /// <setwhen>Creating an item in the Android app.</setwhen>
        /// <modifywhen>Saving an existing item.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public List<ItemAttribute> attributes;
        /// <summary>
        /// A set of attributes that generally applies to all items, regardless of subcategory.
        ///
        /// <setby>The user when completing the item creation wizard.</setby>
        /// <setwhen>Creating an item in the Android app.</setwhen>
        /// <modifywhen>Saving an existing item.</modifywhen>
        /// <modifiedby>The user.</modifiedby>
        /// </summary>
        public GeneralItemAttributes generalItemAttributes { get; set; }
        /// <summary>
        /// Default constructor to initialize fields.
        /// </summary>
        public Item()
        {
            this.measurements = new List<ItemMeasurement>();
            this.generalItemAttributes = new GeneralItemAttributes();
            this.attributes = new List<ItemAttribute>();
        }
    }
}
