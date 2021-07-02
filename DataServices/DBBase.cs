using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices
{

    public class DBBase
    {
        
            
        public MySqlConnection conn = new MySqlConnection("Server=160.153.253.132;Database=birol;Uid=smartdora;Pwd=Smart#dora1!;Port=3306;charset=latin5;");
        

    }
}
