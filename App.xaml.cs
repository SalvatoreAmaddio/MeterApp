using Backend.Database;
using Backend.Utils;
using FrontEnd.ExtensionMethods;
using MeterApp.Model;
using System.Windows;

namespace MeterApp
{
    public partial class App : Application
    {
        public App() 
        {
            Sys.LoadAllEmbeddedDll();

            DatabaseManager.Add(new SQLiteDatabase<City>());
            DatabaseManager.Add(new SQLiteDatabase<PostCode>());
            DatabaseManager.Add(new SQLiteDatabase<Address>());

            DatabaseManager.Add(new SQLiteDatabase<Pricing>());
            DatabaseManager.Add(new SQLiteDatabase<Invoice>());
            DatabaseManager.Add(new SQLiteDatabase<InvoicedReading>());

            DatabaseManager.Add(new SQLiteDatabase<Tenant>());
            DatabaseManager.Add(new SQLiteDatabase<TenantAddress>());
            DatabaseManager.Add(new SQLiteDatabase<Reading>());

            this.DisposeOnExit();
        }
    }
}