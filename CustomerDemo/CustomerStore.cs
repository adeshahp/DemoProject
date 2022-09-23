using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Model;
namespace CustomerDemo
{
    public class CustomerStore
    {
        private FileStream fs = null;
        private BinaryFormatter bf=null;

        public CustomerStore()
        {
            //fs = new FileStream("C:\\Users\\Adesha.Hp\\source\\repos\\SoleraFiles\\Customer.txt", FileMode.Create, FileAccess.ReadWrite);
            bf = new BinaryFormatter();
        }

        public void StoreCustomers(CustomerCollection cstData)
        {
            if (File.Exists("C:\\Users\\Adesha.Hp\\source\\repos\\SoleraFiles\\Customer.txt"))
            {
                File.Delete("C:\\Users\\Adesha.Hp\\source\\repos\\SoleraFiles\\Customer.txt");
            }
            fs = new FileStream("C:\\Users\\Adesha.Hp\\source\\repos\\SoleraFiles\\Customer.txt", FileMode.Create, FileAccess.ReadWrite);
            bf.Serialize(fs,cstData);
        }

        public CustomerCollection RetrieveCustomers()
        {
            if (File.Exists("C:\\Users\\Adesha.Hp\\source\\repos\\SoleraFiles\\Customer.txt"))
            {
                fs = new FileStream("C:\\Users\\Adesha.Hp\\source\\repos\\SoleraFiles\\Customer.txt", FileMode.Create, FileAccess.ReadWrite);
                CustomerCollection c= (CustomerCollection)bf.Deserialize(fs);
                fs.Flush();
                fs.Close();
                return c;
            }
            else
            {
                return new CustomerCollection();
            }
        }
    }
}
