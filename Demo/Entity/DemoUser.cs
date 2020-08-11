namespace Demo.Entity
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public class DemoUser
  {
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Password { get; set; }

    [StringLength(200)]
    public string Email { get; set; }

    [StringLength(30)]
    public string Mobile { get; set; }
  }
}
