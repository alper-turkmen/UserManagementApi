using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Business.Queries;
using UserManagement.Api.Common;
using UserManagement.Api.Data;
using UserManagement.Api.Models.DTOs;

namespace UserManagement.Api.Business.Handlers
{
    public class UserQueryHandler
    {
        private readonly ApplicationDbContext _context;

        public UserQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<UserDto>>> HandleAsync(GetAllUsersQuery query)
        {
            try
            {
                var users = await _context.Users
                    .Skip((query.Page - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Address = u.Address,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt
                    })
                    .ToListAsync();

                var totalCount = await _context.Users.CountAsync();

                return ApiResponse<List<UserDto>>.Success(users, 
                    $"Toplam {totalCount} kullanıcıdan {users.Count} tanesi listelendi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<List<UserDto>>.Failure($"Kullanıcılar listelenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponse<UserDto>> HandleAsync(GetUserByIdQuery query)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.Id == query.Id)
                    .Select(u => new UserDto
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Address = u.Address,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return ApiResponse<UserDto>.Failure("Kullanıcı bulunamadı.");
                }

                return ApiResponse<UserDto>.Success(user, "Kullanıcı başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Failure($"Kullanıcı getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}