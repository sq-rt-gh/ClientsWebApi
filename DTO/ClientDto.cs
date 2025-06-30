using ClientsWebApi.Models;

namespace ClientsWebApi.DTO;

public class ClientDto
{
    public string Inn { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ClientType Type { get; set; }
    public List<string>? FounderInns { get; set; } 
}
