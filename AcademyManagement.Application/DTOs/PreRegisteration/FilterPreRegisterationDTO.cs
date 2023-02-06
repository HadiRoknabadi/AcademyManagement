using System.ComponentModel.DataAnnotations;
using AcademyManagement.Application.DTOs.Paging;

namespace AcademyManagement.Application.DTOs.PreRegisteration
{
    public class FilterPreRegisterationDTO:BasePaging
    {
         public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Grade { get; set; }
        public FilterPreRegisterationOrder OrderBy { get; set; }
        public List<AcademyManagement.Domain.Entities.Account.PreRegisteration> PreRegisterations { get; set; }

        #region Methods

        public FilterPreRegisterationDTO SetPreRegisterations(List<AcademyManagement.Domain.Entities.Account.PreRegisteration> preRegisterations)
        {
            this.PreRegisterations = preRegisterations;
            return this;
        }

        public FilterPreRegisterationDTO SetPaging(BasePaging paging)
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

    public enum FilterPreRegisterationOrder
    {
        [Display(Name = "صعودی")]
        CreateDate_ASC,

        [Display(Name = "نزولی")]
        CreateDate_DES
    }
}