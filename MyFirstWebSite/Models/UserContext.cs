using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace MyFirstWebSite.Models
{

    //Establish a connection to a databaase (DB Context will do all this)
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string EmailConfirmationToken { get; set; }


        //One User has Many Login Sessions
      //  public virtual ICollection<UserLoginSessionRecords> Records { get; set; }


        //table split
        [ForeignKey("Id")]
        public virtual UserLogin UserLoginModel { get; set; }
        [ForeignKey("Id")]
        public virtual UserRegisterModel userRegisterModel { get; set; }

        [ForeignKey("Id")]
        public virtual UserChangePasswordModel UserChangePasswordModel { get; set; }
    }


    public class UserLoginSessionRecords
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

   
        [Key]
        public virtual int Id { get; set; }
        public virtual DateTime UserLoginTimeAndDate { get; set; }


        [ForeignKey("Id")]
        public virtual UserSessionModel UserSessionModel { get; set; }
        //   [ForeignKey("Id")]
        //   public virtual User User { get; set; }

    }//user login session records





    public class UserContext : DbContext
    {
        public UserContext() :base("name=UserContext")
        {

        }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserLoginSessionRecords> loginSession { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //shared key
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserLoginSessionRecords>().ToTable("UserSession");

            //user Dependancy Models
            modelBuilder.Entity<User>().HasRequired(p => p.UserLoginModel).WithRequiredPrincipal(c=>c.user);
            modelBuilder.Entity<User>().HasRequired(p => p.userRegisterModel).WithRequiredPrincipal(c => c.user);
            modelBuilder.Entity<User>().HasRequired(p => p.UserChangePasswordModel).WithRequiredPrincipal(c => c.User);


            modelBuilder.Entity<UserLoginSessionRecords>().HasRequired(p => p.UserSessionModel).WithRequiredPrincipal(c => c.UserLoginSessionRecords);
            base.OnModelCreating(modelBuilder);
        }
    }
}