using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceRecipe
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<GetAllFoodName> GetAllFoodName();

        [OperationContract]
        Recipes SearchFoodName(string inputfoodname);

        [OperationContract]
        void AddFoodName(Recipes addfoodname);

        [OperationContract]
        void UpdateFoodName(Recipes updatefoodname);

        [OperationContract]
        void DeleteFoodName(RecipesDelete foodname);

        // TODO: Add your service operations here
    }

    public class GetAllFoodName
    {

        [DataMember]
        public string FoodName { get; set; }
    }

    public class Recipes
    {

        [DataMember]
        public string FoodName { get; set; }

        [DataMember]
        public string RawMaterial { get; set; }

        [DataMember]
        public string Recipe { get; set; }
    }

    public class RecipesDelete
    {

        [DataMember]
        public string FoodName { get; set; }
    }

}
