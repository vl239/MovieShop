﻿using System;
namespace ApplicationCore.Entities
{
	public class UserRole
	{
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}

