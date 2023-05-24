using System;
using System.ComponentModel.DataAnnotations;

public class Task
{
	public Task()
	{
	}
    [Key]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Location { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsComplete { get; set; }
}


