﻿using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Models
{
    public enum UserRole
    {
        owner,
        guest1,
        guide,
        guest2
    }
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserRole Role { get; set; }

        public User() { }

        public User(string username, string password, UserRole role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = (UserRole)Enum.Parse(typeof(UserRole), values[3], false);
        }
    }
}
