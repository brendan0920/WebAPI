using System.Text;

namespace PrsRequestNumberGeneratorV02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // Request Number Biz Requirements:
            // R240927001
            // 'R' - first letter
            // 2 digit year
            // 2 digit month
            // 2 digit day
            // 4 digit counter

            // generate 10 request numbers
            int counter = 0;
            for (int i = 0; i < 10; i++)
            {
                //counter++;
                //DateOnly today = DateOnly.FromDateTime(DateTime.Now); // create a new instance of DateOnly of Current date
                
                //string yy = today.Year.ToString().Substring(2);
                //string mm = today.Month.ToString().PadLeft(2, '0');
                //string dd = today.Day.ToString().PadLeft(2, '0');
                //string counterStr = counter.ToString().PadLeft(4, '0');

                //StringBuilder requestNumber = new StringBuilder("");
                //requestNumber.Append("R");
                //// append yy
                //requestNumber.Append(yy);
                //// append mm
                //requestNumber.Append(mm);
                //// append dd
                //requestNumber.Append(dd);
                //// append ####
                //requestNumber.Append(counterStr);

                //Console.WriteLine(requestNumber);

                StringBuilder counterStr = new StringBuilder("R");
                string dateStr = String.Format("{0:yymmdd}", DateTime.Now);

                counter++;
                string count = counter.ToString().PadLeft(3, '0');
                counterStr.Append(dateStr).Append(count);
                Console.WriteLine($"Request[{i}]: {counterStr}");



            }



        }
    }
}
