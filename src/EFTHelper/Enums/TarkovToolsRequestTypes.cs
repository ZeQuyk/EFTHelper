namespace EFTHelper.Enums;

public enum TarkovToolsRequestTypes
{
    /// <summary>
    /// Get a single item by ID
    /// </summary>
    Item,

    /// <summary>
    /// Get a list of items by type
    /// </summary>
    ItemsByType,

    /// <summary>
    /// Get a list of items by name
    /// </summary>
    ItemsByName,

    /// <summary>
    /// Get history of pricings and timestamps for an item by ID
    /// </summary>
    HistoricalItemPrices,

    /// <summary>
    /// Get the list of all barters, including required items and reward items
    /// </summary>
    Barters,

    /// <summary>
    /// Get the list of possible crafts, including duration, required items and reward items
    /// </summary>
    Crafts,

    /// <summary>
    /// Get the list of all quests
    /// </summary>
    Quests,


    /// <summary>
    /// Get the list of hideout modules and their possible enhancements and requirements
    /// </summary>
    HideoutModules,

    /// <summary>
    /// Get the latest Tarkov status updates
    /// </summary>
    Status,

    /// <summary>
    /// Get the reset times for trader stores.
    /// </summary>
    TraderResetTimes
}
