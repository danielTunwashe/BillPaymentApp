
using BillPaymentApp;
UserRegistration userReg = new UserRegistration();

try
{
    Console.WriteLine("-----------Welcome to this billing Application App--------");

    userReg.addDecoderType();
    userReg.AppMenuOne();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}















