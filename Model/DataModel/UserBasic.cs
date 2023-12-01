namespace cotr.backend.Model.DataModel
{
    public class UserBasic
    {
        public int UserId { get; set; }
        public string NickName { get; set; }

        public UserBasic(int userId, string nickName)
        {
            UserId = userId;
            NickName = nickName;
        }
    }
}
