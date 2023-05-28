using System;
using System.ComponentModel.DataAnnotations;

public class Task
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsComplete { get; set; }
}
