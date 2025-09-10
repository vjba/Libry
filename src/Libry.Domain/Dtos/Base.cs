using System.Text.Json.Serialization;

namespace Libry.Domain.Dtos;

public record Base
{
    [JsonIgnore]
    public Guid Id { get; set; }
}
