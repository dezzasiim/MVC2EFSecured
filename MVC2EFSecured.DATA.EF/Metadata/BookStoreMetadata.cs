using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //Built in Metadata (Data Annotations) features or methods

namespace MVC2EFSecured.DATA.EF//Make sure this namespace matches the Namespace in your .tt file
{
    //  MOD 5.3 add Metadata Notes
    /*General rules and standard practices for metadata:     * 1) All metadata can exist in a single file for all classes associated to EF      * 2) Or, metadata can be split between files (1 file for metadata and application) for each class associated to EF. We will be doing 1) above     * 3) Metadata classes MUST be in the same namespace as the original EF class.     * 4) Short guide for connecting each pair of metadata class and EF class     *      a) Ensure that the namespaces for files match (match the .tt namespace)     *      b) Create the metadata class (empty)     *          EX> public class MyTableMetadata                    {                    }     *      c) Apply the MetadataType attribute to the metadata partial class     *          EX>  [MetadataType(typeof(MyTableMetadata))]     *      d) Create the metadata partial class with the same EXACT name as the EF class     *          EX> public partial class MyTable                    {                    *      e) Use the EF class to copy in the properties for the metadata class     *          EX> public partial class MyTable (in the .tt file)                    {                        public int MyTableID { get; set; }                        public string MyField { get; set; }                    }     *      f) Apply all necessary metadata attributes     *          EX> public class MyTableMetadata                    {                        [Required(ErrorMessage = "*")]                        [Display(Name = "My Field")]                        public string MyField { get; set; }                    }     * 5) Use the ER diagram from SSMS to ensure that all validation and metadata is correct.     */

          /*     * MOD 5.6 Notes for Metadata Attributes     *  1) If DB column name changes then the manually associated Metadata names need to be changed too      *  2) Primary keys don’t need annotations      *  3) To know field length and if nulls are allowed, etc, we can look directly in the DB at the field properties      *  4) We can change how DB field names are displayed in the UI by using: [Display(Name = "First Name")] -- often DB field names are clunky and beyond our control     * */


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

        [Display(Name = "Genre")]
        public Nullable<int> GenreID { get; set; }

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

        [Display(Name = "Publisher")]
          public int PublisherID { get; set; }

        [Display(Name = "Image")]
        [DisplayFormat(NullDisplayText = "NA")]
        public string BookImage { get; set; }

        [Display(Name = "Site Feature")]
        public bool IsSiteFeature { get; set; }

        [Display(Name = "Genre Feature")]
        public bool IsGenreFeature { get; set; }

        [Display(Name ="Book Status")]
        public int BookStatusID { get; set; }
    }
    [MetadataType(typeof(BookMetaData))]
    public partial class Book
    {

    }
    #endregion

    #region Book metadata
    public class BookStatusMetaData
    {
        //public int BookStatusID { get; set; }
        [Display(Name = "Book Status")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(25, ErrorMessage = "* The value must be 25 characters or less.")]
        public string BookStatusName { get; set; }


        [DisplayFormat(NullDisplayText = "NA")]
        [StringLength(100, ErrorMessage = "* The value must be 100 characters or less.")]
        public string Notes { get; set; }
    }

    [MetadataType(typeof(BookStatusMetaData))]
    public partial class BookStatus
    {

    }
    #endregion

    #region Genre Metadata
    public class GenreMetaData
    {
        //public int GenreID { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, ErrorMessage = "* The value must be 50 characters or less.")]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }

    [MetadataType(typeof(GenreMetaData))]
    public partial class Genre
    {

    }
    
    #endregion

    #region Publisher Metadata    public class PublisherMetadata    {
        //public int PublisherID { get; set; }
        [Display(Name = "Publisher")]        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]        [Required(ErrorMessage = "*")]        public string PublisherName { get; set; }        [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]        [DisplayFormat(NullDisplayText = "[-N/A-]")]        public string City { get; set; }        [StringLength(2, ErrorMessage = "* Value must be 2 characters.")]        [DisplayFormat(NullDisplayText = "[-N/A-]")]        public string State { get; set; }        [Display(Name = "Active")]        public bool IsActive { get; set; }    }
    [MetadataType(typeof(PublisherMetadata))]    public partial class Publisher { }    #endregion


   
}

