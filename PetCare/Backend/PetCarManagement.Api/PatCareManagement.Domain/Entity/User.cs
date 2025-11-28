using PetCareManagement.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetCareManagement.Domain.Entity
{

 
  // User Entity
        public class User
        {
            [Key]
            public int UserId { get; private set; }

            [Required, EmailAddress]
            public string Email { get; private set; }

            [Required]
            public string Name { get; private set; }

            [Phone]
            public string? PhoneNumber { get; private set; }

            [Required]
            public string PasswordHash { get; private set; }

            [Required]
            public DateTime CreatedAt { get; private set; }

            public IReadOnlyCollection<Pets> Pets => _pets.AsReadOnly();
            private readonly List<Pets> _pets = new();

            public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();
            private readonly List<RefreshToken> _refreshTokens = new();

            private User() { } 

            public User(string email, string name, string phoneNumber)
            {
                Email = email;
                Name = name;
                PhoneNumber = phoneNumber;
                CreatedAt = DateTime.UtcNow;
            }

            public void SetPasswordHash(string passwordHash)
            {
                PasswordHash = passwordHash;
            }

            public void UpdateName(string name)
            {
                Name = name;
            }

            public void UpdatePhone(string? phone)
            {
                PhoneNumber = phone;
            }

            public void AddRefreshToken(RefreshToken token)
            {
                _refreshTokens.Add(token);
            }

            public void AddPet(Pets pet)
            {
                if (pet == null) throw new ArgumentNullException(nameof(pet));
                _pets.Add(pet);
            }
        }
    }













