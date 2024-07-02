using System.ComponentModel.DataAnnotations;
using CRUDOperations.Models;

public class Issue
{
    public int IssueId { get; set; }
    public int UserId { get; set; }
    public UsersEntity User { get; set; }
    public int AssetId { get; set; }
    [Required]
    public Asset Asset { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}