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
    public class Service1 : IService1
    {
        //เชือมต่อฐานข้อมูล
        private string connectdatabase = "server=localhost; database=wikifood; user=root; password=;";

//แสดงชื่อเมนูทั้งหมดโดยเรียงจาก a-z ก-ฮ
        // รับจาก  return FoodName
        public List<ShowAll> ShowAllFoods()
        {
            //ตัวแปรใช้ในการเก็บค่า
            List<ShowAll> FoodNames = new List<ShowAll>();

            using (MySqlConnection conn = new MySqlConnection(connectdatabase))
            {
                conn.Open();
                string query = "SELECT food_name, food_id FROM wikifoods ORDER BY food_name COLLATE utf8_thai_520_w2";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))

                //ExecuteReader อ่านข้อมูลออกมา
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    //อ่านข้อมูลที่ละแถว
                    while (reader.Read())
                    {
                        FoodNames.Add(new ShowAll
                        {
                            FoodID = reader.GetInt32("food_id"),
                            FoodName = reader.GetString("food_name"),
                        });
                    }
                }
            }
            // ส่งค่าไป List<GetAll>
            return FoodNames;
        }
//ค้นหาวัตถุดิบและวิธีการ จากชื่อ หรือ idเมนู
        public SearchAndUpdate SearchFoodName(string input_foodname)
        {
            SearchAndUpdate foodnames = null;

            using (MySqlConnection conn = new MySqlConnection(connectdatabase))
            {
                conn.Open();
                string query = "SELECT * FROM wikifoods where food_name=@foodname OR food_id=@foodid";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@foodname", input_foodname);
                    cmd.Parameters.AddWithValue("@foodid", input_foodname);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foodnames = new SearchAndUpdate
                            {
                                FoodID = reader.GetInt32("food_id"),
                                FoodName = reader.GetString("food_name"),
                                RawMaterial = reader.GetString("raw_material"),
                                Recipe = reader.GetString("recipe")

                            };
                        }
                    }
                }
            }
            return foodnames;
        }
//เพิ่มข้อมูล ชื้อเมนู วัตถุดิบ วิธีทำ และจะสร้าง idเมนูอัตโนมัต
        public void AddDataFood(AddData add_data_food)
        {
            using (MySqlConnection conn = new MySqlConnection(connectdatabase))
            {
                //คำัส่งในการเพิ่มข้อมูล
                conn.Open();
                string query = "INSERT INTO wikifoods (food_name, raw_material, recipe) VALUES (@foodname, @rawmaterial, @recipe)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@foodname", add_data_food.FoodName);
                    cmd.Parameters.AddWithValue("@rawmaterial", add_data_food.RawMaterial.Replace("\r\n", "\n"));
                    cmd.Parameters.AddWithValue("@recipe", add_data_food.Recipe.Replace("\r\n", "\n"));
                    cmd.ExecuteNonQuery();
                }
            }
        }
//อัพเดทข้องมูลโดยการใช้ idเมนูเพื่อระบุเมนูที่ต้องการแก้ไข
        public void UpdateDataFood(SearchAndUpdate update_data_food)
        {
            using (MySqlConnection conn = new MySqlConnection(connectdatabase))
            {
                //คำัส่งในการเพิ่มข้อมูล
                conn.Open();
                string query = "UPDATE wikifoods SET food_name=@foodname, raw_material=@raw_material, recipe=@recipe WHERE food_id=@foodid";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@foodid", update_data_food.FoodID);
                    cmd.Parameters.AddWithValue("@foodname", update_data_food.FoodName);
                    cmd.Parameters.AddWithValue("@raw_material", update_data_food.RawMaterial);
                    cmd.Parameters.AddWithValue("@recipe", update_data_food.Recipe);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //ลบข้อมูลโดยการป้อนชื่ออาหาร
        public bool DeleteDataFood(DeleteData delete_data_food)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectdatabase))
                {

                    //เริ่มเชื่อมต่อฐานข้อมูล
                    conn.Open();
                    string query = "DELETE FROM wikifoods WHERE food_name=@foodname";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@foodname", delete_data_food.FoodName);

                        // ExecuteNonQuery() จะส่งคืนจำนวนแถวที่ได้รับผลกระทบ
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // ถ้ามีการลบข้อมูลอย่างน้อย 1 แถว แสดงว่าลบสำเร็จ
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
