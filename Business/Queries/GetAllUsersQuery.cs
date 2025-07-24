namespace UserManagement.Api.Business.Queries
{
    public class GetAllUsersQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetAllUsersQuery(int page = 1, int pageSize = 10)
        {
            Page = page > 0 ? page : 1;
            PageSize = pageSize > 0 && pageSize <= 100 ? pageSize : 10;
        }
    }
}