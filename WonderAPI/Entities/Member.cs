﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WonderAPI.Entities
{
    /// <summary>
    /// Gender enumeration
    /// </summary>
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    /// <summary>
    /// Represent Member business entity
    /// </summary>
    public class Member
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        public string OptionalEmail { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Password { get; set; }

        [Required]
        [MaxLength(24)]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
    }

    /// <summary>
    /// Represent update member request model
    /// </summary>
    public class MemberUpdateRequest
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string OptionalEmail { get; set; }

        [Required]
        [MaxLength(24)]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
    }

    /// <summary>
    /// Represent MemberInfo model, it's actually a Member model, but without password
    /// </summary>
    public class MemberInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OptionalEmail { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    } 
}
