using System.ComponentModel.DataAnnotations;

namespace CRUDOperations.Models;

public class Request
{
    public int RequestId { get; set; }
    public int UserId { get; set; }
    public UsersEntity User { get; set; }
    public int AssetId { get; set; }
    [Required]
    public Asset Asset { get; set; }
    public string Status { get; set; }
}