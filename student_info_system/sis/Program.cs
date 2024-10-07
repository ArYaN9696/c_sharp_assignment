using sis.model.Exceptions;
using sis.model;
using sis.Repository.Interfaces;
using sis.Repository;
using sis.Main;
using sis.Service;
using sis.Utility;

namespace sis
{
    internal class Program
    {
        static void Main(string[] args)
        {


            SIS sis = new SIS(DbConnUtil.GetConnString());

            // Initialize SISRepository
            isis sisRepository = new sisRepo(sis);

            // Initialize Service
            HospitalService service = new HospitalService(sisRepository);

            // Initialize MainModule and show the menu
            MainModule mainModule = new MainModule(service);
            mainModule.ShowMenu();


        }
    }
}
