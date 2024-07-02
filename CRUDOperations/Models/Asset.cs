using System.ComponentModel.DataAnnotations;

public class Asset
{
    [Key]
    public string Asset_Id { get; set; }

    [Required(ErrorMessage = "Employee Id is required")]
    public string Employee_Id { get; set; }

    [Required(ErrorMessage = "Asset name is required")]
    [StringLength(50, ErrorMessage = "Asset name cannot exceed 50 characters")]
    public string Asset_name { get; set; }

    [Required(ErrorMessage = "Make company is required")]
    [StringLength(50, ErrorMessage = "Make company cannot exceed 50 characters")]
    public string Make_Company { get; set; }

    [Required(ErrorMessage = "Value is required")]
    public int Value { get; set; }

    [Required(ErrorMessage = "Date of assignment is required")]
    [Display(Name = "Date of assignment")]
    [DataType(DataType.DateTime)]
    public DateTime Date_of_assign { get; set; }

    [Required(ErrorMessage = "Date of request is required")]
    [Display(Name = "Date of request")]
    [DataType(DataType.DateTime)]
    public DateTime Date_of_req { get; set; }
}
