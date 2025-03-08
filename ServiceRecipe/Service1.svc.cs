using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;

namespace ServiceRecipe
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        //เชือมต่อฐานข้อมูล
        private string connect = "server=localhost; database=testrecipe; user=root;password=;";

        //แสดงชื่อเมนูทั้งหมดโดยเรียงจาก a-z ก-ฮ
        // รับจาก  return FoodName
        public List<GetAllFoodName> GetAllFoodName()
        {
            //ตัวแปรใช้ในการเก็บค่า
            List<GetAllFoodName> FoodNames = new List<GetAllFoodName>();

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                string query = "SELECT foodname FROM listrecipe ORDER BY foodname COLLATE utf8_thai_520_w2";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))

                //ExecuteReader อ่านข้อมูลออกมา
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    //อ่านข้อมูลที่ละแถว
                    while (reader.Read())
                    {
                        FoodNames.Add(new GetAllFoodName
                        {
                            FoodName = reader.GetString("foodname"),
                        });
                    }
                }
            }

            // ส่งค่าไป List<GetAllFoodName>
            return FoodNames;
        }

        //ค้นหาวัตถุดิบและวิธีการจากชื่อ
        public Recipes SearchFoodName(string inputfoodname)
        {
            Recipes foodnames = null;

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                // โชว์ข้อมูลของ foodname
                conn.Open();
                string query = "SELECT * FROM listrecipe where FoodName=@foodname";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@foodname", inputfoodname);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foodnames = new Recipes
                            {
                                FoodName = reader.GetString("FoodName"),
                                RawMaterial = reader.GetString("raw_material"),
                                Recipe = reader.GetString("recipe")

                            };
                        }
                    }
                }
            }
            return foodnames;
        }

        //เพิ่มชื่อแกง-วัตุถุดิบ-วิธีการทำ
        public void AddFoodName(Recipes addfoodname)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                //คำัส่งในการเพิ่มข้อมูล
                conn.Open();
                string query = "INSERT INTO listrecipe (foodname, raw_material, recipe) VALUES (@foodname, @raw_material, @recipe)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@foodname", addfoodname.FoodName);
                    cmd.Parameters.AddWithValue("@raw_material", addfoodname.RawMaterial);
                    cmd.Parameters.AddWithValue("@recipe", addfoodname.Recipe);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //อัพเดทข้องมูลของวัตถุดิบ-วิธีทำ
        public void UpdateFoodName(Recipes updatefoodname)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                //คำัส่งในการเพิ่มข้อมูล
                conn.Open();
                string query = "UPDATE listrecipe SET foodname=@foodname ,raw_material=@raw_material,recipe=@recipe WHERE foodname =@foodname";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@foodname", updatefoodname.FoodName);
                    cmd.Parameters.AddWithValue("@raw_material", updatefoodname.RawMaterial);
                    cmd.Parameters.AddWithValue("@recipe", updatefoodname.Recipe);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //ลบเมนูที่ไม่ต้องการ
        public void DeleteFoodName(RecipesDelete deletefoodname)
        {
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                //คำัส่งในการเพิ่มข้อมูล
                conn.Open();
                string query = "DELETE FROM listrecipe WHERE foodname =@foodname";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@foodname", deletefoodname.FoodName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
