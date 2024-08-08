using InforceTask.Data;

namespace InforceTask.DTOs
{
    public class MessageDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public static MessageDTO CreateSuccessful(string title, string value) {
            MessageDTO message = new MessageDTO()
            {
                Status = Constants.STATUS_SUCCESSFUL,
                Title = title,
                Value = value
            };

            return message;
        }

        public static MessageDTO CreateFailed(string title, string value)
        {
            MessageDTO message = new MessageDTO()
            {
                Status = Constants.STATUS_FAILED,
                Title = title,
                Value = value
            };

            return message;
        }
    }
}
