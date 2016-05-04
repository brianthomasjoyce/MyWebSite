namespace MyFirstWebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(10)]
        public string UserName { get; set; }

        [StringLength(10)]
        public string Password { get; set; }

        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(10)]
        public string Surname { get; set; }

        [StringLength(10)]
        public string Email { get; set; }
    }
}