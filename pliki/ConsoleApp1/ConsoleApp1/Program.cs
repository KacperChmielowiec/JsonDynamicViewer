using System.Net.Http;
using System.Text.RegularExpressions;
using System.Net.Http;

// klasa jedna z wielu jakie chcemy filtrować


class Program
{
    public void Main()
    {
        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;

        // Sprawdzenie, czy dany miesiąc ma 31 dni
        bool check = (month == 1 || month == 3 || month == 5 || month == 7 ||
                             month == 8 || month == 10 || month == 12);

        // Utworzenie wyrażenia regularnego w zależności od check wartośći
        string dayPattern = check ? "(0[1-9]|[12][0-9]|3[01])" : "(0[1-9]|[12][0-9]|30)";
        string regex = "^[A-Z]{1}[a-x]{2,10}" + dayPattern + "(0[1-9]|1[0-2])\\.zip$";
    }
}




