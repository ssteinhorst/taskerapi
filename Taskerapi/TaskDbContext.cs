﻿//using System;
//namespace Taskerapi
//{
//	public class TaskDbContext
//	{
//		public TaskDbContext()
//		{
//		}
//	}
//}

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

    public DbSet<Task> Tasks { get; set; }
}
