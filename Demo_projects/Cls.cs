using Demo_projects.Models;
using System.Data.SQLite;

namespace Demo_projects
{
    public class Cls
    {

        public Cls()
        {
            using (var connection = new SQLiteConnection("Data Source=billingNew.db"))
            {
                connection.Open();

                // Create the Billing table if it doesn't exist
                CreateBillingTable(connection);

                
               
            }
        }

           
        

        static void CreateBillingTable(SQLiteConnection connection)
        {
            using (var cmd = new SQLiteCommand("CREATE TABLE IF NOT EXISTS Billing (BillNo INTEGER PRIMARY KEY AUTOINCREMENT, CustomerNo INTEGER, Date DATETIME, Amount TEXT, IsPaid BOOLEAN)", connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertBillingRecord(SQLiteConnection connection, Billing billing)
        {
            using (var cmd = new SQLiteCommand("INSERT INTO Billing (CustomerNo, Date, Amount, IsPaid) VALUES (@CustomerNo, @Date, @Amount, @IsPaid)", connection))
            {
                connection.Open();
                //cmd.Parameters.AddWithValue("@BillNo", billing.BillNo);
                cmd.Parameters.AddWithValue("@CustomerNo", billing.CustomertNo);
                cmd.Parameters.AddWithValue("@Date", billing.Date);
                cmd.Parameters.AddWithValue("@Amount", billing.Amount);
                cmd.Parameters.AddWithValue("@IsPaid", billing.IsPaid);

                cmd.ExecuteNonQuery();
            }
        }

        public static List<Billing> DisplayBillingTable(SQLiteConnection connection)
        {
            List<Billing> billing = new List<Billing>();
            using (var cmd = new SQLiteCommand("SELECT * FROM Billing", connection))
            {
                connection.Open();
                CreateBillingTable(connection);
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("{0,5} {1,10} {2,15} {3,10} {4,10}", "BillNo", "CustomerNo", "Date", "Amount", "IsPaid");
                    Console.WriteLine(new string('-', 50));

                    while (reader.Read())
                    {
                        Billing model = new Billing();
                        model.BillNo = Convert.ToInt32(reader["BillNo"]);
                        model.CustomertNo = Convert.ToInt32(reader["CustomerNo"]);
                        model.Date = Convert.ToDateTime(reader["Date"]);
                        model.Amount = Convert.ToString(reader["Amount"]);
                        model.IsPaid = Convert.ToBoolean(reader["IsPaid"]);
                        billing.Add(model);
                    }
                }
            }
            return billing;
        }

        public static Billing EditBillingRecordGetById(SQLiteConnection connection, int id)
        {
            List<Billing> billing = new List<Billing>();
            using (var cmd = new SQLiteCommand("SELECT * FROM Billing Where BillNo = @BillNo", connection))
            {
                cmd.Parameters.AddWithValue("@BillNo", id);
                connection.Open();
                CreateBillingTable(connection);
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("{0,5} {1,10} {2,15} {3,10} {4,10}", "BillNo", "CustomerNo", "Date", "Amount", "IsPaid");
                    Console.WriteLine(new string('-', 50));

                    while (reader.Read())
                    {
                        Billing model = new Billing();
                        model.BillNo = Convert.ToInt32(reader["BillNo"]);
                        model.CustomertNo = Convert.ToInt32(reader["CustomerNo"]);
                        model.Date = Convert.ToDateTime(reader["Date"]);
                        model.Amount = Convert.ToString(reader["Amount"]);
                        model.IsPaid = Convert.ToBoolean(reader["IsPaid"]);
                        billing.Add(model);
                    }
                }
            }
            return billing[0];
        }

        public static void EditBillingRecord(SQLiteConnection connection, Billing billing)
        {
            using (var cmd = new SQLiteCommand("UPDATE Billing SET CustomerNo = @CustomerNo, Date = @Date, Amount = @Amount, IsPaid = @IsPaid WHERE BillNo = @BillNo", connection))
            {
                connection.Open();
                cmd.Parameters.AddWithValue("@BillNo", billing.BillNo);
                cmd.Parameters.AddWithValue("@CustomerNo", billing.CustomertNo);
                cmd.Parameters.AddWithValue("@Date", billing.Date);
                cmd.Parameters.AddWithValue("@Amount", billing.Amount);
                cmd.Parameters.AddWithValue("@IsPaid", billing.IsPaid);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
