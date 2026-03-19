using ClassLibrary1;

namespace ClassLibrary2
{
    public class Class2
    {
        public string strReverseBL()
        {
            Class1 b = new Class1();
            string beforeReverse = b.strReverseDAL();
            string afterReverse = new string(beforeReverse.Reverse().ToArray());
            return afterReverse;
        }
    }
}
