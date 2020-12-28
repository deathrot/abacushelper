using System;
using System.Threading.Tasks;


namespace Logic.Providers
{
    public class QuestionsProvider
    {

        public async Task<Logic.ViewModels.QuestionVM[]> Get(Interfaces.IConnectionUtility connectionUtility)
        {
            var entities = await DB.DBUtility.GetData<DBModels.QuestionEntity>(connectionUtility, "select * from questions where is_deleted = 0", null);

            return Mappers.ObjectMapper.Instance.Mapper.Map<Logic.ViewModels.QuestionVM[]>(entities);
        }

        public async Task<Logic.Models.DBSaveResult> SaveResult(Interfaces.IConnectionUtility connectionUtility, Models.QuestionSaveRequest saveRequest)
        {
            var result = new Logic.Models.DBSaveResult();
            try
            {


                if (saveRequest != null && !saveRequest.IsEmpty)
                {
                    if (saveRequest.entitesToUpdate != null && saveRequest.entitesToUpdate.Length > 0)
                    {
                        var itemsToUpdate = Mappers.ObjectMapper.Instance.Mapper.Map<DBModels.QuestionEntity[]>(saveRequest.entitesToUpdate);
                        result.TotalRecordSentToSave += itemsToUpdate.Length;
                        if (itemsToUpdate != null && itemsToUpdate.Length > 0)
                        {
                            if (await DB.DBUtility.Update<DBModels.QuestionEntity>(connectionUtility, itemsToUpdate))
                            {
                                result.TotalRecordUpdated = itemsToUpdate.Length;
                            }
                        }
                    }

                    if (saveRequest.entitesToDelete != null && saveRequest.entitesToDelete.Length > 0)
                    {
                        var itemsToDelete = Mappers.ObjectMapper.Instance.Mapper.Map<DBModels.QuestionEntity[]>(saveRequest.entitesToDelete);
                        result.TotalRecordSentToSave += itemsToDelete.Length;
                        if (itemsToDelete != null && itemsToDelete.Length > 0)
                        {
                            if (await DB.DBUtility.Delete<DBModels.QuestionEntity>(connectionUtility, itemsToDelete))
                            {
                                result.TotalRecordDeleted = itemsToDelete.Length;
                            }
                        }
                    }

                    if (saveRequest.entitesToInsert != null && saveRequest.entitesToInsert.Length > 0)
                    {
                        var itemsToInsert = Mappers.ObjectMapper.Instance.Mapper.Map<DBModels.QuestionEntity[]>(saveRequest.entitesToInsert);
                        result.TotalRecordSentToSave += itemsToInsert.Length;
                        if (itemsToInsert != null && itemsToInsert.Length > 0)
                        {
                            result.TotalRecordInserted += await DB.DBUtility.Insert<DBModels.QuestionEntity>(connectionUtility, itemsToInsert);
                        }
                    }
                }
                result.Success = true;
            }
            finally
            {

            }

            return result;
        }
    }
}