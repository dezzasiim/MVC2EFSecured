﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Built in Metadata (Data Annotations) features or methods

namespace MVC2EFSecured.DATA.EF//Make sure this namespace matches the Namespace in your .tt file
{
    //  MOD 5.3 add Metadata Notes
    /*General rules and standard practices for metadata:

          /*


    #region Author Metadata
        //1) create public classs xxxMetaData
    public class AuthorMetaData
    {

        //public int AuthorID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(15,ErrorMessage = "* First Name must be 15 characters or less")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(15, ErrorMessage = "* Last Name must be 15 characters or less")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "* City must be 50 characters or less")]
        [DisplayFormat(NullDisplayText = "value is null")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* State must be 2 characters or less")]
        [DisplayFormat(NullDisplayText = "value is null")]
        public string State { get; set; }

        [StringLength(6, ErrorMessage = "* Zip Code  must be 6 characters or less")]
        [DisplayFormat(NullDisplayText = "value is null")]
        public string ZipCode { get; set; }

        [StringLength(30, ErrorMessage = "* Country  must be 30 characters or less")]
        [DisplayFormat(NullDisplayText = "value is null")]
        public string Country { get; set; }
    }

    //2) Do a "typeof" attribute
    [MetadataType(typeof(AuthorMetaData))]

    //3) Declare the Partial class stub
    public partial class Author
    {
        // you do not alwasy have to have code int here
        // This is a great place for data transformation

        [Display(Name ="Author")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion
    #region Books Metadata
    public class BookMetaData
    {
        [StringLength(14, ErrorMessage = "* The value must be 14 characters or less.")]
        [DisplayFormat(NullDisplayText ="NA")]
        public string ISBN { get; set; }

        [StringLength(100, ErrorMessage = "* The value must be 100 characters or less.")]
        [Required(ErrorMessage = "* Required")]
        [Display(Name = "Book")]
        [UIHint("MultilineText")]
        public string BookTitle { get; set; }

        [UIHint("MultilineText")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string Description { get; set; }

       // public Nullable<int> GenreID { get; set; }

       [Range(0,double.MaxValue, ErrorMessage = "* Price must be a valid number, 0 or larger")]
       [DisplayFormat(NullDisplayText ="NA",DataFormatString ="{0:c}")]
        public Nullable<decimal> Price { get; set; }

        [DisplayFormat(NullDisplayText = "NA")]
        [Range(0, double.MaxValue, ErrorMessage = "* Units sold must be a valid number, 0 or larger")]
        [Display(Name = "Units Sold")]
        public Nullable<int> UnitsSold { get; set; }

        [Display(Name = "Published")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true,NullDisplayText = "NA")]
        public Nullable<System.DateTime> PublishDate { get; set; }
        //  public int PublisherID { get; set; }

        [Display(Name = "Image")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string BookImage { get; set; }

        [Display(Name = "Site Feature")]
        public bool IsSiteFeature { get; set; }

        [Display(Name = "Genre Feature")]
        public bool IsGenreFeature { get; set; }
      //  public int BookStatusID { get; set; }
    }
    [MetadataType(typeof(BookMetaData))]
    public partial class Book
    {

    }
    #endregion
}