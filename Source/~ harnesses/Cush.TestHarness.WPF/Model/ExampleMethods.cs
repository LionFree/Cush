using System;
using System.Diagnostics;
using System.ServiceModel.Web;
using Cush.MVVM;
using Cush.TestHarness.WPF.Model;
using Cush.TestHarness.WPF.ViewModels;
using Cush.TestHarness.WPF.Views;
using Cush.Testing;
using Cush.Testing.RandomObjects;
using Cush.Windows;
using Cush.Windows.Services;
using Strings = Cush.TestHarness.WPF.Resources.Strings;

namespace Cush.TestHarness.WPF.Infrastructure
{
    public static class ExampleMethods
    {
        private static WebServiceHost _host;

        internal static void ExceptionHandler_Show()
        {
            //var obj = File.Open(Path.GetRandomFileName(), FileMode.Open);
            // NullReferenceException
            string x = null;
            Console.WriteLine(x.Length);
            Environment.Exit(0);

            // Out of bounds
            //var array = new int[10];
            //var pdq = array[11];


            // Fatal
            var one = 1;
            var divideByZero = one/decimal.Zero;
            Trace.WriteLine("Divided by zero: " + divideByZero);
        }

        internal static void ApplicationType_Show()
        {
            Trace.WriteLine(ApplicationType.Current);
        }

        internal static void GenericRandom_Show()
        {
            // Using Lambda Expression
            GetRandom.AddType<double, double, double>((x, y) =>
            {
                // Generate a random number between x and y.
                return GetRandom.Double(x, y);
            });
            var thingA = GetRandom.Object<double>(12.0, 17.0);
            var thingB = GetRandom.Object<double>(12.0, 17.0);
            Trace.WriteLine(thingA);
            Trace.WriteLine(thingB);


            // Using delegate
            GetRandom.AddType(RandomMethods.RandomPatient);
            var thing1 = GetRandom.Object<Patient>();
            var thing2 = GetRandom.Object<Patient>();
            Trace.WriteLine(thing1.Name, thing1.Identifier);
            Trace.WriteLine(thing2.Name, thing2.Identifier);
        }

        internal static void OpenHost<TService>(int port, bool useSsl, string apiBasePath)
        {
            _host = HostBuilder
                .CreateRESTfulHost<TService>(useSsl, port, apiBasePath)
                .With().BasicConfiguration()
                .And().PublishMetadata();


            //var host = HostBuilder.CreateRESTfulHost<PatientService, IPatientService>(port, apiBasePath, false);

            try
            {
                _host.Open();

                Console.WriteLine(Strings.HostOpened, _host.Description.Name, port);
                Console.WriteLine(Strings.HostListening, _host.Description.Endpoints[0].Address);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }

        internal static void Service_Stop()
        {
            _host.Close();
        }

        internal static void Randoms_Show()
        {
            var rng = RandomObjectGenerator.GetInstance();
            //var rlg = RandomArrayGenerator.GetInstance();


            for (var i = 0; i < 1000; i++)
            {
                //var tvI = rlg.GetRandomArrayOfStrings(1, 6, 1, 20, Sets.AlphaNumeric);

                //var chars = new[] {'c', 'd', 'e'};
                //var tvS = GetRandomArray.OfStrings(chars);

                //var tvB = GetRandomArray.OfBytes(200);


                var tv1 = rng.GetRandomDouble(0, 150, Scale.Flat);
                var tv2 = GetRandom.Double(0, 150, Scale.Flat);

                Trace.WriteLine(string.Format("{0,4}: {1}, {2}", i + 1, tv1, tv2));
            }
            Trace.WriteLine("Done.");
        }

        internal static void MVVM_Show()
        {
            var vmFactory = Factory<ViewModel>.GetInstance();
            var viewFactory = Factory<View>.GetInstance();

            var mainVM = vmFactory.Create<MainViewModel>();
            var mainView = viewFactory.Create<MainView>(mainVM);

            mainView.Show();
        }
    }
}