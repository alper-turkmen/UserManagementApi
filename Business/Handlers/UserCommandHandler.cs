using Microsoft.EntityFrameworkCore;
using UserManagement.Api.Business.Commands;
using UserManagement.Api.Common;
using UserManagement.Api.Data;
using UserManagement.Api.Models.DTOs;
using UserManagement.Api.Models.Entities;

namespace UserManagement.Api.Business.Handlers
{
    public class UserCommandHandler
    {
        private readonly ApplicationDbContext _context;

        public UserCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<UserDto>> HandleAsync(CreateUserCommand command)
        {
            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == command.UserDto.Email);

                if (existingUser != null)
                {
                    return ApiResponse<UserDto>.Failure("Bu email adresi kullanılmaktadır");
                }

                var user = new User
                {
                    FirstName = command.UserDto.FirstName,
                    LastName = command.UserDto.LastName,
                    Email = command.UserDto.Email,
                    Address = command.UserDto.Address
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return ApiResponse<UserDto>.Success(userDto, "Kullanıcı oluşturuldu");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Failure($"Kullanıcı oluşturulurken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponse<UserDto>> HandleAsync(UpdateUserCommand command)
        {
            try
            {
                var user = await _context.Users.FindAsync(command.Id);
                if (user == null)
                {
                    return ApiResponse<UserDto>.Failure("Kullanıcı bulunamadı.");
                }

                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == command.UserDto.Email && u.Id != command.Id);

                if (existingUser != null)
                {
                    return ApiResponse<UserDto>.Failure("Bu email adresi zaten kullanılmaktadır.");
                }

                user.FirstName = command.UserDto.FirstName;
                user.LastName = command.UserDto.LastName;
                user.Email = command.UserDto.Email;
                user.Address = command.UserDto.Address;

                await _context.SaveChangesAsync();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                };

                return ApiResponse<UserDto>.Success(userDto, "Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Failure($"Kullanıcı güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ApiResponse<bool>> HandleAsync(DeleteUserCommand command)
        {
            try
            {
                var user = await _context.Users.FindAsync(command.Id);
                if (user == null)
                {
                    return ApiResponse<bool>.Failure("Kullanıcı bulunamadı.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return ApiResponse<bool>.Success(true, "Kullanıcı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Failure($"Kullanıcı silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}