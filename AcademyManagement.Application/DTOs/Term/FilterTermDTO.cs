using AcademyManagement.Application.DTOs.Paging;

namespace AcademyManagement.Application.DTOs.Term
{
    public class FilterTermDTO:BasePaging
    {
        #region Properties
        public string Name { get; set; }
        public List<AcademyManagement.Domain.Entities.Term.Term> Terms { get; set; }

        #endregion

            #region Methods

        public FilterTermDTO SetTerms(List<AcademyManagement.Domain.Entities.Term.Term> terms)
        {
            this.Terms = terms;
            return this;
        }

        public FilterTermDTO SetPaging(BasePaging paging)
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
}