using System.ComponentModel.DataAnnotations;

namespace ClientsWebApi.Models;

public class Founder
{
    [Key]
    [Required]
    public required string Inn { get; set; }

    public string FullName { get; set; } = "No Name";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}
