

using System.ComponentModel.DataAnnotations;
using AcademyManagement.Application.DTOs.Paging;

namespace AcademyManagement.Application.DTOs.User
{
    public class FilterUsersDTO:BasePaging
    {
        #region Properties

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public FilterUserState State { get; set; }
        public FilterUserRole UserRole { get; set; }
        public FilterUserOrder OrderBy { get; set; }
        public List<AcademyManagement.Domain.Entities.Account.User> Users { get; set; }

        #endregion

         #region Methods

        public FilterUsersDTO SetUsers(List<AcademyManagement.Domain.Entities.Account.User> users)
        {
            this.Users = users;
            return this;
        }

        public FilterUsersDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;

            return this;
        }



        #endregion
    }

      public enum FilterUserState
    {
        [Display(Name = "همه")]
        All,

        [Display(Name = "بلاک شده")]
        Blocked,

        [Display(Name = "بلاک نشده")]
        NotBlocked,

        [Display(Name = "فعال")]
        Actived,

        [Display(Name = "غیر فعال")]
        NotActived
        
    }

    public enum FilterUserRole
    {
        [Display(Name = "همه")]
        All,

        [Display(Name = "مدیر")]
        Admin,

        [Display(Name = "کاربر عادی")]
        NormalUser
    }

    public enum FilterUserOrder
    {
        [Display(Name = "صعودی")]
        CreateDate_ASC,

        [Display(Name = "نزولی")]
        CreateDate_DES
    }
}