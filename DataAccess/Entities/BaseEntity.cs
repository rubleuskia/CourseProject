﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public User CreatedBy { get; set; }

        [Required]
        public string UpdatedById { get; set; }

        public User UpdatedBy { get; set; }
    }
}
