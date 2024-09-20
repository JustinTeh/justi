// using Primary Constructor here just to try it out

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
class FridgeItem(string title, DateTime dayIn, DateTime expirationDate) {
    public int FridgeItemID { get; set; }
    public string Title { get; private set; } = title;
    public DateTime DayIn { get; private set; } = dayIn;
    public DateTime ExpirationDate { get; private set; } = expirationDate;
    public string GetFridgeItemID() {
        string json = JsonSerializer.Serialize(this.FridgeItemID);
        return json;
    }

    private void SetTitle(string newTitle) {
        if (!string.IsNullOrWhiteSpace(newTitle))
            this.Title = newTitle;
        // else
            // TODO: throw InvalidTitleException
    }
    private void SetDayIn(DateTime newDate) {
        ValidateDates();
        this.DayIn = newDate;
    }
    private void SetExpirationDate(DateTime newDate) {
        ValidateDates();
        this.ExpirationDate = newDate;
    }

    private bool ValidateDates() {
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