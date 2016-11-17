//using Dnx.Identity.MongoDB;
//using Microsoft.AspNetCore.Identity;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Dnx.Identity.MongoDB.Models;

//namespace WebApplication.Models.WebApplication
//{
//    [BsonIgnoreExtraElements]
//    [BsonSerializer]
//    [DataContract]

//    public class ApplicationUser : MongoIdentityUser
//    {
//        [BsonConstructor]
//        public ApplicationUser(
//            string userName,
//            string email,
//            string firstName,
//            string lastName,
//            DateTimeOffset dateOfBirth,
//            string birthCountry,
//            string currentCountry,
//            string image) : base(userName, email)
//        {
//            this.FirstName = firstName;
//            this.LastName = lastName;
//            this.DateOfBirth = dateOfBirth;
//            this.BirthCountry = birthCountry;
//            this.CurrentCountry = currentCountry;
//            this.Image = image;
//        }

//        [BsonConstructor]


//        public ApplicationUser(string userName,
//            string email,
//            DateTimeOffset dateOfBirth,
//            string birthCountry, string currentCountry, string image) : base(userName, email)
//        {
//            this.DateOfBirth = dateOfBirth;
//            this.BirthCountry = birthCountry;
//            this.CurrentCountry = currentCountry;
//            this.Image = image;

//        }

//        public ApplicationUser(string userName, string email) : base(userName, email)
//        {
//        }

//        [Display(Name = "First Name")]
//        [DataMember]
//        [BsonElement]
//        public string FirstName { get; set; }

//        [DataMember]
//        [BsonElement]
//        [Display(Name = "Last Name")]
//        public string LastName { get; set; }
//        [DataMember]
//        [BsonElement]
//        [Display(Name = "Date Of Birth")]
//        public DateTimeOffset DateOfBirth { get; set; }

//        [Display(Name = "Birth Country")]
//        [DataMember]
//        [BsonElement]
//        public string BirthCountry { get; set; }
//        [DataMember]
//        [BsonElement]
//        [Display(Name = "Current Country")]
//        public string CurrentCountry { get; set; }
//        [DataMember]
//        [BsonElement]
//        [DataType(DataType.ImageUrl)]
//        public string Image { get; set; }

//    }

//    }
