using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;
public class UserService {

    private readonly AppDbContext _appDbContext;

    public UserService(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
    }

    public async Task<List<User>> GetUsersAsync() {
        return await _appDbContext.Users.ToListAsync();
    }

    public async Task<bool> IfUserExists(int userID) {
        return await _appDbContext.Users.FindAsync(userID) != null;
    }

    public async Task PostUserAsync(User user) {
        _appDbContext.Users.Add(user);
        await _appDbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Frontend should send a User-Json with the new profile changes
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public async Task<IResult> UpdateUserProfileAsync(User userToUpdate) {
        User? existingUserRecord = await _appDbContext.Users.FindAsync(userToUpdate.UserID);
        if (existingUserRecord != null) {
            // TO DO: make this update more automated (find the fields that differ and change them)
            existingUserRecord.Email = userToUpdate.Email;
            existingUserRecord.FirstName = userToUpdate.FirstName;
            existingUserRecord.LastName = userToUpdate.LastName;
            existingUserRecord.IsCurrentTenant = userToUpdate.IsCurrentTenant;
            await _appDbContext.SaveChangesAsync();
            return Results.Ok("User profile updated successfully!");
        }
        return Results.NotFound("ERROR: User not found");
    }

    public async Task<IResult> DeleteUserAsync(int userID) {
        User? userToDelete = await _appDbContext.Users.FindAsync(userID);
        if (userToDelete != null) {
            _appDbContext.Users.Remove(userToDelete);
            await _appDbContext.SaveChangesAsync();
            return Results.Ok("User deleted successfully!");
        }
        return Results.NotFound("ERROR: User not found.");
    }
    


}