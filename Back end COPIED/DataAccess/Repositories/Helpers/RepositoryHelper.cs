using DataAccess.Models;
using Framework.Enums;

namespace DataAccess.Repositories.Helpers
{
    public static class RepositoryHelper
    {
        public static void SetAuditFields<T>(T item, DbAction dbAction) where T : EntityBase
        {
            switch (dbAction)
            {
                case DbAction.Add:
                    item.CreatedOn = DateTime.Now;
                    item.CreatedBy = "Jan-Pieter";
                    item.IsDeleted = false;
                    break;
                case DbAction.Update:
                    item.UpdatedOn = DateTime.Now;
                    item.UpdatedBy = "Jan-Pieter-CHG";
                    break;
                case DbAction.Delete:
                    item.UpdatedOn = DateTime.Now;
                    item.UpdatedBy = "Jan-Pieter-DEL";
                    item.IsDeleted = true;
                    break;
                default:
                    break;
            }
        }
    }
}
