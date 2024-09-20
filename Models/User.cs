using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

public class User {
    public int UserID { get; set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Email { get; private set; }
    public bool IsCurrentTenant { get; private set; }  = true;
    public User() {}
    public string GetUserID() {
        string json = JsonSerializer.Serialize(this.UserID);
        return json;
    }
    private void SetFirstName(string newFirst) {
        if (!string.IsNullOrWhiteSpace(newFirst))
            this.FirstName = newFirst;
    }
    private void SetLastName(string newLast) {
        if (!string.IsNullOrWhiteSpace(newLast))
            this.LastName = newLast;
    }
    private void SetEmail(string newEmail) {
        if (!string.IsNullOrWhiteSpace(newEmail))
            this.Email = newEmail;
    }

}

