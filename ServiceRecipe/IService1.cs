using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceRecipe
{
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
    public class ShowAll
    {
        [DataMember]
        public int FoodID { get; set; }

        [DataMember]
        public string FoodName { get; set; }
    }
    public class AddData
    {
        [DataMember]
        public string FoodName { get; set; }

        [DataMember]
        public string RawMaterial { get; set; }

        [DataMember]
        public string Recipe { get; set; }
    }
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
    public class DeleteData
    {
        [DataMember]
        public string FoodName { get; set; }
    }
}
