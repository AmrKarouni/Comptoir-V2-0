﻿using COMPTOIR.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace COMPTOIR.Models.AppModels
{
    public class Place
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long", MinimumLength = 6)]
        [Display(Name = "Place Name")]
        public string? Name { get; set; }
        public string? LocationAddress { get; set; }
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumberOne { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumbertwo { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumberThree { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? EmailAddress { get; set; }
        public string? IpAddress { get; set; }
        public string? ImageUrl { get; set; }
        public string? ThumbUrl { get; set; }
        public double AverageRentalFees { get; set; } = 0;
        public bool IsPos { get; set; }
        public bool IsManufacturer { get; set;}
        public virtual ICollection<PaymentChannel>? PaymentChannels { get; set; }
        public virtual ICollection<PaymentMethod>? PaymentMethods { get; set; }
        public virtual ICollection<Stock>? Stocks { get; set; }
        public virtual ICollection<ApplicationUser>? Users { get; set; }
        public virtual ICollection<Terminal>? Terminals { get; set; }
    }
}
