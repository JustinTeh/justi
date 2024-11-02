using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

public class User {
    
    public int UserID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool IsCurrentTenant { get; set; }  = true;
    public string Password { get; set; }
    public User() {
    }
    public ICollection<FridgeItem> FridgeItems { get; } = [];
    // public int GetUserID() {
    //     return UserID;
    // }
    // public void SetFirstName(string newFirst) {
    //     if (!string.IsNullOrWhiteSpace(newFirst))
    //         this.FirstName = newFirst;
    // }
    // public void SetLastName(string newLast) {
    //     if (!string.IsNullOrWhiteSpace(newLast))
    //         this.LastName = newLast;
    // }
    // public void SetEmail(string newEmail) {
    //     if (!string.IsNullOrWhiteSpace(newEmail))
    //         this.Email = newEmail;
    // }

    // public void SetTenantStatus(bool status) {
    //     IsCurrentTenant = status;
    // }
}

