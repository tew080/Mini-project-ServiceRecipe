using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceRecipe
{
    /*
     * กำหนด Service เพื่อที่จะสามารถ เรียกใช้จาก ฝั่งของ Client ได้
     * 
     * List<ShowAll>ShowAllFoods(); เรียกแสดง รายการอาหารทั้งหมด จาก class ShowAll ที่เก็บ FoodID, FoodName
     * 
     * SearchAndUpdate SearchFoodName(string input_foodname); เรียกแสดง รายการอาหาร  จาก class SearchAndUpdate 
     * ที่เก็บ FoodID, FoodName, RawMaterial, Recipe โดยการรับค่าผ่านตัวแปร
     * 
     * void AddDataFood(AddData add_data_food); เพิ่มข้อมูล ใช้ class AddData ที่เก็บ FoodName, RawMaterial, Recipe โดยการรับค่าผ่านตัวแปร เเละจะไม่มีการส่งค่า Return กลับ
     * 
     * void UpdateDataFood(SearchAndUpdate update_data_food); อัพเดทข้อมูล ใช้ class SearchAndUpdate ที่เก็บ FoodID, FoodName, RawMaterial, Recipe โดยการรับค่าผ่านตัวแปร เเละจะไม่มีการส่งค่า Return กลับ
     * 
     * bool DeleteDataFood(DeleteData delete_data_food); ลบข้อมูล ใช้ class DeleteData  ที่เก็บ FoodName โดยการรับค่าผ่านตัวแปร และมีการส่งค่ากลับ(True/False)
     */
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<ShowAll>ShowAllFoods();

        [OperationContract]
        SearchAndUpdate SearchFoodName(string input_foodname);

        [OperationContract]
        void AddDataFood(AddData add_data_food);

        [OperationContract]
        void UpdateDataFood(SearchAndUpdate update_data_food);

        [OperationContract]
        bool DeleteDataFood(DeleteData delete_data_food);
    }

    /*
     * กำหนด class ShowAll เพื่อเก็บ FoodID เป็น int , FoodName เป็น string 
     * และสามารถเรียกดูและกำหนดค่าลงไปได้
     * จะถูกเรียกใช้โดย Service List<ShowAll>ShowAllFoods();
     */
    public class ShowAll
    {
        [DataMember]
        public int FoodID { get; set; }

        [DataMember]
        public string FoodName { get; set; }
    }

    /*
     * กำหนด class ShowAll เพื่อเก็บ FoodName เป็น string,  RawMaterial เป็น string,  Recipe เป็น string 
     * และสามารถเรียกดูและกำหนดค่าลงไปได้
     * จะถูกเรียกใช้โดย Service void AddDataFood(AddData add_data_food);
     */
    public class AddData
    {
        [DataMember]
        public string FoodName { get; set; }

        [DataMember]
        public string RawMaterial { get; set; }

        [DataMember]
        public string Recipe { get; set; }
    }

    /*
     * กำหนด class ShowAll เพื่อเก็บ FoodID เป็น int , FoodName เป็น string, RawMaterial เป็น string,  Recipe เป็น string 
     * และสามารถเรียกดูและกำหนดค่าลงไปได้
     * จะถูกเรียกใช้โดย Service SearchAndUpdate SearchFoodName(string input_foodname); 
     * และ Service void UpdateDataFood(SearchAndUpdate update_data_food);
     */
    public class SearchAndUpdate
    {
        [DataMember]
        public int FoodID { get; set; }

        [DataMember]
        public string FoodName { get; set; }

        [DataMember]
        public string RawMaterial { get; set; }

        [DataMember]
        public string Recipe { get; set; }
    }

    /*
     * กำหนด class DeleteData เพื่อเก็บ FoodName เป็น string 
     * และสามารถเรียกดูและกำหนดค่าลงไปได้
     * จะถูกเรียกใช้โดย Service bool DeleteDataFood(DeleteData delete_data_food);
     */
    public class DeleteData
    {
        [DataMember]
        public string FoodName { get; set; }

    }
}
