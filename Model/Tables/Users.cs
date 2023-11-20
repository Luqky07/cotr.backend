﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cotr.backend.Model.Tables
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nickname { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? SecondSurname { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Birthdate { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? Affiliation { get; set; }

        public Users() { }

        public Users(string nickname, string email, string name, string surname, string? secondSurname, DateTime birthdate, string? affiliation)
        {
            Nickname = nickname;
            Email = email;
            Name = name;
            Surname = surname;
            SecondSurname = secondSurname;
            Birthdate = birthdate;
            Affiliation = affiliation;
        }

        public Users(int userId, string nickname, string email, string name, string surname, string? secondSurname, DateTime birthdate, string? affiliation)
        {
            UserId = userId;
            Nickname = nickname;
            Email = email;
            Name = name;
            Surname = surname;
            SecondSurname = secondSurname;
            Birthdate = birthdate;
            Affiliation = affiliation;
        }
    }
}
