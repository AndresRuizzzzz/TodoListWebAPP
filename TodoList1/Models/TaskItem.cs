﻿

using System;
using System.ComponentModel.DataAnnotations;

namespace TodoList1.Models

{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
