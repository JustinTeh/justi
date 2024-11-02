// using Primary Constructor here just to try it out

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
public class FridgeItem(string title, DateTime dayIn, DateTime expirationDate) {
    public int FridgeItemID { get; set; }
    public string Title { get; set; } = title;
    public DateTime DayIn { get; set; } = dayIn;
    public DateTime ExpirationDate { get; set; } = expirationDate;

    public int OwnedByUserID { get; set; }
    public User User { get; set; } = null!; // Required reference navigation to principal. AKA a FridgeItem must belong to a User. It cannot exist freely.
    public string? GroceryStore { get; set; }
    public bool IsTossedOut { get; set; }
    public string? Description { get; set; }

    public string GetFridgeItemID() {
        string json = JsonSerializer.Serialize(this.FridgeItemID);
        return json;
    }

    public void SetTitle(string newTitle) {
        if (!string.IsNullOrWhiteSpace(newTitle))
            this.Title = newTitle;
        // else
            // TODO: throw InvalidTitleException
    }
    public void SetDayIn(DateTime newDate) {
        ValidateDates();
        this.DayIn = newDate;
    }
    public void SetExpirationDate(DateTime newDate) {
        ValidateDates();
        this.ExpirationDate = newDate;
    }

    public bool ValidateDates() {
        if (DayIn.CompareTo(ExpirationDate) > 0) {
            // dayIn is later than ExpirationDate
            return false;
        }
        else if (ExpirationDate.CompareTo(DayIn) < 0) {
            // expirationDate is earlier than DayIn
            return false;
        }
        return true;
    }



}