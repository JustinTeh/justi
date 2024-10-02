using System;
using Microsoft.EntityFrameworkCore;
public class UserService {

    private readonly AppDbContext _appDbContext;

    public UserService(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
    }

    public async Task<List<User>> GetUsersAsync() {
        return await _appDbContext.Users.ToListAsync();
    }

}