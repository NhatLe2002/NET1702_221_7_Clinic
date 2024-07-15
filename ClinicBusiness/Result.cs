using ClinicData.Models;

namespace ClinicBusiness
{
    public class Result<T>
    {
        private List<User> users;
        private int sUCCESS_READ_CODE;

        public Result(List<User> users, int sUCCESS_READ_CODE)
        {
            this.users = users;
            this.sUCCESS_READ_CODE = sUCCESS_READ_CODE;
        }

        public Result(List<User> users, int sUCCESS_READ_CODE, string message)
        {
            this.users = users;
            this.sUCCESS_READ_CODE = sUCCESS_READ_CODE;
        }

        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Result(T data, int status, string message = "")
        {
            Data = data;
            Status = status;
            Message = message;
        }
    }
}