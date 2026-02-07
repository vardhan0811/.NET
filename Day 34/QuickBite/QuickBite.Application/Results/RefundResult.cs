namespace QuickBite.Application.Results
{
    public class RefundResult
    {
        public bool IsSuccess { get; set; }

        public string ReferenceId { get; set; }

        public string FailureReason { get; set; }
    }
}
