namespace Dropify.Logics
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context , int orderId);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
