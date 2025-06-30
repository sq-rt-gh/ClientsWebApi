using System.ComponentModel.DataAnnotations;

namespace ClientsWebApi.Models;

public enum ClientType
{
    UL, // Юридическое лицо
    IP  // Индивидуальный предприниматель
}

public class Client
{
    [Key]
    [Required]
    public required string Inn { get; set; }

    public string Name { get; set; } = "No Name";

    public ClientType Type { get; set; } = ClientType.IP;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // ЮЛ может иметь нескольких учредителей
    public ICollection<Founder>? Founders { get; set; } = null;
}
