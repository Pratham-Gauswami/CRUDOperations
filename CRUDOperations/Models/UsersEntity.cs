using System.ComponentModel.DataAnnotations;

namespace CRUDOperations.Models;

public class UsersEntity{

    [Key]
    public int user_id { get; set;}
    public string username { get; set;}
    public string email { get; set;}
    public string password { get; set;}

}