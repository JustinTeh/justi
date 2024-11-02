using Microsoft.EntityFrameworkCore;

public class FridgeItemService {
    private readonly AppDbContext _appDbContext;
    private List<FridgeItem> AllFridgeItems;
    public FridgeItemService(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
        AllFridgeItems = [];
    }
    /// <summary>
    /// Returns a list of fridge items that are not tossed out.
    /// </summary>
    /// <returns>List<FridgeItem></returns>
    public List<FridgeItem> GetAllFridgeItems() {
        AllFridgeItems = [.. _appDbContext.FridgeItems.Where(f => !f.IsTossedOut)];
        return AllFridgeItems;
    }

    /// <summary>
    /// Returns a list of fridge items owned by a specified user that are not tossed out.
    /// </summary>
    /// <param name="userID"></param>
    /// <returns>List<FridgeItem></returns>
    public List<FridgeItem> GetAllFridgeItems(int userID) {
        List<FridgeItem> fridgeItems = [.. GetAllFridgeItems()]; // not sure if this is shallow copy or deep copy
        fridgeItems.RemoveAll(f => f.OwnedByUserID != userID);
        return fridgeItems;
    }

    /// <summary>
    /// The frontend should not input a User member in the API call. If it does, an extraneous entry in the Users table will be generated.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task PostFridgeItemAsync(FridgeItem item) {
        _appDbContext.FridgeItems.Add(item);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IResult> UpdateFridgeItemAsync(FridgeItem itemToUpdate) {
        FridgeItem? existingFridgeItem = await _appDbContext.FridgeItems.FindAsync(itemToUpdate.FridgeItemID);
        if (existingFridgeItem != null) {
            existingFridgeItem.Title = itemToUpdate.Title;
            existingFridgeItem.DayIn = itemToUpdate.DayIn;
            existingFridgeItem.ExpirationDate = itemToUpdate.ExpirationDate;
            existingFridgeItem.IsTossedOut = itemToUpdate.IsTossedOut;
            existingFridgeItem.GroceryStore = itemToUpdate.GroceryStore;
            existingFridgeItem.Description = itemToUpdate.Description;
            await _appDbContext.SaveChangesAsync();
            return Results.Ok("Fridge item updated successfully.");
        }
        return Results.NotFound("ERROR: Fridge item not found. Could not update.");


    }
    /// <summary>
    /// "Tosses out" a fridge item. fridgeItemID is the primary key of this table.
    /// </summary>
    /// <param name="fridgeItemID"></param>
    /// <returns></returns>
    public async Task<IResult> TossOutFridgeItemAsync(int fridgeItemID) {
        FridgeItem? fridgeItem = await _appDbContext.FridgeItems.FindAsync(fridgeItemID);
        if (fridgeItem != null) {
            fridgeItem.IsTossedOut = true;
            await _appDbContext.SaveChangesAsync();
            return Results.Ok("Item tossed out successfully");
        }
        return Results.NotFound("ERROR: Item not found.");
    } 

}