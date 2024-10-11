using SpinsNew.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.ViewModel
{
    public class GisViewModel : GisModel
    {
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtName { get; set; }
        [Display(Name = "IDOSCA")]
        public string IdOsca { get; set; }
        [Display(Name = "IDTYPE")]
        public string IdType { get; set; }
        public string Grantee { get; set; }
        public string Respondent { get; set; } // Fullname
        [Display(Name = "Permanent Region")]
        public string Region { get; set; }
        [Display(Name = "Permanent Province")]
        public string Province { get; set; }
        [Display(Name = "Permanent Municipality")]
        public string Municipality { get; set; }
        [Display(Name = "Permanent Barangay")]
        public string Barangay { get; set; }
        [Display(Name = "Permanent Purok")]
        public string Address { get; set; }
        [Display(Name = "Permanent Street")]
        public string permanentStreet { get; set; }

        [Display(Name = "Present Region")]
        public string PresentRegion { get; set; }
        [Display(Name = "Present Province")]
        public string PresentProvince { get; set; }
        [Display(Name = "Present Municipality")]
        public string PresentMunicipality { get; set; }
        [Display(Name = "Present Barangay")]
        public string PresentBarangay { get; set; }
        [Display(Name = "Present Purok")]
        public string PresentAddress { get; set; }
        [Display(Name = "Present Street")]
        public string PresentpermanentStreet { get; set; }


        public string Sex { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }//Add age property
        public string BirthPlace { get; set; }//Masterlist
        public string CareGiver { get; set; }
        public string Relationship { get; set; }
        public string MaritalStatus { get; set; }//Masterlist
        [Display(Name = "TIN")]
        public string Tin { get; set; }
        [Display(Name = "Mobile Number")]
        public string ContactNumber { get; set; }//masterlist
        public string Nationality { get; set; }//masterlist
        public string Profession { get; set; }
        public string SourceOfFunds { get; set; }
        public string GrossSalary { get; set; }
        public string Email { get; set; }
        public string MothersMaiden { get; set; }//GIs
        public string EmbossName { get; set; }
        [Display(Name = "LBP Bank #")]
        public string LbpBank { get; set; }
        //Defualt value below
        public string ReceivePension { get; set; }
        public string DswdSocialPension { get; set; }
        public string Gsis { get; set; }
        public string Sss { get; set; }
        public string Afpslai { get; set; }
        public string Otherpension { get; set; }
        public string WagesSalaries { get; set; }
        public string WagesSalariesRegular { get; set; }
        public int WagesSalariesAmount { get; set; }
        public string Profit { get; set; }
        public string ProfitRegular { get; set; }
        public int ProfitAmount { get; set; }
        public string Household { get; set; }
        public string HouseholdRegular { get; set; }
        public int HouseholdAmount { get; set; }
        public string Domestic { get; set; }
        public string DomesticRegular { get; set; }
        public int DomesticAmount { get; set; }
        public string International { get; set; }
        public string InternationalRegular { get; set; }
        public int InternationalAmount { get; set; }
        public string Friends { get; set; }
        public string FriendsRegular { get; set; }
        public int FriendsAmount { get; set; }
        public string Government { get; set; }
        public string GovernmentRegular { get; set; }
        public int GovernmentAmount { get; set; }
        public string Others { get; set; }
        public string OthersRegular { get; set; }
        public int OthersAmount { get; set; }
        public int TotalAmount { get; set; }
        [Display (Name = "Living With")]
        public string LivingConditions { get; set; } // Living condition ID
        public string Older85 { get; set; }
        public string HealthLimitActivities { get; set; } 
        public string NeedSomeoneHelp { get; set; }
        public string NeedStayHome { get; set; } 
        public string CanCountOnSomeone { get; set; } 
        public string NeedMobilityAssistance { get; set; } 
        public string Disability { get; set; } 
        public string IllnessDisease { get; set; } 
        public string Food { get; set; } 
        public string MedicineVitamins { get; set; } 
        public string HealthCheckUp { get; set; } 
        public string Clothes { get; set; }
        public string Utilities { get; set; } 
        public string DebtPayment { get; set; }
        public string Livelihood { get; set; } 
        public string UtilizationOthers { get; set; } 
        [Display(Name = "Name of Worker")]
        public string ValidatorName { get; set; }//gis validatedbyid
        public string Assessment { get; set; }
        [Display(Name = "SPIS Remarks")]
        public string SpisRemarks { get; set; }
        public string AuthorizeRepresentative { get; set; }//Authorize representatives
    }
}
